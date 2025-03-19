using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace WpfApp1.EditPages
{
    public partial class EditPump1 : Window
    {
        private DispatcherTimer clockTimer;
        private Dictionary<string, ToggleButton> tubeTypeButtons;
        private Dictionary<string, ToggleButton> featureButtons;
        private Dictionary<string, ToggleButton> displayCountUnitButtons;

        public EditPump1()
        {
            InitializeComponent();
            InitializeButtonDictionaries();
            InitializeClock();

            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            Topmost = true;

            Loaded += EditPump1_Loaded;
        }

        private void EditPump1_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadSettings();

                // If pH is set to Acid or Base->Acid, ensure Feed is not checked
                if (Properties.Settings.Default.EditpHCascade == 2 ||
                    Properties.Settings.Default.EditpHCascade == 3)
                {
                    if (Feed.IsChecked == true)
                    {
                        Feed.IsChecked = false;
                        Acid.IsChecked = true;
                        Properties.Settings.Default.EditPump1Feature = 0;
                        Properties.Settings.Default.Save();
                    }
                }

                // Başlangıçta seçilen özelliğe göre HidePump1Border'ı ayarla
                if (Acid.IsChecked == true)
                {
                    Properties.Settings.Default.Pump1TargetBorder = 1;
                    Properties.Settings.Default.Save();
                }
                else if (Feed.IsChecked == true)
                {
                    Properties.Settings.Default.Pump1TargetBorder = 0;
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading settings: {ex.Message}");
            }
        }

        private void InitializeButtonDictionaries()
        {
            try
            {
                // TUBE TYPE buttons
                tubeTypeButtons = new Dictionary<string, ToggleButton>();
                AddToButtonDictionary(tubeTypeButtons, "#13", Button13);
                AddToButtonDictionary(tubeTypeButtons, "#14", Button14);
                AddToButtonDictionary(tubeTypeButtons, "#19", Button19);
                AddToButtonDictionary(tubeTypeButtons, "#16", Button16);
                AddToButtonDictionary(tubeTypeButtons, "#25", Button25);
                AddToButtonDictionary(tubeTypeButtons, "#17", Button17);
                AddToButtonDictionary(tubeTypeButtons, "#18", Button18);

                // FEATURE buttons
                featureButtons = new Dictionary<string, ToggleButton>();
                AddToButtonDictionary(featureButtons, "Acid", Acid);
                AddToButtonDictionary(featureButtons, "Feed", Feed);

                // DISPLAY COUNT UNIT buttons
                displayCountUnitButtons = new Dictionary<string, ToggleButton>();
                AddToButtonDictionary(displayCountUnitButtons, "Count", Count);
                AddToButtonDictionary(displayCountUnitButtons, "-ml", ml);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing button dictionaries: {ex.Message}");
            }
        }

        private void AddToButtonDictionary(Dictionary<string, ToggleButton> dictionary, string key, ToggleButton button)
        {
            if (button != null)
            {
                dictionary[key] = button;
            }
        }

        private void LoadSettings()
        {
            // Check if settings exist in Settings.settings, if not provide defaults
            var tubeType = GetSetting("EditPump1TubeType", "#13");
            var feature = GetSetting("EditPump1Feature", "Acid");
            var displayCountUnit = GetSetting("EditPump1DisplayCountUnit", "-ml");

            LoadButtonGroupSettings(tubeTypeButtons, tubeType, Button13);
            LoadButtonGroupSettings(featureButtons, feature, Acid);
            LoadButtonGroupSettings(displayCountUnitButtons, displayCountUnit, ml);
        }

        private string GetSetting(string settingName, string defaultValue)
        {
            // Try to get setting, return default if not found
            try
            {
                var property = Properties.Settings.Default.GetType().GetProperty(settingName);
                if (property != null)
                {
                    return (string)property.GetValue(Properties.Settings.Default) ?? defaultValue;
                }
            }
            catch
            {
                // If setting doesn't exist, we'll just use the default
            }
            return defaultValue;
        }

        private void LoadButtonGroupSettings(Dictionary<string, ToggleButton> buttonDictionary,
                                             string savedSetting,
                                             ToggleButton defaultButton)
        {
            try
            {
                // Reset all buttons
                foreach (var btn in buttonDictionary.Values)
                {
                    btn.IsChecked = false;
                }

                // Set the saved or default button
                if (!string.IsNullOrEmpty(savedSetting) && buttonDictionary.TryGetValue(savedSetting, out var button))
                {
                    button.IsChecked = true;
                }
                else if (defaultButton != null)
                {
                    defaultButton.IsChecked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading button settings: {ex.Message}");
            }
        }

        private void SaveSettings()
        {
            try
            {
                // Setting may not exist yet in Settings.settings, so use dynamic approach
                SaveSetting("EditPump1TubeType", GetSelectedButtonKey(tubeTypeButtons));
                SaveSetting("EditPump1Feature", GetSelectedButtonKey(featureButtons));
                SaveSetting("EditPump1DisplayCountUnit", GetSelectedButtonKey(displayCountUnitButtons));

                // Save any value changes
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}");
            }
        }

        private void SaveSetting(string settingName, string value)
        {
            try
            {
                var property = Properties.Settings.Default.GetType().GetProperty(settingName);
                if (property != null)
                {
                    property.SetValue(Properties.Settings.Default, value);
                }
                else
                {
                    // If you want to be notified when settings are missing
                    // MessageBox.Show($"Setting {settingName} not found in Settings.settings");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving setting {settingName}: {ex.Message}");
            }
        }

        private string GetSelectedButtonKey(Dictionary<string, ToggleButton> buttonDictionary)
        {
            var selectedButton = buttonDictionary.FirstOrDefault(x => x.Value?.IsChecked == true);
            return selectedButton.Key ?? "";
        }

        private void HandleButtonToggle(object sender, RoutedEventArgs e)
        {
            try
            {
                var clickedButton = sender as ToggleButton;
                if (clickedButton == null) return;

                // Check if Feed button is clicked while LastSelectedpHCascadeItem is "Acid" or "Base->Acid"
                if (clickedButton == Feed &&
                    (Properties.Settings.Default.EditpHCascade == 2 ||
                     Properties.Settings.Default.EditpHCascade == 3))
                {
                    // Prevent selection
                    clickedButton.IsChecked = false;

                    // Show error message
                    MessageBox.Show("Feed özelliği seçilemez çünkü pH ayarında Acid veya Base->Acid seçilmiş durumda.",
                                   "Özellik Kısıtlaması", MessageBoxButton.OK, MessageBoxImage.Warning);

                    // Make sure Acid is checked
                    if (Acid != null)
                    {
                        Acid.IsChecked = true;
                    }

                    return;
                }

                // Ensure dictionaries are initialized
                if (tubeTypeButtons == null || featureButtons == null || displayCountUnitButtons == null)
                {
                    InitializeButtonDictionaries();
                }

                // Handle buttons in each group
                HandleButtonInGroup(clickedButton, tubeTypeButtons);
                HandleButtonInGroup(clickedButton, featureButtons);
                HandleButtonInGroup(clickedButton, displayCountUnitButtons);

                // Acid butonunun seçilmesini kontrol et ve HidePump1Border'ı ayarla
                if (clickedButton == Acid && clickedButton.IsChecked == true)
                {
                    Properties.Settings.Default.Pump1TargetBorder = 1;
                    Properties.Settings.Default.Save(); // Değişikliği kaydet
                }
                else if (clickedButton == Feed && clickedButton.IsChecked == true && featureButtons.ContainsValue(clickedButton))
                {
                    // Feed seçildiğinde HidePump1Border'ı false yap
                    Properties.Settings.Default.Pump1TargetBorder = 0;
                    Properties.Settings.Default.Save(); // Değişikliği kaydet
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in HandleButtonToggle: {ex.Message}");
            }
        }

        private bool HandleButtonInGroup(ToggleButton clickedButton, Dictionary<string, ToggleButton> buttonGroup)
        {
            if (buttonGroup == null) return false;

            // Check if the clicked button belongs to this group
            if (!buttonGroup.ContainsValue(clickedButton)) return false;

            // Set mutual exclusivity
            foreach (var btn in buttonGroup.Values)
            {
                if (btn != clickedButton && btn != null)
                {
                    btn.IsChecked = false;
                }
            }

            // Ensure the clicked button is checked
            clickedButton.IsChecked = true;
            return true;
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                var clickedButton = sender as ToggleButton;
                if (clickedButton == null) return;

                // Ensure at least one button remains checked in each group
                EnsureOneButtonChecked(clickedButton, tubeTypeButtons);
                EnsureOneButtonChecked(clickedButton, featureButtons);
                EnsureOneButtonChecked(clickedButton, displayCountUnitButtons);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in ToggleButton_Unchecked: {ex.Message}");
            }
        }

        private void EnsureOneButtonChecked(ToggleButton clickedButton, Dictionary<string, ToggleButton> buttonGroup)
        {
            if (buttonGroup == null) return;

            if (buttonGroup.ContainsValue(clickedButton) &&
                buttonGroup.Values.All(b => b?.IsChecked == false))
            {
                clickedButton.IsChecked = true;
            }
        }

        private void InitializeClock()
        {
            clockTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
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
            SaveSettings();
            this.Close();
        }
    }
}
