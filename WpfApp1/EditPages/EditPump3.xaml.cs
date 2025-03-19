using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace WpfApp1.EditPages
{
    public partial class EditPump3 : Window
    {
        private DispatcherTimer clockTimer;
        private Dictionary<int, ToggleButton> tubeTypeButtons;
        private Dictionary<int, ToggleButton> featureButtons;
        private Dictionary<int, ToggleButton> displayCountUnitButtons;

        // Dictionaries to map indices to string values for settings
        private Dictionary<int, string> tubeTypeValues;
        private Dictionary<int, string> featureValues;
        private Dictionary<int, string> displayCountUnitValues;

        public EditPump3()
        {
            InitializeComponent();
            InitializeButtonDictionaries();
            InitializeClock();

            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            Topmost = true;

            Loaded += EditPump3_Loaded;
        }

        private void EditPump3_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadSettings();

                // Başlangıçta seçilen özelliğe göre HidePump1Border'ı ayarla
                if (Foam.IsChecked == true)
                {
                    Properties.Settings.Default.Pump3TargetBorder = 1;
                    Properties.Settings.Default.Save();
                }
                else if (Feed.IsChecked == true)
                {
                    Properties.Settings.Default.Pump3TargetBorder = 0;
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
                // Initialize mapping dictionaries first
                tubeTypeValues = new Dictionary<int, string>
                    {
                        { 0, "#13" },
                        { 1, "#14" },
                        { 2, "#19" },
                        { 3, "#16" },
                        { 4, "#25" },
                        { 5, "#17" },
                        { 6, "#18" }
                    };

                featureValues = new Dictionary<int, string>
                    {
                        { 0, "Foam" },
                        { 1, "Feed" }
                    };

                displayCountUnitValues = new Dictionary<int, string>
                    {
                        { 0, "Count" },
                        { 1, "-ml" }
                    };

                // TUBE TYPE buttons
                tubeTypeButtons = new Dictionary<int, ToggleButton>();
                AddToButtonDictionary(tubeTypeButtons, 0, Button13);
                AddToButtonDictionary(tubeTypeButtons, 1, Button14);
                AddToButtonDictionary(tubeTypeButtons, 2, Button19);
                AddToButtonDictionary(tubeTypeButtons, 3, Button16);
                AddToButtonDictionary(tubeTypeButtons, 4, Button25);
                AddToButtonDictionary(tubeTypeButtons, 5, Button17);
                AddToButtonDictionary(tubeTypeButtons, 6, Button18);

                // FEATURE buttons
                featureButtons = new Dictionary<int, ToggleButton>();
                AddToButtonDictionary(featureButtons, 0, Foam);
                AddToButtonDictionary(featureButtons, 1, Feed);

                // DISPLAY COUNT UNIT buttons
                displayCountUnitButtons = new Dictionary<int, ToggleButton>();
                AddToButtonDictionary(displayCountUnitButtons, 0, Count);
                AddToButtonDictionary(displayCountUnitButtons, 1, ml);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing button dictionaries: {ex.Message}");
            }
        }

        private void AddToButtonDictionary(Dictionary<int, ToggleButton> dictionary, int key, ToggleButton button)
        {
            if (button != null)
            {
                dictionary[key] = button;
            }
        }

        private void LoadSettings()
        {
            int tubeTypeIndex = (int)Properties.Settings.Default.EditPump3TubeType;
            int featureIndex = (int)Properties.Settings.Default.EditPump3Feature;
            int displayCountUnitIndex = (int)Properties.Settings.Default.EditPump3DisplayCountUnit;

            LoadButtonGroupSettings(tubeTypeButtons, tubeTypeIndex, Button13);
            LoadButtonGroupSettings(featureButtons, featureIndex, Foam);
            LoadButtonGroupSettings(displayCountUnitButtons, displayCountUnitIndex, ml);
        }

        private void LoadButtonGroupSettings(Dictionary<int, ToggleButton> buttonDictionary,
                                             int savedIndex,
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
                if (buttonDictionary.TryGetValue(savedIndex, out var button))
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
                Properties.Settings.Default.EditPump3TubeType = GetSelectedButtonKey(tubeTypeButtons);
                Properties.Settings.Default.EditPump3Feature = GetSelectedButtonKey(featureButtons);
                Properties.Settings.Default.EditPump3DisplayCountUnit = GetSelectedButtonKey(displayCountUnitButtons);
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}");
            }
        }

        private double GetSelectedButtonKey(Dictionary<int, ToggleButton> buttonDictionary)
        {
            var selectedButton = buttonDictionary.FirstOrDefault(x => x.Value?.IsChecked == true);
            return selectedButton.Key;
        }

        private void HandleButtonToggle(object sender, RoutedEventArgs e)
        {
            try
            {
                var clickedButton = sender as ToggleButton;
                if (clickedButton == null) return;

                // Check if the Foam button is clicked and FoamSelectedMode is None
                if (clickedButton == Foam && Properties.Settings.Default.EditFoamCascade == 0)
                {
                    // Prevent selection
                    clickedButton.IsChecked = false;
                    // Show error message
                    MessageBox.Show("The foam feature cannot be selected because FoamSelectedMode is set to 'None'.",
                                   "Özellik Kısıtlaması", MessageBoxButton.OK, MessageBoxImage.Warning);
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

                // Foam butonunun seçilmesini kontrol et ve HidePump3Border'ı ayarla
                if (clickedButton == Foam && clickedButton.IsChecked == true)
                {
                    Properties.Settings.Default.Pump3TargetBorder = 1;
                    Properties.Settings.Default.Save(); // Değişikliği kaydet
                }
                else if (clickedButton == Feed && clickedButton.IsChecked == true && featureButtons.ContainsValue(clickedButton))
                {
                    // Feed seçildiğinde HidePump3Border'ı false yap
                    Properties.Settings.Default.Pump3TargetBorder = 0;
                    Properties.Settings.Default.Save(); // Değişikliği kaydet
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in HandleButtonToggle: {ex.Message}");
            }
        }

        private bool HandleButtonInGroup(ToggleButton clickedButton, Dictionary<int, ToggleButton> buttonGroup)
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

        private void EnsureOneButtonChecked(ToggleButton clickedButton, Dictionary<int, ToggleButton> buttonGroup)
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
