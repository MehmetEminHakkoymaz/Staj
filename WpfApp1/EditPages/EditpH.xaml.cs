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
    /// <summary>
    /// Interaction logic for EditpH.xaml
    /// </summary>
    public partial class EditpH : Window
    {
        private DispatcherTimer clockTimer;
        public event EventHandler<string> ValueSelected;
        private Dictionary<string, (int Min, int Max)> textBoxLimits;
        private TextBox activeTextBox = null;

        public EditpH()
        {
            InitializeComponent();
            InitializeClock();
            InitializeComboBox();
            InitializeTextBoxLimits();
            this.Loaded += EditpH_Loaded;
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
                { "pHBaseBase", (0, 14) },
                { "pHAcidAcid", (0, 14) },
                { "pHBaseAcidBase", (0, 14) },
                { "pHBaseAcidAcid", (0, 14) }
            };
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
            // Sistem saatini "HH:mm:ss" formatında güncelle
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
        private void EditpH_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string savedCascade = Properties.Settings.Default.pHSelectedCascade;
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
                Properties.Settings.Default.pHSelectedCascade = content;

                contentArea.Content = null;
                contentArea.ContentTemplate = null;

                switch (content)
                {
                    case "None":
                        contentArea.ContentTemplate = (DataTemplate)contentArea.Resources["NoneTemplate"];
                        break;
                    case "Base":
                        var baseTamplate = (DataTemplate)contentArea.Resources["BaseTemplate"];
                        var baseContent = baseTamplate.LoadContent() as FrameworkElement;
                        if (baseContent != null)
                        {
                            var textBox = FindChild<TextBox>(baseContent, "pHBaseBase");
                            if (textBox != null)
                            {
                                textBox.GotFocus += TextBox_GotFocus;
                                textBox.Text = Properties.Settings.Default.pHBaseBase.ToString();
                            }
                            contentArea.Content = baseContent;
                        }
                        break;
                    case "Acid":
                        var acidTamplate = (DataTemplate)contentArea.Resources["AcidTemplate"];
                        var acidContent = acidTamplate.LoadContent() as FrameworkElement;
                        if (acidContent != null)
                        {
                            var textBox = FindChild<TextBox>(acidContent, "pHAcidAcid");
                            if (textBox != null)
                            {
                                textBox.GotFocus += TextBox_GotFocus;
                                textBox.Text = Properties.Settings.Default.pHAcidAcid.ToString();
                            }
                            contentArea.Content = acidContent;
                        }
                        break;
                    case "Base->Acid":
                        var baseAcidTemplate = (DataTemplate)contentArea.Resources["BaseAcidTemplate"];
                        var baseAcidContent = baseAcidTemplate.LoadContent() as FrameworkElement;
                        if (baseAcidContent != null)
                        {
                            var textBoxBase = FindChild<TextBox>(baseAcidContent, "pHBaseAcidBase");
                            if (textBoxBase != null)
                            {
                                textBoxBase.GotFocus += TextBox_GotFocus;
                                textBoxBase.Text = Properties.Settings.Default.pHBaseAcidBase.ToString();
                            }
                            var textBoxAcid = FindChild<TextBox>(baseAcidContent, "pHBaseAcidAcid");
                            if (textBoxAcid != null)
                            {
                                textBoxAcid.GotFocus += TextBox_GotFocus;
                                textBoxAcid.Text = Properties.Settings.Default.pHBaseAcidAcid.ToString();
                            }
                            contentArea.Content = baseAcidContent;
                        }
                        break;
                }
            }
        }

        private void LoadBaseValues()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                var basee = FindChild<TextBox>(contentArea, "pHBaseBase");
                if (basee != null)
                {
                    basee.Text = Properties.Settings.Default.pHBaseBase.ToString();
                    basee.PreviewTextInput += TextBox_PreviewTextInput;
                    basee.TextChanged += TextBox_TextChanged;
                }
            }));
        }

        private void SaveBaseValues()
        {
            var basee = FindChild<TextBox>(contentArea, "pHBaseBase");
            if (basee != null && int.TryParse(basee.Text, out int baseValue))
            {
                Properties.Settings.Default.pHBaseBase = baseValue;
            }
        }

        private void LoadAcidValues()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                var acid = FindChild<TextBox>(contentArea, "pHAcidAcid");
                if (acid != null)
                {
                    acid.Text = Properties.Settings.Default.pHAcidAcid.ToString();
                    acid.PreviewTextInput += TextBox_PreviewTextInput;
                    acid.TextChanged += TextBox_TextChanged;
                }
            }));
        }

        private void SaveAcidValues()
        {
            var acid = FindChild<TextBox>(contentArea, "pHAcidAcid");
            if (acid != null && int.TryParse(acid.Text, out int acidValue))
            {
                Properties.Settings.Default.pHAcidAcid = acidValue;
            }
        }

        private void LoadBaseAcidValues()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                var basee = FindChild<TextBox>(contentArea, "pHBaseAcidBase");
                if (basee != null)
                {
                    basee.Text = Properties.Settings.Default.pHBaseAcidBase.ToString();
                    basee.PreviewTextInput += TextBox_PreviewTextInput;
                    basee.TextChanged += TextBox_TextChanged;
                }
                var acid = FindChild<TextBox>(contentArea, "pHBaseAcidAcid");
                if (acid != null)
                {
                    acid.Text = Properties.Settings.Default.pHBaseAcidAcid.ToString();
                    acid.PreviewTextInput += TextBox_PreviewTextInput;
                    acid.TextChanged += TextBox_TextChanged;
                }
            }));
        }

        private void SaveBaseAcidValues()
        {
            var basee = FindChild<TextBox>(contentArea, "pHBaseAcidBase");
            if (basee != null && int.TryParse(basee.Text, out int baseValue))
            {
                Properties.Settings.Default.pHBaseAcidBase = baseValue;
            }
            var acid = FindChild<TextBox>(contentArea, "pHBaseAcidAcid");
            if (acid != null && int.TryParse(acid.Text, out int acidValue))
            {
                Properties.Settings.Default.pHBaseAcidAcid = acidValue;
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

        private void SaveCurrentValues()
        {
            if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string content = selectedItem.Content.ToString();
                Properties.Settings.Default.pHSelectedCascade = content;

                switch (content)
                {
                    case "Base":
                        SaveBaseValues();
                        break;
                    case "Acid":
                        SaveAcidValues();
                        break;
                    case "Base->Acid":
                        SaveBaseAcidValues();
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