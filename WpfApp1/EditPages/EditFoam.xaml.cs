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

namespace WpfApp1.EditPages
{
    /// <summary>
    /// Interaction logic for EditFoam.xaml
    /// </summary>
    public partial class EditFoam : Window
    {
        public EditFoam()
        {
            InitializeComponent();
            this.Loaded += EditFoam_Loaded;

            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
            this.Topmost = true;
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

                // İçeriği oluştur
                var content = ParametersContent.ContentTemplate.LoadContent() as FrameworkElement;
                if (content != null)
                {
                    // İçeriği temizle ve yeni içeriği ata
                    ParametersContent.Content = null;
                    ParametersContent.Content = content;

                    // Visual tree oluştuktan sonra kontrolleri bul
                    Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
                    {
                        var doseTime = FindChild<TextBox>(ParametersContent, "AntiFoamDoseTime");
                        var waitTime = FindChild<TextBox>(ParametersContent, "AntiFoamWaitTime");
                        var alarmTime = FindChild<TextBox>(ParametersContent, "AntiFoamAlarmTime");

                        if (doseTime != null)
                        {
                            doseTime.Clear(); // Önce temizle
                            doseTime.Text = Properties.Settings.Default.AntiFoamDoseTime.ToString();
                        }
                        if (waitTime != null)
                        {
                            waitTime.Clear(); // Önce temizle
                            waitTime.Text = Properties.Settings.Default.AntiFoamWaitTime.ToString();
                        }
                        if (alarmTime != null)
                        {
                            alarmTime.Clear(); // Önce temizle
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

                // İçeriği oluştur
                var content = ParametersContent.ContentTemplate.LoadContent() as FrameworkElement;
                if (content != null)
                {
                    // İçeriği temizle ve yeni içeriği ata
                    ParametersContent.Content = null;
                    ParametersContent.Content = content;

                    // Visual tree oluştuktan sonra kontrolleri bul
                    Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
                    {
                        var doseTime = FindChild<TextBox>(ParametersContent, "LevelDoseTime");
                        var waitTime = FindChild<TextBox>(ParametersContent, "LevelWaitTime");
                        var alarmTime = FindChild<TextBox>(ParametersContent, "LevelAlarmTime");

                        if (doseTime != null)
                        {
                            doseTime.Clear(); // Önce temizle
                            doseTime.Text = Properties.Settings.Default.LevelDoseTime.ToString();
                        }
                        if (waitTime != null)
                        {
                            waitTime.Clear(); // Önce temizle
                            waitTime.Text = Properties.Settings.Default.LevelWaitTime.ToString();
                        }
                        if (alarmTime != null)
                        {
                            alarmTime.Clear(); // Önce temizle
                            alarmTime.Text = Properties.Settings.Default.LevelAlarmTime.ToString();
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
                    Properties.Settings.Default.FoamSelectedMode = "None";
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

                    if (doseTime != null && int.TryParse(doseTime.Text, out int doseVal))
                    {
                        Properties.Settings.Default.AntiFoamDoseTime = doseVal;
                        Console.WriteLine($"Saving AntiFoamDoseTime: {doseVal}");
                    }
                    if (waitTime != null && int.TryParse(waitTime.Text, out int waitVal))
                    {
                        Properties.Settings.Default.AntiFoamWaitTime = waitVal;
                        Console.WriteLine($"Saving AntiFoamWaitTime: {waitVal}");
                    }
                    if (alarmTime != null && int.TryParse(alarmTime.Text, out int alarmVal))
                    {
                        Properties.Settings.Default.AntiFoamAlarmTime = alarmVal;
                        Console.WriteLine($"Saving AntiFoamAlarmTime: {alarmVal}");
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

                    if (doseTime != null && int.TryParse(doseTime.Text, out int doseVal))
                    {
                        Properties.Settings.Default.LevelDoseTime = doseVal;
                        Console.WriteLine($"Saving LevelDoseTime: {doseVal}");
                    }
                    if (waitTime != null && int.TryParse(waitTime.Text, out int waitVal))
                    {
                        Properties.Settings.Default.LevelWaitTime = waitVal;
                        Console.WriteLine($"Saving LevelWaitTime: {waitVal}");
                    }
                    if (alarmTime != null && int.TryParse(alarmTime.Text, out int alarmVal))
                    {
                        Properties.Settings.Default.LevelAlarmTime = alarmVal;
                        Console.WriteLine($"Saving LevelAlarmTime: {alarmVal}");
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
            try
            {
                var button = sender as ToggleButton;
                if (button == null) return;

                if (None == null || AntiFoam == null || Level == null || ParametersContent == null)
                {
                    MessageBox.Show("One or more controls are not initialized properly.");
                    return;
                }

                // Önce içeriği temizle
                ParametersContent.ContentTemplate = null;
                ParametersContent.Content = null;

                if (button == None)
                {
                    AntiFoam.IsChecked = false;
                    Level.IsChecked = false;
                }
                else if (button == AntiFoam)
                {
                    None.IsChecked = false;
                    Level.IsChecked = false;
                    LoadAntiFoamValues();
                }
                else if (button == Level)
                {
                    None.IsChecked = false;
                    AntiFoam.IsChecked = false;
                    LoadLevelValues();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in HandleButtonToggle: {ex.Message}");
            }
        }



        // Yardımcı metod: Control ağacında belirli bir kontrolü bul
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

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as ToggleButton;
                if (button == null) return;

                // Null kontrolleri
                if (None == null || AntiFoam == null || Level == null)
                {
                    MessageBox.Show("One or more toggle buttons are not initialized properly.");
                    return;
                }

                // En az bir buton seçili olmalı
                if (!None.IsChecked.Value && !AntiFoam.IsChecked.Value && !Level.IsChecked.Value)
                {
                    button.IsChecked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in ToggleButton_Unchecked: {ex.Message}");
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Sadece sayısal değer girişine izin ver
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var textBox = sender as TextBox;
                if (textBox == null || string.IsNullOrEmpty(textBox.Tag?.ToString())) return;

                // Min-Max değerlerini al
                var range = textBox.Tag.ToString().Split(',');
                if (range.Length != 2) return;

                if (!string.IsNullOrEmpty(textBox.Text) &&
                    int.TryParse(textBox.Text, out int value) &&
                    int.TryParse(range[0], out int min) &&
                    int.TryParse(range[1], out int max))
                {
                    // Değer aralık dışındaysa düzelt
                    if (value < min) textBox.Text = min.ToString();
                    if (value > max) textBox.Text = max.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in TextBox_TextChanged: {ex.Message}");
            }
        }
    }
}
