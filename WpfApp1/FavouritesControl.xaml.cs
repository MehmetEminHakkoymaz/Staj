using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    public partial class FavouritesControl : UserControl
    {
        public List<Border> borders;
        public List<Ellipse> ellipses;
        public List<Grid> grids;
        public List<CheckBox> checkBoxes;
        public List<Button> buttons;
        private DispatcherTimer updateTimer;


        private MainWindow mainWindow;

        public FavouritesControl(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            ellipse1.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse2.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse3.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse4.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse5.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse6.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse7.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse8.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse9.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse10.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse11.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse12.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse13.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse14.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse15.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse16.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse17.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse18.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse19.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            grids = new List<Grid> 
            {
                ElRedux,
                ElGas5Flow,
                ElGas4Flow,
                ElGas3Flow,
                ElExitBalance,
                ElExitTurbidity,
                ElGas2Flow,
                ElAirFlow,
                ElBalance,
                ElTurbidity,
                ElFoam,
                ElGas4,
                ElGas3,
                ElGas2,
                ElGas1,
                ElpO2,
                ElpH,
                ElStirrer,
                ElTemperature
            };

            borders = new List<Border> 
            {
                Redux,
                Gas5Flow,
                Gas4Flow,
                Gas3Flow,
                ExitBalance,
                ExitTurbidity,
                Gas2Flow,
                AirFlow,
                Balance,
                Turbidity,
                Foam,
                Gas4,
                Gas3,
                Gas2,
                Gas1,
                pO2,
                pH,
                Stirrer,
                Temperature
            };

            ellipses = new List<Ellipse> 
            {
                ellipse19,
                ellipse18,
                ellipse17,
                ellipse16,
                ellipse15,
                ellipse14,
                ellipse13,
                ellipse12,
                ellipse11,
                ellipse10,
                ellipse9,
                ellipse8,
                ellipse7,
                ellipse6,    
                ellipse5,
                ellipse4,
                ellipse3,
                ellipse2,
                ellipse1
            };

            buttons = new List<Button>
            {
                conditionalButtonRedux,
                conditionalButtonGas5Flow,
                conditionalButtonGas4Flow,
                conditionalButtonGas3Flow,
                conditionalButtonExitBalance,
                conditionalButtonExitTurbidity,
                conditionalButtonGas2Flow,
                conditionalButtonAirFlow,
                conditionalButtonBalance,
                conditionalButtonTurbidity,
                conditionalButtonFoam,
                conditionalButtonGas4,
                conditionalButtonGas3,
                conditionalButtonGas2,
                conditionalButtonGas1,
                conditionalButtonpO2,
                conditionalButtonpH,
                conditionalButtonStirrer,
                conditionalButton
            };
            //int total = TotalManager.Instance.Total;
            string totalAsBinary = TotalManager.Instance.GetTotalAsBinary();

            UpdateVisibilityBasedOnBinary();

            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromSeconds(1); // 1 saniyelik aralıklarla
            updateTimer.Tick += UpdateTimer_Tickk;
            updateTimer.Start();



            var editViewControl = (EditViewControl)FindName("editViewControl");
            if (editViewControl != null)
            {
                checkBoxes = new List<CheckBox>
                {

                    editViewControl.Temperature,
                    editViewControl.Stirrer,
                    editViewControl.pH,
                    editViewControl.pO2,
                    //editViewControl.Gas1,
                    //editViewControl.Gas2,
                    //editViewControl.Gas3,
                    //editViewControl.Gas4,
                    editViewControl.Foam,
                    editViewControl.Turbidity,
                    editViewControl.Balance,
                    editViewControl.AirFlow,
                    editViewControl.Gas2Flow,
                    editViewControl.ExitTurbidity,
                    editViewControl.ExitBalance,
                    //editViewControl.Gas3Flow,
                    //editViewControl.Gas4Flow,
                    //editViewControl.Gas5Flow,
                    editViewControl.Redux
                    // other properties
                };
            }
            else
            {
                // Handle the case where editViewControl is not found
                //throw new InvalidOperationException("editViewControl not found in the XAML.");
            }
            
            
            //ellipse1.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            KeypadControl.ValueSelected += KeyPadControl_ValueSelected!;
            comparisonTimer.Interval = TimeSpan.FromSeconds(1); // 1 saniyelik aralıklarla
            comparisonTimer.Tick += ComparisonTimer_Tick!; // Zamanlayıcı olayı

            DataContext = this;
            InitializeTemperatureMonitoring();

        }
        private double currentTemperature;
        private double maxTemperature = 37.0; // Maksimum sıcaklık değeri
        private double warningOffset = 10.0;  // Uyarı için düşülecek derece
        private DispatcherTimer temperatureTimer;


        private void UpdateTimer_Tickk(object sender, EventArgs e)
        {
            UpdateVisibilityBasedOnBinary();
        }
        private void UpdateVisibilityBasedOnBinary()
        {
            //MessageBox.Show("UpdateVisibilityBasedOnBinary called"); // Metodun çağrıldığını doğrulamak için

            string binaryString = TotalManager.Instance.GetTotalAsBinary().PadLeft(borders.Count, '0');

            for (int i = 0; i < binaryString.Length; i++)
            {
                if (binaryString[i] == '1')
                {
                    if (i < borders.Count)
                    {
                        borders[i].Visibility = Visibility.Visible;
                    }
                    if (i < grids.Count)
                    {
                        grids[i].Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    if (i < borders.Count)
                    {
                        borders[i].Visibility = Visibility.Collapsed;
                    }
                    if (i < grids.Count)
                    {
                        grids[i].Visibility = Visibility.Collapsed;
                    }
                }
            }
        }


        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox? focusedTextBox = sender as TextBox;
            if (focusedTextBox != null)
            {
                // TextBox'ın ebeveyninin ebeveynini bul (Grid varsayıyoruz)
                DependencyObject? parent = VisualTreeHelper.GetParent(focusedTextBox);
                DependencyObject? grandParent = parent != null ? VisualTreeHelper.GetParent(parent) : null;
                Grid? parentGrid = grandParent as Grid;
                if (parentGrid != null)
                {
                    // Grid içindeki ilk Label'ı bul
                    Label? firstLabel = parentGrid.Children.OfType<Label>().FirstOrDefault();
                    if (firstLabel != null)
                    {
                        // Label'ın içeriğini al
                        string labelContent = firstLabel.Content?.ToString() ?? string.Empty;
                        // KeyPad'e label içeriğini gönder
                        activeTextBox = sender as TextBox;
                        KeypadPopup.IsOpen = true;
                        KeypadControl.SetLabelContent(labelContent);
                    }
                }
            }
        }

        private TextBox? activeTextBox = null;

        private TextBox? currentTextBox = null;

        private DispatcherTimer comparisonTimer = new DispatcherTimer();

        private void KeyPadControl_ValueSelected(object? sender, string value)
        {
            if (activeTextBox != null)
            {
                activeTextBox.Text = value; // KeyPad'den gelen değeri aktif TextBox'a atayın
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
                Canvas? parentCanvas = clickedEllipse.Parent as Canvas;
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

                    // MainControl'daki ilgili ellipse'i güncelle
                    mainWindow.mainControl.UpdateEllipsePosition(clickedEllipse.Name, targetLeft);
                    // MainControl'daki ilgili conditionalButton'ı güncelle
                    mainWindow.mainControl.UpdateConditionalButtonVisibility(clickedEllipse.Name, targetLeft);
                    // ExtendedControl'daki ilgili ellipse'i güncelle
                    mainWindow.extendedControl.UpdateEllipsePosition(clickedEllipse.Name, targetLeft);
                    // ExtendedControl'daki ilgili conditionalButton'ı güncelle
                    mainWindow.extendedControl.UpdateConditionalButtonVisibility(clickedEllipse.Name, targetLeft);
                    // ExitGasControl'daki ilgili ellipse'i güncelle
                    mainWindow.exitGasControl.UpdateEllipsePosition(clickedEllipse.Name, targetLeft);
                    // ExitGasControl'daki ilgili conditionalButton'ı güncelle
                    mainWindow.exitGasControl.UpdateConditionalButtonVisibility(clickedEllipse.Name, targetLeft);
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
                "ellipse5" => conditionalButtonGas1,
                "ellipse6" => conditionalButtonGas2,
                "ellipse7" => conditionalButtonGas3,
                "ellipse8" => conditionalButtonGas4,
                "ellipse9" => conditionalButtonFoam,
                "ellipse10" => conditionalButtonTurbidity,
                "ellipse11" => conditionalButtonBalance,
                "ellipse12" => conditionalButtonAirFlow,
                "ellipse13" => conditionalButtonGas2Flow,
                "ellipse14" => conditionalButtonExitTurbidity,
                "ellipse15" => conditionalButtonExitBalance,
                "ellipse16" => conditionalButtonGas3Flow,
                "ellipse17" => conditionalButtonGas4Flow,
                "ellipse18" => conditionalButtonGas5Flow,
                "ellipse19" => conditionalButtonRedux,
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
            CheckEllipsePositionAndSetButtonVisibility(ellipse5, conditionalButtonGas1);
            CheckEllipsePositionAndSetButtonVisibility(ellipse6, conditionalButtonGas2);
            CheckEllipsePositionAndSetButtonVisibility(ellipse7, conditionalButtonGas3);
            CheckEllipsePositionAndSetButtonVisibility(ellipse8, conditionalButtonGas4);
            CheckEllipsePositionAndSetButtonVisibility(ellipse9, conditionalButtonFoam);
            CheckEllipsePositionAndSetButtonVisibility(ellipse10, conditionalButtonTurbidity);
            CheckEllipsePositionAndSetButtonVisibility(ellipse11, conditionalButtonBalance);
            CheckEllipsePositionAndSetButtonVisibility(ellipse12, conditionalButtonAirFlow);
            CheckEllipsePositionAndSetButtonVisibility(ellipse13, conditionalButtonGas2Flow);
            CheckEllipsePositionAndSetButtonVisibility(ellipse14, conditionalButtonExitTurbidity);
            CheckEllipsePositionAndSetButtonVisibility(ellipse15, conditionalButtonExitBalance);
            CheckEllipsePositionAndSetButtonVisibility(ellipse16, conditionalButtonGas3Flow);
            CheckEllipsePositionAndSetButtonVisibility(ellipse17, conditionalButtonGas4Flow);
            CheckEllipsePositionAndSetButtonVisibility(ellipse18, conditionalButtonGas5Flow);
            CheckEllipsePositionAndSetButtonVisibility(ellipse19, conditionalButtonRedux);
        }

        public void CheckEllipsePositionAndSetButtonVisibility(Ellipse ellipse, Button button)
        {
            // Ellipse'in parent'ını Canvas olarak al
            Canvas? parentCanvas = ellipse.Parent as Canvas;
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
            //UpdateVisibility();
            // Zamanlayıcıyı başlatmadan önce, tıklanan butonu belirle
            Button? clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Tıklanan butonun Tag'inde ilişkili Grid'i saklayın (XAML'de veya başka bir yerde ayarlanmalıdır)
                clickedButton.Tag = clickedButton.Parent as Grid;
            }

            // Zamanlayıcıyı başlat
            comparisonTimer.Start();
        }

        private void ComparisonTimer_Tick(object? sender, EventArgs e)
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
                        if (secondLabel.Content?.ToString() == textBox.Text)
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

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject? depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject? child = VisualTreeHelper.GetChild(depObj, i);
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

        private void EditpH_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var editpHWindow = new WpfApp1.EditPages.EditpH();
            editpHWindow.Show();
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

    }
}
