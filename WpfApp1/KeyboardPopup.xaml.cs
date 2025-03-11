using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace WpfApp1
{
    public static class VisualTreeHelperExtensions
    {
        public static IEnumerable<DependencyObject> GetVisualChildren(this DependencyObject parent)
        {
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(parent);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                int childCount = VisualTreeHelper.GetChildrenCount(current);

                for (int i = 0; i < childCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(current, i);
                    yield return child;
                    queue.Enqueue(child);
                }
            }
        }
    }

    public partial class KeyboardPopup : UserControl
    {
        #region Fields
        private readonly Dictionary<Button, string> originalButtonContents;
        private bool isShiftActive;
        private bool isSymbolsActive;
        private bool isInitialized;
        #endregion

        #region Events
        public event EventHandler<string> KeyPressed;
        #endregion

        #region Constructor
        public KeyboardPopup()
        {
            try
            {
                InitializeComponent();
                originalButtonContents = new Dictionary<Button, string>();
                isInitialized = false;
                InitializeKeyboard();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing keyboard: {ex.Message}");
            }
        }
        #endregion

        #region Private Methods
        private void InitializeKeyboard()
        {
            try
            {
                if (!isInitialized)
                {
                    isShiftActive = false;
                    isSymbolsActive = false;
                    SaveOriginalButtonContents();
                    isInitialized = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in InitializeKeyboard: {ex.Message}");
            }
        }

        private void SaveOriginalButtonContents()
        {
            try
            {
                originalButtonContents.Clear();
                var buttons = GetAllButtons();

                foreach (var button in buttons)
                {
                    if (IsRegularButton(button))
                    {
                        originalButtonContents[button] = button.Content?.ToString() ?? string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving button contents: {ex.Message}");
            }
        }

        private bool IsRegularButton(Button button)
        {
            if (button?.Content == null) return false;

            string content = button.Content.ToString();
            return !new[] { "ShiftButton", "BackspaceButton", "SymbolsButton" }.Contains(button.Name) &&
                   !new[] { "Space", "Enter", "123", "ABC", "Shift", "Backspace" }.Contains(content);
        }

        private List<Button> GetAllButtons()
        {
            try
            {
                return this.GetVisualChildren()
                    .OfType<WrapPanel>()
                    .SelectMany(panel => panel.Children.OfType<Button>())
                    .ToList();
            }
            catch (Exception)
            {
                return new List<Button>();
            }
        }

        private void UpdateButtonsToSymbols(List<Button> buttons)
        {
            try
            {
                string[] symbols = GetSymbolsArray();
                int symbolIndex = 0;

                foreach (var button in buttons.Where(IsRegularButton))
                {
                    if (symbolIndex < symbols.Length)
                    {
                        button.Content = symbols[symbolIndex++];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating symbols: {ex.Message}");
            }
        }

        private void RestoreOriginalButtonContents(List<Button> buttons)
        {
            try
            {
                foreach (var button in buttons)
                {
                    if (originalButtonContents.ContainsKey(button))
                    {
                        button.Content = originalButtonContents[button];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error restoring button contents: {ex.Message}");
            }
        }

        private string[] GetSymbolsArray()
        {
            return new[]
            {
                "!", "@", "#", "$", "%", "^", "&", "*", "(", ")",
                "~", "`", "-", "_", "=", "+", "[", "]", "{", "}", "\\", "|",
                ";", ":", "'", "\"", "<", ">", "?", "/", "€", "£", "¥", "₺",
                "±", "×", "÷", "≠", "≈", "≤", "≥", "∞", "π", "µ"
            };
        }

        private string[] GetNormalKeysArray()
        {
            return new[]
            {
                "1", "2", "3", "4", "5", "6", "7", "8", "9", "0",
                "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "Ğ", "Ü",
                "A", "S", "D", "F", "G", "H", "J", "K", "L", "Ş", "İ",
                "Z", "X", "C", "V", "B", "N", "M", "Ö", "Ç", ",", "."
            };
        }
        #endregion

        #region Event Handlers
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Content != null)
                {
                    string value = button.Content.ToString();
                    value = !isShiftActive ? value.ToLower() : value;
                    KeyPressed?.Invoke(this, value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing button click: {ex.Message}");
            }
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            KeyPressed?.Invoke(this, "{BACKSPACE}");
        }

        private void Space_Click(object sender, RoutedEventArgs e)
        {
            KeyPressed?.Invoke(this, " ");
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            KeyPressed?.Invoke(this, "{ENTER}");
        }

        private void ShiftButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                isShiftActive = !isShiftActive;
                if (sender is Button shiftButton)
                {
                    shiftButton.Background = new SolidColorBrush(
                        isShiftActive ? Colors.LightGray : Colors.White
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error toggling shift: {ex.Message}");
            }
        }

        private void SymbolsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isInitialized)
                {
                    SaveOriginalButtonContents();
                    isInitialized = true;
                }

                isSymbolsActive = !isSymbolsActive;
                var buttons = GetAllButtons();

                if (sender is Button symbolsButton)
                {
                    symbolsButton.Content = isSymbolsActive ? "ABC" : "123";

                    string[] newContents = isSymbolsActive ? GetSymbolsArray() : GetNormalKeysArray();
                    int index = 0;

                    foreach (var button in buttons.Where(IsRegularButton))
                    {
                        if (index < newContents.Length)
                        {
                            button.Content = newContents[index++];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error toggling symbols: {ex.Message}");
            }
        }
        #endregion
    }
}
