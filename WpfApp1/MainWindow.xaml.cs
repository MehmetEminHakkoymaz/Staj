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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private TimeSpan time;
        public MainWindow()
        {
            StartClock();
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // 1 saniyelik aralıklarla güncellenir
            timer.Tick += Timer_Tick;
            time = TimeSpan.Zero;
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Zamanı güncelle ve göster
            time = time.Add(TimeSpan.FromSeconds(1));
            clockTextBlock.Text = time.ToString(@"hh\:mm\:ss");
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void Main_Button_Click(object sender, RoutedEventArgs e)
        {
            MainControl mainControl = new MainControl();
            contentArea.Content = mainControl;
        }

        private void Favourites_Button_Click(object sender, RoutedEventArgs e)
        {
            FavouritesControl favouritesControl = new FavouritesControl();
            contentArea.Content = favouritesControl;
        }

        private void Extended_Button_Click(object sender, RoutedEventArgs e)
        {
            ExtendedControl extendedControl = new ExtendedControl();
            contentArea.Content = extendedControl;
        }

        private void Exit_Gas_Button_Click(object sender, RoutedEventArgs e)
        {
            ExitGasControl exitGasControl = new ExitGasControl();
            contentArea.Content = exitGasControl;
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
    }
}