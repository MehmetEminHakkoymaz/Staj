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
        private TextBox activeTextBox = null;

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

                // Önce mevcut içeriği temizle
                contentArea.Content = null;
                contentArea.ContentTemplate = null;

                switch (content)
                {
                    case "None":
                        contentArea.ContentTemplate = (DataTemplate)contentArea.Resources["NoneTemplate"];
                        break;
                    case "Stirrer":
                        var stirrerTemplate = (DataTemplate)contentArea.Resources["StirrerTemplate"];
                        var stirrerContent = stirrerTemplate.LoadContent() as FrameworkElement;
                        if (stirrerContent != null)
                        {
                            var textBox = FindChild<TextBox>(stirrerContent, "pO2StirrerRPM");
                            if (textBox != null)
                            {
                                textBox.GotFocus += TextBox_GotFocus;
                                textBox.Text = Properties.Settings.Default.pO2StirrerRPM.ToString();
                            }
                            contentArea.Content = stirrerContent;
                        }
                        break;
                    case "TotalFlow":
                        var totalFlowTemplate = (DataTemplate)contentArea.Resources["TotalFlowTemplate"];
                        var totalFlowContent = totalFlowTemplate.LoadContent() as FrameworkElement;
                        if (totalFlowContent != null)
                        {
                            var textBox = FindChild<TextBox>(totalFlowContent, "pO2TotalFlowTotalFlow");
                            if (textBox != null)
                            {
                                textBox.GotFocus += TextBox_GotFocus;
                                textBox.Text = Properties.Settings.Default.pO2TotalFlowTotalFlow.ToString();
                            }
                            contentArea.Content = totalFlowContent;
                        }
                        break;
                    case "GasMix":
                        var gasMixTemplate = (DataTemplate)contentArea.Resources["GasMixTemplate"];
                        var gasMixContent = gasMixTemplate.LoadContent() as FrameworkElement;
                        if (gasMixContent != null)
                        {
                            var textBox = FindChild<TextBox>(gasMixContent, "pO2GasMixGasMix");
                            if (textBox != null)
                            {
                                textBox.GotFocus += TextBox_GotFocus;
                                textBox.Text = Properties.Settings.Default.pO2GasMixGasMix.ToString();
                            }
                            contentArea.Content = gasMixContent;
                        }
                        break;
                    case "Stirrer->TotalFlow":
                        var stirrerTotalFlowTemplate = (DataTemplate)contentArea.Resources["StirrerTotalFlowTemplate"];
                        var stirrerTotalFlowContent = stirrerTotalFlowTemplate.LoadContent() as FrameworkElement;
                        if (stirrerTotalFlowContent != null)
                        {
                            var rpm = FindChild<TextBox>(stirrerTotalFlowContent, "pO2StirrerTotalFlowRPM");
                            if (rpm != null)
                            {
                                rpm.GotFocus += TextBox_GotFocus;
                                rpm.Text = Properties.Settings.Default.pO2StirrerTotalFlowRPM.ToString();
                            }
                            var totalFlow = FindChild<TextBox>(stirrerTotalFlowContent, "pO2StirrerTotalFlowTotalFlow");
                            if (totalFlow != null)
                            {
                                totalFlow.GotFocus += TextBox_GotFocus;
                                totalFlow.Text = Properties.Settings.Default.pO2StirrerTotalFlowTotalFlow.ToString();
                            }
                            contentArea.Content = stirrerTotalFlowContent;
                        }
                        break;
                    case "Stirrer->GasMix":
                        var stirrerGasMixTemplate = (DataTemplate)contentArea.Resources["StirrerGasMixTemplate"];
                        var stirrerGasMixContent = stirrerGasMixTemplate.LoadContent() as FrameworkElement;
                        if (stirrerGasMixContent != null)
                        {
                            var rpm = FindChild<TextBox>(stirrerGasMixContent, "pO2StirrerGasMixRPM");
                            if (rpm != null)
                            {
                                rpm.GotFocus += TextBox_GotFocus;
                                rpm.Text = Properties.Settings.Default.pO2StirrerGasMixRPM.ToString();
                            }
                            var gasMix = FindChild<TextBox>(stirrerGasMixContent, "pO2StirrerGasMixGasMix");
                            if (gasMix != null)
                            {
                                gasMix.GotFocus += TextBox_GotFocus;
                                gasMix.Text = Properties.Settings.Default.pO2StirrerGasMixGasMix.ToString();
                            }
                            contentArea.Content = stirrerGasMixContent;
                        }
                        break;
                    case "Stirrer->TotalFlow->GasMix":
                        var stirrerTotalFlowGasMixTemplate = (DataTemplate)contentArea.Resources["StirrerTotalFlowGasMixTemplate"];
                        var stirrerTotalFlowGasMixContent = stirrerTotalFlowGasMixTemplate.LoadContent() as FrameworkElement;
                        if (stirrerTotalFlowGasMixContent != null)
                        {
                            var rpm = FindChild<TextBox>(stirrerTotalFlowGasMixContent, "pO2StirrerTotalFlowGasMixRPM");
                            if (rpm != null)
                            {
                                rpm.GotFocus += TextBox_GotFocus;
                                rpm.Text = Properties.Settings.Default.pO2StirrerTotalFlowGasMixRPM.ToString();
                            }
                            var totalFlow = FindChild<TextBox>(stirrerTotalFlowGasMixContent, "pO2StirrerTotalFlowGasMixTotalFlow");
                            if (totalFlow != null)
                            {
                                totalFlow.GotFocus += TextBox_GotFocus;
                                totalFlow.Text = Properties.Settings.Default.pO2StirrerTotalFlowGasMixTotalFlow.ToString();
                            }
                            var gasMix = FindChild<TextBox>(stirrerTotalFlowGasMixContent, "pO2StirrerTotalFlowGasMixGasMix");
                            if (gasMix != null)
                            {
                                gasMix.GotFocus += TextBox_GotFocus;
                                gasMix.Text = Properties.Settings.Default.pO2StirrerTotalFlowGasMixGasMix.ToString();
                            }
                            contentArea.Content = stirrerTotalFlowGasMixContent;
                        }
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

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                activeTextBox = textBox;

                // Label içeriğini bulmak için parent container'ı kontrol et
                var parent = VisualTreeHelper.GetParent(textBox);
                while (parent != null)
                {
                    if (parent is Grid grid)
                    {
                        var label = grid.Children.OfType<Label>().FirstOrDefault();
                        if (label != null)
                        {
                            KeypadPopup.IsOpen = true;
                            KeypadControl.SetLabelContent(label.Content.ToString());
                            break;
                        }
                    }
                    parent = VisualTreeHelper.GetParent(parent);
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

        private void RegisterTextBoxEvents(DependencyObject parent)
        {
            // Verilen parent içindeki tüm TextBox'ları bul
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is TextBox textBox)
                {
                    // TextBox'a GotFocus event'ini bağla
                    textBox.GotFocus += TextBox_GotFocus;
                }
                else
                {
                    // Recursive olarak diğer container'ları kontrol et
                    RegisterTextBoxEvents(child);
                }
            }
        }
    }
}
