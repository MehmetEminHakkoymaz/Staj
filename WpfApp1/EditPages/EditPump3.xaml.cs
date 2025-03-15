using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class EditPump3 : Window
    {
        private DispatcherTimer clockTimer;
        private Dictionary<string, ToggleButton> tubeTypeButtons;
        private Dictionary<string, ToggleButton> featureButtons;
        private Dictionary<string, ToggleButton> displayCountUnitButtons;

        public EditPump3()
        {
            // Initialize components
            InitializeComponent();

            // Initialize button dictionaries immediately
            InitializeButtonDictionaries();

            // Start the clock
            InitializeClock();

            // Window settings
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
            this.Topmost = true;

            // Move loading settings to Loaded event
            this.Loaded += EditPump3_Loaded;
        }

        private void EditPump3_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Load saved settings only after window is fully loaded
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

                if (Button13 != null) tubeTypeButtons["#13"] = Button13;
                if (Button14 != null) tubeTypeButtons["#14"] = Button14;
                if (Button19 != null) tubeTypeButtons["#19"] = Button19;
                if (Button16 != null) tubeTypeButtons["#16"] = Button16;
                if (Button25 != null) tubeTypeButtons["#25"] = Button25;
                if (Button17 != null) tubeTypeButtons["#17"] = Button17;
                if (Button18 != null) tubeTypeButtons["#18"] = Button18;

                // FEATURE buttons
                featureButtons = new Dictionary<string, ToggleButton>();
                if (Foam != null) featureButtons["Foam"] = Foam;
                if (Feed != null) featureButtons["Feed"] = Feed;

                // DISPLAY COUNT UNIT buttons
                displayCountUnitButtons = new Dictionary<string, ToggleButton>();
                if (Count != null) displayCountUnitButtons["Count"] = Count;
                if (ml != null) displayCountUnitButtons["-ml"] = ml;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing button dictionaries: {ex.Message}");
            }
        }


        private void LoadSettings()
        {
            // Load TUBE TYPE setting
            string savedTubeType = Properties.Settings.Default.EditPump3TubeType;
            if (!string.IsNullOrEmpty(savedTubeType) && tubeTypeButtons.ContainsKey(savedTubeType))
            {
                // Uncheck all tube type buttons first
                foreach (var btn in tubeTypeButtons.Values)
                {
                    btn.IsChecked = false;
                }

                // Check the saved one
                tubeTypeButtons[savedTubeType].IsChecked = true;
            }
            else
            {
                // Default to #13 if no setting is saved
                Button13.IsChecked = true;
            }

            // Load FEATURE setting
            string savedFeature = Properties.Settings.Default.EditPump3Feature;
            if (!string.IsNullOrEmpty(savedFeature) && featureButtons.ContainsKey(savedFeature))
            {
                foreach (var btn in featureButtons.Values)
                {
                    btn.IsChecked = false;
                }
                featureButtons[savedFeature].IsChecked = true;
            }
            else
            {
                // Default to Foam
                Foam.IsChecked = true;
            }

            // Load DISPLAY COUNT UNIT setting
            string savedDisplayCountUnit = Properties.Settings.Default.EditPump3DisplayCountUnit;
            if (!string.IsNullOrEmpty(savedDisplayCountUnit) && displayCountUnitButtons.ContainsKey(savedDisplayCountUnit))
            {
                foreach (var btn in displayCountUnitButtons.Values)
                {
                    btn.IsChecked = false;
                }
                displayCountUnitButtons[savedDisplayCountUnit].IsChecked = true;
            }
            else
            {
                // Default to -ml
                ml.IsChecked = true;
            }
        }

        private void SaveSettings()
        {
            // Save TUBE TYPE setting
            string selectedTubeType = tubeTypeButtons.FirstOrDefault(x => x.Value.IsChecked == true).Key;
            Properties.Settings.Default.EditPump3TubeType = selectedTubeType;

            // Save FEATURE setting
            string selectedFeature = featureButtons.FirstOrDefault(x => x.Value.IsChecked == true).Key;
            Properties.Settings.Default.EditPump3Feature = selectedFeature;

            // Save DISPLAY COUNT UNIT setting
            string selectedDisplayCountUnit = displayCountUnitButtons.FirstOrDefault(x => x.Value.IsChecked == true).Key;
            Properties.Settings.Default.EditPump3DisplayCountUnit = selectedDisplayCountUnit;

            // Save settings
            Properties.Settings.Default.Save();
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
                    if (tubeTypeButtons == null || featureButtons == null || displayCountUnitButtons == null)
                    {
                        MessageBox.Show("Button dictionaries could not be initialized.");
                        return;
                    }
                }

                // Handle TUBE TYPE buttons 
                bool isTubeTypeButton = false;
                foreach (var btn in tubeTypeButtons.Values)
                {
                    if (btn == clickedButton)
                    {
                        isTubeTypeButton = true;
                        break;
                    }
                }

                if (isTubeTypeButton)
                {
                    foreach (var btn in tubeTypeButtons.Values)
                    {
                        if (btn != clickedButton && btn != null)
                        {
                            btn.IsChecked = false;
                        }
                    }
                    clickedButton.IsChecked = true;
                    return;
                }

                // Feature buttons
                bool isFeatureButton = false;
                foreach (var btn in featureButtons.Values)
                {
                    if (btn == clickedButton)
                    {
                        isFeatureButton = true;
                        break;
                    }
                }

                if (isFeatureButton)
                {
                    foreach (var btn in featureButtons.Values)
                    {
                        if (btn != clickedButton && btn != null)
                        {
                            btn.IsChecked = false;
                        }
                    }
                    clickedButton.IsChecked = true;
                    return;
                }

                // Display count unit buttons
                bool isDisplayCountUnitButton = false;
                foreach (var btn in displayCountUnitButtons.Values)
                {
                    if (btn == clickedButton)
                    {
                        isDisplayCountUnitButton = true;
                        break;
                    }
                }

                if (isDisplayCountUnitButton)
                {
                    foreach (var btn in displayCountUnitButtons.Values)
                    {
                        if (btn != clickedButton && btn != null)
                        {
                            btn.IsChecked = false;
                        }
                    }
                    clickedButton.IsChecked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in HandleButtonToggle: {ex.Message}");
            }
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                var clickedButton = sender as ToggleButton;
                if (clickedButton == null) return;

                // Prevent unchecking - ensure one button remains checked in each group
                if (tubeTypeButtons.ContainsValue(clickedButton) &&
                    tubeTypeButtons.Values.All(b => b.IsChecked == false))
                {
                    clickedButton.IsChecked = true;
                }
                else if (featureButtons.ContainsValue(clickedButton) &&
                         featureButtons.Values.All(b => b.IsChecked == false))
                {
                    clickedButton.IsChecked = true;
                }
                else if (displayCountUnitButtons.ContainsValue(clickedButton) &&
                         displayCountUnitButtons.Values.All(b => b.IsChecked == false))
                {
                    clickedButton.IsChecked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in ToggleButton_Unchecked: {ex.Message}");
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
            SaveSettings();
            this.Close();
        }
    }
}
