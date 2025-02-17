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
using System;
using System.IO.Ports;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;



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
        private PumpsControl pumpsControl; // PumpsControl'ü burada bir kez oluşturun
        private OpenAutoWindow openAutoWindow; // OpenAutoWindow'ü burada bir kez oluşturun

        SerialPort port;
        int incomingFlag = 0;
        string selectedPort = "";
        int incomingSensState = 0;
        double incommingBatteryVal;


        //public void SafeAction(Action action, bool message = true)
        //{
        //    try
        //    {
        //        action();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (message)
        //        {
        //            // MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            MessageBox.Show(ex.Message, "Exception");
        //        }
        //    }
        //}

        //public void InitializeArduino(String listeningPort/*, int baudRate*/)
        //{
        //    SafeAction(() =>
        //    {
        //        port = new SerialPort(listeningPort,/* baudRate*/9600);
        //        port.Parity = Parity.None;
        //        port.StopBits = StopBits.One;
        //        port.DataBits = 8;
        //        port.Handshake = Handshake.None;
        //        port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

        //        port.Open();
             
        //            if (port.IsOpen && port != null)
        //            {
        //                selectedPort = listeningPort;
                        
        //            }
              
        //    });

        //}
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string line = sp.ReadLine();
            this.Dispatcher.BeginInvoke(new LineReceivedEvent(LineReceived), line);
        }
        private delegate void LineReceivedEvent(string line);
        private void LineReceived(string line)
        {

            String[] incommingWords = line.Split(',');

            if (incommingWords[0].StartsWith("$"))
            {
                //label2.Text = line.ToString();
                label2.Content = incommingWords[1] + " - " + incommingWords[2] + " - " + incommingWords[3] + " - " + incommingWords[4] + " - " + incommingWords[5] + " - " + 
                    incommingWords[6] + " - " + incommingWords[7] + " - " + incommingWords[8] + " - " + incommingWords[9] + " - " + incommingWords[10] + " - " + 
                    incommingWords[11] + " - " + incommingWords[12] + " - " + incommingWords[13] + " - " + incommingWords[14] + " - " + incommingWords[15] + " - " + 
                    incommingWords[16] + " - " + incommingWords[17] + " - " + incommingWords[18] + " - " + incommingWords[19];

                mainControl.TemperatureValue.Content = incommingWords[1];
                mainControl.StirrerValue.Content = incommingWords[2];
                mainControl.pHValue.Content = incommingWords[3];
                mainControl.pO2Value.Content = incommingWords[4];
                mainControl.Gas1Value.Content = incommingWords[5];
                mainControl.Gas2Value.Content = incommingWords[6];
                mainControl.Gas3Value.Content = incommingWords[7];
                mainControl.Gas4Value.Content = incommingWords[8];
                mainControl.FoamValue.Content = incommingWords[9];
                extendedControl.TurbidityValue.Content = incommingWords[10];
                extendedControl.BalanceValue.Content = incommingWords[11];
                extendedControl.AirFlowValue.Content = incommingWords[12];
                extendedControl.Gas2FlowValue.Content = incommingWords[13];
                exitGasControl.ExitTurbidityValue.Content = incommingWords[14];
                exitGasControl.ExitBalanceValue.Content = incommingWords[15];
                pumpsControl.Pump1Value.Content = incommingWords[16];
                pumpsControl.Pump2Value.Content = incommingWords[17];
                pumpsControl.Pump3Value.Content = incommingWords[18];
                pumpsControl.Pump4Value.Content = incommingWords[19];
            }


        }

        public void LineSend(string line)
        {
            String sendingWords = "$,";


            sendingWords += mainControl.TemperatureTarget.Text;
            sendingWords += ",";
            sendingWords += mainControl.StirrerTarget.Text;
            sendingWords += ",";
            sendingWords += mainControl.pHTarget.Text;
            sendingWords += ",";
            sendingWords += mainControl.pO2Target.Text;
            sendingWords += ",";
            sendingWords += mainControl.Gas1Target.Text;
            sendingWords += ",";
            sendingWords += mainControl.Gas2Target.Text;
            sendingWords += ",";
            sendingWords += mainControl.Gas3Target.Text;
            sendingWords += ",";
            sendingWords += mainControl.Gas4Target.Text;
            sendingWords += ",";
            sendingWords += mainControl.FoamTarget.Text;
            sendingWords += ",";
            sendingWords += extendedControl.TurbidityTarget.Text;
            sendingWords += ",";
            sendingWords += extendedControl.BalanceTarget.Text;
            sendingWords += ",";
            sendingWords += extendedControl.AirFlowTarget.Text;
            sendingWords += ",";
            sendingWords += extendedControl.Gas2FlowTarget.Text;
            sendingWords += ",";
            sendingWords += exitGasControl.ExitTurbidityTarget.Text;
            sendingWords += ",";
            sendingWords += exitGasControl.ExitBalanceTarget.Text;
            sendingWords += ",";
            sendingWords += pumpsControl.Pump1Target.Text;
            sendingWords += ",";
            sendingWords += pumpsControl.Pump2Target.Text;
            sendingWords += ",";
            sendingWords += pumpsControl.Pump3Target.Text;
            sendingWords += ",";
            sendingWords += pumpsControl.Pump4Target.Text;
            sendingWords += ",";
            sendingWords += Convert.ToString(Pump1Fill1ButtonPressDuration);
            sendingWords += ",";
            sendingWords += Convert.ToString(Pump2FillButtonPressDuration);
            sendingWords += ",";
            sendingWords += Convert.ToString(Pump3FillButtonPressDuration);
            sendingWords += ",";
            sendingWords += Convert.ToString(Pump4FillButtonPressDuration);
            sendingWords += ",";
            sendingWords += Convert.ToString(Pump1EmptyButtonPressDuration);
            sendingWords += ",";
            sendingWords += Convert.ToString(Pump2EmptyButtonPressDuration);
            sendingWords += ",";
            sendingWords += Convert.ToString(Pump3EmptyButtonPressDuration);
            sendingWords += ",";
            sendingWords += Convert.ToString(Pump4EmptyButtonPressDuration);
            sendingWords += ",";
            sendingWords += openAutoWindow.Pump1Fill.Text;
            sendingWords += ",";
            sendingWords += openAutoWindow.Pump2Fill.Text;
            sendingWords += ",";
            sendingWords += openAutoWindow.Pump3Fill.Text;
            sendingWords += ",";
            sendingWords += openAutoWindow.Pump4Fill.Text;
            sendingWords += ",";
            sendingWords += openAutoWindow.Pump1Empty.Text;
            sendingWords += ",";
            sendingWords += openAutoWindow.Pump2Empty.Text;
            sendingWords += ",";
            sendingWords += openAutoWindow.Pump3Empty.Text;
            sendingWords += ",";
            sendingWords += openAutoWindow.Pump4Empty.Text;
            sendingWords += "\n";

            label2.Content = sendingWords;

            port.Write(sendingWords);


        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            if (port.IsOpen)
            {
                LineSend("line");
            }           
            else
            {
                MessageBox.Show("Port is not open");
            }
        }



        // Buton basma sürelerini tutar
        public static double Pump1Fill1ButtonPressDuration { get; set; }
        public static double Pump2FillButtonPressDuration { get; set; }
        public static double Pump3FillButtonPressDuration { get; set; }
        public static double Pump4FillButtonPressDuration { get; set; }
        public static double Pump1EmptyButtonPressDuration { get; set; }
        public static double Pump2EmptyButtonPressDuration { get; set; }
        public static double Pump3EmptyButtonPressDuration { get; set; }
        public static double Pump4EmptyButtonPressDuration { get; set; }
        public static double Pump1FillButton { get; set; }

       

        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
            StartClock();
            //SafeAction(() => InitializeArduino("COM3"));
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // 1 saniyelik aralıklarla güncellenir
            timer.Tick += Timer_Tick;
            time = TimeSpan.Zero;
            this.Height = SystemParameters.PrimaryScreenHeight;
            mainControl = new MainControl(this); // MainControl'ü burada bir kez oluşturun
            extendedControl = new ExtendedControl(this); // ExtendedControl'ü burada bir kez oluşturun
            exitGasControl = new ExitGasControl(this); // ExitGasControl'ü burada bir kez oluşturun
            editViewControl = new EditViewControl(this); // EditViewControl'ü burada bir kez oluşturun
            favouritesControl = new FavouritesControl(this);
            pumpsControl = new PumpsControl(this); // PumpsControl'ü burada bir kez oluşturun
            openAutoWindow = new OpenAutoWindow(this); // OpenAutoWindow'ü burada bir kez oluşturun

            string connectionType = Properties.Settings.Default.ConnectionType;
            string configuration = Properties.Settings.Default.Configuration;
            string ipAddress = Properties.Settings.Default.IpAddress;
            string subnetMask = Properties.Settings.Default.SubnetMask;
            string ssid = Properties.Settings.Default.Ssid;
            string password = Properties.Settings.Default.Password;

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
            pumpsControl.CheckEllipsePositionAndSetButtonVisibility(pumpsControl.ellipse16, pumpsControl.conditionalButtonPump1);
            pumpsControl.CheckEllipsePositionAndSetButtonVisibility(pumpsControl.ellipse17, pumpsControl.conditionalButtonPump2);
            pumpsControl.CheckEllipsePositionAndSetButtonVisibility(pumpsControl.ellipse18, pumpsControl.conditionalButtonPump3);
            pumpsControl.CheckEllipsePositionAndSetButtonVisibility(pumpsControl.ellipse19, pumpsControl.conditionalButtonPump4);
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