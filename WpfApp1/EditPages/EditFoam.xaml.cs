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
            string lastSelectedMode = Properties.Settings.Default.FoamSelectedMode;

            // Toggle butonlar arasında bir döngü yap ve son seçilen modu bul
            foreach (var button in new[] { None, AntiFoam, Level })
            {
                if (button.Name == lastSelectedMode)
                {
                    button.IsChecked = true;
                    LoadContent(button.Name); // İlgili içeriği yükle
                    break;
                }
            }

            // Eğer seçilen mod bulunamazsa varsayılan olarak None seç
            if (string.IsNullOrEmpty(lastSelectedMode) || lastSelectedMode == "None")
            {
                None.IsChecked = true;
            }
        }

        private void LoadContent(string mode)
        {
            // Seçilen moda göre içeriği yükle
            switch (mode)
            {
                case "AntiFoam":
                    ParametersContent.ContentTemplate = (DataTemplate)ParametersContent.Resources["AntiFoamTemplate"];

                    // ParametersContent'in içeriği oluşturulduktan sonra TextBox'ları bul ve değerleri yükle
                    ParametersContent.ApplyTemplate();

                    var antiFoamContent = ParametersContent.ContentTemplate.LoadContent() as FrameworkElement;
                    if (antiFoamContent != null)
                    {
                        var doseTimeBox = FindChild<TextBox>(antiFoamContent, "AntiFoamDoseTime");
                        if (doseTimeBox != null)
                        {
                            doseTimeBox.Text = Properties.Settings.Default.AntiFoamDoseTime.ToString(CultureInfo.CurrentCulture);
                            doseTimeBox.GotFocus += TextBox_GotFocus;
                            doseTimeBox.PreviewTextInput += TextBox_PreviewTextInput;
                            doseTimeBox.TextChanged += TextBox_TextChanged;
                        }

                        var waitTimeBox = FindChild<TextBox>(antiFoamContent, "AntiFoamWaitTime");
                        if (waitTimeBox != null)
                        {
                            waitTimeBox.Text = Properties.Settings.Default.AntiFoamWaitTime.ToString(CultureInfo.CurrentCulture);
                            waitTimeBox.GotFocus += TextBox_GotFocus;
                            waitTimeBox.PreviewTextInput += TextBox_PreviewTextInput;
                            waitTimeBox.TextChanged += TextBox_TextChanged;
                        }

                        var alarmTimeBox = FindChild<TextBox>(antiFoamContent, "AntiFoamAlarmTime");
                        if (alarmTimeBox != null)
                        {
                            alarmTimeBox.Text = Properties.Settings.Default.AntiFoamAlarmTime.ToString(CultureInfo.CurrentCulture);
                            alarmTimeBox.GotFocus += TextBox_GotFocus;
                            alarmTimeBox.PreviewTextInput += TextBox_PreviewTextInput;
                            alarmTimeBox.TextChanged += TextBox_TextChanged;
                        }

                        ParametersContent.Content = antiFoamContent;
                    }
                    break;

                case "Level":
                    ParametersContent.ContentTemplate = (DataTemplate)ParametersContent.Resources["LevelTemplate"];

                    // ParametersContent'in içeriği oluşturulduktan sonra TextBox'ları bul ve değerleri yükle
                    ParametersContent.ApplyTemplate();

                    var levelContent = ParametersContent.ContentTemplate.LoadContent() as FrameworkElement;
                    if (levelContent != null)
                    {
                        var doseTimeBox = FindChild<TextBox>(levelContent, "LevelDoseTime");
                        if (doseTimeBox != null)
                        {
                            doseTimeBox.Text = Properties.Settings.Default.LevelDoseTime.ToString(CultureInfo.CurrentCulture);
                            doseTimeBox.GotFocus += TextBox_GotFocus;
                            doseTimeBox.PreviewTextInput += TextBox_PreviewTextInput;
                            doseTimeBox.TextChanged += TextBox_TextChanged;
                        }

                        var waitTimeBox = FindChild<TextBox>(levelContent, "LevelWaitTime");
                        if (waitTimeBox != null)
                        {
                            waitTimeBox.Text = Properties.Settings.Default.LevelWaitTime.ToString(CultureInfo.CurrentCulture);
                            waitTimeBox.GotFocus += TextBox_GotFocus;
                            waitTimeBox.PreviewTextInput += TextBox_PreviewTextInput;
                            waitTimeBox.TextChanged += TextBox_TextChanged;
                        }

                        var alarmTimeBox = FindChild<TextBox>(levelContent, "LevelAlarmTime");
                        if (alarmTimeBox != null)
                        {
                            alarmTimeBox.Text = Properties.Settings.Default.LevelAlarmTime.ToString(CultureInfo.CurrentCulture);
                            alarmTimeBox.GotFocus += TextBox_GotFocus;
                            alarmTimeBox.PreviewTextInput += TextBox_PreviewTextInput;
                            alarmTimeBox.TextChanged += TextBox_TextChanged;
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
                // Kaydedilmiş modu yükle
                string savedMode = Properties.Settings.Default.FoamSelectedMode;
                switch (savedMode)
                {
                    case "None":
                        None.IsChecked = true;
                        break;
                    case "AntiFoam":
                        AntiFoam.IsChecked = true;
                        LoadAntiFoamValues();
                        break;
                    case "Level":
                        Level.IsChecked = true;
                        LoadLevelValues();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading saved settings: {ex.Message}");
            }
        }

        private void LoadAntiFoamValues()
        {
            try
            {
                if (ParametersContent.ContentTemplate == null)
                {
                    ParametersContent.ContentTemplate = (DataTemplate)ParametersContent.Resources["AntiFoamTemplate"];
                }

                var content = ParametersContent.ContentTemplate.LoadContent() as FrameworkElement;
                if (content != null)
                {
                    ParametersContent.Content = content;  // ContentTemplate'i ayarla

                    // Visual tree oluştuktan sonra kontrolleri bul ve event'leri bağla
                    Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
                    {
                        var doseTime = FindChild<TextBox>(ParametersContent, "AntiFoamDoseTime");
                        var waitTime = FindChild<TextBox>(ParametersContent, "AntiFoamWaitTime");
                        var alarmTime = FindChild<TextBox>(ParametersContent, "AntiFoamAlarmTime");

                        if (doseTime != null)
                        {
                            doseTime.GotFocus += TextBox_GotFocus;
                            doseTime.Text = Properties.Settings.Default.AntiFoamDoseTime.ToString();
                        }
                        if (waitTime != null)
                        {
                            waitTime.GotFocus += TextBox_GotFocus;
                            waitTime.Text = Properties.Settings.Default.AntiFoamWaitTime.ToString();
                        }
                        if (alarmTime != null)
                        {
                            alarmTime.GotFocus += TextBox_GotFocus;
                            alarmTime.Text = Properties.Settings.Default.AntiFoamAlarmTime.ToString();
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading AntiFoam values: {ex.Message}");
            }
        }

        private void LoadLevelValues()
        {
            try
            {
                if (ParametersContent.ContentTemplate == null)
                {
                    ParametersContent.ContentTemplate = (DataTemplate)ParametersContent.Resources["LevelTemplate"];
                }

                var content = ParametersContent.ContentTemplate.LoadContent() as FrameworkElement;
                if (content != null)
                {
                    ParametersContent.Content = null;
                    ParametersContent.Content = content;

                    Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
                    {
                        var doseTime = FindChild<TextBox>(ParametersContent, "LevelDoseTime");
                        var waitTime = FindChild<TextBox>(ParametersContent, "LevelWaitTime");
                        var alarmTime = FindChild<TextBox>(ParametersContent, "LevelAlarmTime");

                        if (doseTime != null)
                        {
                            doseTime.Clear();
                            doseTime.Text = Properties.Settings.Default.LevelDoseTime.ToString();
                            doseTime.GotFocus += TextBox_GotFocus;  // Event handler'ı ekle
                        }
                        if (waitTime != null)
                        {
                            waitTime.Clear();
                            waitTime.Text = Properties.Settings.Default.LevelWaitTime.ToString();
                            waitTime.GotFocus += TextBox_GotFocus;  // Event handler'ı ekle
                        }
                        if (alarmTime != null)
                        {
                            alarmTime.Clear();
                            alarmTime.Text = Properties.Settings.Default.LevelAlarmTime.ToString();
                            alarmTime.GotFocus += TextBox_GotFocus;  // Event handler'ı ekle
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Level values: {ex.Message}");
            }
        }


        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Seçili modu kaydet
                if (None.IsChecked == true)
                {
                    Properties.Settings.Default.FoamSelectedMode = "None";
                    Properties.Settings.Default.EditPump3Feature = "Feed";
                }
                else if (AntiFoam.IsChecked == true)
                {
                    Properties.Settings.Default.FoamSelectedMode = "AntiFoam";
                    SaveAntiFoamValues();
                }
                else if (Level.IsChecked == true)
                {
                    Properties.Settings.Default.FoamSelectedMode = "Level";
                    SaveLevelValues();
                }

                Properties.Settings.Default.Save();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}");
            }
        }

        private void SaveAntiFoamValues()
        {
            try
            {
                var container = VisualTreeHelper.GetChild(ParametersContent, 0) as FrameworkElement;
                if (container != null)
                {
                    var doseTime = FindChild<TextBox>(container, "AntiFoamDoseTime");
                    var waitTime = FindChild<TextBox>(container, "AntiFoamWaitTime");
                    var alarmTime = FindChild<TextBox>(container, "AntiFoamAlarmTime");

                    // int.TryParse yerine double.TryParse kullanın
                    if (doseTime != null && double.TryParse(doseTime.Text, NumberStyles.Any,
                        CultureInfo.CurrentCulture, out double doseVal))
                    {
                        Properties.Settings.Default.AntiFoamDoseTime = doseVal;
                    }
                    if (waitTime != null && double.TryParse(waitTime.Text, NumberStyles.Any,
                        CultureInfo.CurrentCulture, out double waitVal))
                    {
                        Properties.Settings.Default.AntiFoamWaitTime = waitVal;
                    }
                    if (alarmTime != null && double.TryParse(alarmTime.Text, NumberStyles.Any,
                        CultureInfo.CurrentCulture, out double alarmVal))
                    {
                        Properties.Settings.Default.AntiFoamAlarmTime = alarmVal;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving AntiFoam values: {ex.Message}");
            }
        }

        private void SaveLevelValues()
        {
            try
            {
                var container = VisualTreeHelper.GetChild(ParametersContent, 0) as FrameworkElement;
                if (container != null)
                {
                    var doseTime = FindChild<TextBox>(container, "LevelDoseTime");
                    var waitTime = FindChild<TextBox>(container, "LevelWaitTime");
                    var alarmTime = FindChild<TextBox>(container, "LevelAlarmTime");

                    // int.TryParse yerine double.TryParse kullanın
                    if (doseTime != null && double.TryParse(doseTime.Text, NumberStyles.Any,
                        CultureInfo.CurrentCulture, out double doseVal))
                    {
                        Properties.Settings.Default.LevelDoseTime = doseVal;
                    }
                    if (waitTime != null && double.TryParse(waitTime.Text, NumberStyles.Any,
                        CultureInfo.CurrentCulture, out double waitVal))
                    {
                        Properties.Settings.Default.LevelWaitTime = waitVal;
                    }
                    if (alarmTime != null && double.TryParse(alarmTime.Text, NumberStyles.Any,
                        CultureInfo.CurrentCulture, out double alarmVal))
                    {
                        Properties.Settings.Default.LevelAlarmTime = alarmVal;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving Level values: {ex.Message}");
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

                // Seçilen modu kaydet
                Properties.Settings.Default.FoamSelectedMode = selectedMode;

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
        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            // Eğer tüm butonlar unchecked ise, varsayılan olarak None'ı seç
            if (!None.IsChecked.Value && !AntiFoam.IsChecked.Value && !Level.IsChecked.Value)
            {
                None.IsChecked = true;
            }
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
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                SaveTextBoxValue(textBox);
            }
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

        private void SaveTextBoxValue(TextBox textBox)
        {
            try
            {
                string normalizedText = textBox.Text.Replace(',', '.');

                if (double.TryParse(normalizedText, NumberStyles.Any,
                                  CultureInfo.InvariantCulture, out double value))
                {
                    // TextBox'ın adına göre değeri ilgili ayara kaydet
                    switch (textBox.Name)
                    {
                        case "AntiFoamDoseTime":
                            Properties.Settings.Default.AntiFoamDoseTime = value;
                            break;
                        case "AntiFoamWaitTime":
                            Properties.Settings.Default.AntiFoamWaitTime = value;
                            break;
                        case "AntiFoamAlarmTime":
                            Properties.Settings.Default.AntiFoamAlarmTime = value;
                            break;
                        case "LevelDoseTime":
                            Properties.Settings.Default.LevelDoseTime = value;
                            break;
                        case "LevelWaitTime":
                            Properties.Settings.Default.LevelWaitTime = value;
                            break;
                        case "LevelAlarmTime":
                            Properties.Settings.Default.LevelAlarmTime = value;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving value: {ex.Message}");
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

    }
}
