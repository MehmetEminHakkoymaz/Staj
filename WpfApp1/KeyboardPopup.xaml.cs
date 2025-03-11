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
        public string PreviewText
        {
            get => PreviewTextBox?.Text ?? string.Empty;
            set
            {
                if (PreviewTextBox != null)
                    PreviewTextBox.Text = value;
            }
        }

        #region Events
        public event EventHandler<string> KeyPressed;
        public event EventHandler CloseRequested;

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
                    SetInitialKeyboardState();
                    isInitialized = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in InitializeKeyboard: {ex.Message}");
            }
        }

        private void SetInitialKeyboardState()
        {
            try
            {
                var buttons = GetAllButtons();
                int index = 0;
                var initialContents = GetLowerCaseArray();

                foreach (var button in buttons.Where(IsRegularButton))
                {
                    if (index < initialContents.Length)
                    {
                        button.Content = initialContents[index++];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting initial keyboard state: {ex.Message}");
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

        private string[] GetSymbolsArray() => new[]
        {
            "!", "@", "#", "$", "%", "^", "&", "*", "(", ")",
            "~", "`", "-", "_", "=", "+", "[", "]", "{", "}", "\\", "|",
            ";", ":", "'", "\"", "<", ">", "?", "/", "€", "£", "¥", "₺",
            "±", "×", "÷", "≠", "≈", "≤", "≥", "∞", "π", "µ"
        };

        private string[] GetLowerCaseArray() => new[]
        {
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "0",
            "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "ğ", "ü",
            "a", "s", "d", "f", "g", "h", "j", "k", "l", "ş", "i",
            "z", "x", "c", "v", "b", "n", "m", "ö", "ç", ",", "."
        };

        private string[] GetUpperCaseArray() => new[]
        {
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "0",
            "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "Ğ", "Ü",
            "A", "S", "D", "F", "G", "H", "J", "K", "L", "Ş", "İ",
            "Z", "X", "C", "V", "B", "N", "M", "Ö", "Ç", ",", "."
        };

        private void UpdateButtonContents(string[] newContents)
        {
            try
            {
                var buttons = GetAllButtons();
                int index = 0;

                foreach (var button in buttons.Where(IsRegularButton))
                {
                    if (index < newContents.Length)
                    {
                        button.Content = newContents[index++];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating button contents: {ex.Message}");
            }
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
                    KeyPressed?.Invoke(this, value);
                    // Önizleme eklentisi
                    if (PreviewTextBox != null)
                        PreviewTextBox.Text += value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing button click: {ex.Message}");
            }
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            // Önizleme için backspace işlemi
            if (PreviewTextBox?.Text?.Length > 0)
                PreviewTextBox.Text = PreviewTextBox.Text.Substring(0, PreviewTextBox.Text.Length - 1);

            KeyPressed?.Invoke(this, "{BACKSPACE}");
        }

        private void Space_Click(object sender, RoutedEventArgs e)
        {
            // Önizleme için space işlemi
            if (PreviewTextBox != null)
                PreviewTextBox.Text += " ";

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
                if (isSymbolsActive) return; // Sembol modunda Shift işlevsiz

                isShiftActive = !isShiftActive;
                if (sender is Button shiftButton)
                {
                    // Shift butonunun görsel değişimi
                    shiftButton.Background = new SolidColorBrush(
                        isShiftActive ? Colors.LightGray : Colors.White
                    );

                    // Tuş içeriklerini güncelle
                    UpdateButtonContents(isShiftActive ? GetUpperCaseArray() : GetLowerCaseArray());
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
                
                if (sender is Button symbolsButton)
                {
                    symbolsButton.Content = isSymbolsActive ? "ABC" : "123";

                    if (isSymbolsActive)
                    {
                        // Sembol moduna geçerken
                        UpdateButtonContents(GetSymbolsArray());
                        // Shift'i sıfırla
                        isShiftActive = false;
                        if (ShiftButton != null)
                        {
                            ShiftButton.Background = new SolidColorBrush(Colors.White);
                        }
                    }
                    else
                    {
                        // Normal moda dönerken
                        UpdateButtonContents(isShiftActive ? GetUpperCaseArray() : GetLowerCaseArray());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error toggling symbols: {ex.Message}");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // CloseRequested event'ini tetikle
                CloseRequested?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error closing keyboard: {ex.Message}");
            }
        }
        #endregion
    }
}
