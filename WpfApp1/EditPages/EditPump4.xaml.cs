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

        // Track the last valid feature selection
        private string lastValidFeatureSelection;

        public static void SynchronizeSettings()
        {
            try
            {
                // TurbiditySelectedCascade ve EditPump4Feature'ı senkronize et
                if (Properties.Settings.Default.TurbiditySelectedCascade == "Feed" &&
                    Properties.Settings.Default.EditPump4Feature != "Turbidity")
                {
                    Properties.Settings.Default.EditPump4Feature = "Turbidity";
                    Properties.Settings.Default.Save();
                }
                else if (Properties.Settings.Default.TurbiditySelectedCascade != "Feed" &&
                         Properties.Settings.Default.EditPump4Feature == "Turbidity")
                {
                    Properties.Settings.Default.EditPump4Feature = "Feed";
                    Properties.Settings.Default.Save();
                }
            }
            catch
            {
                // Sessizce hatayı yok say
            }
        }
        public EditPump4()
        {
            // Sayfayı başlatmadan önce ayarları senkronize et
            SynchronizeSettings();

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
                // Tüm düğmeleri sıfırla
                foreach (var btn in tubeTypeButtons.Values) btn.IsChecked = false;
                foreach (var btn in featureButtons.Values) btn.IsChecked = false;
                foreach (var btn in displayCountUnitButtons.Values) btn.IsChecked = false;
        
                // TubeType ayarlarını yükle
                if (!string.IsNullOrEmpty(Properties.Settings.Default.EditPump4TubeType) && 
                    tubeTypeButtons.TryGetValue(Properties.Settings.Default.EditPump4TubeType, out var tubeButton))
                {
                    tubeButton.IsChecked = true;
                }
                else if (Button13 != null)
                {
                    Button13.IsChecked = true;
                }
        
                // DisplayCountUnit ayarlarını yükle
                if (!string.IsNullOrEmpty(Properties.Settings.Default.EditPump4DisplayCountUnit) && 
                    displayCountUnitButtons.TryGetValue(Properties.Settings.Default.EditPump4DisplayCountUnit, out var displayButton))
                {
                    displayButton.IsChecked = true;
                }
                else if (ml != null)
                {
                    ml.IsChecked = true;
                }
        
                // Feature ayarlarını yükle - hiç mesaj göstermeden
                if (Properties.Settings.Default.TurbiditySelectedCascade == "Feed")
                {
                    // Feed modunda sadece Turbidity geçerli
                    if (Turbidity != null) Turbidity.IsChecked = true;
                    lastValidFeatureSelection = "Turbidity";
                }
                else
                {
                    // TurbiditySelectedCascade Feed değilse
                    string feature = Properties.Settings.Default.EditPump4Feature;
                    if (!string.IsNullOrEmpty(feature) && feature != "Turbidity" && 
                        featureButtons.TryGetValue(feature, out var featureButton))
                    {
                        featureButton.IsChecked = true;
                        lastValidFeatureSelection = feature;
                    }
                    else
                    {
                        // Varsayılan Feed
                        if (Feed != null) Feed.IsChecked = true;
                        lastValidFeatureSelection = "Feed";
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata olursa güvenli bir şekilde defaults'a dön
                foreach (var btn in tubeTypeButtons.Values) btn.IsChecked = false;
                foreach (var btn in featureButtons.Values) btn.IsChecked = false;
                foreach (var btn in displayCountUnitButtons.Values) btn.IsChecked = false;
        
                Button13.IsChecked = true;
                ml.IsChecked = true;
        
                if (Properties.Settings.Default.TurbiditySelectedCascade == "Feed")
                    Turbidity.IsChecked = true;
                else
                    Feed.IsChecked = true;
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
                    // İlgili buton featureButtons'da ve geçersiz bir düğme ise, seçilmemeli
                    if (buttonDictionary == featureButtons)
                    {
                        if (button == Turbidity && Properties.Settings.Default.TurbiditySelectedCascade != "Feed")
                        {
                            // Turbidity seçilemez çünkü TurbiditySelectedCascade "Feed" değil
                            if (defaultButton != null && defaultButton != Turbidity)
                            {
                                defaultButton.IsChecked = true;
                            }
                            else if (Feed != null && Properties.Settings.Default.TurbiditySelectedCascade != "Feed")
                            {
                                Feed.IsChecked = true;
                            }
                            else if (BalanceFeed != null && Properties.Settings.Default.TurbiditySelectedCascade != "Feed")
                            {
                                BalanceFeed.IsChecked = true;
                            }
                            return;
                        }
                        else if ((button == BalanceFeed || button == Feed) &&
                                 Properties.Settings.Default.TurbiditySelectedCascade == "Feed")
                        {
                            // Balance Feed veya Feed seçilemez çünkü TurbiditySelectedCascade "Feed"
                            if (Turbidity != null)
                            {
                                Turbidity.IsChecked = true;
                            }
                            return;
                        }
                    }

                    button.IsChecked = true;
                }
                else if (defaultButton != null)
                {
                    // Varsayılan düğmeyi kontrol edin
                    if (buttonDictionary == featureButtons)
                    {
                        if (defaultButton == Turbidity && Properties.Settings.Default.TurbiditySelectedCascade != "Feed")
                        {
                            // Turbidity seçilemez, Feed veya Balance Feed'e geç
                            if (Feed != null)
                            {
                                Feed.IsChecked = true;
                            }
                            else if (BalanceFeed != null)
                            {
                                BalanceFeed.IsChecked = true;
                            }
                            return;
                        }
                        else if ((defaultButton == BalanceFeed || defaultButton == Feed) &&
                                 Properties.Settings.Default.TurbiditySelectedCascade == "Feed")
                        {
                            // Balance Feed veya Feed seçilemez, Turbidity'e geç
                            if (Turbidity != null)
                            {
                                Turbidity.IsChecked = true;
                            }
                            return;
                        }
                    }

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

                // Ensure lastValidFeatureSelection is initialized
                if (string.IsNullOrEmpty(lastValidFeatureSelection))
                {
                    lastValidFeatureSelection = Properties.Settings.Default.TurbiditySelectedCascade == "Feed"
                        ? "Turbidity"
                        : "Feed";
                }

                // Special handling for Turbidity button
                if (clickedButton == Turbidity)
                {
                    // Check if TurbiditySelectedCascade is set to "Feed"
                    if (Properties.Settings.Default.TurbiditySelectedCascade != "Feed")
                    {
                        // Prevent the Turbidity button from being checked
                        clickedButton.IsChecked = false;

                        // Show a warning message
                        MessageBox.Show("To select Turbidity feature, the TurbiditySelectedCascade must be set to 'Feed' in EditTurbidity settings.",
                                     "Configuration Required",
                                     MessageBoxButton.OK,
                                     MessageBoxImage.Warning);

                        // Restore the previous feature selection
                        RestoreFeatureSelection();
                        return;
                    }
                }
                // Special handling for Balance Feed or Feed buttons
                else if (clickedButton == BalanceFeed || clickedButton == Feed)
                {
                    // Check if TurbiditySelectedCascade is set to "Feed"
                    if (Properties.Settings.Default.TurbiditySelectedCascade == "Feed")
                    {
                        // Prevent the button from being checked
                        clickedButton.IsChecked = false;

                        // Show a warning message
                        MessageBox.Show("To select Balance Feed or Feed feature, the TurbiditySelectedCascade must not be set to 'Feed' in EditTurbidity settings.",
                                     "Configuration Required",
                                     MessageBoxButton.OK,
                                     MessageBoxImage.Warning);

                        // Restore the previous feature selection
                        RestoreFeatureSelection();
                        return;
                    }
                }

                // For feature buttons, update the last valid selection when a valid button is selected
                if (featureButtons.ContainsValue(clickedButton))
                {
                    // Safely find the key for the clicked button
                    foreach (var pair in featureButtons)
                    {
                        if (pair.Value == clickedButton)
                        {
                            lastValidFeatureSelection = pair.Key;
                            break;
                        }
                    }
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

        private void RestoreFeatureSelection()
        {
            try
            {
                // Reset all buttons
                foreach (var btn in featureButtons.Values)
                {
                    btn.IsChecked = false;
                }

                // TurbiditySelectedCascade değerine göre geçerli düğmeyi belirle
                if (Properties.Settings.Default.TurbiditySelectedCascade == "Feed")
                {
                    // TurbiditySelectedCascade "Feed" ise sadece Turbidity geçerlidir
                    if (Turbidity != null)
                    {
                        Turbidity.IsChecked = true;
                        lastValidFeatureSelection = "Turbidity";
                    }
                }
                else
                {
                    // TurbiditySelectedCascade "Feed" değilse, BalanceFeed veya Feed geçerlidir
                    if (featureButtons.TryGetValue(lastValidFeatureSelection, out var button) &&
                        (button == BalanceFeed || button == Feed))
                    {
                        button.IsChecked = true;
                    }
                    else if (Feed != null)
                    {
                        Feed.IsChecked = true;
                        lastValidFeatureSelection = "Feed";
                    }
                    else if (BalanceFeed != null)
                    {
                        BalanceFeed.IsChecked = true;
                        lastValidFeatureSelection = "Balance Feed";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error restoring selection: {ex.Message}");

                // Hata durumunda varsayılan olarak geri dön
                if (Properties.Settings.Default.TurbiditySelectedCascade == "Feed" && Turbidity != null)
                {
                    Turbidity.IsChecked = true;
                    lastValidFeatureSelection = "Turbidity";
                }
                else if (Feed != null)
                {
                    Feed.IsChecked = true;
                    lastValidFeatureSelection = "Feed";
                }
                else if (BalanceFeed != null)
                {
                    BalanceFeed.IsChecked = true;
                    lastValidFeatureSelection = "Balance Feed";
                }
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

            // Ensure the clicked button is checked (unless it has invalid settings)
            if (featureButtons.ContainsValue(clickedButton))
            {
                // Feature button validation
                if (clickedButton == Turbidity)
                {
                    // Only check if valid
                    clickedButton.IsChecked = Properties.Settings.Default.TurbiditySelectedCascade == "Feed";
                }
                else if (clickedButton == BalanceFeed || clickedButton == Feed)
                {
                    // Only check if valid
                    clickedButton.IsChecked = Properties.Settings.Default.TurbiditySelectedCascade != "Feed";
                }
                else
                {
                    // Other feature buttons
                    clickedButton.IsChecked = true;
                }
            }
            else
            {
                // Non-feature buttons
                clickedButton.IsChecked = true;
            }

            return true;
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                var clickedButton = sender as ToggleButton;
                if (clickedButton == null) return;

                // Geçersiz ayarlara sahip düğmelerin işaretlerinin kaldırılmasına izin ver
                if ((clickedButton == Turbidity && Properties.Settings.Default.TurbiditySelectedCascade != "Feed") ||
                    ((clickedButton == BalanceFeed || clickedButton == Feed) &&
                     Properties.Settings.Default.TurbiditySelectedCascade == "Feed"))
                {
                    return;
                }

                // Her grupta en az bir düğmenin işaretli kalmasını sağla
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
            // Validate Turbidity selection
            if (Turbidity.IsChecked == true && Properties.Settings.Default.TurbiditySelectedCascade != "Feed")
            {
                MessageBox.Show("To select Turbidity feature, the TurbiditySelectedCascade must be set to 'Feed' in EditTurbidity settings.",
                             "Configuration Required",
                             MessageBoxButton.OK,
                             MessageBoxImage.Warning);
                RestoreFeatureSelection();
                return;
            }

            // Validate Balance Feed and Feed selections
            if ((BalanceFeed.IsChecked == true || Feed.IsChecked == true) &&
                Properties.Settings.Default.TurbiditySelectedCascade == "Feed")
            {
                MessageBox.Show("To select Balance Feed or Feed feature, the TurbiditySelectedCascade must not be set to 'Feed' in EditTurbidity settings.",
                             "Configuration Required",
                             MessageBoxButton.OK,
                             MessageBoxImage.Warning);
                RestoreFeatureSelection();
                return;
            }

            SaveSettings();
            this.Close();
        }
    }
}
