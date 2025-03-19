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
                if (Properties.Settings.Default.EditTurbidityCascade == 1 &&
                    Properties.Settings.Default.EditPump4Feature != 2)
                {
                    Properties.Settings.Default.EditPump4Feature = 2;
                    Properties.Settings.Default.Save();
                }
                else if (Properties.Settings.Default.EditTurbidityCascade != 1 &&
                         Properties.Settings.Default.EditPump4Feature == 2)
                {
                    Properties.Settings.Default.EditPump4Feature = 1;
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
                double tubeType = Properties.Settings.Default.EditPump4TubeType;
                switch (tubeType)
                {
                    case 0: Button13.IsChecked = true; break;
                    case 1: Button14.IsChecked = true; break;
                    case 2: Button19.IsChecked = true; break;
                    case 3: Button16.IsChecked = true; break;
                    case 4: Button25.IsChecked = true; break;
                    case 5: Button17.IsChecked = true; break;
                    case 6: Button18.IsChecked = true; break;
                    default: Button13.IsChecked = true; break;
                }

                // DisplayCountUnit ayarlarını yükle
                double displayUnit = Properties.Settings.Default.EditPump4DisplayCountUnit;
                switch (displayUnit)
                {
                    case 0: Count.IsChecked = true; break;
                    case 1: ml.IsChecked = true; break;
                    default: ml.IsChecked = true; break; // varsayılan
                }

                // Feature ayarlarını yükle - hiç mesaj göstermeden
                if (Properties.Settings.Default.EditTurbidityCascade == 1)
                {
                    // Feed modunda sadece Turbidity geçerli
                    if (Turbidity != null) Turbidity.IsChecked = true;
                    lastValidFeatureSelection = "Turbidity";
                }
                else
                {
                    // TurbiditySelectedCascade Feed değilse
                    double feature = Properties.Settings.Default.EditPump4Feature;
                    switch (feature)
                    {
                        case 0: // Balance Feed
                            if (BalanceFeed != null) BalanceFeed.IsChecked = true;
                            lastValidFeatureSelection = "Balance Feed";
                            break;
                        case 1: // Feed
                            if (Feed != null) Feed.IsChecked = true;
                            lastValidFeatureSelection = "Feed";
                            break;
                        case 2: // Turbidity (bu durumda geçersiz, çünkü TurbiditySelectedCascade != 1)
                            // Varsayılan Feed
                            if (Feed != null) Feed.IsChecked = true;
                            lastValidFeatureSelection = "Feed";
                            break;
                        default:
                            // Varsayılan Feed
                            if (Feed != null) Feed.IsChecked = true;
                            lastValidFeatureSelection = "Feed";
                            break;
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
        
                if (Properties.Settings.Default.EditTurbidityCascade == 1)
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

        //private void LoadSettings()
        //{
        //    LoadButtonGroupSettings(tubeTypeButtons, Properties.Settings.Default.EditPump4TubeType, Button13);
        //    LoadButtonGroupSettings(featureButtons, Properties.Settings.Default.EditPump4Feature, BalanceFeed);
        //    LoadButtonGroupSettings(displayCountUnitButtons, Properties.Settings.Default.EditPump4DisplayCountUnit, ml);
        //}

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
                        if (button == Turbidity && Properties.Settings.Default.EditTurbidityCascade != 1)
                        {
                            // Turbidity seçilemez çünkü TurbiditySelectedCascade "Feed" değil
                            if (defaultButton != null && defaultButton != Turbidity)
                            {
                                defaultButton.IsChecked = true;
                            }
                            else if (Feed != null && Properties.Settings.Default.EditTurbidityCascade != 1)
                            {
                                Feed.IsChecked = true;
                            }
                            else if (BalanceFeed != null && Properties.Settings.Default.EditTurbidityCascade != 1)
                            {
                                BalanceFeed.IsChecked = true;
                            }
                            return;
                        }
                        else if ((button == BalanceFeed || button == Feed) &&
                                 Properties.Settings.Default.EditTurbidityCascade == 1)
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
                        if (defaultButton == Turbidity && Properties.Settings.Default.EditTurbidityCascade != 1)
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
                                 Properties.Settings.Default.EditTurbidityCascade == 1)
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
                // Her şey zaten HandleButtonToggle'da ayarlandı
                // Sadece tekrar kaydetmek için
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}");
            }
        }
        private double GetFeatureValue()
        {
            if (BalanceFeed?.IsChecked == true) return 0;
            if (Feed?.IsChecked == true) return 1;
            if (Turbidity?.IsChecked == true) return 2;
            return 0; // varsayılan değer
        }

        // DisplayCount değerini almak için yardımcı metod
        private double GetDisplayCountValue()
        {
            if (Count?.IsChecked == true) return 0;
            if (ml?.IsChecked == true) return 1;
            return 0; // varsayılan değer
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

                // TubeType buttons handling
                if (tubeTypeButtons.ContainsValue(clickedButton))
                {
                    // Tıklanan butonun değerini belirle
                    if (clickedButton == Button13) Properties.Settings.Default.EditPump4TubeType = 0;
                    else if (clickedButton == Button14) Properties.Settings.Default.EditPump4TubeType = 1;
                    else if (clickedButton == Button19) Properties.Settings.Default.EditPump4TubeType = 2;
                    else if (clickedButton == Button16) Properties.Settings.Default.EditPump4TubeType = 3;
                    else if (clickedButton == Button25) Properties.Settings.Default.EditPump4TubeType = 4;
                    else if (clickedButton == Button17) Properties.Settings.Default.EditPump4TubeType = 5;
                    else if (clickedButton == Button18) Properties.Settings.Default.EditPump4TubeType = 6;

                    Properties.Settings.Default.Save();
                }

                // Feature buttons handling - direct update of setting
                if (featureButtons.ContainsValue(clickedButton))
                {
                    if (clickedButton == BalanceFeed && Properties.Settings.Default.EditTurbidityCascade != 1)
                    {
                        Properties.Settings.Default.EditPump4Feature = 0;
                        Properties.Settings.Default.Save();
                    }
                    else if (clickedButton == Feed && Properties.Settings.Default.EditTurbidityCascade != 1)
                    {
                        Properties.Settings.Default.EditPump4Feature = 1;
                        Properties.Settings.Default.Save();
                    }
                    else if (clickedButton == Turbidity && Properties.Settings.Default.EditTurbidityCascade == 1)
                    {
                        Properties.Settings.Default.EditPump4Feature = 2;
                        Properties.Settings.Default.Save();
                    }
                }

                // DisplayCount buttons handling
                if (displayCountUnitButtons.ContainsValue(clickedButton))
                {
                    if (clickedButton == Count) Properties.Settings.Default.EditPump4DisplayCountUnit = 0;
                    else if (clickedButton == ml) Properties.Settings.Default.EditPump4DisplayCountUnit = 1;

                    Properties.Settings.Default.Save();
                }

                // Remaining code (validation logic) stays the same
                // ...

                // Continue with existing code...
                if (string.IsNullOrEmpty(lastValidFeatureSelection))
                {
                    lastValidFeatureSelection = Properties.Settings.Default.EditTurbidityCascade == 1
                        ? "Turbidity"
                        : "Feed";
                }

                // Special handling for Turbidity button
                if (clickedButton == Turbidity)
                {
                    // Check if TurbiditySelectedCascade is set to "Feed"
                    if (Properties.Settings.Default.EditTurbidityCascade != 1)
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
                    if (Properties.Settings.Default.EditTurbidityCascade == 1)
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
                if (Properties.Settings.Default.EditTurbidityCascade == 1)
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
                if (Properties.Settings.Default.EditTurbidityCascade == 1 && Turbidity != null)
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
                    clickedButton.IsChecked = Properties.Settings.Default.EditTurbidityCascade == 1;
                }
                else if (clickedButton == BalanceFeed || clickedButton == Feed)
                {
                    // Only check if valid
                    clickedButton.IsChecked = Properties.Settings.Default.EditTurbidityCascade != 1;
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
                if ((clickedButton == Turbidity && Properties.Settings.Default.EditTurbidityCascade != 1) ||
                    ((clickedButton == BalanceFeed || clickedButton == Feed) &&
                     Properties.Settings.Default.EditTurbidityCascade == 1))
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
            if (Turbidity.IsChecked == true && Properties.Settings.Default.EditTurbidityCascade != 1)
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
                Properties.Settings.Default.EditTurbidityCascade == 1)
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
