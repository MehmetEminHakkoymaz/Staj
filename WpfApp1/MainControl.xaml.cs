using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    public partial class MainControl : UserControl, INotifyPropertyChanged
    {
        private MainWindow mainWindow;

        public MainControl(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            ellipse1.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse2.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse3.MouseLeftButtonDown += ellipse3_MouseDown;
            // ellipse4 için özel event handler kullan
            ellipse4.MouseLeftButtonDown += ellipse4_MouseDown;
            //ellipse5.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            //ellipse6.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            //ellipse7.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            //ellipse8.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse9.MouseLeftButtonDown += ellipse9_MouseDown;
            ellipse19.MouseLeftButtonDown += ellipse19_MouseLeftButtonDown;
            KeypadControl.ValueSelected += KeyPadControl_ValueSelected;
            comparisonTimer.Interval = TimeSpan.FromSeconds(1); // 1 saniyelik aralıklarla
            comparisonTimer.Tick += ComparisonTimer_Tick; // Zamanlayıcı olayı

            DataContext = this;
            InitializeTemperatureMonitoring();
            UpdateFoamLevelStatus();
            // Settings değişikliklerini dinle
            Properties.Settings.Default.PropertyChanged += Settings_PropertyChanged;
            UpdateBorderVisibilities();
            //CompareGas2Values();


        }

        private double currentTemperature;
        private double maxTemperature = 37.0; // Maksimum sıcaklık değeri
        private double warningOffset = 10.0;  // Uyarı için düşülecek derece
        private DispatcherTimer temperatureTimer;

        private bool isGas2ValueLessThanTarget;
        public bool IsGas2ValueLessThanTarget
        {
            get => isGas2ValueLessThanTarget;
            set
            {
                isGas2ValueLessThanTarget = value;
                OnPropertyChanged(nameof(IsGas2ValueLessThanTarget));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private bool isHighTemperature;
        public bool IsHighTemperature
        {
            get => isHighTemperature;
            set
            {
                isHighTemperature = value;
                OnPropertyChanged(nameof(IsHighTemperature));
            }
        }

        private bool isWarningTemperature;
        public bool IsWarningTemperature
        {
            get => isWarningTemperature;
            set
            {
                isWarningTemperature = value;
                OnPropertyChanged(nameof(IsWarningTemperature));
            }
        }

        private bool isNormalTemperature;
        public bool IsNormalTemperature
        {
            get => isNormalTemperature;
            set
            {
                isNormalTemperature = value;
                OnPropertyChanged(nameof(IsNormalTemperature));
            }
        }
        //private void CompareGas2Values()
        //{
        //    if (double.TryParse(Gas2Value.Content?.ToString(), out double value) &&
        //        double.TryParse(Gas2Target.Text, out double target))
        //    {
        //        IsGas2ValueLessThanTarget = value < target;
        //    }
        //    else
        //    {
        //        IsGas2ValueLessThanTarget = false;
        //    }
        //}


        private void InitializeTemperatureMonitoring()
        {
            temperatureTimer = new DispatcherTimer();
            temperatureTimer.Interval = TimeSpan.FromSeconds(1);
            temperatureTimer.Tick += TemperatureTimer_Tick;
            temperatureTimer.Start();
        }

        private void TemperatureTimer_Tick(object sender, EventArgs e)
        {
            // Burada sensörden sıcaklık değerini okuyacak kodunuz olacak
            // Şimdilik test için rastgele değer üretiyoruz
            // pH için mevcut kod
            currentTemperature = GetTemperatureFromSensor();
            TemperatureIndicator.Text = currentTemperature.ToString("F1");
            UpdateTemperatureStatus();

            // pO2 için
            pO2CurrentTemperature = GetTemperatureFromSensor(); // veya farklı bir sensör
            pO2TemperatureIndicator.Text = pO2CurrentTemperature.ToString("F1");
            UpdatepO2TemperatureStatus();

            // Redox için
            redoxCurrentTemperature = GetTemperatureFromSensor(); // veya farklı bir sensör
            RedoxTemperatureIndicator.Text = redoxCurrentTemperature.ToString("F1");
            UpdateRedoxTemperatureStatus();
        }

        private double GetTemperatureFromSensor()
        {
            // Burada gerçek sensör kodunuz olacak
            // Şimdilik test için rastgele değer üretiyoruz
            Random rand = new Random();
            return 20 + rand.NextDouble() * 30; // 20-50 derece arası
        }

        private void UpdateTemperatureStatus()
        {
            if (currentTemperature >= maxTemperature)
            {
                IsHighTemperature = true;
                IsWarningTemperature = false;
                IsNormalTemperature = false;
            }
            else if (currentTemperature >= (maxTemperature - warningOffset))
            {
                IsHighTemperature = false;
                IsWarningTemperature = true;
                IsNormalTemperature = false;
            }
            else
            {
                IsHighTemperature = false;
                IsWarningTemperature = false;
                IsNormalTemperature = true;
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // pO2 için property'ler
        private bool ispO2HighTemperature;
        public bool IspO2HighTemperature
        {
            get => ispO2HighTemperature;
            set
            {
                ispO2HighTemperature = value;
                OnPropertyChanged(nameof(IspO2HighTemperature));
            }
        }

        private bool ispO2WarningTemperature;
        public bool IspO2WarningTemperature
        {
            get => ispO2WarningTemperature;
            set
            {
                ispO2WarningTemperature = value;
                OnPropertyChanged(nameof(IspO2WarningTemperature));
            }
        }

        private bool ispO2NormalTemperature;
        public bool IspO2NormalTemperature
        {
            get => ispO2NormalTemperature;
            set
            {
                ispO2NormalTemperature = value;
                OnPropertyChanged(nameof(IspO2NormalTemperature));
            }
        }

        // Redox için property'ler
        private bool isRedoxHighTemperature;
        public bool IsRedoxHighTemperature
        {
            get => isRedoxHighTemperature;
            set
            {
                isRedoxHighTemperature = value;
                OnPropertyChanged(nameof(IsRedoxHighTemperature));
            }
        }

        private bool isRedoxWarningTemperature;
        public bool IsRedoxWarningTemperature
        {
            get => isRedoxWarningTemperature;
            set
            {
                isRedoxWarningTemperature = value;
                OnPropertyChanged(nameof(IsRedoxWarningTemperature));
            }
        }

        private bool isRedoxNormalTemperature;
        public bool IsRedoxNormalTemperature
        {
            get => isRedoxNormalTemperature;
            set
            {
                isRedoxNormalTemperature = value;
                OnPropertyChanged(nameof(IsRedoxNormalTemperature));
            }
        }

        // Sıcaklık değerleri için field'lar
        private double pO2CurrentTemperature;
        private double redoxCurrentTemperature;

        private void UpdatepO2TemperatureStatus()
        {
            if (pO2CurrentTemperature >= maxTemperature)
            {
                IspO2HighTemperature = true;
                IspO2WarningTemperature = false;
                IspO2NormalTemperature = false;
            }
            else if (pO2CurrentTemperature >= (maxTemperature - warningOffset))
            {
                IspO2HighTemperature = false;
                IspO2WarningTemperature = true;
                IspO2NormalTemperature = false;
            }
            else
            {
                IspO2HighTemperature = false;
                IspO2WarningTemperature = false;
                IspO2NormalTemperature = true;
            }
        }

        private void UpdateRedoxTemperatureStatus()
        {
            if (redoxCurrentTemperature >= maxTemperature)
            {
                IsRedoxHighTemperature = true;
                IsRedoxWarningTemperature = false;
                IsRedoxNormalTemperature = false;
            }
            else if (redoxCurrentTemperature >= (maxTemperature - warningOffset))
            {
                IsRedoxHighTemperature = false;
                IsRedoxWarningTemperature = true;
                IsRedoxNormalTemperature = false;
            }
            else
            {
                IsRedoxHighTemperature = false;
                IsRedoxWarningTemperature = false;
                IsRedoxNormalTemperature = true;
            }
        }





        public Label testlabel => TemperatureValue;

        private TextBox activeTextBox = null;

        private TextBox currentTextBox = null;

        private DispatcherTimer comparisonTimer = new DispatcherTimer();

        
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox focusedTextBox = sender as TextBox;
            if (focusedTextBox != null)
            {
                // TextBox'ın ebeveyninin ebeveynini bul (Grid varsayıyoruz)
                DependencyObject parent = VisualTreeHelper.GetParent(focusedTextBox);
                DependencyObject grandParent = parent != null ? VisualTreeHelper.GetParent(parent) : null;
                Grid parentGrid = grandParent as Grid;
                activeTextBox = sender as TextBox; // Odaklanan TextBox'ı aktif olarak ayarla
                if (parentGrid != null)
                {
                    // Grid içindeki ilk Label'ı bul
                    Label firstLabel = parentGrid.Children.OfType<Label>().FirstOrDefault();
                    if (firstLabel != null)
                    {
                        // Label'ın içeriğini al
                        string labelContent = firstLabel.Content.ToString();
                        // KeyPad'e label içeriğini gönder
                        KeypadPopup.IsOpen = true;
                        KeypadControl.SetLabelContent(labelContent);
                    }
                }
            }
        }


        private void ToggleRightGridButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                Thickness targetMargin;
                string targetImage;

                // RightGrid'in görünürlüğünü kontrol et ve güncelle
                if (RightGrid.Margin.Right == 0)
                {
                    // RightGrid'i gizle
                    targetMargin = new Thickness(0, 50, -325, 0); // Margin değerlerini projenize göre ayarlayın
                    targetImage = "pack://application:,,,/WpfApp1;component/images/Left.png";
                }
                else
                {
                    // RightGrid'i göster
                    targetMargin = new Thickness(0, 50, 0, 0); // Margin değerlerini projenize göre ayarlayın
                    targetImage = "pack://application:,,,/WpfApp1;component/images/Right.png";
                }

                ThicknessAnimation marginAnimation = new ThicknessAnimation
                {
                    To = targetMargin,
                    Duration = TimeSpan.FromSeconds(0.2), // Animasyon süresi
                    FillBehavior = FillBehavior.Stop
                };

                marginAnimation.Completed += (s, args) =>
                {
                    RightGrid.Margin = targetMargin; // Animasyon tamamlandığında Margin'i güncelle
                    button.Tag = targetImage; // Butonun görselini güncelle
                };

                RightGrid.BeginAnimation(FrameworkElement.MarginProperty, marginAnimation);
            }
        }

        //ÇOK ÖNEMLİ
        private void EditpH_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var editpHWindow = new WpfApp1.EditPages.EditpH();
            editpHWindow.Show();
        }

        private void EditpO2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var editpO2Window = new WpfApp1.EditPages.EditpO2();
            editpO2Window.Show();
        }

        private void EditFoam_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var editFoamWindow = new WpfApp1.EditPages.EditFoam();
            editFoamWindow.Show();
        }
        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Ellipse clickedEllipse)
            {
                // Canvas'ı bul
                Canvas parentCanvas = clickedEllipse.Parent as Canvas;
                if (parentCanvas == null) return;

                double canvasWidth = parentCanvas.ActualWidth;
                double ellipseWidth = clickedEllipse.ActualWidth;
                double maxRight = canvasWidth - ellipseWidth - 12; // 12 = 6 (sol boşluk) + 6 (sağ boşluk)
                double targetLeft = Canvas.GetLeft(clickedEllipse) == 6 ? maxRight : 6; // Yuvarlağın hedef pozisyonu

                DoubleAnimation animation = new DoubleAnimation
                {
                    To = targetLeft,
                    Duration = TimeSpan.FromSeconds(0.5),
                    FillBehavior = FillBehavior.Stop
                };

                animation.Completed += (s, a) =>
                {
                    Canvas.SetLeft(clickedEllipse, targetLeft); // Animasyon tamamlandığında yuvarlağın pozisyonunu güncelle

                    if (targetLeft == maxRight)
                    {
                        clickedEllipse.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101")); // Sağdaysa kırmızı yap
                    }
                    else
                    {
                        clickedEllipse.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF")); // Soldaysa gri yap
                    }

                    // Ellipse'in rengi değiştikten sonra butonun görünürlüğünü kontrol et
                    CheckEllipsePositionAndSetButtonVisibility(clickedEllipse, GetConditionalButton(clickedEllipse));

                    // FavouritesControl'daki ilgili ellipse'i güncelle
                    mainWindow.favouritesControl.UpdateEllipsePosition(clickedEllipse.Name, targetLeft);
                    // FavouritesControl'daki ilgili conditionalButton'ı güncelle
                    mainWindow.favouritesControl.UpdateConditionalButtonVisibility(clickedEllipse.Name, targetLeft);
                };

                clickedEllipse.BeginAnimation(Canvas.LeftProperty, animation);
            }
        }

        private void ellipse4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Önce Properties.Settings.Default'tan cascade değerini kontrol edin
            if (Properties.Settings.Default.pO2SelectedCascade == "None")
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("pO2 cascade selection is required. Please go to EditpO2 settings and select a cascade option.",
                              "Configuration Required",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return; // Ellipse4'ün durumunu değiştirmeden fonksiyonu sonlandır
            }

            // Normal ellipse tıklama olayını çağır
            Ellipse_MouseLeftButtonDown(sender, e);
        }
        private void ellipse3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Önce Properties.Settings.Default'tan cascade değerini kontrol edin
            if (Properties.Settings.Default.pHSelectedCascade == "None")
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("pH cascade selection is required. Please go to EditpH settings and select a cascade option.",
                              "Configuration Required",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return; // Ellipse4'ün durumunu değiştirmeden fonksiyonu sonlandır
            }

            // Normal ellipse tıklama olayını çağır
            Ellipse_MouseLeftButtonDown(sender, e);
        }

        private void ellipse9_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Önce Properties.Settings.Default'tan cascade değerini kontrol edin
            if (Properties.Settings.Default.FoamSelectedMode == "None")
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("Foam selection is required. Please go to EditFoam settings and select an option.",
                              "Configuration Required",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return; // Ellipse4'ün durumunu değiştirmeden fonksiyonu sonlandır
            }

            // Normal ellipse tıklama olayını çağır
            Ellipse_MouseLeftButtonDown(sender, e);
        }
        private Button GetConditionalButton(Ellipse ellipse)
        {
            return ellipse.Name switch
            {
                "ellipse1" => conditionalButton,
                "ellipse2" => conditionalButtonStirrer,
                "ellipse3" => conditionalButtonpH,
                "ellipse4" => conditionalButtonpO2,
                //"ellipse5" => conditionalButtonGas1,
                //"ellipse6" => conditionalButtonGas2,
                //"ellipse7" => conditionalButtonGas3,
                //"ellipse8" => conditionalButtonGas4,
                "ellipse9" => conditionalButtonFoam,
                "ellipse19" => conditionalButtonRedox,
                _ => null
            };
        }

        public void UpdateEllipsePosition(string ellipseName, double targetLeft)
        {
            Ellipse targetEllipse = FindName(ellipseName) as Ellipse;
            if (targetEllipse == null) return;

            DoubleAnimation animation = new DoubleAnimation
            {
                To = targetLeft,
                Duration = TimeSpan.FromSeconds(0.5),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) =>
            {
                Canvas.SetLeft(targetEllipse, targetLeft); // Animasyon tamamlandığında yuvarlağın pozisyonunu güncelle

                if (targetLeft == 6)
                {
                    targetEllipse.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF")); // Soldaysa gri yap
                }
                else
                {
                    targetEllipse.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101")); // Sağdaysa kırmızı yap
                }
            };

            targetEllipse.BeginAnimation(Canvas.LeftProperty, animation);
        }
        public void UpdateConditionalButtonVisibility(string ellipseName, double targetLeft)
        {
            Ellipse targetEllipse = FindName(ellipseName) as Ellipse;
            if (targetEllipse == null) return;

            Button targetButton = GetConditionalButton(targetEllipse);
            if (targetButton == null) return;

            Canvas parentCanvas = targetEllipse.Parent as Canvas;
            if (parentCanvas == null) return;

            double canvasWidth = parentCanvas.ActualWidth;
            double ellipseRightPosition = targetLeft + targetEllipse.Width;

            targetButton.Visibility = ellipseRightPosition > canvasWidth / 2 ? Visibility.Visible : Visibility.Collapsed;
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckEllipsePositionAndSetButtonVisibility(ellipse1, conditionalButton);
            CheckEllipsePositionAndSetButtonVisibility(ellipse2, conditionalButtonStirrer);
            CheckEllipsePositionAndSetButtonVisibility(ellipse3, conditionalButtonpH);
            CheckEllipsePositionAndSetButtonVisibility(ellipse4, conditionalButtonpO2);
            //CheckEllipsePositionAndSetButtonVisibility(ellipse5, conditionalButtonGas1);
            //CheckEllipsePositionAndSetButtonVisibility(ellipse6, conditionalButtonGas2);
            //CheckEllipsePositionAndSetButtonVisibility(ellipse7, conditionalButtonGas3);
            //CheckEllipsePositionAndSetButtonVisibility(ellipse8, conditionalButtonGas4);
            CheckEllipsePositionAndSetButtonVisibility(ellipse9, conditionalButtonFoam);
            CheckEllipsePositionAndSetButtonVisibility(ellipse19, conditionalButtonRedox);
            UpdateFoamLevelStatus();

        }

        public void CheckEllipsePositionAndSetButtonVisibility(Ellipse ellipse, Button button)
        {
            // Ellipse'in parent'ını Canvas olarak al
            Canvas parentCanvas = ellipse.Parent as Canvas;
            if (parentCanvas == null) return;

            double canvasWidth = parentCanvas.ActualWidth; // Canvas'ın gerçek genişliğini kullan
            double ellipseRightPosition = Canvas.GetLeft(ellipse) + ellipse.Width; // Ellipse'in sağ kenarının konumu

            // Ellipse, Canvas'ın sağ yarısında ise butonu göster, değilse gizle
            //button.Visibility = ellipseRightPosition > canvasWidth / 2 ? Visibility.Visible : Visibility.Collapsed;
            if (ellipseRightPosition > canvasWidth / 2 && mainWindow.FirstStartButton.Visibility == Visibility.Collapsed)
            {
                button.Visibility = Visibility.Visible;
            }
            else
            {
                button.Visibility = Visibility.Collapsed;
            }
        }

        private void ConditionalButton_Click(object sender, RoutedEventArgs e)
        {
            // Zamanlayıcıyı başlatmadan önce, tıklanan butonu belirle
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Tıklanan butonun Tag'inde ilişkili Grid'i saklayın (XAML'de veya başka bir yerde ayarlanmalıdır)
                clickedButton.Tag = clickedButton.Parent as Grid;
            }

            // Zamanlayıcıyı başlat
            comparisonTimer.Start();
        }

        private void ComparisonTimer_Tick(object sender, EventArgs e)
        {
            // Tüm butonları kontrol et
            foreach (Button button in FindVisualChildren<Button>(this))
            {
                if (button.Tag is Grid parentGrid)
                {
                    // Grid içindeki ikinci Label'ı bul
                    var secondLabel = parentGrid.Children.OfType<Label>().ElementAtOrDefault(1);
                    // Grid içindeki TextBox'ı bul
                    var textBox = parentGrid.Children.OfType<Border>().FirstOrDefault()?.Child as TextBox;

                    if (secondLabel != null && textBox != null)
                    {
                        // Label ve TextBox içindeki değerleri karşılaştır
                        if (secondLabel.Content.ToString() == textBox.Text)
                        {
                            // Değerler aynıysa butonun arka planını yeşil yap
                            button.Background = new SolidColorBrush(Colors.Green);
                        }
                        else
                        {
                            // Değerler farklıysa butonun arka planını sarı yap
                            button.Background = new SolidColorBrush(Colors.Yellow);
                        }
                    }
                }
            }
            //CompareGas2Values();

        }

        // Yardımcı metod: Belirli bir türdeki tüm görsel çocukları bulur
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }


        private void KeyPadControl_ValueSelected(object sender, string value)
        {
            if (activeTextBox != null)
            {
                if (activeTextBox.Tag is string tag && ParseRange(tag, out int min, out int max))
                {
                    if (int.TryParse(value, out int intValue) && intValue >= min && intValue <= max)
                    {
                        activeTextBox.Text = value; // KeyPad'den gelen değeri aktif TextBox'a atayın
                    }
                    else
                    {
                        KeypadPopup.IsOpen = false; // Hata durumunda KeyPad'i tekrar aç

                        MessageBox.Show($"Please enter a value between {min} and {max}.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        private bool ParseRange(string tag, out int min, out int max)
        {
            var parts = tag.Split(',');
            if (parts.Length == 2 && int.TryParse(parts[0], out min) && int.TryParse(parts[1], out max))
            {
                return true;
            }
            min = max = 0;
            return false;
        }


        private void UpdateBorderVisibilities()
        {
            // Stirrer border görünürlüğünü güncelle
            if (FindName("StirrerTargetBorder") is Border stirrerBorder)
            {
                stirrerBorder.Visibility = Properties.Settings.Default.HideStirrerBorder ?
                    Visibility.Collapsed : Visibility.Visible;
            }

            // Gas1 border görünürlüğünü güncelle
            if (FindName("Gas1TargetBorder") is Border gas1Border)
            {
                gas1Border.Visibility = Properties.Settings.Default.HideAirFlowBorder ?
                    Visibility.Collapsed : Visibility.Visible;
            }
        }

        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Eğer ilgili ayarlar değiştiyse border görünürlüklerini güncelle
            if (e.PropertyName == "HideStirrerBorder" ||
                e.PropertyName == "HideGas1Border" ||
                e.PropertyName == "pO2SelectedCascade")
            {
                UpdateBorderVisibilities();
            }
            // FoamLevel değiştiyse foam durumunu güncelle
            if (e.PropertyName == "FoamLevel")
            {
                UpdateFoamLevelStatus();
            }
        }
        private void Gas2Target_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TextBox değeri değiştiğinde karşılaştırma yap
            //CompareGas2Values();
        }

        // FoamLevel için property
        private bool isHighFoamLevel;
        public bool IsHighFoamLevel
        {
            get => isHighFoamLevel;
            set
            {
                isHighFoamLevel = value;
                OnPropertyChanged(nameof(IsHighFoamLevel));
            }
        }

        // FoamLevel durumunu kontrol eden metot
        private void UpdateFoamLevelStatus()
        {
            // FoamLevel 0'dan farklı ise üst seviyede
            IsHighFoamLevel = Properties.Settings.Default.FoamLevel != 0;

            // Label görünürlüklerini güncelle
            if (UnderFoam != null && AboveFoam != null)
            {
                UnderFoam.Visibility = IsHighFoamLevel ? Visibility.Collapsed : Visibility.Visible;
                AboveFoam.Visibility = IsHighFoamLevel ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public void SetFoamLevel(int level)
        {
            Properties.Settings.Default.FoamLevel = level;
            Properties.Settings.Default.Save();
            // UpdateFoamLevelStatus(); // Gerek yok, Settings_PropertyChanged tetiklenecek
        }

        private void EditRedox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var editRedoxWindow = new WpfApp1.EditPages.EditRedox();
            editRedoxWindow.Show();
        }

        private void ellipse19_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Önce Properties.Settings.Default'tan cascade değerini kontrol edin
            if (Properties.Settings.Default.RedoxSelectedCascade == "None")
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("Redox selection is required. Please go to EditRedox settings and select an option.",
                              "Configuration Required",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return; // Ellipse4'ün durumunu değiştirmeden fonksiyonu sonlandır
            }

            // Normal ellipse tıklama olayını çağır
            Ellipse_MouseLeftButtonDown(sender, e);
        }
    }
}
