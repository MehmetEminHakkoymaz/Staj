using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfApp1;

namespace WpfApp1.EditPages
{
    public partial class EditpO2 : Window
    {
        public event EventHandler<string> ValueSelected;
        private DispatcherTimer clockTimer;
        private Dictionary<string, (int Min, int Max)> textBoxLimits;

        public EditpO2()
        {
            InitializeComponent();
            InitializeClock();
            InitializeComboBox();
            InitializeTextBoxLimits();
            this.Loaded += EditpO2_Loaded;
            KeypadControl.ValueSelected += KeyPadControl_ValueSelected;

            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
            this.Topmost = true;
        }

        private void InitializeTextBoxLimits()
        {
            textBoxLimits = new Dictionary<string, (int Min, int Max)>
            {
                {"pO2StirrerRPM", (0, 1400)},
                {"pO2TotalFlowTotalFlow", (0, 4000)},
                {"pO2GasMixGasMix", (21, 100)},
                {"pO2StirrerTotalFlowRPM", (0, 1400)},
                {"pO2StirrerTotalFlowTotalFlow", (0, 4000)},
                {"pO2StirrerGasMixRPM", (0, 1400)},
                {"pO2StirrerGasMixGasMix", (21, 100)},
                {"pO2StirrerTotalFlowGasMixRPM", (0, 1400)},
                {"pO2StirrerTotalFlowGasMixTotalFlow", (0, 4000)},
                {"pO2StirrerTotalFlowGasMixGasMix", (21, 100)}
            };
        }

        private void EditpO2_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string savedCascade = Properties.Settings.Default.pO2SelectedCascade;
                contentComboBox.SelectedValue = savedCascade;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading saved settings: {ex.Message}");
            }
        }

        private void InitializeComboBox()
        {
            // ComboBox'a SelectionChanged event'ini ekle
            contentComboBox.SelectionChanged += ContentComboBox_SelectionChanged;

            // Başlangıçta None seçili olsun
            contentComboBox.SelectedIndex = 0;
        }

        private void ContentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string content = selectedItem.Content.ToString();
                Properties.Settings.Default.pO2SelectedCascade = content;

                switch (content)
                {
                    case "None":
                        contentArea.ContentTemplate = (DataTemplate)contentArea.Resources["NoneTemplate"];
                        break;
                    case "Stirrer":
                        contentArea.ContentTemplate = (DataTemplate)contentArea.Resources["StirrerTemplate"];
                        LoadStirrerValues();
                        break;
                    case "TotalFlow":
                        contentArea.ContentTemplate = (DataTemplate)contentArea.Resources["TotalFlowTemplate"];
                        loadTotalFlowValues();
                        break;
                    case "GasMix":
                        contentArea.ContentTemplate = (DataTemplate)contentArea.Resources["GasMixTemplate"];
                        loadGasMixValues();
                        break;
                    case "Stirrer->TotalFlow":
                        contentArea.ContentTemplate = (DataTemplate)contentArea.Resources["StirrerTotalFlowTemplate"];
                        loadStirrerTotalFlowValues();
                        break;
                    case "Stirrer->GasMix":
                        contentArea.ContentTemplate = (DataTemplate)contentArea.Resources["StirrerGasMixTemplate"];
                        loadStirrerGasMixValues();
                        break;
                    case "Stirrer->TotalFlow->GasMix":
                        contentArea.ContentTemplate = (DataTemplate)contentArea.Resources["StirrerTotalFlowGasMixTemplate"];
                        loadStirrerTotalFlowGasMix();
                        break;
                }
            }
        }

        private void LoadStirrerValues()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                var rpm = FindChild<TextBox>(contentArea, "pO2StirrerRPM");
                if (rpm != null)
                {
                    rpm.Text = Properties.Settings.Default.pO2StirrerRPM.ToString();
                    rpm.PreviewTextInput += TextBox_PreviewTextInput;
                    rpm.TextChanged += TextBox_TextChanged;
                }
            }));
        }

        private void SaveStirrerValues()
        {
            var rpm = FindChild<TextBox>(contentArea, "pO2StirrerRPM");
            if (rpm != null && int.TryParse(rpm.Text, out int rpmValue))
            {
                Properties.Settings.Default.pO2StirrerRPM = rpmValue;
            }
        }

        private void loadTotalFlowValues()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                var totalFlow = FindChild<TextBox>(contentArea, "pO2TotalFlowTotalFlow");
                if (totalFlow != null)
                {
                    totalFlow.Text = Properties.Settings.Default.pO2TotalFlowTotalFlow.ToString();
                    totalFlow.PreviewTextInput += TextBox_PreviewTextInput;
                    totalFlow.TextChanged += TextBox_TextChanged;
                }
            }));
        }

        private void SaveTotalFlowValues()
        {
            var totalFlow = FindChild<TextBox>(contentArea, "pO2TotalFlowTotalFlow");
            if (totalFlow != null && int.TryParse(totalFlow.Text, out int totalFlowValue))
            {
                Properties.Settings.Default.pO2TotalFlowTotalFlow = totalFlowValue;
            }
        }

        private void loadGasMixValues()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                var gasMix = FindChild<TextBox>(contentArea, "pO2GasMixGasMix");
                if (gasMix != null)
                {
                    gasMix.Text = Properties.Settings.Default.pO2GasMixGasMix.ToString();
                    gasMix.PreviewTextInput += TextBox_PreviewTextInput;
                    gasMix.TextChanged += TextBox_TextChanged;
                }
            }));
        }

        private void SaveGasMixValues()
        {
            var gasMix = FindChild<TextBox>(contentArea, "pO2GasMixGasMix");
            if (gasMix != null && int.TryParse(gasMix.Text, out int gasMixValue))
            {
                Properties.Settings.Default.pO2GasMixGasMix = gasMixValue;
            }
        }

        private void loadStirrerTotalFlowValues()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                var rpm = FindChild<TextBox>(contentArea, "pO2StirrerTotalFlowRPM");
                if (rpm != null)
                {
                    rpm.Text = Properties.Settings.Default.pO2StirrerTotalFlowRPM.ToString();
                    rpm.PreviewTextInput += TextBox_PreviewTextInput;
                    rpm.TextChanged += TextBox_TextChanged;
                }
                var totalFlow = FindChild<TextBox>(contentArea, "pO2StirrerTotalFlowTotalFlow");
                if (totalFlow != null)
                {
                    totalFlow.Text = Properties.Settings.Default.pO2StirrerTotalFlowTotalFlow.ToString();
                    totalFlow.PreviewTextInput += TextBox_PreviewTextInput;
                    totalFlow.TextChanged += TextBox_TextChanged;
                }
            }));
        }

        private void SaveStirrerTotalFlowValues()
        {
            var rpm = FindChild<TextBox>(contentArea, "pO2StirrerTotalFlowRPM");
            if (rpm != null && int.TryParse(rpm.Text, out int rpmValue))
            {
                Properties.Settings.Default.pO2StirrerTotalFlowRPM = rpmValue;
            }
            var totalFlow = FindChild<TextBox>(contentArea, "pO2StirrerTotalFlowTotalFlow");
            if (totalFlow != null && int.TryParse(totalFlow.Text, out int totalFlowValue))
            {
                Properties.Settings.Default.pO2StirrerTotalFlowTotalFlow = totalFlowValue;
            }
        }

        private void loadStirrerGasMixValues()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                var rpm = FindChild<TextBox>(contentArea, "pO2StirrerGasMixRPM");
                if (rpm != null)
                {
                    rpm.Text = Properties.Settings.Default.pO2StirrerGasMixRPM.ToString();
                    rpm.PreviewTextInput += TextBox_PreviewTextInput;
                    rpm.TextChanged += TextBox_TextChanged;
                }
                var gasMix = FindChild<TextBox>(contentArea, "pO2StirrerGasMixGasMix");
                if (gasMix != null)
                {
                    gasMix.Text = Properties.Settings.Default.pO2StirrerGasMixGasMix.ToString();
                    gasMix.PreviewTextInput += TextBox_PreviewTextInput;
                    gasMix.TextChanged += TextBox_TextChanged;
                }
            }));
        }

        private void SaveStirrerGasMixValues()
        {
            var rpm = FindChild<TextBox>(contentArea, "pO2StirrerGasMixRPM");
            if (rpm != null && int.TryParse(rpm.Text, out int rpmValue))
            {
                Properties.Settings.Default.pO2StirrerGasMixRPM = rpmValue;
            }
            var gasMix = FindChild<TextBox>(contentArea, "pO2StirrerGasMixGasMix");
            if (gasMix != null && int.TryParse(gasMix.Text, out int gasMixValue))
            {
                Properties.Settings.Default.pO2StirrerGasMixGasMix = gasMixValue;
            }
        }

        private void loadStirrerTotalFlowGasMix()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                var rpm = FindChild<TextBox>(contentArea, "pO2StirrerTotalFlowGasMixRPM");
                if (rpm != null)
                {
                    rpm.Text = Properties.Settings.Default.pO2StirrerTotalFlowGasMixRPM.ToString();
                    rpm.PreviewTextInput += TextBox_PreviewTextInput;
                    rpm.TextChanged += TextBox_TextChanged;
                }
                var totalFlow = FindChild<TextBox>(contentArea, "pO2StirrerTotalFlowGasMixTotalFlow");
                if (totalFlow != null)
                {
                    totalFlow.Text = Properties.Settings.Default.pO2StirrerTotalFlowGasMixTotalFlow.ToString();
                    totalFlow.PreviewTextInput += TextBox_PreviewTextInput;
                    totalFlow.TextChanged += TextBox_TextChanged;
                }
                var gasMix = FindChild<TextBox>(contentArea, "pO2StirrerTotalFlowGasMixGasMix");
                if (gasMix != null)
                {
                    gasMix.Text = Properties.Settings.Default.pO2StirrerTotalFlowGasMixGasMix.ToString();
                    gasMix.PreviewTextInput += TextBox_PreviewTextInput;
                    gasMix.TextChanged += TextBox_TextChanged;
                }
            }));
        }

        private void SaveStirrerTotalFlowGasMix()
        {
            var rpm = FindChild<TextBox>(contentArea, "pO2StirrerTotalFlowGasMixRPM");
            if (rpm != null && int.TryParse(rpm.Text, out int rpmValue))
            {
                Properties.Settings.Default.pO2StirrerTotalFlowGasMixRPM = rpmValue;
            }
            var totalFlow = FindChild<TextBox>(contentArea, "pO2StirrerTotalFlowGasMixTotalFlow");
            if (totalFlow != null && int.TryParse(totalFlow.Text, out int totalFlowValue))
            {
                Properties.Settings.Default.pO2StirrerTotalFlowGasMixTotalFlow = totalFlowValue;
            }
            var gasMix = FindChild<TextBox>(contentArea, "pO2StirrerTotalFlowGasMixGasMix");
            if (gasMix != null && int.TryParse(gasMix.Text, out int gasMixValue))
            {
                Properties.Settings.Default.pO2StirrerTotalFlowGasMixGasMix = gasMixValue;
            }
        }


        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox && !string.IsNullOrEmpty(textBox.Text))
            {
                if (textBoxLimits.TryGetValue(textBox.Name, out var limits))
                {
                    if (int.TryParse(textBox.Text, out int value))
                    {
                        if (value < limits.Min) textBox.Text = limits.Min.ToString();
                        if (value > limits.Max) textBox.Text = limits.Max.ToString();
                    }
                }
            }
        }

        private void InitializeClock()
        {
            clockTimer = new DispatcherTimer();
            clockTimer.Interval = TimeSpan.FromSeconds(1);
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            ClockTextBlock.Text = DateTime.Now.ToString("HH : mm : ss");
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveCurrentValues();
                Properties.Settings.Default.Save();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}");
            }
        }

        private void SaveCurrentValues()
        {
            if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string content = selectedItem.Content.ToString();
                Properties.Settings.Default.pO2SelectedCascade = content;

                switch (content)
                {
                    case "Stirrer":
                        SaveStirrerValues();
                        break;
                    case "TotalFlow":
                        SaveTotalFlowValues();
                        break;
                    case "GasMix":
                        SaveGasMixValues();
                        break;
                    case "Stirrer->TotalFlow":
                        SaveStirrerTotalFlowValues();
                        break;
                    case "Stirrer->GasMix":
                        SaveStirrerGasMixValues();
                        break;
                    case "Stirrer->TotalFlow->GasMix":
                        SaveStirrerTotalFlowGasMix();
                        break;
                        // Diğer case'ler...
                }
            }
        }

        private static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T childType)
                {
                    if (child is FrameworkElement frameworkElement && frameworkElement.Name == childName)
                    {
                        foundChild = childType;
                        break;
                    }
                }

                foundChild = FindChild<T>(child, childName);
                if (foundChild != null) break;
            }
            return foundChild;
        }

        private TextBox activeTextBox = null;

        private TextBox currentTextBox = null;

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox focusedTextBox = sender as TextBox;
            if (focusedTextBox != null)
            {
                // TextBox'ın ebeveyninin ebeveynini bul (Grid varsayıyoruz)
                DependencyObject parent = VisualTreeHelper.GetParent(focusedTextBox);
                DependencyObject grandParent = parent != null ? VisualTreeHelper.GetParent(parent) : null;
                Grid parentGrid = grandParent as Grid;
                activeTextBox = sender as TextBox; // Odaklanan TextBox'ı aktif olarak ayarla
                if (parentGrid != null)
                {
                    // Grid içindeki ilk Label'ı bul
                    Label firstLabel = parentGrid.Children.OfType<Label>().FirstOrDefault();
                    if (firstLabel != null)
                    {
                        // Label'ın içeriğini al
                        string labelContent = firstLabel.Content.ToString();
                        // KeyPad'e label içeriğini gönder
                        activeTextBox = sender as TextBox;
                        KeypadPopup.IsOpen = true;
                        KeypadControl.SetLabelContent(labelContent);
                    }
                }
            }
        }

        private void KeyPadControl_ValueSelected(object sender, string value)
        {
            if (activeTextBox != null)
            {
                activeTextBox.Text = value; // KeyPad'den gelen değeri aktif TextBox'a atayın
            }
        }
    }
}
