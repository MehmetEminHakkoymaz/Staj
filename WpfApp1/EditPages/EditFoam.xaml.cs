using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Globalization;  
using WpfApp1;


namespace WpfApp1.EditPages
{


    public partial class EditFoam : Window
    {
        private TextBox activeTextBox = null;
        private DispatcherTimer clockTimer;

        public EditFoam()
        {
            InitializeComponent();
            InitializeClock();
            this.Loaded += EditFoam_Loaded;
            KeypadControl.ValueSelected += KeyPadControl_ValueSelected;

            // Son seçilen modu yükle ve ilgili toggle butonunu işaretle
            LoadLastSelectedMode();
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
            this.Topmost = true;
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
        private void LoadLastSelectedMode()
        {
            // EditFoamCascade değerini al (0=None, 1=AntiFoam, 2=Level)
            double cascadeValue = Properties.Settings.Default.EditFoamCascade;
            string selectedMode;

            // Değeri string moda dönüştür
            switch ((int)cascadeValue)
            {
                case 1:
                    selectedMode = "AntiFoam";
                    AntiFoam.IsChecked = true;
                    break;
                case 2:
                    selectedMode = "Level";
                    Level.IsChecked = true;
                    break;
                default: // 0 ve diğer durumlar
                    selectedMode = "None";
                    None.IsChecked = true;
                    break;
            }

            // İçeriği yükle
            LoadContent(selectedMode);
        }

        private void LoadContent(string mode)
        {
            // Seçilen moda göre içeriği yükle
            switch (mode)
            {
                case "AntiFoam":
                    ParametersContent.ContentTemplate = (DataTemplate)ParametersContent.Resources["AntiFoamTemplate"];
                    ParametersContent.ApplyTemplate();

                    var antiFoamContent = ParametersContent.ContentTemplate.LoadContent() as FrameworkElement;
                    if (antiFoamContent != null)
                    {
                        var doseTimeBox = FindChild<TextBox>(antiFoamContent, "AntiFoamDoseTime");
                        if (doseTimeBox != null)
                        {
                            doseTimeBox.Text = Properties.Settings.Default.EditFoamDoseTime.ToString(CultureInfo.CurrentCulture);
                            doseTimeBox.GotFocus += TextBox_GotFocus;
                            doseTimeBox.PreviewTextInput += TextBox_PreviewTextInput;
                        }

                        var waitTimeBox = FindChild<TextBox>(antiFoamContent, "AntiFoamWaitTime");
                        if (waitTimeBox != null)
                        {
                            waitTimeBox.Text = Properties.Settings.Default.EditFoamWaitTime.ToString(CultureInfo.CurrentCulture);
                            waitTimeBox.GotFocus += TextBox_GotFocus;
                            waitTimeBox.PreviewTextInput += TextBox_PreviewTextInput;
                        }

                        var alarmTimeBox = FindChild<TextBox>(antiFoamContent, "AntiFoamAlarmTime");
                        if (alarmTimeBox != null)
                        {
                            alarmTimeBox.Text = Properties.Settings.Default.EditFoamAlarmTime.ToString(CultureInfo.CurrentCulture);
                            alarmTimeBox.GotFocus += TextBox_GotFocus;
                            alarmTimeBox.PreviewTextInput += TextBox_PreviewTextInput;
                        }

                        ParametersContent.Content = antiFoamContent;
                    }
                    break;

                case "Level":
                    ParametersContent.ContentTemplate = (DataTemplate)ParametersContent.Resources["LevelTemplate"];
                    ParametersContent.ApplyTemplate();

                    var levelContent = ParametersContent.ContentTemplate.LoadContent() as FrameworkElement;
                    if (levelContent != null)
                    {
                        var doseTimeBox = FindChild<TextBox>(levelContent, "LevelDoseTime");
                        if (doseTimeBox != null)
                        {
                            doseTimeBox.Text = Properties.Settings.Default.EditFoamDoseTime.ToString(CultureInfo.CurrentCulture);
                            doseTimeBox.GotFocus += TextBox_GotFocus;
                            doseTimeBox.PreviewTextInput += TextBox_PreviewTextInput;
                        }

                        var waitTimeBox = FindChild<TextBox>(levelContent, "LevelWaitTime");
                        if (waitTimeBox != null)
                        {
                            waitTimeBox.Text = Properties.Settings.Default.EditFoamWaitTime.ToString(CultureInfo.CurrentCulture);
                            waitTimeBox.GotFocus += TextBox_GotFocus;
                            waitTimeBox.PreviewTextInput += TextBox_PreviewTextInput;
                        }

                        var alarmTimeBox = FindChild<TextBox>(levelContent, "LevelAlarmTime");
                        if (alarmTimeBox != null)
                        {
                            alarmTimeBox.Text = Properties.Settings.Default.EditFoamAlarmTime.ToString(CultureInfo.CurrentCulture);
                            alarmTimeBox.GotFocus += TextBox_GotFocus;
                            alarmTimeBox.PreviewTextInput += TextBox_PreviewTextInput;
                        }

                        ParametersContent.Content = levelContent;
                    }
                    break;

                default:
                    // None seçildiğinde içerik temizlenir
                    ParametersContent.ContentTemplate = null;
                    ParametersContent.Content = null;
                    break;
            }
        }


        private void EditFoam_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // EditFoamCascade değerine göre doğru içeriği yükle
                double cascadeValue = Properties.Settings.Default.EditFoamCascade;

                switch ((int)cascadeValue)
                {
                    case 0:
                        None.IsChecked = true;
                        break;
                    case 1:
                        AntiFoam.IsChecked = true;
                        LoadFoamValues("AntiFoam");
                        break;
                    case 2:
                        Level.IsChecked = true;
                        LoadFoamValues("Level");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading saved settings: {ex.Message}");
            }
        }



        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Seçili moda göre EditFoamCascade değerini ayarla
                if (None.IsChecked == true)
                {
                    Properties.Settings.Default.EditFoamCascade = 0;
                    Properties.Settings.Default.EditPump3Feature = 1;
                    Properties.Settings.Default.Pump3TargetBorder = 0;
                    Properties.Settings.Default.Save();
                    // EditPump3Feature double tipinde
                }
                else if (AntiFoam.IsChecked == true)
                {
                    Properties.Settings.Default.EditFoamCascade = 1;
                    Properties.Settings.Default.Pump3TargetBorder = 1;
                    Properties.Settings.Default.Save();
                    SaveFoamValues("AntiFoam");
                }
                else if (Level.IsChecked == true)
                {
                    Properties.Settings.Default.EditFoamCascade = 2;
                    Properties.Settings.Default.Pump3TargetBorder = 1;
                    Properties.Settings.Default.Save();
                    SaveFoamValues("Level");
                }

                Properties.Settings.Default.Save();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}");
            }
        }

        // LogicalTreeHelper için yardımcı extension metod
        private static IEnumerable<T> FindLogicalChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                foreach (object rawChild in LogicalTreeHelper.GetChildren(depObj))
                {
                    if (rawChild is DependencyObject)
                    {
                        DependencyObject child = (DependencyObject)rawChild;
                        if (child is T)
                            yield return (T)child;

                        foreach (T childOfChild in FindLogicalChildren<T>(child))
                            yield return childOfChild;
                    }
                }
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void HandleButtonToggle(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton toggleButton)
            {
                // Diğer düğmelerin seçimini kaldır
                foreach (var button in new[] { None, AntiFoam, Level })
                {
                    if (button != toggleButton)
                        button.IsChecked = false;
                }

                // Seçilen düğmenin adını al
                string selectedMode = toggleButton.Name;

                // EditFoamCascade değerini seçilen moda göre ayarla
                switch (selectedMode)
                {
                    case "None":
                        Properties.Settings.Default.EditFoamCascade = 0;
                        break;
                    case "AntiFoam":
                        Properties.Settings.Default.EditFoamCascade = 1;
                        break;
                    case "Level":
                        Properties.Settings.Default.EditFoamCascade = 2;
                        break;
                }

                // İlgili içeriği yükle
                LoadContent(selectedMode);
            }
        }


        private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = System.Windows.Media.VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = System.Windows.Media.VisualTreeHelper.GetChild(parent, i);

                if (child is T t)
                {
                    if (child is FrameworkElement element && element.Name == childName)
                    {
                        foundChild = t;
                        break;
                    }
                }

                foundChild = FindChild<T>(child, childName);

                if (foundChild != null)
                    break;
            }

            return foundChild;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Sadece rakam, nokta ve virgüle izin ver
            bool isValid = e.Text.All(c => char.IsDigit(c) || c == '.' || c == ',');

            // Eğer nokta veya virgül ise, TextBox'ta zaten bir tane var mı kontrol et
            if (isValid && (e.Text == "." || e.Text == ","))
            {
                if (sender is TextBox textBox)
                {
                    isValid = !textBox.Text.Contains(".") && !textBox.Text.Contains(",");
                }
            }

            e.Handled = !isValid;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            activeTextBox = sender as TextBox;
            if (activeTextBox != null)
            {
                // Label içeriğini bulma girişimi yap
                DependencyObject parent = System.Windows.Media.VisualTreeHelper.GetParent(activeTextBox);
                while (parent != null)
                {
                    if (parent is Grid grid)
                    {
                        var label = grid.Children.OfType<Label>().FirstOrDefault();
                        if (label != null)
                        {
                            // Label'ı KeyPad'e göndererek açılır klavyeyi etkinleştir
                            KeypadPopup.IsOpen = true;
                            KeypadControl.SetLabelContent(label.Content.ToString());
                            break;
                        }
                    }
                    parent = System.Windows.Media.VisualTreeHelper.GetParent(parent);
                }
            }
        }

        private void KeyPadControl_ValueSelected(object sender, string value)
        {
            if (activeTextBox != null)
            {
                // Nokta veya virgül içeren değerleri düzgün işle
                string normalizedValue = value.Replace(',', '.');

                // Tag'de belirtilen limit değerlerini kontrol et
                if (activeTextBox.Tag is string tag)
                {
                    string[] limits = tag.ToString().Split(',');

                    if (limits.Length == 2 &&
                        double.TryParse(limits[0], out double min) &&
                        double.TryParse(limits[1], out double max) &&
                        double.TryParse(normalizedValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double doubleValue))
                    {
                        // Değer limitler içinde mi?
                        if (doubleValue >= min && doubleValue <= max)
                        {
                            activeTextBox.Text = doubleValue.ToString(CultureInfo.CurrentCulture);
                        }
                        else
                        {
                            KeypadPopup.IsOpen = true; // Hata durumunda KeyPad'i açık tut
                            MessageBox.Show($"Lütfen {min} ile {max} arasında bir değer girin.",
                                          "Geçersiz Giriş", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                    else
                    {
                        activeTextBox.Text = normalizedValue;
                    }
                }
                else
                {
                    activeTextBox.Text = normalizedValue;
                }

                KeypadPopup.IsOpen = false;
            }
        }

        private void LoadFoamValues(string mode)
        {
            try
            {
                string templateName = mode == "AntiFoam" ? "AntiFoamTemplate" : "LevelTemplate";
                string prefix = mode == "AntiFoam" ? "AntiFoam" : "Level";

                if (ParametersContent.ContentTemplate == null)
                {
                    ParametersContent.ContentTemplate = (DataTemplate)ParametersContent.Resources[templateName];
                }

                var content = ParametersContent.ContentTemplate.LoadContent() as FrameworkElement;
                if (content != null)
                {
                    ParametersContent.Content = content;

                    Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
                    {
                        var doseTime = FindChild<TextBox>(ParametersContent, $"{prefix}DoseTime");
                        var waitTime = FindChild<TextBox>(ParametersContent, $"{prefix}WaitTime");
                        var alarmTime = FindChild<TextBox>(ParametersContent, $"{prefix}AlarmTime");

                        if (doseTime != null)
                        {
                            doseTime.GotFocus += TextBox_GotFocus;
                            doseTime.Text = Properties.Settings.Default.EditFoamDoseTime.ToString();
                        }
                        if (waitTime != null)
                        {
                            waitTime.GotFocus += TextBox_GotFocus;
                            waitTime.Text = Properties.Settings.Default.EditFoamWaitTime.ToString();
                        }
                        if (alarmTime != null)
                        {
                            alarmTime.GotFocus += TextBox_GotFocus;
                            alarmTime.Text = Properties.Settings.Default.EditFoamAlarmTime.ToString();
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading {mode} values: {ex.Message}");
            }
        }

        private void SaveFoamValues(string mode)
        {
            try
            {
                var container = VisualTreeHelper.GetChild(ParametersContent, 0) as FrameworkElement;
                if (container != null)
                {
                    string prefix = mode == "AntiFoam" ? "AntiFoam" : "Level";

                    var doseTime = FindChild<TextBox>(container, $"{prefix}DoseTime");
                    var waitTime = FindChild<TextBox>(container, $"{prefix}WaitTime");
                    var alarmTime = FindChild<TextBox>(container, $"{prefix}AlarmTime");

                    if (doseTime != null && double.TryParse(doseTime.Text, NumberStyles.Any,
                        CultureInfo.CurrentCulture, out double doseVal))
                    {
                        Properties.Settings.Default.EditFoamDoseTime = doseVal;
                    }
                    if (waitTime != null && double.TryParse(waitTime.Text, NumberStyles.Any,
                        CultureInfo.CurrentCulture, out double waitVal))
                    {
                        Properties.Settings.Default.EditFoamWaitTime = waitVal;
                    }
                    if (alarmTime != null && double.TryParse(alarmTime.Text, NumberStyles.Any,
                        CultureInfo.CurrentCulture, out double alarmVal))
                    {
                        Properties.Settings.Default.EditFoamAlarmTime = alarmVal;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving {mode} values: {ex.Message}");
            }
        }

    }
}
