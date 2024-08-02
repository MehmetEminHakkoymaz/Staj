using System.Text;
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
    public partial class MainWindow : Window
    {
        private DispatcherTimer checkButtonTimer;
        private DispatcherTimer timer;
        private TimeSpan time;
        private MainControl mainControl; // MainControl'ü sınıf değişkeni olarak tanımlayın
        private ExtendedControl extendedControl; // ExtendedControl'ü sınıf değişkeni olarak tanımlayın
        private ExitGasControl exitGasControl; // ExitGasControl'ü burada bir kez oluşturun
        private EditViewControl editViewControl; // EditViewControl'ü burada bir kez oluşturun
        private FavouritesControl favouritesControl; // FavouritesControl'ü burada bir kez oluşturun
        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
            StartClock();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // 1 saniyelik aralıklarla güncellenir
            timer.Tick += Timer_Tick;
            time = TimeSpan.Zero;
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight;
            mainControl = new MainControl(this); // MainControl'ü burada bir kez oluşturun
            extendedControl = new ExtendedControl(this); // ExtendedControl'ü burada bir kez oluşturun
            exitGasControl = new ExitGasControl(this); // ExitGasControl'ü burada bir kez oluşturun
            editViewControl = new EditViewControl(this); // EditViewControl'ü burada bir kez oluşturun
            favouritesControl = new FavouritesControl(this);
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Zamanı güncelle ve göster
            time = time.Add(TimeSpan.FromSeconds(1));
            clockTextBlock.Text = time.ToString(@"hh\:mm\:ss");
        }



        private void Main_Button_Click(object sender, RoutedEventArgs e)
        {
            contentArea.Content = mainControl;
        }

        private void Extended_Button_Click(object sender, RoutedEventArgs e)
        {
            contentArea.Content = extendedControl;
        }

        private void Exit_Gas_Button_Click(object sender, RoutedEventArgs e)
        {
            contentArea.Content = exitGasControl;
        }

        private void EditView_Button_Click(object sender, RoutedEventArgs e)
        {
            contentArea.Content = editViewControl;
        }

        private void Favourites_Button_Click(object sender, RoutedEventArgs e)
        {
            contentArea.Content = favouritesControl;
        }


        private void Pumps_Button_Click(object sender, RoutedEventArgs e)
        {
            PumpsControl pumpsControl = new PumpsControl();
            contentArea.Content = pumpsControl;
        }


        //ÜstMenü
        private void TogglePopupButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button) // Güvenli tür dönüşümü ve null kontrolü
            {
                Thickness targetMargin;

                bool isExpanded = TopGrid.Margin.Top == 0;
                if (isExpanded)
                {
                    targetMargin = new Thickness(0, -30, 0, 0);
                    // Image kaynağını güncelle
                    button.Tag = "pack://application:,,,/WpfApp1;component/images/Down.png";
                }
                else
                {
                    targetMargin = new Thickness(0, 0, 0, 0);
                    // Image kaynağını güncelle
                    button.Tag = "pack://application:,,,/WpfApp1;component/images/Up.png";
                }

                ThicknessAnimation marginAnimation = new ThicknessAnimation
                {
                    To = targetMargin,
                    Duration = TimeSpan.FromSeconds(0.2),
                    FillBehavior = FillBehavior.Stop
                };

                marginAnimation.Completed += (s, args) =>
                {
                    TopGrid.Margin = targetMargin;
                    // Burada artık Image kaynağını güncellemeye gerek yok, çünkü Tag özelliği ile bağlama yapıldı
                };

                TopGrid.BeginAnimation(MarginProperty, marginAnimation);
            }
        }
        private Image CreateArrowImage(string imagePath)
        {
            return new Image
            {
                Source = new BitmapImage(new Uri(imagePath))
            };
        }

        private void StartClock()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick_Now;
            timer.Start();
        }
        private void Timer_Tick_Now(object sender, EventArgs e)
        {
            ClockTextBlock.Text = DateTime.Now.ToString("HH : mm : ss");
        }

        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {
            // Yeni pencere örneği oluştur (namespace'i dikkate alarak)
            var settingsWindow = new WpfApp1.Settings.SettingsWindow();

            // Pencerenin boyutunu ayarla
            settingsWindow.Width = 1024;
            settingsWindow.Height = 600;

            // Pencereyi göster
            settingsWindow.Show();
        }

        private void FirstStartButton_Click(object sender, RoutedEventArgs e)
        {
            FirstStartButton.Visibility = Visibility.Collapsed;
            StartButton.Visibility = Visibility.Visible;
            StartButton.Opacity = 0.5;
            StartButton.IsEnabled = false;

            mainControl.CheckEllipsePositionAndSetButtonVisibility(mainControl.ellipse1, mainControl.conditionalButton);
            mainControl.CheckEllipsePositionAndSetButtonVisibility(mainControl.ellipse2, mainControl.conditionalButtonStirrer);
            mainControl.CheckEllipsePositionAndSetButtonVisibility(mainControl.ellipse3, mainControl.conditionalButtonpH);
            mainControl.CheckEllipsePositionAndSetButtonVisibility(mainControl.ellipse4, mainControl.conditionalButtonpO2);
            mainControl.CheckEllipsePositionAndSetButtonVisibility(mainControl.ellipse5, mainControl.conditionalButtonGas1);
            mainControl.CheckEllipsePositionAndSetButtonVisibility(mainControl.ellipse6, mainControl.conditionalButtonGas2);
            mainControl.CheckEllipsePositionAndSetButtonVisibility(mainControl.ellipse7, mainControl.conditionalButtonGas3);
            mainControl.CheckEllipsePositionAndSetButtonVisibility(mainControl.ellipse8, mainControl.conditionalButtonGas4);
            mainControl.CheckEllipsePositionAndSetButtonVisibility(mainControl.ellipse9, mainControl.conditionalButtonFoam);
            extendedControl.CheckEllipsePositionAndSetButtonVisibility(extendedControl.ellipse10, extendedControl.conditionalButtonTurbidity);
            extendedControl.CheckEllipsePositionAndSetButtonVisibility(extendedControl.ellipse11, extendedControl.conditionalButtonBalance);
            extendedControl.CheckEllipsePositionAndSetButtonVisibility(extendedControl.ellipse12, extendedControl.conditionalButtonAirFlow);
            extendedControl.CheckEllipsePositionAndSetButtonVisibility(extendedControl.ellipse13, extendedControl.conditionalButtonGas2Flow);
            exitGasControl.CheckEllipsePositionAndSetButtonVisibility(exitGasControl.ellipse14, exitGasControl.conditionalButtonTurbidity);
            exitGasControl.CheckEllipsePositionAndSetButtonVisibility(exitGasControl.ellipse15, exitGasControl.conditionalButtonBalance);
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            StartButton.Visibility = Visibility.Collapsed;
            StopButton.Visibility = Visibility.Visible;
        }

        private void StartButtonContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!StartButton.IsEnabled)
            {
                MessageBox.Show("Paramaters are not same as target value.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                e.Handled = true; // Olayın daha fazla işlenmesini engelle
            }
        }

        private bool AreVisibleButtonsGreen(params Button[] buttons)
        {
            foreach (var button in buttons)
            {
                if (button.Visibility == Visibility.Visible)
                {
                    if (!(button.Background is SolidColorBrush brush) || brush.Color != Colors.Green)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void InitializeTimer()
        {
            checkButtonTimer = new DispatcherTimer();
            checkButtonTimer.Interval = TimeSpan.FromSeconds(1); // Kontrol aralığını belirleyin
            checkButtonTimer.Tick += CheckButtonTimer_Tick;
            checkButtonTimer.Start();
        }

        private void CheckButtonTimer_Tick(object sender, EventArgs e)
        {
            // Sadece görünür olan butonların yeşil olup olmadığını kontrol et
            if (AreVisibleButtonsGreen(
                mainControl.conditionalButton,
                mainControl.conditionalButtonStirrer,
                mainControl.conditionalButtonpH,
                mainControl.conditionalButtonpO2,
                mainControl.conditionalButtonGas1,
                mainControl.conditionalButtonGas2,
                mainControl.conditionalButtonGas3,
                mainControl.conditionalButtonGas4,
                mainControl.conditionalButtonFoam,
                extendedControl.conditionalButtonTurbidity,
                extendedControl.conditionalButtonBalance,
                extendedControl.conditionalButtonAirFlow,
                extendedControl.conditionalButtonGas2Flow,
                exitGasControl.conditionalButtonTurbidity,
                exitGasControl.conditionalButtonBalance))
            {
                StartButton.Opacity = 1.0;
                StartButton.IsEnabled = true;
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            StopButton.Visibility = Visibility.Collapsed;
            FirstStartButton.Visibility = Visibility.Visible;
        }
    }
}