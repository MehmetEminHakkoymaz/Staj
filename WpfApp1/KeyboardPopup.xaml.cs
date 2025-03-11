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
    // Önce sınıfın dışında yeni bir static sınıf oluşturuyoruz
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
            isShiftActive = false;
            isSymbolsActive = false;
            SaveOriginalButtonContents();
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
            return button.Name != "ShiftButton" &&
                   button.Name != "BackspaceButton" &&
                   content != "Space" &&
                   content != "Enter" &&
                   content != "123" &&
                   content != "ABC";
        }

        private List<Button> GetAllButtons()
        {
            try
            {
                // this parametresini ekleyerek GetVisualChildren'ı çağırıyoruz
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

        private string[] GetSymbolsArray()
        {
            return new[]
            {
                // Sayılar ve semboller
                "!", "@", "#", "$", "%", "^", "&", "*", "(", ")",
                
                // Özel karakterler
                "~", "`", "-", "_", "=", "+", "[", "]", "{", "}", "\\", "|",
                
                // Noktalama işaretleri
                ";", ":", "'", "\"", "<", ">", "?", "/", "€", "£", "¥", "₺",
                
                // Matematik sembolleri
                "±", "×", "÷", "≠", "≈", "≤", "≥", "∞", "π", "µ"
            };
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
        #endregion

        #region Event Handlers
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button)
                {
                    string value = button.Content?.ToString() ?? string.Empty;
                    if (!isShiftActive)
                    {
                        value = value.ToLower();
                    }
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
                if (ShiftButton != null)
                {
                    ShiftButton.Background = new SolidColorBrush(
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
                isSymbolsActive = !isSymbolsActive;

                if (sender is Button button)
                {
                    button.Content = isSymbolsActive ? "ABC" : "123";
                }

                var buttons = GetAllButtons();
                if (isSymbolsActive)
                {
                    UpdateButtonsToSymbols(buttons);
                }
                else
                {
                    RestoreOriginalButtonContents(buttons);
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