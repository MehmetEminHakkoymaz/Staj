using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfApp1.EditPages
{
    public partial class EditTurbidity : Window
    {
        private readonly DispatcherTimer clockTimer;
        private TextBox activeTextBox = null;
        public EditTurbidity()
        {
            InitializeComponent();
            clockTimer = InitializeClock();
            contentComboBox.SelectionChanged += ContentComboBox_SelectionChanged;
            KeypadControl.ValueSelected += KeyPadControl_ValueSelected;
            LoadLastSelectedCascade();
            //LoadPIDSettings();
            UpdateUIForVesselType(); // Add this line to update UI based on vessel type

            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            Topmost = true;
        }

        private DispatcherTimer InitializeClock()
        {
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += ClockTimer_Tick;
            timer.Start();
            return timer;
        }
        private void LoadLastSelectedCascade()
        {
            try
            {
                double cascadeValue = Properties.Settings.Default.EditTurbidityCascade;
                string selectedItem = cascadeValue switch
                {
                    0 => "None",
                    1 => "Feed",
                    2 => "Harvesting->Inoculate",
                    _ => "None" // Default to None for any other values
                };

                foreach (ComboBoxItem item in contentComboBox.Items)
                {
                    if (item.Content.ToString() == selectedItem)
                    {
                        contentComboBox.SelectedItem = item;
                        return;
                    }
                }
                contentComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading last selected cascade: {ex.Message}");
                contentComboBox.SelectedIndex = 0;
            }
        }

        private void ContentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string content = selectedItem.Content.ToString();
                double cascadeValue = content switch
                {
                    "None" => 0,
                    "Feed" => 1,
                    "Harvesting->Inoculate" => 2,
                    _ => 0 // Default to None for any other values
                };

                Properties.Settings.Default.EditTurbidityCascade = cascadeValue;

                // EditPump4Feature ayarını HEMEN güncelle ve kaydet
                if (content == "Feed")
                {
                    Properties.Settings.Default.EditPump4Feature = 2;
                }
                else
                {
                    // Feed değilse, Turbidity özelliğini kullanamaz
                    if (Properties.Settings.Default.EditPump4Feature == 2)
                        Properties.Settings.Default.EditPump4Feature = 1;
                }

                // Harvesting->Inoculate kontrolü
                if (content == "Harvesting->Inoculate")
                {
                    Properties.Settings.Default.EditTurbidityCascade = 0; // Set to None (0)
                    Properties.Settings.Default.EditPump4Feature = 1;
                    LoadLastSelectedCascade();
                    MessageBox.Show("To use Harvesting->Inoculate mode, an external peristaltic pump must be attached.",
                                  "Configuration Required",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Warning);
                    return;
                }

                // Ayarları derhal kaydet
                Properties.Settings.Default.Save();

                // Tam emin olmak için EditPump4 ayarlarını senkronize et
                try
                {
                    // Eğer EditPump4 sınıfı erişilebilirse, onun senkronizasyon metodu çağrılabilir
                    if (typeof(EditPump4).GetMethod("SynchronizeSettings", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static) != null)
                    {
                        typeof(EditPump4).GetMethod("SynchronizeSettings", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Invoke(null, null);
                    }
                }
                catch
                {
                    // Sessizce hatayı yok say
                }

                // Şablonu uygula
                contentArea.Content = null;
                contentArea.ContentTemplate = null;
                ApplyTemplate(content);
            }
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            ClockTextBlock.Text = DateTime.Now.ToString("HH : mm : ss");
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                activeTextBox = textBox;
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
                if (activeTextBox.Tag is string tag && ParseRange(tag, out double min, out double max))
                {
                    string normalizedValue = value.Replace(',', '.');
                    if (double.TryParse(normalizedValue, System.Globalization.NumberStyles.Any,
                                       System.Globalization.CultureInfo.InvariantCulture, out double doubleValue))
                    {
                        if (doubleValue >= min && doubleValue <= max)
                        {
                            // Set the value
                            activeTextBox.Text = doubleValue.ToString(System.Globalization.CultureInfo.CurrentCulture);

                            // Additional validation for Harvesting->Inoculate
                            if (activeTextBox.Name == "TurbidityHarvestingInoculateInoculate")
                            {
                                ValidateInoculateValue();
                            }
                        }
                        else
                        {
                            KeypadPopup.IsOpen = true;
                            MessageBox.Show($"Please enter a value between {min} and {max}.",
                                           "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        KeypadPopup.IsOpen = true;
                        MessageBox.Show("Please enter a valid number.",
                                       "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    activeTextBox.Text = value;
                }
            }
        }

        private void ValidateInoculateValue()
        {
            var content = contentArea.Content as FrameworkElement;
            if (content == null) return;

            var harvestingTextBox = FindChild<TextBox>(content, "TurbidityHarvestingInoculateHarvesting");
            var inoculateTextBox = FindChild<TextBox>(content, "TurbidityHarvestingInoculateInoculate");

            if (harvestingTextBox == null || inoculateTextBox == null) return;

            // Parse values
            if (!double.TryParse(harvestingTextBox.Text,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.CurrentCulture,
                out double harvestingValue))
                return;

            if (!double.TryParse(inoculateTextBox.Text,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.CurrentCulture,
                out double inoculateValue))
                return;

            // If inoculate value is greater than harvesting value, set it to harvesting value
            if (inoculateValue > harvestingValue)
            {
                inoculateTextBox.Text = harvestingValue.ToString(System.Globalization.CultureInfo.CurrentCulture);

                // Show a message to inform the user
                MessageBox.Show("Inoculate value cannot be greater than Harvesting value. The value has been adjusted.",
                              "Value Adjusted",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information);
            }
        }

        private bool ParseRange(string tag, out double min, out double max)
        {
            min = max = 0;
            if (string.IsNullOrEmpty(tag)) return false;

            var parts = tag.Split(',');
            if (parts.Length == 2 &&
                double.TryParse(parts[0], System.Globalization.NumberStyles.Any,
                              System.Globalization.CultureInfo.InvariantCulture, out min) &&
                double.TryParse(parts[1], System.Globalization.NumberStyles.Any,
                              System.Globalization.CultureInfo.InvariantCulture, out max))
            {
                return true;
            }

            return false;
        }
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    string content = selectedItem.Content.ToString();
                    double cascadeValue = content switch
                    {
                        "None" => 0,
                        "Feed" => 1,
                        "Harvesting->Inoculate" => 2,
                        _ => 0 // Default to None for any other values
                    };

                    Properties.Settings.Default.EditTurbidityCascade = cascadeValue;

                    // EditPump4Feature ayarını güncelle - bu kritik!
                    if (content == "None" || content == "Harvesting->Inoculate")
                    {
                        Properties.Settings.Default.EditPump4Feature = 1;
                    }
                    else if (content == "Feed")
                    {
                        Properties.Settings.Default.EditPump4Feature = 2;
                    }
                }

                SaveCurrentValues();
                //SavePIDSettings();
                Properties.Settings.Default.Save();

                // Ayarları derhal kaydet ve EditPump4 için senkronize et
                try
                {
                    // Eğer EditPump4 sınıfı erişilebilirse
                    if (typeof(EditPump4).GetMethod("SynchronizeSettings", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static) != null)
                    {
                        typeof(EditPump4).GetMethod("SynchronizeSettings", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Invoke(null, null);
                    }
                }
                catch
                {
                    // Sessizce hatayı yok say
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}");
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
                ValidateTextBoxValue(textBox);
            }
        }
        private void ApplyTemplate(string templateName)
        {
            string resourceKey = $"{templateName}Template".Replace("->", "");

            try
            {
                var template = contentArea.Resources[resourceKey] as DataTemplate;
                if (template != null)
                {
                    var content = template.LoadContent() as FrameworkElement;
                    if (content != null)
                    {
                        SetupTemplateControls(content, templateName);
                        contentArea.Content = content;

                        UpdateTemplateForVesselType(content);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying template: {ex.Message}");
            }
        }
        private void SetupTemplateControls(FrameworkElement content, string templateName)
        {
            switch (templateName)
            {
                case "Feed":
                    SetupTextBox(content, "TurbidityFeedml", Properties.Settings.Default.EditTurbidityFeed);
                    break;

                case "Harvesting->Inoculate":
                    var harvestingTextBox = SetupTextBox(content, "TurbidityHarvestingInoculateHarvesting",
                                                       Properties.Settings.Default.EditTurbidityHarvesting);
                    var inoculateTextBox = SetupTextBox(content, "TurbidityHarvestingInoculateInoculate",
                                                      Properties.Settings.Default.EditTurbidityInoculate);

                    // Add TextChanged event handlers for both TextBoxes
                    if (harvestingTextBox != null)
                        harvestingTextBox.TextChanged += HarvestingInoculate_TextChanged;

                    if (inoculateTextBox != null)
                        inoculateTextBox.TextChanged += HarvestingInoculate_TextChanged;

                    break;
            }
        }
        private void ValidateTextBoxValue(TextBox textBox)
        {
            if (textBox.Tag is string tag && ParseRange(tag, out double min, out double max))
            {
                if (double.TryParse(textBox.Text,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.CurrentCulture,
                    out double value))
                {
                    if (value < min)
                        textBox.Text = min.ToString(System.Globalization.CultureInfo.CurrentCulture);
                    else if (value > max)
                        textBox.Text = max.ToString(System.Globalization.CultureInfo.CurrentCulture);
                }
            }
        }
        private void SaveCurrentValues()
        {
            if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string content = selectedItem.Content.ToString();
                double cascadeValue = content switch
                {
                    "None" => 0,
                    "Feed" => 1,
                    "Harvesting->Inoculate" => 2,
                    _ => 0 // Default to None for any other values
                };

                Properties.Settings.Default.EditTurbidityCascade = cascadeValue;

                // None seçildiğinde EditPump4Feature'ı Feed olarak ayarla
                if (content == "None")
                {
                    Properties.Settings.Default.EditPump4Feature = 1;
                }
                // Feed seçildiğinde de EditPump4Feature'ı Turbidity olarak ayarla
                else if (content == "Feed")
                {
                    Properties.Settings.Default.EditPump4Feature = 2;
                }

                switch (content)
                {
                    case "Feed":
                        SaveValue("TurbidityFeedml", "EditTurbidityFeed");
                        break;
                    case "Harvesting->Inoculate":
                        SaveValue("TurbidityHarvestingInoculateHarvesting", "EditTurbidityHarvesting");
                        SaveValue("TurbidityHarvestingInoculateInoculate", "EditTurbidityInoculate");
                        break;
                }
            }
        }
        private void SaveValue(string controlName, string settingsProperty)
        {
            var textBox = FindChild<TextBox>(contentArea, controlName);
            if (textBox != null && double.TryParse(textBox.Text,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.CurrentCulture,
                out double value))
            {
                var property = Properties.Settings.Default.GetType().GetProperty(settingsProperty);
                if (property != null)
                {
                    property.SetValue(Properties.Settings.Default, value);
                }
            }
        }
        private TextBox SetupTextBox(FrameworkElement parent, string name, double value)
        {
            var textBox = FindChild<TextBox>(parent, name);
            if (textBox != null)
            {
                textBox.GotFocus += TextBox_GotFocus;
                textBox.Text = value.ToString(System.Globalization.CultureInfo.CurrentCulture);
            }
            return textBox;
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

        private void UpdateUIForVesselType()
        {
            // Get the current vessel type from settings as double
            double vesselType = Properties.Settings.Default.SelectedVesselType;

            // Update the template setup method to handle vessel type specifics
            if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string content = selectedItem.Content.ToString();
                ApplyTemplate(content);
            }
        }

        private void UpdateTemplateForVesselType(FrameworkElement content)
        {
            // Get vessel type as double (0=500ml, 1=1L, 2=2L, 3=3L)
            double vesselType = Properties.Settings.Default.SelectedVesselType;

            // Find the label and textbox only in the Feed template
            var mlLabel = FindChild<Label>(content, "ml");
            var feedTextBox = FindChild<TextBox>(content, "TurbidityFeedml");

            int maxVolume = 3000; // Default to 3L

            switch (vesselType)
            {
                case 0: // 500ml
                    maxVolume = 500;
                    if (mlLabel != null)
                        mlLabel.Content = "(MAX 500ml)";
                    break;
                case 1: // 1L
                    maxVolume = 1000;
                    if (mlLabel != null)
                        mlLabel.Content = "(MAX 1000ml)";
                    break;
                case 2: // 2L
                    maxVolume = 2000;
                    if (mlLabel != null)
                        mlLabel.Content = "(MAX 2000ml)";
                    break;
                case 3: // 3L
                    maxVolume = 3000;
                    if (mlLabel != null)
                        mlLabel.Content = "(MAX 3000ml)";
                    break;
                default: // Default to 3L
                    maxVolume = 3000;
                    if (mlLabel != null)
                        mlLabel.Content = "(MAX 3000ml)";
                    break;
            }

            // Update the textbox tag and ensure value doesn't exceed maximum
            if (feedTextBox != null)
            {
                feedTextBox.Tag = $"0,{maxVolume}";

                // Make sure the value doesn't exceed the new maximum
                if (double.TryParse(feedTextBox.Text, out double currentValue) && currentValue > maxVolume)
                {
                    feedTextBox.Text = maxVolume.ToString(System.Globalization.CultureInfo.CurrentCulture);
                }
            }
        }


        private bool isUpdatingInoculateText = false;

        private void HarvestingInoculate_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isUpdatingInoculateText) return;

            var content = contentArea.Content as FrameworkElement;
            if (content == null) return;

            var harvestingTextBox = FindChild<TextBox>(content, "TurbidityHarvestingInoculateHarvesting");
            var inoculateTextBox = FindChild<TextBox>(content, "TurbidityHarvestingInoculateInoculate");

            if (harvestingTextBox == null || inoculateTextBox == null) return;

            // If this is the harvesting TextBox and it's changing, validate the inoculate value
            if (sender == harvestingTextBox)
            {
                // Parse values
                if (!double.TryParse(harvestingTextBox.Text,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.CurrentCulture,
                    out double harvestingValue))
                    return;

                if (!double.TryParse(inoculateTextBox.Text,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.CurrentCulture,
                    out double inoculateValue))
                    return;

                // If inoculate value is greater than harvesting value, set it to harvesting value
                if (inoculateValue > harvestingValue)
                {
                    isUpdatingInoculateText = true;
                    inoculateTextBox.Text = harvestingValue.ToString(System.Globalization.CultureInfo.CurrentCulture);
                    isUpdatingInoculateText = false;

                    MessageBox.Show("Inoculate value has been reduced to match the new Harvesting value.",
                                 "Value Adjusted",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Information);
                }
            }
            // If this is the inoculate TextBox changing, make sure it doesn't exceed harvesting
            else if (sender == inoculateTextBox)
            {
                // Parse values
                if (!double.TryParse(harvestingTextBox.Text,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.CurrentCulture,
                    out double harvestingValue))
                    return;

                if (!double.TryParse(inoculateTextBox.Text,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.CurrentCulture,
                    out double inoculateValue))
                    return;

                // If inoculate value is greater than harvesting value, set it to harvesting value
                if (inoculateValue > harvestingValue)
                {
                    isUpdatingInoculateText = true;
                    inoculateTextBox.Text = harvestingValue.ToString(System.Globalization.CultureInfo.CurrentCulture);
                    isUpdatingInoculateText = false;

                    MessageBox.Show("Inoculate value cannot be greater than Harvesting value. The value has been adjusted.",
                                 "Value Adjusted",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Information);
                }
            }
        }

    }
}
