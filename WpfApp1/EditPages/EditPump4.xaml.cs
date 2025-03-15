using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace WpfApp1.EditPages
{
    public partial class EditPump4 : Window
    {
        private DispatcherTimer clockTimer;
        private Dictionary<string, ToggleButton> tubeTypeButtons;
        private Dictionary<string, ToggleButton> featureButtons;
        private Dictionary<string, ToggleButton> displayCountUnitButtons;

        public EditPump4()
        {
            InitializeComponent();
            InitializeButtonDictionaries();
            InitializeClock();

            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            Topmost = true;

            Loaded += EditPump4_Loaded;
        }

        private void EditPump4_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadSettings();
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

                // FEATURE buttons - EditPump4'ün farklı feature butonları var
                featureButtons = new Dictionary<string, ToggleButton>();
                AddToButtonDictionary(featureButtons, "Balance Feed", BalanceFeed);
                AddToButtonDictionary(featureButtons, "Feed", Feed);
                AddToButtonDictionary(featureButtons, "Turbidity", Turbidity);

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
            LoadButtonGroupSettings(tubeTypeButtons, Properties.Settings.Default.EditPump4TubeType, Button13);
            LoadButtonGroupSettings(featureButtons, Properties.Settings.Default.EditPump4Feature, BalanceFeed);
            LoadButtonGroupSettings(displayCountUnitButtons, Properties.Settings.Default.EditPump4DisplayCountUnit, ml);
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
                Properties.Settings.Default.EditPump4TubeType = GetSelectedButtonKey(tubeTypeButtons);
                Properties.Settings.Default.EditPump4Feature = GetSelectedButtonKey(featureButtons);
                Properties.Settings.Default.EditPump4DisplayCountUnit = GetSelectedButtonKey(displayCountUnitButtons);
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}");
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

                // Ensure dictionaries are initialized
                if (tubeTypeButtons == null || featureButtons == null || displayCountUnitButtons == null)
                {
                    InitializeButtonDictionaries();
                }

                // Handle buttons in each group
                HandleButtonInGroup(clickedButton, tubeTypeButtons);
                HandleButtonInGroup(clickedButton, featureButtons);
                HandleButtonInGroup(clickedButton, displayCountUnitButtons);
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
