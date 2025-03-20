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

            // conditionalButtonTemperature düğmesinin başlangıç görünürlüğünü ve rengini ayarla
            if (Properties.Settings.Default.TemperatureEllipse == 1 &&
                Properties.Settings.Default.StartButton == 1 &&
                Properties.Settings.Default.TemperatureConditionalButtonVisibility == 1)
            {
                conditionalButtonTemperature.Visibility = Visibility.Visible;

                // Butonun rengini Settings'e göre ayarla
                switch (Properties.Settings.Default.TemperatureConditionalButton)
                {
                    case 0: // Kırmızı
                        conditionalButtonTemperature.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1: // Sarı
                        conditionalButtonTemperature.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2: // Yeşil
                        conditionalButtonTemperature.Background = new SolidColorBrush(Colors.Green);
                        break;
                    default:
                        conditionalButtonTemperature.Background = new SolidColorBrush(Colors.Red);
                        break;
                }
            }
            else
            {
                conditionalButtonTemperature.Visibility = Visibility.Collapsed;
            }

            // conditionalButtonStirrer düğmesinin başlangıç görünürlüğünü ve rengini ayarla
            if (Properties.Settings.Default.StirrerEllipse == 1 &&
                Properties.Settings.Default.StartButton == 1 &&
                Properties.Settings.Default.StirrerConditionalButtonVisibility == 1)
            {
                conditionalButtonStirrer.Visibility = Visibility.Visible;

                // Butonun rengini Settings'e göre ayarla
                switch (Properties.Settings.Default.StirrerConditionalButton)
                {
                    case 0: // Kırmızı
                        conditionalButtonStirrer.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1: // Sarı
                        conditionalButtonStirrer.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2: // Yeşil
                        conditionalButtonStirrer.Background = new SolidColorBrush(Colors.Green);
                        break;
                    default:
                        conditionalButtonStirrer.Background = new SolidColorBrush(Colors.Red);
                        break;
                }
            }
            else
            {
                conditionalButtonStirrer.Visibility = Visibility.Collapsed;
            }

            // conditionalButtonpH düğmesinin başlangıç görünürlüğünü ve rengini ayarla
            if (Properties.Settings.Default.pHEllipse == 1 &&
                Properties.Settings.Default.StartButton == 1 &&
                Properties.Settings.Default.pHConditionalButtonVisibility == 1)
            {
                conditionalButtonpH.Visibility = Visibility.Visible;

                // Butonun rengini Settings'e göre ayarla
                switch (Properties.Settings.Default.pHConditionalButton)
                {
                    case 0: // Kırmızı
                        conditionalButtonpH.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1: // Sarı
                        conditionalButtonpH.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2: // Yeşil
                        conditionalButtonpH.Background = new SolidColorBrush(Colors.Green);
                        break;
                    default:
                        conditionalButtonpH.Background = new SolidColorBrush(Colors.Red);
                        break;
                }
            }
            else
            {
                conditionalButtonpH.Visibility = Visibility.Collapsed;
            }

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

            // conditionalButtonFoam düğmesinin başlangıç görünürlüğünü ve rengini ayarla
            if (Properties.Settings.Default.FoamEllipse == 1 &&
                Properties.Settings.Default.StartButton == 1 &&
                Properties.Settings.Default.FoamConditionalButtonVisibility == 1)
            {
                conditionalButtonFoam.Visibility = Visibility.Visible;

                // Butonun rengini Settings'e göre ayarla
                switch (Properties.Settings.Default.FoamConditionalButton)
                {
                    case 0: // Kırmızı
                        conditionalButtonFoam.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1: // Sarı
                        conditionalButtonFoam.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2: // Yeşil
                        conditionalButtonFoam.Background = new SolidColorBrush(Colors.Green);
                        break;
                    default:
                        conditionalButtonFoam.Background = new SolidColorBrush(Colors.Red);
                        break;
                }
            }
            else
            {
                conditionalButtonFoam.Visibility = Visibility.Collapsed;
            }

            // conditionalButtonRedox düğmesinin başlangıç görünürlüğünü ve rengini ayarla
            if (Properties.Settings.Default.RedoxEllipse == 1 &&
                Properties.Settings.Default.StartButton == 1 &&
                Properties.Settings.Default.RedoxConditionalButtonVisibility == 1)
            {
                conditionalButtonRedox.Visibility = Visibility.Visible;

                // Butonun rengini Settings'e göre ayarla
                switch (Properties.Settings.Default.RedoxConditionalButton)
                {
                    case 0: // Kırmızı
                        conditionalButtonRedox.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1: // Sarı
                        conditionalButtonRedox.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2: // Yeşil
                        conditionalButtonRedox.Background = new SolidColorBrush(Colors.Green);
                        break;
                    default:
                        conditionalButtonRedox.Background = new SolidColorBrush(Colors.Red);
                        break;
                }
            }
            else
            {
                conditionalButtonRedox.Visibility = Visibility.Collapsed;
            }


            // Click event handler'ı conditionalButtonpO2'ye bağla
            conditionalButtonTemperature.Click -= conditionalButtonTemperature_Click; // Önce eski bağlantıyı kaldır
            conditionalButtonTemperature.Click += conditionalButtonTemperature_Click; // Yeni bağlantıyı ekle

            conditionalButtonStirrer.Click -= conditionalButtonStirrer_Click; // Önce eski bağlantıyı kaldır
            conditionalButtonStirrer.Click += conditionalButtonStirrer_Click; // Yeni bağlantıyı ekle

            conditionalButtonpH.Click -= conditionalButtonpH_Click; // Önce eski bağlantıyı kaldır
            conditionalButtonpH.Click += conditionalButtonpH_Click; // Yeni bağlantıyı ekle

            conditionalButtonpO2.Click -= conditionalButtonpO2_Click; // Önce eski bağlantıyı kaldır
            conditionalButtonpO2.Click += conditionalButtonpO2_Click; // Yeni bağlantıyı ekle

            conditionalButtonFoam.Click -= conditionalButtonFoam_Click; // Önce eski bağlantıyı kaldır
            conditionalButtonFoam.Click += conditionalButtonFoam_Click; // Yeni bağlantıyı ekle

            conditionalButtonRedox.Click -= conditionalButtonRedox_Click; // Önce eski bağlantıyı kaldır
            conditionalButtonRedox.Click += conditionalButtonRedox_Click; // Yeni bağlantıyı ekle

            // Load saved text box values
            LoadTextBoxValues();

            // Register TextChanged events
            RegisterTextChangedEvents();

            ellipse1.MouseLeftButtonDown += ellipse1_MouseDown;
            ellipse2.MouseLeftButtonDown += ellipse2_MouseDown;
            ellipse3.MouseLeftButtonDown += ellipse3_MouseDown;
            // ellipse4 için özel event handler kullan
            ellipse4.MouseLeftButtonDown += ellipse4_MouseDown;
            //ellipse5.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            //ellipse6.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            //ellipse7.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            //ellipse8.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse9.MouseLeftButtonDown += ellipse9_MouseDown;
            ellipse19.MouseLeftButtonDown += ellipse19_MouseDown;
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

        //BELKİ FAVOURİTESE GEÇİREBİLİRİZ BUNU
        //BELKİ FAVOURİTESE GEÇİREBİLİRİZ BUNU
        //BELKİ FAVOURİTESE GEÇİREBİLİRİZ BUNU
        //BELKİ FAVOURİTESE GEÇİREBİLİRİZ BUNU
        //BELKİ FAVOURİTESE GEÇİREBİLİRİZ BUNU
        //BELKİ FAVOURİTESE GEÇİREBİLİRİZ BUNU
        //BELKİ FAVOURİTESE GEÇİREBİLİRİZ BUNU

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

                    //mainWindow.favouritesControl.UpdateEllipsePosition(clickedEllipse.Name, targetLeft);
                    //mainWindow.favouritesControl.UpdateConditionalButtonVisibility(clickedEllipse.Name, targetLeft);
                };

                clickedEllipse.BeginAnimation(Canvas.LeftProperty, animation);
            }
        }

        private Button GetConditionalButton(Ellipse ellipse)
        {
            return ellipse.Name switch
            {
                "ellipse1" => conditionalButtonTemperature,
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

            // Set ellipse1 position based on TemperatureEllipse setting
            Canvas canvas1 = ellipse1.Parent as Canvas;
            if (canvas1 != null)
            {
                double canvasWidth = canvas1.ActualWidth;
                double ellipseWidth = ellipse1.ActualWidth;
                double maxRight = canvasWidth - ellipseWidth - 12;

                // If TemperatureEllipse is 1, position ellipse to the right
                if (Properties.Settings.Default.TemperatureEllipse == 1)
                {
                    Canvas.SetLeft(ellipse1, maxRight);
                    ellipse1.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101"));
                }
                else
                {
                    Canvas.SetLeft(ellipse1, 6);
                    ellipse1.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF"));
                }
            }

            // Butonun görünürlüğünü ve rengini ayarla
            if (Properties.Settings.Default.TemperatureEllipse == 1 &&
                Properties.Settings.Default.StartButton == 1 &&
                Properties.Settings.Default.TemperatureConditionalButtonVisibility == 1)
            {
                conditionalButtonTemperature.Visibility = Visibility.Visible;

                // Butonun rengini Settings'deki değere göre ayarla
                switch (Properties.Settings.Default.TemperatureConditionalButton)
                {
                    case 0: // Kırmızı
                        conditionalButtonTemperature.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1: // Sarı
                        conditionalButtonTemperature.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2: // Yeşil
                        conditionalButtonTemperature.Background = new SolidColorBrush(Colors.Green);
                        break;
                    default:
                        conditionalButtonTemperature.Background = new SolidColorBrush(Colors.Red);
                        break;
                }
            }
            else
            {
                conditionalButtonTemperature.Visibility = Visibility.Collapsed;
            }
            // Click event handler'ını ekle
            conditionalButtonTemperature.Click -= conditionalButtonTemperature_Click; // Önce eski bağlantıyı kaldır
            conditionalButtonTemperature.Click += conditionalButtonTemperature_Click; // Yeni bağlantıyı ekle

            // Set ellipse2 position based on StirrerEllipse setting
            Canvas canvas2 = ellipse2.Parent as Canvas;
            if (canvas2 != null)
            {
                double canvasWidth = canvas2.ActualWidth;
                double ellipseWidth = ellipse2.ActualWidth;
                double maxRight = canvasWidth - ellipseWidth - 12;

                // If StirrerEllipse is 1, position ellipse to the right
                if (Properties.Settings.Default.StirrerEllipse == 1)
                {
                    Canvas.SetLeft(ellipse2, maxRight);
                    ellipse2.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101"));
                }
                else
                {
                    Canvas.SetLeft(ellipse2, 6);
                    ellipse2.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF"));
                }
            }

            // Butonun görünürlüğünü ve rengini ayarla
            if (Properties.Settings.Default.StirrerEllipse == 1 &&
                Properties.Settings.Default.StartButton == 1 &&
                Properties.Settings.Default.StirrerConditionalButtonVisibility == 1)
            {
                conditionalButtonStirrer.Visibility = Visibility.Visible;

                // Butonun rengini Settings'deki değere göre ayarla
                switch (Properties.Settings.Default.StirrerConditionalButton)
                {
                    case 0: // Kırmızı
                        conditionalButtonStirrer.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1: // Sarı
                        conditionalButtonStirrer.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2: // Yeşil
                        conditionalButtonStirrer.Background = new SolidColorBrush(Colors.Green);
                        break;
                    default:
                        conditionalButtonStirrer.Background = new SolidColorBrush(Colors.Red);
                        break;
                }
            }
            else
            {
                conditionalButtonStirrer.Visibility = Visibility.Collapsed;
            }
            // Click event handler'ını ekle
            conditionalButtonStirrer.Click -= conditionalButtonStirrer_Click; // Önce eski bağlantıyı kaldır
            conditionalButtonStirrer.Click += conditionalButtonStirrer_Click; // Yeni bağlantıyı ekle

            // Set ellipse3 position based on pHEllipse setting
            Canvas canvas3 = ellipse3.Parent as Canvas;
            if (canvas3 != null)
            {
                double canvasWidth = canvas3.ActualWidth;
                double ellipseWidth = ellipse3.ActualWidth;
                double maxRight = canvasWidth - ellipseWidth - 12;

                // If pHEllipse is 1, position ellipse to the right
                if (Properties.Settings.Default.pHEllipse == 1)
                {
                    Canvas.SetLeft(ellipse3, maxRight);
                    ellipse3.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101"));
                }
                else
                {
                    Canvas.SetLeft(ellipse3, 6);
                    ellipse3.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF"));
                }
            }

            // Butonun görünürlüğünü ve rengini ayarla
            if (Properties.Settings.Default.pHEllipse == 1 &&
                Properties.Settings.Default.StartButton == 1 &&
                Properties.Settings.Default.pHConditionalButtonVisibility == 1)
            {
                conditionalButtonpH.Visibility = Visibility.Visible;

                // Butonun rengini Settings'deki değere göre ayarla
                switch (Properties.Settings.Default.pHConditionalButton)
                {
                    case 0: // Kırmızı
                        conditionalButtonpH.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1: // Sarı
                        conditionalButtonpH.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2: // Yeşil
                        conditionalButtonpH.Background = new SolidColorBrush(Colors.Green);
                        break;
                    default:
                        conditionalButtonpH.Background = new SolidColorBrush(Colors.Red);
                        break;
                }
            }
            else
            {
                conditionalButtonpH.Visibility = Visibility.Collapsed;
            }
            // Click event handler'ını ekle
            conditionalButtonpH.Click -= conditionalButtonpH_Click; // Önce eski bağlantıyı kaldır
            conditionalButtonpH.Click += conditionalButtonpH_Click; // Yeni bağlantıyı ekle

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

            // Set ellipse9 position based on FoamEllipse setting
            Canvas canvas9 = ellipse9.Parent as Canvas;
            if (canvas9 != null)
            {
                double canvasWidth = canvas9.ActualWidth;
                double ellipseWidth = ellipse9.ActualWidth;
                double maxRight = canvasWidth - ellipseWidth - 12;

                // If FoamEllipse is 1, position ellipse to the right
                if (Properties.Settings.Default.FoamEllipse == 1)
                {
                    Canvas.SetLeft(ellipse9, maxRight);
                    ellipse9.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101"));
                }
                else
                {
                    Canvas.SetLeft(ellipse9, 6);
                    ellipse9.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF"));
                }
            }

            // Butonun görünürlüğünü ve rengini ayarla
            if (Properties.Settings.Default.FoamEllipse == 1 &&
                Properties.Settings.Default.StartButton == 1 &&
                Properties.Settings.Default.FoamConditionalButtonVisibility == 1)
            {
                conditionalButtonFoam.Visibility = Visibility.Visible;

                // Butonun rengini Settings'deki değere göre ayarla
                switch (Properties.Settings.Default.FoamConditionalButton)
                {
                    case 0: // Kırmızı
                        conditionalButtonFoam.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1: // Sarı
                        conditionalButtonFoam.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2: // Yeşil
                        conditionalButtonFoam.Background = new SolidColorBrush(Colors.Green);
                        break;
                    default:
                        conditionalButtonFoam.Background = new SolidColorBrush(Colors.Red);
                        break;
                }
            }
            else
            {
                conditionalButtonFoam.Visibility = Visibility.Collapsed;
            }
            // Click event handler'ını ekle
            conditionalButtonFoam.Click -= conditionalButtonFoam_Click; // Önce eski bağlantıyı kaldır
            conditionalButtonFoam.Click += conditionalButtonFoam_Click; // Yeni bağlantıyı ekle

            // Set ellipse19 position based on RedoxEllipse setting
            Canvas canvas19 = ellipse19.Parent as Canvas;
            if (canvas19 != null)
            {
                double canvasWidth = canvas19.ActualWidth;
                double ellipseWidth = ellipse19.ActualWidth;
                double maxRight = canvasWidth - ellipseWidth - 12;

                // If RedoxEllipse is 1, position ellipse to the right
                if (Properties.Settings.Default.RedoxEllipse == 1)
                {
                    Canvas.SetLeft(ellipse19, maxRight);
                    ellipse19.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101"));
                }
                else
                {
                    Canvas.SetLeft(ellipse19, 6);
                    ellipse19.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF"));
                }
            }

            // Butonun görünürlüğünü ve rengini ayarla
            if (Properties.Settings.Default.RedoxEllipse == 1 &&
                Properties.Settings.Default.StartButton == 1 &&
                Properties.Settings.Default.RedoxConditionalButtonVisibility == 1)
            {
                conditionalButtonRedox.Visibility = Visibility.Visible;

                // Butonun rengini Settings'deki değere göre ayarla
                switch (Properties.Settings.Default.RedoxConditionalButton)
                {
                    case 0: // Kırmızı
                        conditionalButtonRedox.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1: // Sarı
                        conditionalButtonRedox.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2: // Yeşil
                        conditionalButtonRedox.Background = new SolidColorBrush(Colors.Green);
                        break;
                    default:
                        conditionalButtonRedox.Background = new SolidColorBrush(Colors.Red);
                        break;
                }
            }
            else
            {
                conditionalButtonRedox.Visibility = Visibility.Collapsed;
            }
            // Click event handler'ını ekle
            conditionalButtonRedox.Click -= conditionalButtonRedox_Click; // Önce eski bağlantıyı kaldır
            conditionalButtonRedox.Click += conditionalButtonRedox_Click; // Yeni bağlantıyı ekle










            CheckEllipsePositionAndSetButtonVisibility(ellipse1, conditionalButtonTemperature);
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
            // When TemperatureTarget changes in Settings, update the TextBox
            if (e.PropertyName == "TemperatureTarget" && TemperatureTarget != null)
            {
                // Only update if the TextBox doesn't have focus to avoid triggering a text changed event loop
                if (!TemperatureTarget.IsFocused)
                {
                    TemperatureTarget.Text = Properties.Settings.Default.TemperatureTarget.ToString();
                }
            }
            // When StirrerTarget changes in Settings, update the TextBox
            if (e.PropertyName == "StirrerTarget" && StirrerTarget != null)
            {
                // Only update if the TextBox doesn't have focus to avoid triggering a text changed event loop
                if (!StirrerTarget.IsFocused)
                {
                    StirrerTarget.Text = Properties.Settings.Default.StirrerTarget.ToString();
                }
            }
            // When pHTarget changes in Settings, update the TextBox
            if (e.PropertyName == "pHTarget" && pHTarget != null)
            {
                // Only update if the TextBox doesn't have focus to avoid triggering a text changed event loop
                if (!pHTarget.IsFocused)
                {
                    pHTarget.Text = Properties.Settings.Default.pHTarget.ToString();
                }
            }
            // When pO2Target changes in Settings, update the TextBox
            if (e.PropertyName == "pO2Target" && pO2Target != null)
            {
                // Only update if the TextBox doesn't have focus to avoid triggering a text changed event loop
                if (!pO2Target.IsFocused)
                {
                    pO2Target.Text = Properties.Settings.Default.pO2Target.ToString();
                }
            }
            // When FoamTarget changes in Settings, update the TextBox
            //if (e.PropertyName == "FoamTarget" && FoamTarget != null)
            //{
            //    // Only update if the TextBox doesn't have focus to avoid triggering a text changed event loop
            //    if (!pO2Target.IsFocused)
            //    {
            //        pO2Target.Text = Properties.Settings.Default.pO2Target.ToString();
            //    }
            //}
            // When RedoxTarget changes in Settings, update the TextBox
            if (e.PropertyName == "RedoxTarget" && RedoxTarget != null)
            {
                // Only update if the TextBox doesn't have focus to avoid triggering a text changed event loop
                if (!RedoxTarget.IsFocused)
                {
                    RedoxTarget.Text = Properties.Settings.Default.RedoxTarget.ToString();
                }
            }

            // Eğer ilgili ayarlar değiştiyse border görünürlüklerini güncelle
            if (e.PropertyName == "StirrerTargetBorder" ||
                e.PropertyName == "HideGas1Border" ||
                e.PropertyName == "pO2SelectedCascade" ||
                e.PropertyName == "TemperatureEllipse" ||
                e.PropertyName == "StirrerEllipse" ||
                e.PropertyName == "pHEllipse" ||
                e.PropertyName == "pO2Ellipse" ||
                e.PropertyName == "FoamEllipse" ||
                e.PropertyName == "RedoxEllipse" ||
                e.PropertyName == "StartButton" ||
                e.PropertyName == "FoamValue" ||
                e.PropertyName == "StartButton" ||
                e.PropertyName == "TemperatureConditionalButtonVisibility" ||
                e.PropertyName == "StirrerConditionalButtonVisibility" ||
                e.PropertyName == "pHConditionalButtonVisibility" ||
                e.PropertyName == "pO2ConditionalButtonVisibility" ||
                e.PropertyName == "FoamConditionalButtonVisibility" ||
                e.PropertyName == "pO2ConditionalButtonVisibility")  // Bu satırları ekledik
            {
                CheckComparisonTimer();
                UpdateBorderVisibilities();
                CheckEllipsePositionAndSetButtonVisibility(ellipse1, conditionalButtonTemperature);
                CheckEllipsePositionAndSetButtonVisibility(ellipse2, conditionalButtonStirrer);
                CheckEllipsePositionAndSetButtonVisibility(ellipse3, conditionalButtonpH);
                CheckEllipsePositionAndSetButtonVisibility(ellipse4, conditionalButtonpO2);
                CheckEllipsePositionAndSetButtonVisibility(ellipse9, conditionalButtonFoam);
                CheckEllipsePositionAndSetButtonVisibility(ellipse19, conditionalButtonRedox);

                // If TemperatureEllipse, StartButton or TemperatureConditionalButtonVisibility changed, update conditionalButtonTemperature visibility
                if (e.PropertyName == "TemperatureEllipse" || e.PropertyName == "StartButton" || e.PropertyName == "TemperatureConditionalButtonVisibility")
                {
                    // conditionalButtonTemperature düğmesinin görünürlüğünü güncelle
                    if (Properties.Settings.Default.TemperatureEllipse == 1 &&
                        Properties.Settings.Default.StartButton == 1 &&
                        Properties.Settings.Default.TemperatureConditionalButtonVisibility == 1)
                    {
                        conditionalButtonTemperature.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        conditionalButtonTemperature.Visibility = Visibility.Collapsed;
                    }

                    Canvas canvas1 = ellipse1.Parent as Canvas;
                    if (canvas1 != null && e.PropertyName == "TemperatureEllipse")
                    {
                        double canvasWidth = canvas1.ActualWidth;
                        double ellipseWidth = ellipse1.ActualWidth;
                        double maxRight = canvasWidth - ellipseWidth - 12;

                        double targetLeft = Properties.Settings.Default.TemperatureEllipse == 1 ? maxRight : 6;
                        Canvas.SetLeft(ellipse1, targetLeft);

                        if (Properties.Settings.Default.TemperatureEllipse == 1)
                        {
                            ellipse1.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101"));
                        }
                        else
                        {
                            ellipse1.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF"));
                        }
                    }
                }

                // If StirrerEllipse, StartButton or StirrerConditionalButtonVisibility changed, update conditionalButtonStirrer visibility
                if (e.PropertyName == "StirrerEllipse" || e.PropertyName == "StartButton" || e.PropertyName == "StirrerConditionalButtonVisibility")
                {
                    // conditionalButtonStirrer düğmesinin görünürlüğünü güncelle
                    if (Properties.Settings.Default.StirrerEllipse == 1 &&
                        Properties.Settings.Default.StartButton == 1 &&
                        Properties.Settings.Default.StirrerConditionalButtonVisibility == 1)
                    {
                        conditionalButtonStirrer.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        conditionalButtonStirrer.Visibility = Visibility.Collapsed;
                    }

                    Canvas canvas2 = ellipse2.Parent as Canvas;
                    if (canvas2 != null && e.PropertyName == "StirrerEllipse")
                    {
                        double canvasWidth = canvas2.ActualWidth;
                        double ellipseWidth = ellipse2.ActualWidth;
                        double maxRight = canvasWidth - ellipseWidth - 12;

                        double targetLeft = Properties.Settings.Default.StirrerEllipse == 1 ? maxRight : 6;
                        Canvas.SetLeft(ellipse2, targetLeft);

                        if (Properties.Settings.Default.StirrerEllipse == 1)
                        {
                            ellipse2.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101"));
                        }
                        else
                        {
                            ellipse2.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF"));
                        }
                    }
                }

                // If pHEllipse, StartButton or pHConditionalButtonVisibility changed, update conditionalButtonpH visibility
                if (e.PropertyName == "pHEllipse" || e.PropertyName == "StartButton" || e.PropertyName == "pHConditionalButtonVisibility")
                {
                    // conditionalButtonpH düğmesinin görünürlüğünü güncelle
                    if (Properties.Settings.Default.pHEllipse == 1 &&
                        Properties.Settings.Default.StartButton == 1 &&
                        Properties.Settings.Default.pHConditionalButtonVisibility == 1)
                    {
                        conditionalButtonpH.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        conditionalButtonpH.Visibility = Visibility.Collapsed;
                    }

                    Canvas canvas3 = ellipse3.Parent as Canvas;
                    if (canvas3 != null && e.PropertyName == "pHEllipse")
                    {
                        double canvasWidth = canvas3.ActualWidth;
                        double ellipseWidth = ellipse3.ActualWidth;
                        double maxRight = canvasWidth - ellipseWidth - 12;

                        double targetLeft = Properties.Settings.Default.pHEllipse == 1 ? maxRight : 6;
                        Canvas.SetLeft(ellipse3, targetLeft);

                        if (Properties.Settings.Default.pHEllipse == 1)
                        {
                            ellipse3.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101"));
                        }
                        else
                        {
                            ellipse3.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF"));
                        }
                    }
                }

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

                // If FoamEllipse, StartButton or FoamConditionalButtonVisibility changed, update conditionalButtonFoam visibility
                if (e.PropertyName == "FoamEllipse" || e.PropertyName == "StartButton" || e.PropertyName == "FoamConditionalButtonVisibility")
                {
                    // conditionalButtonFoam düğmesinin görünürlüğünü güncelle
                    if (Properties.Settings.Default.FoamEllipse == 1 &&
                        Properties.Settings.Default.StartButton == 1 &&
                        Properties.Settings.Default.FoamConditionalButtonVisibility == 1)
                    {
                        conditionalButtonFoam.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        conditionalButtonFoam.Visibility = Visibility.Collapsed;
                    }

                    Canvas canvas9 = ellipse9.Parent as Canvas;
                    if (canvas9 != null && e.PropertyName == "FoamEllipse")
                    {
                        double canvasWidth = canvas9.ActualWidth;
                        double ellipseWidth = ellipse9.ActualWidth;
                        double maxRight = canvasWidth - ellipseWidth - 12;

                        double targetLeft = Properties.Settings.Default.FoamEllipse == 1 ? maxRight : 6;
                        Canvas.SetLeft(ellipse9, targetLeft);

                        if (Properties.Settings.Default.FoamEllipse == 1)
                        {
                            ellipse9.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101"));
                        }
                        else
                        {
                            ellipse9.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF"));
                        }
                    }
                }

                // If RedoxEllipse, StartButton or RedoxConditionalButtonVisibility changed, update conditionalButtonRedox visibility
                if (e.PropertyName == "RedoxEllipse" || e.PropertyName == "StartButton" || e.PropertyName == "RedoxConditionalButtonVisibility")
                {
                    // conditionalButtonRedox düğmesinin görünürlüğünü güncelle
                    if (Properties.Settings.Default.RedoxEllipse == 1 &&
                        Properties.Settings.Default.StartButton == 1 &&
                        Properties.Settings.Default.RedoxConditionalButtonVisibility == 1)
                    {
                        conditionalButtonRedox.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        conditionalButtonRedox.Visibility = Visibility.Collapsed;
                    }

                    Canvas canvas19 = ellipse19.Parent as Canvas;
                    if (canvas19 != null && e.PropertyName == "RedoxEllipse")
                    {
                        double canvasWidth = canvas19.ActualWidth;
                        double ellipseWidth = ellipse19.ActualWidth;
                        double maxRight = canvasWidth - ellipseWidth - 12;

                        double targetLeft = Properties.Settings.Default.RedoxEllipse == 1 ? maxRight : 6;
                        Canvas.SetLeft(ellipse19, targetLeft);

                        if (Properties.Settings.Default.RedoxEllipse == 1)
                        {
                            ellipse19.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101"));
                        }
                        else
                        {
                            ellipse19.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF"));
                        }
                    }
                }
            }






            // TemperatureConditionalButton, TemperatureValue veya TemperatureTarget değiştiğinde butonun rengini güncelle
            if (e.PropertyName == "TemperatureConditionalButton" || e.PropertyName == "TemperatureValue" || e.PropertyName == "TemperatureTarget")
            {
                if (conditionalButtonTemperature != null)
                {
                    if (e.PropertyName == "TemperatureValue" || e.PropertyName == "TemperatureTarget")
                    {
                        // TemperatureValue veya TemperatureTarget değiştiğinde, değerleri karşılaştır ve butonun rengini ayarla
                        double TemperatureValue = Properties.Settings.Default.TemperatureValue;
                        double TemperatureTarget = Properties.Settings.Default.TemperatureTarget;
                        double difference = Math.Abs(TemperatureValue - TemperatureTarget);

                        if (difference < 1)
                        {
                            // Değerler arasındaki fark 1'den az ise butonun arka planını yeşil yap
                            conditionalButtonTemperature.Background = new SolidColorBrush(Colors.Green);
                            Properties.Settings.Default.TemperatureConditionalButton = 2; // Yeşil için 2
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            // Değerler farklıysa butonun arka planını sarı yap
                            conditionalButtonTemperature.Background = new SolidColorBrush(Colors.Yellow);
                            Properties.Settings.Default.TemperatureConditionalButton = 1; // Sarı için 1
                            Properties.Settings.Default.Save();
                        }
                    }
                    else // TemperatureConditionalButton değiştiğinde
                    {
                        switch (Properties.Settings.Default.TemperatureConditionalButton)
                        {
                            case 0: // Kırmızı
                                conditionalButtonTemperature.Background = new SolidColorBrush(Colors.Red);
                                break;
                            case 1: // Sarı
                                conditionalButtonTemperature.Background = new SolidColorBrush(Colors.Yellow);
                                break;
                            case 2: // Yeşil
                                conditionalButtonTemperature.Background = new SolidColorBrush(Colors.Green);
                                break;
                        }
                    }
                }
            }

            // StirrerConditionalButton, StirrerValue veya StirrerTarget değiştiğinde butonun rengini güncelle
            if (e.PropertyName == "StirrerConditionalButton" || e.PropertyName == "StirrerValue" || e.PropertyName == "StirrerTarget")
            {
                if (conditionalButtonStirrer != null)
                {
                    if (e.PropertyName == "StirrerValue" || e.PropertyName == "StirrerTarget")
                    {
                        // StirrerValue veya StirrerTarget değiştiğinde, değerleri karşılaştır ve butonun rengini ayarla
                        double StirrerValue = Properties.Settings.Default.StirrerValue;
                        double StirrerTarget = Properties.Settings.Default.StirrerTarget;
                        double difference = Math.Abs(StirrerValue - StirrerTarget);

                        if (difference < 1)
                        {
                            // Değerler arasındaki fark 1'den az ise butonun arka planını yeşil yap
                            conditionalButtonStirrer.Background = new SolidColorBrush(Colors.Green);
                            Properties.Settings.Default.StirrerConditionalButton = 2; // Yeşil için 2
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            // Değerler farklıysa butonun arka planını sarı yap
                            conditionalButtonStirrer.Background = new SolidColorBrush(Colors.Yellow);
                            Properties.Settings.Default.StirrerConditionalButton = 1; // Sarı için 1
                            Properties.Settings.Default.Save();
                        }
                    }
                    else // StirrerConditionalButton değiştiğinde
                    {
                        switch (Properties.Settings.Default.StirrerConditionalButton)
                        {
                            case 0: // Kırmızı
                                conditionalButtonStirrer.Background = new SolidColorBrush(Colors.Red);
                                break;
                            case 1: // Sarı
                                conditionalButtonStirrer.Background = new SolidColorBrush(Colors.Yellow);
                                break;
                            case 2: // Yeşil
                                conditionalButtonStirrer.Background = new SolidColorBrush(Colors.Green);
                                break;
                        }
                    }
                }
            }

            // pHConditionalButton, pHValue veya pHTarget değiştiğinde butonun rengini güncelle
            if (e.PropertyName == "pHConditionalButton" || e.PropertyName == "pHValue" || e.PropertyName == "pHTarget")
            {
                if (conditionalButtonpH != null)
                {
                    if (e.PropertyName == "pHValue" || e.PropertyName == "pHTarget")
                    {
                        // pHValue veya pHTarget değiştiğinde, değerleri karşılaştır ve butonun rengini ayarla
                        double pHValue = Properties.Settings.Default.pHValue;
                        double pHTarget = Properties.Settings.Default.pHTarget;
                        double difference = Math.Abs(pHValue - pHTarget);

                        if (difference < 1)
                        {
                            // Değerler arasındaki fark 1'den az ise butonun arka planını yeşil yap
                            conditionalButtonpH.Background = new SolidColorBrush(Colors.Green);
                            Properties.Settings.Default.pHConditionalButton = 2; // Yeşil için 2
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            // Değerler farklıysa butonun arka planını sarı yap
                            conditionalButtonpH.Background = new SolidColorBrush(Colors.Yellow);
                            Properties.Settings.Default.pHConditionalButton = 1; // Sarı için 1
                            Properties.Settings.Default.Save();
                        }
                    }
                    else // pHConditionalButton değiştiğinde
                    {
                        switch (Properties.Settings.Default.pHConditionalButton)
                        {
                            case 0: // Kırmızı
                                conditionalButtonpH.Background = new SolidColorBrush(Colors.Red);
                                break;
                            case 1: // Sarı
                                conditionalButtonpH.Background = new SolidColorBrush(Colors.Yellow);
                                break;
                            case 2: // Yeşil
                                conditionalButtonpH.Background = new SolidColorBrush(Colors.Green);
                                break;
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

            // FoamConditionalButton, FoamValue veya FoamTarget değiştiğinde butonun rengini güncelle
            //if (e.PropertyName == "FoamConditionalButton" || e.PropertyName == "FoamValue" || e.PropertyName == "FoamTarget")
            //{
            //    if (conditionalButtonFoam != null)
            //    {
            //        if (e.PropertyName == "FoamValue" || e.PropertyName == "FoamTarget")
            //        {
            //            // FoamValue veya FoamTarget değiştiğinde, değerleri karşılaştır ve butonun rengini ayarla
            //            double FoamValue = Properties.Settings.Default.FoamValue;
            //            double FoamTarget = Properties.Settings.Default.FoamTarget;
            //            double difference = Math.Abs(FoamValue - FoamTarget);

            //            if (difference < 1)
            //            {
            //                // Değerler arasındaki fark 1'den az ise butonun arka planını yeşil yap
            //                conditionalButtonFoam.Background = new SolidColorBrush(Colors.Green);
            //                Properties.Settings.Default.FoamConditionalButton = 2; // Yeşil için 2
            //                Properties.Settings.Default.Save();
            //            }
            //            else
            //            {
            //                // Değerler farklıysa butonun arka planını sarı yap
            //                conditionalButtonFoam.Background = new SolidColorBrush(Colors.Yellow);
            //                Properties.Settings.Default.FoamConditionalButton = 1; // Sarı için 1
            //                Properties.Settings.Default.Save();
            //            }
            //        }
            //        else // FoamConditionalButton değiştiğinde
            //        {
            //            switch (Properties.Settings.Default.FoamConditionalButton)
            //            {
            //                case 0: // Kırmızı
            //                    conditionalButtonFoam.Background = new SolidColorBrush(Colors.Red);
            //                    break;
            //                case 1: // Sarı
            //                    conditionalButtonFoam.Background = new SolidColorBrush(Colors.Yellow);
            //                    break;
            //                case 2: // Yeşil
            //                    conditionalButtonFoam.Background = new SolidColorBrush(Colors.Green);
            //                    break;
            //            }
            //        }
            //    }
            //}

            // RedoxConditionalButton, RedoxValue veya RedoxTarget değiştiğinde butonun rengini güncelle
            if (e.PropertyName == "RedoxConditionalButton" || e.PropertyName == "RedoxValue" || e.PropertyName == "RedoxTarget")
            {
                if (conditionalButtonRedox != null)
                {
                    if (e.PropertyName == "RedoxValue" || e.PropertyName == "RedoxTarget")
                    {
                        // RedoxValue veya RedoxTarget değiştiğinde, değerleri karşılaştır ve butonun rengini ayarla
                        double RedoxValue = Properties.Settings.Default.RedoxValue;
                        double RedoxTarget = Properties.Settings.Default.RedoxTarget;
                        double difference = Math.Abs(RedoxValue - RedoxTarget);

                        if (difference < 1)
                        {
                            // Değerler arasındaki fark 1'den az ise butonun arka planını yeşil yap
                            conditionalButtonRedox.Background = new SolidColorBrush(Colors.Green);
                            Properties.Settings.Default.RedoxConditionalButton = 2; // Yeşil için 2
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            // Değerler farklıysa butonun arka planını sarı yap
                            conditionalButtonRedox.Background = new SolidColorBrush(Colors.Yellow);
                            Properties.Settings.Default.RedoxConditionalButton = 1; // Sarı için 1
                            Properties.Settings.Default.Save();
                        }
                    }
                    else // RedoxConditionalButton değiştiğinde
                    {
                        switch (Properties.Settings.Default.RedoxConditionalButton)
                        {
                            case 0: // Kırmızı
                                conditionalButtonRedox.Background = new SolidColorBrush(Colors.Red);
                                break;
                            case 1: // Sarı
                                conditionalButtonRedox.Background = new SolidColorBrush(Colors.Yellow);
                                break;
                            case 2: // Yeşil
                                conditionalButtonRedox.Background = new SolidColorBrush(Colors.Green);
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





















        private void ellipse1_MouseDown(object sender, MouseButtonEventArgs e)
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
            else if (Properties.Settings.Default.StartButton != 0)
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("Sadece hazırlık aşamasında ayarlar yapılabilir.",
                              "Configuration Required",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return; // Ellipse1'ün durumunu değiştirmeden fonksiyonu sonlandır
            }
            else
            {
                // Canvas'ı bul
                Canvas parentCanvas = ellipse1.Parent as Canvas;
                if (parentCanvas == null) return;

                double canvasWidth = parentCanvas.ActualWidth;
                double ellipseWidth = ellipse1.ActualWidth;
                double maxRight = canvasWidth - ellipseWidth - 12; // 12 = 6 (sol boşluk) + 6 (sağ boşluk)
                double targetLeft = Canvas.GetLeft(ellipse1) == 6 ? maxRight : 6; // Yuvarlağın hedef pozisyonu

                DoubleAnimation animation = new DoubleAnimation
                {
                    To = targetLeft,
                    Duration = TimeSpan.FromSeconds(0.5),
                    FillBehavior = FillBehavior.Stop
                };

                animation.Completed += (s, a) =>
                {
                    Canvas.SetLeft(ellipse1, targetLeft); // Animasyon tamamlandığında yuvarlağın pozisyonunu güncelle

                    if (targetLeft == maxRight)
                    {
                        ellipse1.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101")); // Sağdaysa kırmızı yap
                                                                                                                   // Ellipse açıldığında pO2Ellipse'i 1 yap
                        Properties.Settings.Default.TemperatureEllipse = 1;
                        Properties.Settings.Default.TemperatureConditionalButtonVisibility = 1;
                    }
                    else
                    {
                        ellipse1.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF")); // Soldaysa gri yap
                                                                                                                   // Ellipse kapandığında pO2Ellipse'i 0 yap
                        Properties.Settings.Default.TemperatureEllipse = 0;
                        Properties.Settings.Default.TemperatureConditionalButtonVisibility = 0;
                    }

                    // Ayarları kaydet
                    Properties.Settings.Default.Save();

                    // Ellipse'in rengi değiştikten sonra butonun görünürlüğünü kontrol et
                    CheckEllipsePositionAndSetButtonVisibility(ellipse1, conditionalButtonTemperature);

                    // FavouritesControl'daki ilgili ellipse'i güncelle
                    mainWindow.favouritesControl.UpdateEllipsePosition(ellipse1.Name, targetLeft);
                    // FavouritesControl'daki ilgili conditionalButton'ı güncelle
                    mainWindow.favouritesControl.UpdateConditionalButtonVisibility(ellipse1.Name, targetLeft);
                };
                ellipse1.BeginAnimation(Canvas.LeftProperty, animation);
            }
            Ellipse_MouseLeftButtonDown(sender, e);
        }

        private void ellipse2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Properties.Settings.Default.EditRedoxCascade == 0)
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("deneme ses 1-2",
                              "Configuration Required",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return; // Ellipse2'ün durumunu değiştirmeden fonksiyonu sonlandır
            }
            else if (Properties.Settings.Default.StartButton != 0)
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("Sadece hazırlık aşamasında ayarlar yapılabilir.",
                              "Configuration Required",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return; // Ellipse2'ün durumunu değiştirmeden fonksiyonu sonlandır
            }
            else
            {
                // Canvas'ı bul
                Canvas parentCanvas = ellipse2.Parent as Canvas;
                if (parentCanvas == null) return;

                double canvasWidth = parentCanvas.ActualWidth;
                double ellipseWidth = ellipse2.ActualWidth;
                double maxRight = canvasWidth - ellipseWidth - 12; // 12 = 6 (sol boşluk) + 6 (sağ boşluk)
                double targetLeft = Canvas.GetLeft(ellipse2) == 6 ? maxRight : 6; // Yuvarlağın hedef pozisyonu

                DoubleAnimation animation = new DoubleAnimation
                {
                    To = targetLeft,
                    Duration = TimeSpan.FromSeconds(0.5),
                    FillBehavior = FillBehavior.Stop
                };

                animation.Completed += (s, a) =>
                {
                    Canvas.SetLeft(ellipse2, targetLeft); // Animasyon tamamlandığında yuvarlağın pozisyonunu güncelle

                    if (targetLeft == maxRight)
                    {
                        ellipse2.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101")); // Sağdaysa kırmızı yap
                                                                                                                   // Ellipse açıldığında pO2Ellipse'i 1 yap
                        Properties.Settings.Default.StirrerEllipse = 1;
                        Properties.Settings.Default.StirrerConditionalButtonVisibility = 1;
                    }
                    else
                    {
                        ellipse2.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF")); // Soldaysa gri yap
                                                                                                                   // Ellipse kapandığında pO2Ellipse'i 0 yap
                        Properties.Settings.Default.StirrerEllipse = 0;
                        Properties.Settings.Default.StirrerConditionalButtonVisibility = 0;
                    }

                    // Ayarları kaydet
                    Properties.Settings.Default.Save();

                    // Ellipse'in rengi değiştikten sonra butonun görünürlüğünü kontrol et
                    CheckEllipsePositionAndSetButtonVisibility(ellipse2, conditionalButtonStirrer);

                    // FavouritesControl'daki ilgili ellipse'i güncelle
                    mainWindow.favouritesControl.UpdateEllipsePosition(ellipse2.Name, targetLeft);
                    // FavouritesControl'daki ilgili conditionalButton'ı güncelle
                    mainWindow.favouritesControl.UpdateConditionalButtonVisibility(ellipse2.Name, targetLeft);
                };

                ellipse2.BeginAnimation(Canvas.LeftProperty, animation);

            }

            Ellipse_MouseLeftButtonDown(sender, e);
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
            else if (Properties.Settings.Default.StartButton != 0)
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("Sadece hazırlık aşamasında ayarlar yapılabilir.",
                              "Configuration Required",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return; // Ellipse3'ün durumunu değiştirmeden fonksiyonu sonlandır
            }
            else
            {
                // Canvas'ı bul
                Canvas parentCanvas = ellipse3.Parent as Canvas;
                if (parentCanvas == null) return;

                double canvasWidth = parentCanvas.ActualWidth;
                double ellipseWidth = ellipse3.ActualWidth;
                double maxRight = canvasWidth - ellipseWidth - 12; // 12 = 6 (sol boşluk) + 6 (sağ boşluk)
                double targetLeft = Canvas.GetLeft(ellipse3) == 6 ? maxRight : 6; // Yuvarlağın hedef pozisyonu

                DoubleAnimation animation = new DoubleAnimation
                {
                    To = targetLeft,
                    Duration = TimeSpan.FromSeconds(0.5),
                    FillBehavior = FillBehavior.Stop
                };

                animation.Completed += (s, a) =>
                {
                    Canvas.SetLeft(ellipse3, targetLeft); // Animasyon tamamlandığında yuvarlağın pozisyonunu güncelle

                    if (targetLeft == maxRight)
                    {
                        ellipse3.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101")); // Sağdaysa kırmızı yap
                                                                                                                   // Ellipse açıldığında pO2Ellipse'i 1 yap
                        Properties.Settings.Default.pHEllipse = 1;
                        Properties.Settings.Default.pHConditionalButtonVisibility = 1;
                    }
                    else
                    {
                        ellipse3.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF")); // Soldaysa gri yap
                                                                                                                   // Ellipse kapandığında pO2Ellipse'i 0 yap
                        Properties.Settings.Default.pHEllipse = 0;
                        Properties.Settings.Default.pHConditionalButtonVisibility = 0;
                    }

                    // Ayarları kaydet
                    Properties.Settings.Default.Save();

                    // Ellipse'in rengi değiştikten sonra butonun görünürlüğünü kontrol et
                    CheckEllipsePositionAndSetButtonVisibility(ellipse3, conditionalButtonpH);

                    // FavouritesControl'daki ilgili ellipse'i güncelle
                    mainWindow.favouritesControl.UpdateEllipsePosition(ellipse3.Name, targetLeft);
                    // FavouritesControl'daki ilgili conditionalButton'ı güncelle
                    mainWindow.favouritesControl.UpdateConditionalButtonVisibility(ellipse3.Name, targetLeft);
                };

                ellipse3.BeginAnimation(Canvas.LeftProperty, animation);

            }

            // Normal ellipse tıklama olayını çağır
            Ellipse_MouseLeftButtonDown(sender, e);
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
            else if (Properties.Settings.Default.StartButton != 0)
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("Sadece hazırlık aşamasında ayarlar yapılabilir.",
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
                        Properties.Settings.Default.pO2ConditionalButtonVisibility = 1;
                    }
                    else
                    {
                        ellipse4.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF")); // Soldaysa gri yap
                                                                                                                   // Ellipse kapandığında pO2Ellipse'i 0 yap
                        Properties.Settings.Default.pO2Ellipse = 0;
                        Properties.Settings.Default.pO2ConditionalButtonVisibility = 0;
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

            else if (Properties.Settings.Default.StartButton != 0)
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("Sadece hazırlık aşamasında ayarlar yapılabilir.",
                              "Configuration Required",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return; // Ellipse9'ün durumunu değiştirmeden fonksiyonu sonlandır
            }
            else
            {
                // Canvas'ı bul
                Canvas parentCanvas = ellipse9.Parent as Canvas;
                if (parentCanvas == null) return;

                double canvasWidth = parentCanvas.ActualWidth;
                double ellipseWidth = ellipse9.ActualWidth;
                double maxRight = canvasWidth - ellipseWidth - 12; // 12 = 6 (sol boşluk) + 6 (sağ boşluk)
                double targetLeft = Canvas.GetLeft(ellipse9) == 6 ? maxRight : 6; // Yuvarlağın hedef pozisyonu

                DoubleAnimation animation = new DoubleAnimation
                {
                    To = targetLeft,
                    Duration = TimeSpan.FromSeconds(0.5),
                    FillBehavior = FillBehavior.Stop
                };

                animation.Completed += (s, a) =>
                {
                    Canvas.SetLeft(ellipse9, targetLeft); // Animasyon tamamlandığında yuvarlağın pozisyonunu güncelle

                    if (targetLeft == maxRight)
                    {
                        ellipse9.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101")); // Sağdaysa kırmızı yap
                                                                                                                   // Ellipse açıldığında pO2Ellipse'i 1 yap
                        Properties.Settings.Default.FoamEllipse = 1;
                        Properties.Settings.Default.FoamConditionalButtonVisibility = 1;
                    }
                    else
                    {
                        ellipse9.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF")); // Soldaysa gri yap
                                                                                                                   // Ellipse kapandığında pO2Ellipse'i 0 yap
                        Properties.Settings.Default.FoamEllipse = 0;
                        Properties.Settings.Default.FoamConditionalButtonVisibility = 0;
                    }

                    // Ayarları kaydet
                    Properties.Settings.Default.Save();

                    // Ellipse'in rengi değiştikten sonra butonun görünürlüğünü kontrol et
                    CheckEllipsePositionAndSetButtonVisibility(ellipse9, conditionalButtonFoam);

                    // FavouritesControl'daki ilgili ellipse'i güncelle
                    mainWindow.favouritesControl.UpdateEllipsePosition(ellipse9.Name, targetLeft);
                    // FavouritesControl'daki ilgili conditionalButton'ı güncelle
                    mainWindow.favouritesControl.UpdateConditionalButtonVisibility(ellipse9.Name, targetLeft);
                };

                ellipse9.BeginAnimation(Canvas.LeftProperty, animation);

            }

            // Normal ellipse tıklama olayını çağır
            Ellipse_MouseLeftButtonDown(sender, e);
        }

        private void ellipse19_MouseDown(object sender, MouseButtonEventArgs e)
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
            else if (Properties.Settings.Default.StartButton != 0)
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("Sadece hazırlık aşamasında ayarlar yapılabilir.",
                              "Configuration Required",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return; // Ellipse19'ün durumunu değiştirmeden fonksiyonu sonlandır
            }
            else
            {
                // Canvas'ı bul
                Canvas parentCanvas = ellipse19.Parent as Canvas;
                if (parentCanvas == null) return;

                double canvasWidth = parentCanvas.ActualWidth;
                double ellipseWidth = ellipse19.ActualWidth;
                double maxRight = canvasWidth - ellipseWidth - 12; // 12 = 6 (sol boşluk) + 6 (sağ boşluk)
                double targetLeft = Canvas.GetLeft(ellipse19) == 6 ? maxRight : 6; // Yuvarlağın hedef pozisyonu

                DoubleAnimation animation = new DoubleAnimation
                {
                    To = targetLeft,
                    Duration = TimeSpan.FromSeconds(0.5),
                    FillBehavior = FillBehavior.Stop
                };

                animation.Completed += (s, a) =>
                {
                    Canvas.SetLeft(ellipse19, targetLeft); // Animasyon tamamlandığında yuvarlağın pozisyonunu güncelle

                    if (targetLeft == maxRight)
                    {
                        ellipse19.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101")); // Sağdaysa kırmızı yap
                                                                                                                   // Ellipse açıldığında pO2Ellipse'i 1 yap
                        Properties.Settings.Default.RedoxEllipse = 1;
                        Properties.Settings.Default.RedoxConditionalButtonVisibility = 1;
                    }
                    else
                    {
                        ellipse19.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF")); // Soldaysa gri yap
                                                                                                                   // Ellipse kapandığında pO2Ellipse'i 0 yap
                        Properties.Settings.Default.RedoxEllipse = 0;
                        Properties.Settings.Default.RedoxConditionalButtonVisibility = 0;
                    }

                    // Ayarları kaydet
                    Properties.Settings.Default.Save();

                    // Ellipse'in rengi değiştikten sonra butonun görünürlüğünü kontrol et
                    CheckEllipsePositionAndSetButtonVisibility(ellipse19, conditionalButtonRedox);

                    // FavouritesControl'daki ilgili ellipse'i güncelle
                    mainWindow.favouritesControl.UpdateEllipsePosition(ellipse19.Name, targetLeft);
                    // FavouritesControl'daki ilgili conditionalButton'ı güncelle
                    mainWindow.favouritesControl.UpdateConditionalButtonVisibility(ellipse19.Name, targetLeft);
                };

                ellipse19.BeginAnimation(Canvas.LeftProperty, animation);

            }

            // Normal ellipse tıklama olayını çağır
            Ellipse_MouseLeftButtonDown(sender, e);
        }

        //CONDİTİONAL BUTTON OALYLARI
        //CONDİTİONAL BUTTON OALYLARI
        //CONDİTİONAL BUTTON OALYLARI
        //CONDİTİONAL BUTTON OALYLARI
        //CONDİTİONAL BUTTON OALYLARI
        //CONDİTİONAL BUTTON OALYLARI
        //CONDİTİONAL BUTTON OALYLARI
        //CONDİTİONAL BUTTON OALYLARI
        //CONDİTİONAL BUTTON OALYLARI
        //CONDİTİONAL BUTTON OALYLARI

        public void CheckEllipsePositionAndSetButtonVisibility(Ellipse ellipse, Button button)
        {
            // Ellipse'in parent'ını Canvas olarak al
            Canvas parentCanvas = ellipse.Parent as Canvas;
            if (parentCanvas == null) return;

            double canvasWidth = parentCanvas.ActualWidth; // Canvas'ın gerçek genişliğini kullan
            double ellipseRightPosition = Canvas.GetLeft(ellipse) + ellipse.Width; // Ellipse'in sağ kenarının konumu

            // Eğer bu conditional Button Temperature için ise özel kontrol uygula
            if (button == conditionalButtonTemperature)
            {
                // Sadece TemperatureEllipse == 1 ve StartButton == 1 ise görünür yap
                if (Properties.Settings.Default.TemperatureEllipse == 1 &&
                    Properties.Settings.Default.StartButton == 1 &&
                    Properties.Settings.Default.TemperatureConditionalButtonVisibility == 1)
                {
                    button.Visibility = Visibility.Visible;
                }
                else
                {
                    button.Visibility = Visibility.Collapsed;
                }
            }
            // Eğer bu conditional Button Stirrer için ise özel kontrol uygula
            if (button == conditionalButtonStirrer)
            {
                // Sadece StirrerEllipse == 1 ve StartButton == 1 ise görünür yap
                if (Properties.Settings.Default.StirrerEllipse == 1 &&
                    Properties.Settings.Default.StartButton == 1 &&
                    Properties.Settings.Default.StirrerConditionalButtonVisibility == 1)
                {
                    button.Visibility = Visibility.Visible;
                }
                else
                {
                    button.Visibility = Visibility.Collapsed;
                }
            }
            // Eğer bu conditional Button pH için ise özel kontrol uygula
            if (button == conditionalButtonpH)
            {
                // Sadece pHEllipse == 1 ve StartButton == 1 ise görünür yap
                if (Properties.Settings.Default.pHEllipse == 1 &&
                    Properties.Settings.Default.StartButton == 1 &&
                    Properties.Settings.Default.pHConditionalButtonVisibility == 1)
                {
                    button.Visibility = Visibility.Visible;
                }
                else
                {
                    button.Visibility = Visibility.Collapsed;
                }
            }
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
            // Eğer bu conditional Button Foam için ise özel kontrol uygula
            if (button == conditionalButtonFoam)
            {
                // Sadece FoamEllipse == 1 ve StartButton == 1 ise görünür yap
                if (Properties.Settings.Default.FoamEllipse == 1 &&
                    Properties.Settings.Default.StartButton == 1 &&
                    Properties.Settings.Default.FoamConditionalButtonVisibility == 1)
                {
                    button.Visibility = Visibility.Visible;
                }
                else
                {
                    button.Visibility = Visibility.Collapsed;
                }
            }
            // Eğer bu conditional Button Redox için ise özel kontrol uygula
            if (button == conditionalButtonRedox)
            {
                // Sadece RedoxEllipse == 1 ve StartButton == 1 ise görünür yap
                if (Properties.Settings.Default.RedoxEllipse == 1 &&
                    Properties.Settings.Default.StartButton == 1 &&
                    Properties.Settings.Default.RedoxConditionalButtonVisibility == 1)
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
        private void conditionalButtonTemperature_Click(object sender, RoutedEventArgs e)
        {
            // Tıklanan butonu belirle
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Butonun Tag'ine Temperature Grid'ini atayın
                clickedButton.Tag = Temperature;

                // Butonu kırmızı yap (varsayılan renk)
                clickedButton.Background = new SolidColorBrush(Colors.Red);
                Properties.Settings.Default.TemperatureConditionalButton = 0; // Kırmızı için 0
                Properties.Settings.Default.Save();
            }

            // Zamanlayıcıyı başlat veya devam ettir
            comparisonTimer.Start();
        }

        private void conditionalButtonStirrer_Click(object sender, RoutedEventArgs e)
        {
            // Tıklanan butonu belirle
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Butonun Tag'ine Stirrer Grid'ini atayın
                clickedButton.Tag = Stirrer;

                // Butonu kırmızı yap (varsayılan renk)
                clickedButton.Background = new SolidColorBrush(Colors.Red);
                Properties.Settings.Default.StirrerConditionalButton = 0; // Kırmızı için 0
                Properties.Settings.Default.Save();
            }

            // Zamanlayıcıyı başlat veya devam ettir
            comparisonTimer.Start();
        }

        private void conditionalButtonpH_Click(object sender, RoutedEventArgs e)
        {
            // Tıklanan butonu belirle
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Butonun Tag'ine pH Grid'ini atayın
                clickedButton.Tag = pH;

                // Butonu kırmızı yap (varsayılan renk)
                clickedButton.Background = new SolidColorBrush(Colors.Red);
                Properties.Settings.Default.pHConditionalButton = 0; // Kırmızı için 0
                Properties.Settings.Default.Save();
            }

            // Zamanlayıcıyı başlat veya devam ettir
            comparisonTimer.Start();
        }

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

        private void conditionalButtonFoam_Click(object sender, RoutedEventArgs e)
        {
            // Tıklanan butonu belirle
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Butonun Tag'ine Foam Grid'ini atayın
                clickedButton.Tag = Foam;

                // Butonu kırmızı yap (varsayılan renk)
                clickedButton.Background = new SolidColorBrush(Colors.Red);
                Properties.Settings.Default.FoamConditionalButton = 0; // Kırmızı için 0
                Properties.Settings.Default.Save();
            }

            // Zamanlayıcıyı başlat veya devam ettir
            comparisonTimer.Start();
        }

        private void conditionalButtonRedox_Click(object sender, RoutedEventArgs e)
        {
            // Tıklanan butonu belirle
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Butonun Tag'ine Redox Grid'ini atayın
                clickedButton.Tag = Redox;

                // Butonu kırmızı yap (varsayılan renk)
                clickedButton.Background = new SolidColorBrush(Colors.Red);
                Properties.Settings.Default.RedoxConditionalButton = 0; // Kırmızı için 0
                Properties.Settings.Default.Save();
            }

            // Zamanlayıcıyı başlat veya devam ettir
            comparisonTimer.Start();
        }

        public void CheckComparisonTimer()
        {
            if (Properties.Settings.Default.StartButton == 0)
            {
                comparisonTimer.Stop();

            }
            //if (Properties.Settings.Default.StartButton == 1)
            //{
            //    comparisonTimer.Start();

            //}
        }
        // ComparisonTimer_Tick metodunu güncelleyin
        private void ComparisonTimer_Tick(object sender, EventArgs e)
        {

            // Tüm butonları kontrol et
            foreach (Button button in FindVisualChildren<Button>(this))
            {
                if (button.Tag is Grid parentGrid)
                {
                    // Özel olarak conditionalButtonTemperature düğmesini kontrol et
                    if (button == conditionalButtonTemperature)
                    {
                        // TemperatureValue ve pO2Target değerlerini Settings.settings'den al
                        double TemperatureValue = Properties.Settings.Default.TemperatureValue;
                        double TemperatureTarget = Properties.Settings.Default.TemperatureTarget;

                        // Değerleri karşılaştır
                        double difference = Math.Abs(TemperatureValue - TemperatureTarget);

                        if (difference < 1)
                        {
                            // Değerler arasındaki fark 1'den az ise butonun arka planını yeşil yap
                            button.Background = new SolidColorBrush(Colors.Green);
                            Properties.Settings.Default.TemperatureConditionalButton = 2; // Yeşil için 2
                        }
                        else
                        {
                            // Değerler farklıysa butonun arka planını sarı yap
                            button.Background = new SolidColorBrush(Colors.Yellow);
                            Properties.Settings.Default.TemperatureConditionalButton = 1; // Sarı için 1
                        }

                        // Settings'i kaydet
                        Properties.Settings.Default.Save();
                    }



                    // Özel olarak conditionalButtonStirrer düğmesini kontrol et
                    if (button == conditionalButtonStirrer)
                    {
                        // StirrerValue ve StirrerTarget değerlerini Settings.settings'den al
                        double StirrerValue = Properties.Settings.Default.StirrerValue;
                        double StirrerTarget = Properties.Settings.Default.StirrerTarget;

                        // Değerleri karşılaştır
                        double difference = Math.Abs(StirrerValue - StirrerTarget);

                        if (difference < 1)
                        {
                            // Değerler arasındaki fark 1'den az ise butonun arka planını yeşil yap
                            button.Background = new SolidColorBrush(Colors.Green);
                            Properties.Settings.Default.StirrerConditionalButton = 2; // Yeşil için 2
                        }
                        else
                        {
                            // Değerler farklıysa butonun arka planını sarı yap
                            button.Background = new SolidColorBrush(Colors.Yellow);
                            Properties.Settings.Default.StirrerConditionalButton = 1; // Sarı için 1
                        }

                        // Settings'i kaydet
                        Properties.Settings.Default.Save();
                    }




                    // Özel olarak conditionalButtonpH düğmesini kontrol et
                    if (button == conditionalButtonpH)
                    {
                        // pHValue ve pHTarget değerlerini Settings.settings'den al
                        double pHValue = Properties.Settings.Default.pHValue;
                        double pHTarget = Properties.Settings.Default.pHTarget;

                        // Değerleri karşılaştır
                        double difference = Math.Abs(pHValue - pHTarget);

                        if (difference < 1)
                        {
                            // Değerler arasındaki fark 1'den az ise butonun arka planını yeşil yap
                            button.Background = new SolidColorBrush(Colors.Green);
                            Properties.Settings.Default.pHConditionalButton = 2; // Yeşil için 2
                        }
                        else
                        {
                            // Değerler farklıysa butonun arka planını sarı yap
                            button.Background = new SolidColorBrush(Colors.Yellow);
                            Properties.Settings.Default.pHConditionalButton = 1; // Sarı için 1
                        }

                        // Settings'i kaydet
                        Properties.Settings.Default.Save();
                    }




                    // Özel olarak conditionalButtonFoam düğmesini kontrol et
                    //if (button == conditionalButtonFoam)
                    //{
                    //    // FoamValue ve FoamTarget değerlerini Settings.settings'den al
                    //    double FoamValue = Properties.Settings.Default.FoamValue;
                    //    double FoamTarget = Properties.Settings.Default.FoamTarget;

                    //    // Değerleri karşılaştır
                    //    double difference = Math.Abs(FoamValue - FoamTarget);

                    //    if (difference < 1)
                    //    {
                    //        // Değerler arasındaki fark 1'den az ise butonun arka planını yeşil yap
                    //        button.Background = new SolidColorBrush(Colors.Green);
                    //        Properties.Settings.Default.FoamConditionalButton = 2; // Yeşil için 2
                    //    }
                    //    else
                    //    {
                    //        // Değerler farklıysa butonun arka planını sarı yap
                    //        button.Background = new SolidColorBrush(Colors.Yellow);
                    //        Properties.Settings.Default.FoamConditionalButton = 1; // Sarı için 1
                    //    }

                    //    // Settings'i kaydet
                    //    Properties.Settings.Default.Save();
                    //}


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


                    // Özel olarak conditionalButtonRedox düğmesini kontrol et
                    if (button == conditionalButtonRedox)
                    {
                        // RedoxValue ve RedoxTarget değerlerini Settings.settings'den al
                        double RedoxValue = Properties.Settings.Default.RedoxValue;
                        double RedoxTarget = Properties.Settings.Default.RedoxTarget;

                        // Değerleri karşılaştır
                        double difference = Math.Abs(RedoxValue - RedoxTarget);

                        if (difference < 1)
                        {
                            // Değerler arasındaki fark 1'den az ise butonun arka planını yeşil yap
                            button.Background = new SolidColorBrush(Colors.Green);
                            Properties.Settings.Default.RedoxConditionalButton = 2; // Yeşil için 2
                        }
                        else
                        {
                            // Değerler farklıysa butonun arka planını sarı yap
                            button.Background = new SolidColorBrush(Colors.Yellow);
                            Properties.Settings.Default.RedoxConditionalButton = 1; // Sarı için 1
                        }

                        // Settings'i kaydet
                        Properties.Settings.Default.Save();
                    }
                    // Özel olarak conditionalButtonpO2 düğmesini kontrol et
                    //else
                    //{
                    //    // Grid içindeki ikinci Label'ı bul
                    //    var secondLabel = parentGrid.Children.OfType<Label>().ElementAtOrDefault(1);
                    //    // Grid içindeki TextBox'ı bul
                    //    var textBox = parentGrid.Children.OfType<Border>().FirstOrDefault()?.Child as TextBox;

                    //    if (secondLabel != null && textBox != null)
                    //    {
                    //        // Label ve TextBox içindeki değerleri karşılaştır
                    //        if (secondLabel.Content.ToString() == textBox.Text)
                    //        {
                    //            // Değerler aynıysa butonun arka planını yeşil yap
                    //            button.Background = new SolidColorBrush(Colors.Green);
                    //        }
                    //        else
                    //        {
                    //            // Değerler farklıysa butonun arka planını sarı yap
                    //            button.Background = new SolidColorBrush(Colors.Yellow);
                    //        }
                    //    }
                    //}
                }
            }
            TemperatureValue.Content = Properties.Settings.Default.TemperatureValue.ToString();
            StirrerValue.Content = Properties.Settings.Default.StirrerValue.ToString();
            pHValue.Content = Properties.Settings.Default.pHValue.ToString();
            pO2Value.Content = Properties.Settings.Default.pO2Value.ToString();
            //FoamValue.Content = Properties.Settings.Default.FoamValue.ToString();
            RedoxValue.Content = Properties.Settings.Default.RedoxValue.ToString();
            if(Properties.Settings.Default.StartButton == 0)
            {
                comparisonTimer.Stop();

            }
        }
    }
}
