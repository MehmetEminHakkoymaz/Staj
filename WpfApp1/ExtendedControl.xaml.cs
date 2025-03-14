﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    public partial class ExtendedControl : UserControl, INotifyPropertyChanged
    {
        private MainWindow mainWindow;

        public ExtendedControl(MainWindow mainWindow)
        {
            InitializeComponent();
            ellipse10.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse11.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse12.MouseLeftButtonDown += ellipse12_MouseLeftButtonDown;
            ellipse13.MouseLeftButtonDown += ellipse13_MouseLeftButtonDown; 
            //ellipse16.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            //ellipse17.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            //ellipse18.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            this.mainWindow = mainWindow;
            KeypadControl.ValueSelected += KePadControl_ValueSelected;
            comparisonTimer.Interval = TimeSpan.FromSeconds(1); // 1 saniyelik aralıklarla
            comparisonTimer.Tick += ComparisonTimer_Tick; // Zamanlayıcı olayı
            DataContext = this;
            InitializeTemperatureMonitoring();
            Properties.Settings.Default.PropertyChanged += Settings_PropertyChanged;
            UpdateBorderVisibilities();
            CompareGas2Values();

        }

        private double turbidityCurrentTemperature;
        private double maxTemperature = 37.0;
        private double warningOffset = 10.0;
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

        // INotifyPropertyChanged implementasyonu
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // Turbidity için property'ler
        private bool isTurbidityHighTemperature;
        public bool IsTurbidityHighTemperature
        {
            get => isTurbidityHighTemperature;
            set
            {
                isTurbidityHighTemperature = value;
                OnPropertyChanged(nameof(IsTurbidityHighTemperature));
            }
        }

        private bool isTurbidityWarningTemperature;
        public bool IsTurbidityWarningTemperature
        {
            get => isTurbidityWarningTemperature;
            set
            {
                isTurbidityWarningTemperature = value;
                OnPropertyChanged(nameof(IsTurbidityWarningTemperature));
            }
        }

        private bool isTurbidityNormalTemperature;
        public bool IsTurbidityNormalTemperature
        {
            get => isTurbidityNormalTemperature;
            set
            {
                isTurbidityNormalTemperature = value;
                OnPropertyChanged(nameof(IsTurbidityNormalTemperature));
            }
        }

        private void InitializeTemperatureMonitoring()
        {
            temperatureTimer = new DispatcherTimer();
            temperatureTimer.Interval = TimeSpan.FromSeconds(1);
            temperatureTimer.Tick += TemperatureTimer_Tick;
            temperatureTimer.Start();
        }

        private void TemperatureTimer_Tick(object sender, EventArgs e)
        {
            // Turbidity için sıcaklık değerini al ve güncelle
            turbidityCurrentTemperature = GetTemperatureFromSensor();
            TurbidityTemperatureIndicator.Text = turbidityCurrentTemperature.ToString("F1");
            UpdateTurbidityTemperatureStatus();
        }

        private double GetTemperatureFromSensor()
        {
            // Gerçek sensör kodunuz buraya gelecek
            // Şimdilik test için rastgele değer üretiyoruz
            Random rand = new Random();
            return 20 + rand.NextDouble() * 30; // 20-50 derece arası
        }

        private void UpdateTurbidityTemperatureStatus()
        {
            if (turbidityCurrentTemperature >= maxTemperature)
            {
                IsTurbidityHighTemperature = true;
                IsTurbidityWarningTemperature = false;
                IsTurbidityNormalTemperature = false;
            }
            else if (turbidityCurrentTemperature >= (maxTemperature - warningOffset))
            {
                IsTurbidityHighTemperature = false;
                IsTurbidityWarningTemperature = true;
                IsTurbidityNormalTemperature = false;
            }
            else
            {
                IsTurbidityHighTemperature = false;
                IsTurbidityWarningTemperature = false;
                IsTurbidityNormalTemperature = true;
            }
        }



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
                if (parentGrid != null)
                {
                    // Grid içindeki ilk Label'ı bul
                    Label firstLabel = parentGrid.Children.OfType<Label>().FirstOrDefault();
                    if (firstLabel != null)
                    {
                        // Label'ın içeriğini al
                        string labelContent = firstLabel.Content.ToString();
                        // KeyPad'e label içeriğini gönder
                        activeTextBox = sender as TextBox;
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
                "ellipse10" => conditionalButtonTurbidity,
                "ellipse11" => conditionalButtonBalance,
                "ellipse12" => conditionalButtonAirFlow,
                "ellipse13" => conditionalButtonGas2Flow,
                //"ellipse16" => conditionalButtonGas3Flow,
                //"ellipse17" => conditionalButtonGas4Flow,
                //"ellipse18" => conditionalButtonGas5Flow,
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
            CheckEllipsePositionAndSetButtonVisibility(ellipse10, conditionalButtonTurbidity);
            CheckEllipsePositionAndSetButtonVisibility(ellipse11, conditionalButtonBalance);
            CheckEllipsePositionAndSetButtonVisibility(ellipse12, conditionalButtonAirFlow);
            CheckEllipsePositionAndSetButtonVisibility(ellipse13, conditionalButtonGas2Flow);
            //CheckEllipsePositionAndSetButtonVisibility(ellipse16, conditionalButtonGas3Flow);
            //CheckEllipsePositionAndSetButtonVisibility(ellipse17, conditionalButtonGas4Flow);
            //CheckEllipsePositionAndSetButtonVisibility(ellipse18, conditionalButtonGas5Flow);
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
            CompareGas2Values();

        }

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


        private void KePadControl_ValueSelected(object sender, string value)
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
            if (FindName("AirFlowTargetBorder") is Border airFlowBorder)
            {
                airFlowBorder.Visibility = Properties.Settings.Default.HideAirFlowBorder ?
                    Visibility.Collapsed : Visibility.Visible;
            }

            if (FindName("Gas2FlowTargetBorder") is Border gas2FlowBorder)
            {
                gas2FlowBorder.Visibility = Properties.Settings.Default.HideGas2FlowBorder ?
                    Visibility.Collapsed : Visibility.Visible;
            }
        }

        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Eğer ilgili ayarlar değiştiyse border görünürlüklerini güncelle
            if (e.PropertyName == "HideAirFlowBorder")
            {
                UpdateBorderVisibilities();
            }
            // ... diğer kodlar ...
        }

        private void CompareGas2Values()
        {
            if (double.TryParse(Gas2FlowValue.Content?.ToString(), out double value) &&
                double.TryParse(Gas2FlowTarget.Text, out double target))
            {
                IsGas2ValueLessThanTarget = value < target;
            }
            else
            {
                IsGas2ValueLessThanTarget = false;
            }
        }

        private void Gas2Target_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TextBox değeri değiştiğinde karşılaştırma yap
            CompareGas2Values();
        }

        private void ellipse12_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Önce Properties.Settings.Default'tan cascade değerini kontrol edin
            if (Properties.Settings.Default.RedoxSelectedCascade == "AirFlow")
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

        private void ellipse13_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Önce Properties.Settings.Default'tan cascade değerini kontrol edin
            if (Properties.Settings.Default.RedoxSelectedCascade == "Gas2")
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("pO2 cascade selection is required. Please go to EditpO2 settings and select a cascade option.",
                              "Configuration Required",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return; // Ellipse4'ün durumunu değiştirmeden fonksiyonu sonlandır
            }

            if (Properties.Settings.Default.RedoxSelectedCascade == "TotalFlow")
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
    }
}
