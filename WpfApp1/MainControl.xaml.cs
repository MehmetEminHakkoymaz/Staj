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

            // conditionalButtonpO2 düğmesinin başlangıç görünürlüğünü ve rengini ayarla
            if (Properties.Settings.Default.pO2Ellipse == 1 &&
                Properties.Settings.Default.StartButton == 1 &&
                Properties.Settings.Default.pO2ConditionalButtonVisibility == 1)
            {
                conditionalButtonpO2.Visibility = Visibility.Visible;

                // Butonun rengini Settings'e göre ayarla
                switch (Properties.Settings.Default.pO2ConditionalButton)
                {
                    case 0: // Kırmızı
                        conditionalButtonpO2.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1: // Sarı
                        conditionalButtonpO2.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2: // Yeşil
                        conditionalButtonpO2.Background = new SolidColorBrush(Colors.Green);
                        break;
                    default:
                        conditionalButtonpO2.Background = new SolidColorBrush(Colors.Red);
                        break;
                }
            }
            else
            {
                conditionalButtonpO2.Visibility = Visibility.Collapsed;
            }
            // Click event handler'ı conditionalButtonpO2'ye bağla
            conditionalButtonpO2.Click -= conditionalButtonpO2_Click; // Önce eski bağlantıyı kaldır
            conditionalButtonpO2.Click += conditionalButtonpO2_Click; // Yeni bağlantıyı ekle





            // Load saved text box values
            LoadTextBoxValues();

            // Register TextChanged events
            RegisterTextChangedEvents();

            ellipse1.MouseLeftButtonDown += ellipse1_MouseLeftButtonDown;
            ellipse2.MouseLeftButtonDown += ellipse2_MouseLeftButtonDown;
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
                stirrerBorder.Visibility = Properties.Settings.Default.StirrerTargetBorder == 1 ?
                    Visibility.Collapsed : Visibility.Visible;
            }

            // Gas1 border görünürlüğünü güncelle
            if (FindName("Gas1TargetBorder") is Border gas1Border)
            {
                gas1Border.Visibility = Properties.Settings.Default.AirFlowTargetBorder == 1 ?
                    Visibility.Collapsed : Visibility.Visible;
            }
            if (FindName("pO2TargetBorder") is Border pO2Border)
            {
                pO2Border.Visibility = Properties.Settings.Default.pO2TargetBorder == 1 ?
                    Visibility.Collapsed : Visibility.Visible;
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
            // FoamLevel yerine FoamValue kullanarak kontrol et
            // FoamValue 0 ise UnderFoam görünür olsun, değilse AboveFoam görünür olsun
            IsHighFoamLevel = Properties.Settings.Default.FoamValue != 0;

            // Label görünürlüklerini güncelle
            if (UnderFoam != null && AboveFoam != null)
            {
                UnderFoam.Visibility = IsHighFoamLevel ? Visibility.Collapsed : Visibility.Visible;
                AboveFoam.Visibility = IsHighFoamLevel ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        // Bu metot artık FoamLevel yerine FoamValue'yi ayarlayacak şekilde değiştirilebilir
        public void SetFoamLevel(double value)
        {
            Properties.Settings.Default.FoamValue = value;
            Properties.Settings.Default.Save();
            // UpdateFoamLevelStatus(); // Gerek yok, Settings_PropertyChanged tetiklenecek
        }

        private void EditRedox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var editRedoxWindow = new WpfApp1.EditPages.EditRedox();
            editRedoxWindow.Show();
        }
        private void ellipse3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Önce Properties.Settings.Default'tan cascade değerini kontrol edin
            if (Properties.Settings.Default.EditpHCascade == 0)
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
            if (Properties.Settings.Default.EditFoamCascade == 0) // 0 = None
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


        private void ellipse19_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Önce Properties.Settings.Default'tan cascade değerini kontrol edin
            if (Properties.Settings.Default.EditRedoxCascade == 0)
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

        private void ellipse1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Properties.Settings.Default.EditRedoxCascade == 0)
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("deneme ses 1-2",
                              "Configuration Required",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return; // Ellipse4'ün durumunu değiştirmeden fonksiyonu sonlandır
            }

            Ellipse_MouseLeftButtonDown(sender, e);

        }

        private void ellipse2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Properties.Settings.Default.EditRedoxCascade == 0)
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("deneme ses 1-2",
                              "Configuration Required",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return; // Ellipse4'ün durumunu değiştirmeden fonksiyonu sonlandır
            }

            Ellipse_MouseLeftButtonDown(sender, e);
        }



        //PUBLİC MAİNCONTROL'DEKİ DEĞİŞİKLİKLER DIŞINDA KALAN TÜM DEĞİŞİKLİKLER BURDA pO2 GRİDİ İÇİN
        //PUBLİC MAİNCONTROL'DEKİ DEĞİŞİKLİKLER DIŞINDA KALAN TÜM DEĞİŞİKLİKLER BURDA pO2 GRİDİ İÇİN
        //PUBLİC MAİNCONTROL'DEKİ DEĞİŞİKLİKLER DIŞINDA KALAN TÜM DEĞİŞİKLİKLER BURDA pO2 GRİDİ İÇİN
        //PUBLİC MAİNCONTROL'DEKİ DEĞİŞİKLİKLER DIŞINDA KALAN TÜM DEĞİŞİKLİKLER BURDA pO2 GRİDİ İÇİN
        //PUBLİC MAİNCONTROL'DEKİ DEĞİŞİKLİKLER DIŞINDA KALAN TÜM DEĞİŞİKLİKLER BURDA pO2 GRİDİ İÇİN
        //PUBLİC MAİNCONTROL'DEKİ DEĞİŞİKLİKLER DIŞINDA KALAN TÜM DEĞİŞİKLİKLER BURDA pO2 GRİDİ İÇİN
        //PUBLİC MAİNCONTROL'DEKİ DEĞİŞİKLİKLER DIŞINDA KALAN TÜM DEĞİŞİKLİKLER BURDA pO2 GRİDİ İÇİN
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Make sure text boxes are loaded with saved values
            LoadTextBoxValues();
            // Set ellipse4 position based on pO2Ellipse setting
            Canvas canvas4 = ellipse4.Parent as Canvas;
            if (canvas4 != null)
            {
                double canvasWidth = canvas4.ActualWidth;
                double ellipseWidth = ellipse4.ActualWidth;
                double maxRight = canvasWidth - ellipseWidth - 12;

                // If pO2Ellipse is 1, position ellipse to the right
                if (Properties.Settings.Default.pO2Ellipse == 1)
                {
                    Canvas.SetLeft(ellipse4, maxRight);
                    ellipse4.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101"));
                }
                else
                {
                    Canvas.SetLeft(ellipse4, 6);
                    ellipse4.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF"));
                }
            }

            // Butonun görünürlüğünü ve rengini ayarla
            if (Properties.Settings.Default.pO2Ellipse == 1 &&
                Properties.Settings.Default.StartButton == 1 &&
                Properties.Settings.Default.pO2ConditionalButtonVisibility == 1)
            {
                conditionalButtonpO2.Visibility = Visibility.Visible;

                // Butonun rengini Settings'deki değere göre ayarla
                switch (Properties.Settings.Default.pO2ConditionalButton)
                {
                    case 0: // Kırmızı
                        conditionalButtonpO2.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1: // Sarı
                        conditionalButtonpO2.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2: // Yeşil
                        conditionalButtonpO2.Background = new SolidColorBrush(Colors.Green);
                        break;
                    default:
                        conditionalButtonpO2.Background = new SolidColorBrush(Colors.Red);
                        break;
                }
            }
            else
            {
                conditionalButtonpO2.Visibility = Visibility.Collapsed;
            }
            // Click event handler'ını ekle
            conditionalButtonpO2.Click -= conditionalButtonpO2_Click; // Önce eski bağlantıyı kaldır
            conditionalButtonpO2.Click += conditionalButtonpO2_Click; // Yeni bağlantıyı ekle


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

        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // When pO2Target changes in Settings, update the TextBox
            if (e.PropertyName == "pO2Target" && pO2Target != null)
            {
                // Only update if the TextBox doesn't have focus to avoid triggering a text changed event loop
                if (!pO2Target.IsFocused)
                {
                    pO2Target.Text = Properties.Settings.Default.pO2Target.ToString();
                }
            }

            // Eğer ilgili ayarlar değiştiyse border görünürlüklerini güncelle
            if (e.PropertyName == "StirrerTargetBorder" ||
                e.PropertyName == "HideGas1Border" ||
                e.PropertyName == "pO2SelectedCascade" ||
                e.PropertyName == "pO2Ellipse" ||
                e.PropertyName == "StartButton" ||
                e.PropertyName == "FoamValue" ||
                e.PropertyName == "pO2ConditionalButtonVisibility")  // Bu satırları ekledik
            {
                UpdateBorderVisibilities();

                // If pO2Ellipse, StartButton or pO2ConditionalButtonVisibility changed, update conditionalButtonpO2 visibility
                if (e.PropertyName == "pO2Ellipse" || e.PropertyName == "StartButton" || e.PropertyName == "pO2ConditionalButtonVisibility")
                {
                    // conditionalButtonpO2 düğmesinin görünürlüğünü güncelle
                    if (Properties.Settings.Default.pO2Ellipse == 1 &&
                        Properties.Settings.Default.StartButton == 1 &&
                        Properties.Settings.Default.pO2ConditionalButtonVisibility == 1)
                    {
                        conditionalButtonpO2.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        conditionalButtonpO2.Visibility = Visibility.Collapsed;
                    }

                    Canvas canvas4 = ellipse4.Parent as Canvas;
                    if (canvas4 != null && e.PropertyName == "pO2Ellipse")
                    {
                        double canvasWidth = canvas4.ActualWidth;
                        double ellipseWidth = ellipse4.ActualWidth;
                        double maxRight = canvasWidth - ellipseWidth - 12;

                        double targetLeft = Properties.Settings.Default.pO2Ellipse == 1 ? maxRight : 6;
                        Canvas.SetLeft(ellipse4, targetLeft);

                        if (Properties.Settings.Default.pO2Ellipse == 1)
                        {
                            ellipse4.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101"));
                        }
                        else
                        {
                            ellipse4.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF"));
                        }
                    }
                }
            }

            // pO2ConditionalButton, pO2Value veya pO2Target değiştiğinde butonun rengini güncelle
            if (e.PropertyName == "pO2ConditionalButton" || e.PropertyName == "pO2Value" || e.PropertyName == "pO2Target")
            {
                if (conditionalButtonpO2 != null)
                {
                    if (e.PropertyName == "pO2Value" || e.PropertyName == "pO2Target")
                    {
                        // pO2Value veya pO2Target değiştiğinde, değerleri karşılaştır ve butonun rengini ayarla
                        double pO2Value = Properties.Settings.Default.pO2Value;
                        double pO2Target = Properties.Settings.Default.pO2Target;
                        double difference = Math.Abs(pO2Value - pO2Target);

                        if (difference < 1)
                        {
                            // Değerler arasındaki fark 1'den az ise butonun arka planını yeşil yap
                            conditionalButtonpO2.Background = new SolidColorBrush(Colors.Green);
                            Properties.Settings.Default.pO2ConditionalButton = 2; // Yeşil için 2
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            // Değerler farklıysa butonun arka planını sarı yap
                            conditionalButtonpO2.Background = new SolidColorBrush(Colors.Yellow);
                            Properties.Settings.Default.pO2ConditionalButton = 1; // Sarı için 1
                            Properties.Settings.Default.Save();
                        }
                    }
                    else // pO2ConditionalButton değiştiğinde
                    {
                        switch (Properties.Settings.Default.pO2ConditionalButton)
                        {
                            case 0: // Kırmızı
                                conditionalButtonpO2.Background = new SolidColorBrush(Colors.Red);
                                break;
                            case 1: // Sarı
                                conditionalButtonpO2.Background = new SolidColorBrush(Colors.Yellow);
                                break;
                            case 2: // Yeşil
                                conditionalButtonpO2.Background = new SolidColorBrush(Colors.Green);
                                break;
                        }
                    }
                }
            }

            // FoamLevel değiştiyse foam durumunu güncelle
            if (e.PropertyName == "FoamLevel")
            {
                UpdateFoamLevelStatus();
            }
        }


        private void ellipse4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Önce Properties.Settings.Default'tan cascade değerini kontrol edin
            if (Properties.Settings.Default.EditpO2Cascade == 0)
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("pO2 cascade selection is required. Please go to EditpO2 settings and select a cascade option.",
                              "Configuration Required",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return; // Ellipse4'ün durumunu değiştirmeden fonksiyonu sonlandır
            }
            else
            {
                // Canvas'ı bul
                Canvas parentCanvas = ellipse4.Parent as Canvas;
                if (parentCanvas == null) return;

                double canvasWidth = parentCanvas.ActualWidth;
                double ellipseWidth = ellipse4.ActualWidth;
                double maxRight = canvasWidth - ellipseWidth - 12; // 12 = 6 (sol boşluk) + 6 (sağ boşluk)
                double targetLeft = Canvas.GetLeft(ellipse4) == 6 ? maxRight : 6; // Yuvarlağın hedef pozisyonu

                DoubleAnimation animation = new DoubleAnimation
                {
                    To = targetLeft,
                    Duration = TimeSpan.FromSeconds(0.5),
                    FillBehavior = FillBehavior.Stop
                };

                animation.Completed += (s, a) =>
                {
                    Canvas.SetLeft(ellipse4, targetLeft); // Animasyon tamamlandığında yuvarlağın pozisyonunu güncelle

                    if (targetLeft == maxRight)
                    {
                        ellipse4.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101")); // Sağdaysa kırmızı yap
                                                                                                                   // Ellipse açıldığında pO2Ellipse'i 1 yap
                        Properties.Settings.Default.pO2Ellipse = 1;
                    }
                    else
                    {
                        ellipse4.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF")); // Soldaysa gri yap
                                                                                                                   // Ellipse kapandığında pO2Ellipse'i 0 yap
                        Properties.Settings.Default.pO2Ellipse = 0;
                    }

                    // Ayarları kaydet
                    Properties.Settings.Default.Save();

                    // Ellipse'in rengi değiştikten sonra butonun görünürlüğünü kontrol et
                    CheckEllipsePositionAndSetButtonVisibility(ellipse4, conditionalButtonpO2);

                    // FavouritesControl'daki ilgili ellipse'i güncelle
                    mainWindow.favouritesControl.UpdateEllipsePosition(ellipse4.Name, targetLeft);
                    // FavouritesControl'daki ilgili conditionalButton'ı güncelle
                    mainWindow.favouritesControl.UpdateConditionalButtonVisibility(ellipse4.Name, targetLeft);
                };

                ellipse4.BeginAnimation(Canvas.LeftProperty, animation);

            }
            // Canvas'ı bul
        }

        //CONDİTİONAL BUTTON OALYLARI
        public void CheckEllipsePositionAndSetButtonVisibility(Ellipse ellipse, Button button)
        {
            // Ellipse'in parent'ını Canvas olarak al
            Canvas parentCanvas = ellipse.Parent as Canvas;
            if (parentCanvas == null) return;

            double canvasWidth = parentCanvas.ActualWidth; // Canvas'ın gerçek genişliğini kullan
            double ellipseRightPosition = Canvas.GetLeft(ellipse) + ellipse.Width; // Ellipse'in sağ kenarının konumu

            // Eğer bu conditional Button pO2 için ise özel kontrol uygula
            if (button == conditionalButtonpO2)
            {
                // Sadece pO2Ellipse == 1 ve StartButton == 1 ise görünür yap
                if (Properties.Settings.Default.pO2Ellipse == 1 &&
                    Properties.Settings.Default.StartButton == 1 &&
                    Properties.Settings.Default.pO2ConditionalButtonVisibility == 1)
                {
                    button.Visibility = Visibility.Visible;
                }
                else
                {
                    button.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                // Diğer tüm butonlar için normal kontrolü uygula
                if (ellipseRightPosition > canvasWidth / 2 && mainWindow.FirstStartButton.Visibility == Visibility.Collapsed)
                {
                    button.Visibility = Visibility.Visible;
                }
                else
                {
                    button.Visibility = Visibility.Collapsed;
                }
            }
        }

        //TEXBOX AKTARMA VE ÇEKME MAKARASI
        //TEXBOX AKTARMA VE ÇEKME MAKARASI
        //TEXBOX AKTARMA VE ÇEKME MAKARASI
        //TEXBOX AKTARMA VE ÇEKME MAKARASI
        //TEXBOX AKTARMA VE ÇEKME MAKARASI
        //TEXBOX AKTARMA VE ÇEKME MAKARASI
        //TEXBOX AKTARMA VE ÇEKME MAKARASI
        // Add these methods to your MainControl class

        // Method to load TextBox values from Settings
        private void LoadTextBoxValues()
        {
            try
            {
                // Load values from Settings.settings for each TextBox
                if (TemperatureTarget != null)
                    TemperatureTarget.Text = Properties.Settings.Default.TemperatureTarget.ToString();

                if (StirrerTarget != null)
                    StirrerTarget.Text = Properties.Settings.Default.StirrerTarget.ToString();

                if (pHTarget != null)
                    pHTarget.Text = Properties.Settings.Default.pHTarget.ToString();

                if (pO2Target != null)
                    pO2Target.Text = Properties.Settings.Default.pO2Target.ToString();

                if (RedoxTarget != null)
                    RedoxTarget.Text = Properties.Settings.Default.RedoxTarget.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading text box values: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Method to save a TextBox value to Settings.settings
        private void SaveTextBoxValue(TextBox textBox)
        {
            try
            {
                if (textBox == null) return;

                // Handle different TextBoxes
                if (textBox == pO2Target)
                {
                    if (double.TryParse(textBox.Text, out double value))
                    {
                        Properties.Settings.Default.pO2Target = value;
                        Properties.Settings.Default.Save();
                    }
                }
                else if (textBox == TemperatureTarget)
                {
                    if (double.TryParse(textBox.Text, out double value))
                    {
                        Properties.Settings.Default.TemperatureTarget = value;
                        Properties.Settings.Default.Save();
                    }
                }
                else if (textBox == StirrerTarget)
                {
                    if (double.TryParse(textBox.Text, out double value))
                    {
                        Properties.Settings.Default.StirrerTarget = value;
                        Properties.Settings.Default.Save();
                    }
                }
                else if (textBox == pHTarget)
                {
                    if (double.TryParse(textBox.Text, out double value))
                    {
                        Properties.Settings.Default.pHTarget = value;
                        Properties.Settings.Default.Save();
                    }
                }
                else if (textBox == RedoxTarget)
                {
                    if (double.TryParse(textBox.Text, out double value))
                    {
                        Properties.Settings.Default.RedoxTarget = value;
                        Properties.Settings.Default.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving text box value: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Register TextChanged event for all TextBoxes
        private void RegisterTextChangedEvents()
        {
            if (pO2Target != null)
                pO2Target.TextChanged += TextBox_TextChanged;

            if (TemperatureTarget != null)
                TemperatureTarget.TextChanged += TextBox_TextChanged;

            if (StirrerTarget != null)
                StirrerTarget.TextChanged += TextBox_TextChanged;

            if (pHTarget != null)
                pHTarget.TextChanged += TextBox_TextChanged;

            if (RedoxTarget != null)
                RedoxTarget.TextChanged += TextBox_TextChanged;
        }

        // TextChanged event handler
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                SaveTextBoxValue(textBox);
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

                        // Save the value to Settings.settings
                        SaveTextBoxValue(activeTextBox);
                    }
                    else
                    {
                        KeypadPopup.IsOpen = false; // Hata durumunda KeyPad'i tekrar aç

                        MessageBox.Show($"Please enter a value between {min} and {max}.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        //CONDİTİONALBUTTON OLAYLARI
        //CONDİTİONALBUTTON OLAYLARI
        //CONDİTİONALBUTTON OLAYLARI
        //CONDİTİONALBUTTON OLAYLARI
        //CONDİTİONALBUTTON OLAYLARI
        //CONDİTİONALBUTTON OLAYLARI
        //CONDİTİONALBUTTON OLAYLARI
        //CONDİTİONALBUTTON OLAYLARI

        private void conditionalButtonpO2_Click(object sender, RoutedEventArgs e)
        {
            // Tıklanan butonu belirle
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Butonun Tag'ine pO2 Grid'ini atayın
                clickedButton.Tag = pO2;

                // Butonu kırmızı yap (varsayılan renk)
                clickedButton.Background = new SolidColorBrush(Colors.Red);
                Properties.Settings.Default.pO2ConditionalButton = 0; // Kırmızı için 0
                Properties.Settings.Default.Save();
            }

            // Zamanlayıcıyı başlat veya devam ettir
            comparisonTimer.Start();
        }

        // ComparisonTimer_Tick metodunu güncelleyin
        private void ComparisonTimer_Tick(object sender, EventArgs e)
        {
            // Tüm butonları kontrol et
            foreach (Button button in FindVisualChildren<Button>(this))
            {
                if (button.Tag is Grid parentGrid)
                {
                    // Özel olarak conditionalButtonpO2 düğmesini kontrol et
                    if (button == conditionalButtonpO2)
                    {
                        // pO2Value ve pO2Target değerlerini Settings.settings'den al
                        double pO2Value = Properties.Settings.Default.pO2Value;
                        double pO2Target = Properties.Settings.Default.pO2Target;

                        // Değerleri karşılaştır
                        double difference = Math.Abs(pO2Value - pO2Target);

                        if (difference < 1)
                        {
                            // Değerler arasındaki fark 1'den az ise butonun arka planını yeşil yap
                            button.Background = new SolidColorBrush(Colors.Green);
                            Properties.Settings.Default.pO2ConditionalButton = 2; // Yeşil için 2
                        }
                        else
                        {
                            // Değerler farklıysa butonun arka planını sarı yap
                            button.Background = new SolidColorBrush(Colors.Yellow);
                            Properties.Settings.Default.pO2ConditionalButton = 1; // Sarı için 1
                        }

                        // Settings'i kaydet
                        Properties.Settings.Default.Save();
                    }
                    else
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
            }
        }
    }
}
