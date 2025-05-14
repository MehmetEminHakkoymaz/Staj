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
using WpfApp1.Models;
using WpfApp1.Data;
using WpfApp1.Settings;
using WpfApp1.Settings.SettingWindows;
using System.Diagnostics;



namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer autoHideTimer; // Sınıf seviyesinde timer tanımı
        private DispatcherTimer checkButtonTimer;
        private DispatcherTimer timer;
        private TimeSpan time;
        public MainControl mainControl; // MainControl'ü sınıf değişkeni olarak tanımlayın
        public ExtendedControl extendedControl; // ExtendedControl'ü sınıf değişkeni olarak tanımlayın
        public ExitGasControl exitGasControl; // ExitGasControl'ü burada bir kez oluşturun
        public EditViewControl editViewControl; // EditViewControl'ü burada bir kez oluşturun
        public FavouritesControl favouritesControl; // FavouritesControl'ü burada bir kez oluşturun
        public PumpsControl pumpsControl; // PumpsControl'ü burada bir kez oluşturun
        public OpenAutoWindow openAutoWindow; // OpenAutoWindow'ü burada bir kez oluşturun
        public AdminPanel adminPanel;
        public SystemInfo systemInfo;

        private User _currentUser;

        //database bağlantısı başlangıç
        private string currentTableName;
        private DispatcherTimer logTimer;
        //database bağlantısı bitiş

        SerialPort port;
        int incomingFlag = 0;
        string selectedPort = "";
        int incomingSensState = 0;
        double incommingBatteryVal;

        public MainWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;

            if (user != null) // Kullanıcı null değilse başlat
            {
                InitializeRoleBasedAccess();
                InitializeTimer();
                StartClock();
                //SafeAction(() => InitializeArduino("COM3"));
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += Timer_Tick;
                time = TimeSpan.Zero;

                this.WindowState = WindowState.Maximized;
                this.WindowStyle = WindowStyle.None;
                this.ResizeMode = ResizeMode.NoResize;
                this.Topmost = true;

                mainControl = new MainControl(this);
                extendedControl = new ExtendedControl(this);
                exitGasControl = new ExitGasControl(this);
                editViewControl = new EditViewControl(this);
                favouritesControl = new FavouritesControl(this);
                pumpsControl = new PumpsControl(this);
                openAutoWindow = new OpenAutoWindow(this);

                string connectionType = Properties.Settings.Default.ConnectionType;
                string configuration = Properties.Settings.Default.Configuration;
                string ipAddress = Properties.Settings.Default.IpAddress;
                string subnetMask = Properties.Settings.Default.SubnetMask;
                string ssid = Properties.Settings.Default.Ssid;
                string password = Properties.Settings.Default.Password;

                totalWorkTime = Properties.Settings.Default.TotalWorkTime;
            }

            // AutoHide timer'ını başlat
            autoHideTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            autoHideTimer.Tick += AutoHideTimer_Tick;
        }

        private void AutoHideTimer_Tick(object sender, EventArgs e)
        {
            // Timer'ı durdur
            autoHideTimer.Stop();

            // TopGrid'i yukarı çek
            if (TopGrid.Margin.Top == 0) // Eğer grid aşağıdaysa
            {
                var targetMargin = new Thickness(0, -30, 0, 0);

                ThicknessAnimation marginAnimation = new ThicknessAnimation
                {
                    To = targetMargin,
                    Duration = TimeSpan.FromSeconds(0.2),
                    FillBehavior = FillBehavior.Stop
                };

                marginAnimation.Completed += (s, args) =>
                {
                    TopGrid.Margin = targetMargin;
                    // Image kaynağını güncelle
                    if (sender is Button button)
                    {
                        button.Tag = "pack://application:,,,/WpfApp1;component/images/Down.png";
                    }
                };

                TopGrid.BeginAnimation(MarginProperty, marginAnimation);
            }
        }


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
                SampleButtonText.Text = incommingWords[1] + " - " + incommingWords[2] + " - " + incommingWords[3] + " - " + incommingWords[4] + " - " + incommingWords[5] + " - " + 
                    incommingWords[6] + " - " + incommingWords[7] + " - " + incommingWords[8] + " - " + incommingWords[9] + " - " + incommingWords[10] + " - " + 
                    incommingWords[11] + " - " + incommingWords[12] + " - " + incommingWords[13] + " - " + incommingWords[14] + " - " + incommingWords[15] + " - " + 
                    incommingWords[16] + " - " + incommingWords[17] + " - " + incommingWords[18] + " - " + incommingWords[19];

                mainControl.TemperatureValue.Content = incommingWords[1];
                mainControl.StirrerValue.Content = incommingWords[2];
                mainControl.pHValue.Content = incommingWords[3];
                mainControl.pO2Value.Content = incommingWords[4];
                //mainControl.Gas1Value.Content = incommingWords[5];
                //mainControl.Gas2Value.Content = incommingWords[6];
                //mainControl.Gas3Value.Content = incommingWords[7];
                //mainControl.Gas4Value.Content = incommingWords[8];
                //mainControl.FoamValue.Content = incommingWords[9];
                extendedControl.TurbidityValue.Content = incommingWords[10];
                extendedControl.BalanceValue.Content = incommingWords[11];
                extendedControl.AirFlowValue.Content = incommingWords[12];
                extendedControl.Gas2Value.Content = incommingWords[13];
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
            //sendingWords += mainControl.Gas1Target.Text;
            //sendingWords += ",";
            //sendingWords += mainControl.Gas2Target.Text;
            //sendingWords += ",";
            //sendingWords += mainControl.Gas3Target.Text;
            //sendingWords += ",";
            //sendingWords += mainControl.Gas4Target.Text;
            //sendingWords += ",";
            //sendingWords += mainControl.FoamTarget.Text;
            //sendingWords += ",";
            sendingWords += extendedControl.TurbidityTarget.Text;
            sendingWords += ",";
            sendingWords += extendedControl.BalanceTarget.Text;
            sendingWords += ",";
            sendingWords += extendedControl.AirFlowTarget.Text;
            sendingWords += ",";
            sendingWords += extendedControl.Gas2Target.Text;
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

            SampleButtonText.Text = sendingWords;

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

        private TimeSpan totalWorkTime;



        private void Timer_Tick(object sender, EventArgs e)
        {
            // Zamanı güncelle ve göster
            time = time.Add(TimeSpan.FromSeconds(1));
            clockTextBlock.Text = time.ToString(@"hh\:mm\:ss");

            // Settings'den mevcut toplam süreyi al
            TimeSpan totalTime = Properties.Settings.Default.TotalWorkTime;

            // Geçerli çalışma süresini ekle
            TimeSpan currentTotalTime = totalTime.Add(time);


            // Eğer Settings penceresi açıksa SystemInfo'yu güncelle
            var settingsWindow = SettingsWindow.Instance;
            if (settingsWindow?.SystemInfoControl != null)
            {
                try
                {
                    settingsWindow.SystemInfoControl.UpdateDisplays(currentTotalTime);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating SystemInfo: {ex.Message}");
                }
            }
        }

        private int CalculateNextMaintenance(int currentHours)
        {
            if (currentHours < 1000)
                return 1000;
            else if (currentHours < 3000)
                return 3000;
            else
            {
                int maintenanceCount = (int)((currentHours - 3000) / 4000);
                int nextMaintenanceCount = maintenanceCount + 1;
                return 3000 + (nextMaintenanceCount * 4000);
            }
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
            try
            {
                // Eğer contentArea'da şu anda EditViewControl varsa
                if (contentArea.Content is EditViewControl)
                {
                    // FavouritesControl'e yönlendir
                    contentArea.Content = favouritesControl;
                    return;
                }

                // Eğer EditViewControl açık değilse, normal işleme devam et
                contentArea.Content = editViewControl;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error switching views: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            if (sender is Button button)
            {
                Thickness targetMargin;
                bool isExpanded = TopGrid.Margin.Top == 0;

                // Mevcut timer'ı durdur
                autoHideTimer.Stop();

                if (isExpanded)
                {
                    targetMargin = new Thickness(0, -30, 0, 0);
                    button.Tag = "pack://application:,,,/WpfApp1;component/images/Down.png";
                }
                else
                {
                    targetMargin = new Thickness(0, 0, 0, 0);
                    button.Tag = "pack://application:,,,/WpfApp1;component/images/Up.png";
                    // Grid aşağı indiğinde timer'ı başlat
                    autoHideTimer.Start();
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
            if (_currentUser.Role == "admin")
            {
                // Yeni pencere örneği oluştur (namespace'i dikkate alarak)
                var settingsWindow = new WpfApp1.Settings.SettingsWindow();

                // Pencerenin boyutunu ayarla
                settingsWindow.Width = 1024;
                settingsWindow.Height = 600;

                // Pencereyi göster
                settingsWindow.Show();
            }
            else
            {
                MessageBox.Show("You do not have permission to access this page.");
            }
        }

        private void FirstStartButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if at least one parameter is enabled (value equals 1)
            bool atLeastOneEnabled =
                Properties.Settings.Default.TemperatureEllipse == 1 ||
                Properties.Settings.Default.StirrerEllipse == 1 ||
                Properties.Settings.Default.pHEllipse == 1 ||
                Properties.Settings.Default.pO2Ellipse == 1 ||
                Properties.Settings.Default.FoamEllipse == 1 ||
                Properties.Settings.Default.RedoxEllipse == 1 ||
                Properties.Settings.Default.TurbidityEllipse == 1 ||
                Properties.Settings.Default.BalanceEllipse == 1 ||
                Properties.Settings.Default.AirFlowEllipse == 1 ||
                Properties.Settings.Default.Gas2Ellipse == 1 ||
                Properties.Settings.Default.ExitGasO2Ellipse == 1 ||
                Properties.Settings.Default.ExitGasCO2Ellipse == 1 ||
                Properties.Settings.Default.Pump1Ellipse == 1 ||
                Properties.Settings.Default.Pump2Ellipse == 1 ||
                Properties.Settings.Default.Pump3Ellipse == 1 ||
                Properties.Settings.Default.Pump4Ellipse == 1 ||
                Properties.Settings.Default.Pump5Ellipse == 1 ||
                Properties.Settings.Default.Pump6Ellipse == 1;

            // If no parameter is enabled, show error message and return
            if (!atLeastOneEnabled)
            {
                MessageBox.Show("En az bir parametre aktif edilmelidir.",
                    "Ayar Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Properties.Settings.Default.StartButton = 1;
            //DATABASE BAĞLANTISI GİRİŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞ
            // Tablo adını al
            var dialog = new TableNameDialog();
            if (dialog.ShowDialog() != true)
            {
                return;
            }

            currentTableName = dialog.TableName;

            // Yeni tablo oluştur
            DatabaseHelper.CreateProjectTable(currentTableName);

            // Debug için tablo yapısını kontrol et
            DatabaseHelper.DebugTableStructure(currentTableName);

            // Sample sayacını sıfırla
            sampleCount = 1;
            SampleButtonText.Text = "1.Sample";

            // Log timer'ı başlat
            logTimer = new DispatcherTimer();
            logTimer.Interval = TimeSpan.FromMinutes(1);
            logTimer.Tick += LogTimer_Tick;
            logTimer.Start();
            //DATABASE BAĞLANTISI ÇIKIŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞ

            FirstStartButton.Visibility = Visibility.Collapsed;
            StartButton.Visibility = Visibility.Visible;
            StartButton.Opacity = 0.5;
            StartButton.IsEnabled = false;

            // Main sayfası kontrolleri
            mainControl.CheckEllipsePositionAndSetButtonVisibility(mainControl.ellipse1, mainControl.conditionalButtonTemperature);
            mainControl.CheckEllipsePositionAndSetButtonVisibility(mainControl.ellipse2, mainControl.conditionalButtonStirrer);
            mainControl.CheckEllipsePositionAndSetButtonVisibility(mainControl.ellipse3, mainControl.conditionalButtonpH);
            mainControl.CheckEllipsePositionAndSetButtonVisibility(mainControl.ellipse4, mainControl.conditionalButtonpO2);
            mainControl.CheckEllipsePositionAndSetButtonVisibility(mainControl.ellipse9, mainControl.conditionalButtonFoam);
            mainControl.CheckEllipsePositionAndSetButtonVisibility(mainControl.ellipse19, mainControl.conditionalButtonRedox);
            extendedControl.CheckEllipsePositionAndSetButtonVisibility(extendedControl.ellipse10, extendedControl.conditionalButtonTurbidity);
            extendedControl.CheckEllipsePositionAndSetButtonVisibility(extendedControl.ellipse11, extendedControl.conditionalButtonBalance);
            extendedControl.CheckEllipsePositionAndSetButtonVisibility(extendedControl.ellipse12, extendedControl.conditionalButtonAirFlow);
            extendedControl.CheckEllipsePositionAndSetButtonVisibility(extendedControl.ellipse13, extendedControl.conditionalButtonGas2);

        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!StartButton.IsEnabled)
            {
                MessageBox.Show("Parameters are not same as target value.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            time = TimeSpan.Zero; // Sayacı sıfırla
            timer.Start();

            Console.WriteLine("StartButton clicked, checking logTimer..."); // Debug log
            // LogTimer'ı yeniden başlat
            if (logTimer != null)
            {
                Console.WriteLine("LogTimer exists, restarting..."); // Debug log
                logTimer.Stop();
                logTimer.Start();

                // İlk veriyi hemen kaydet
                Console.WriteLine("Attempting to log first data..."); // Debug log
                LogTimer_Tick(null, null);
            }
            else
            {
                Console.WriteLine("LogTimer is null!"); // Debug log
            }
            //BURAYA BAKKKKKKKK
            //BURAYA BAKKKKKKKK
            //BURAYA BAKKKKKKKK
            //BURAYA BAKKKKKKKK
            //BURAYA BAKKKKKKKK
            //BURAYA BAKKKKKKKK
            //BURAYA BAKKKKKKKK
            //BURAYA BAKKKKKKKK
            //BURAYA BAKKKKKKKK
            //BURAYA BAKKKKKKKK
            //BURAYA BAKKKKKKKK
            //BURAYA BAKKKKKKKK
            //BURAYA BAKKKKKKKK
            //BURAYA BAKKKKKKKK
            //BURAYA BAKKKKKKKK
            //BURAYA BAKKKKKKKK
            //BURAYA BAKKKKKKKK
            //BURAYA BAKKKKKKKK
            //BURAYA BAKKKKKKKK
            //BURAYA BAKKKKKKKK
            //BURAYA BAKKKKKKKK
            //BURAYA BAKKKKKKKK

            Properties.Settings.Default.StartButton = 2;
            //Properties.Settings.Default.TemperatureConditionalButtonVisibility = 0;
            //Properties.Settings.Default.StirrerConditionalButtonVisibility = 0;
            //Properties.Settings.Default.pHConditionalButtonVisibility = 0;
            //Properties.Settings.Default.pO2ConditionalButtonVisibility = 0;
            //Properties.Settings.Default.FoamConditionalButtonVisibility = 0;
            //Properties.Settings.Default.RedoxConditionalButtonVisibility = 0;
            Properties.Settings.Default.Save();
            mainControl.CheckComparisonTimer();

            StartButton.Visibility = Visibility.Collapsed;
            StopButton.Visibility = Visibility.Visible;
            SampleButton.Visibility = Visibility.Visible;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Timer'ları durdur
                timer?.Stop();

                if (logTimer != null && logTimer.IsEnabled)
                {
                    // Son bir kayıt al
                    LogTimer_Tick(null, null);
                    logTimer.Stop();
                }

                // Mevcut çalışma süresini TimeSpan'e çevir
                if (TimeSpan.TryParse(clockTextBlock.Text, out TimeSpan currentTime))
                {
                    // Settings'den mevcut toplam süreyi al
                    TimeSpan totalTime = Properties.Settings.Default.TotalWorkTime;

                    // Yeni süreyi ekle
                    totalTime = totalTime.Add(currentTime);

                    // Settings'e kaydet
                    Properties.Settings.Default.TotalWorkTime = totalTime;
                    Properties.Settings.Default.Save();

                    // SystemInfo'yu güncelle
                    var settingsWindow = SettingsWindow.Instance;
                    if (settingsWindow?.SystemInfoControl != null)
                    {
                        settingsWindow.SystemInfoControl.TotalWorkHours.Text =
                            $"{totalTime.Days}d {totalTime.Hours}h {totalTime.Minutes}m {totalTime.Seconds}s";
                    }
                }

                // Sayacı sıfırla
                time = TimeSpan.Zero;
                clockTextBlock.Text = "00:00:00";

                // Sample sayacını sıfırla
                sampleCount = 1;
                SampleButtonText.Text = "1.Sample";

                // Görünürlükleri güncelle
                StopButton.Visibility = Visibility.Collapsed;
                FirstStartButton.Visibility = Visibility.Visible;
                SampleButton.Visibility = Visibility.Collapsed;
                //BURAYA BAKKKKKKKK
                //BURAYA BAKKKKKKKK
                //BURAYA BAKKKKKKKK
                //BURAYA BAKKKKKKKK
                //BURAYA BAKKKKKKKK
                //BURAYA BAKKKKKKKK
                //BURAYA BAKKKKKKKK
                //BURAYA BAKKKKKKKK
                //BURAYA BAKKKKKKKK
                //BURAYA BAKKKKKKKK
                Properties.Settings.Default.StartButton = 0;
                Properties.Settings.Default.TemperatureConditionalButton = 0;
                Properties.Settings.Default.StirrerConditionalButton = 0;
                Properties.Settings.Default.pHConditionalButton = 0;
                Properties.Settings.Default.pO2ConditionalButton = 0;
                Properties.Settings.Default.FoamConditionalButton = 0;
                Properties.Settings.Default.RedoxConditionalButton = 0;
                Properties.Settings.Default.TurbidityConditionalButton = 0;
                Properties.Settings.Default.BalanceConditionalButton = 0;
                Properties.Settings.Default.AirFlowConditionalButton = 0;
                Properties.Settings.Default.Gas2ConditionalButton = 0;

                Properties.Settings.Default.TemperatureEllipse = 0;
                Properties.Settings.Default.StirrerEllipse = 0;
                Properties.Settings.Default.pHEllipse = 0;
                Properties.Settings.Default.pO2Ellipse = 0;
                Properties.Settings.Default.FoamEllipse = 0;
                Properties.Settings.Default.RedoxEllipse = 0;
                Properties.Settings.Default.TurbidityEllipse = 0;
                Properties.Settings.Default.BalanceEllipse = 0;
                Properties.Settings.Default.AirFlowEllipse = 0;
                Properties.Settings.Default.Gas2Ellipse = 0;



                
                mainControl.CheckComparisonTimer();
                Properties.Settings.Default.Save();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving total work time: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        //sample button oalyısı



        private int sampleCount = 1;

        private void SampleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var values = new Dictionary<string, string>();

                // Sample değerini butonun mevcut text'i ile doldur
                values["Sample"] = SampleButtonText.Text;

                // Diğer değerleri ekle
                values["Username"] = _currentUser?.Username;
                values["DateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                values["ElapsedTime"] = clockTextBlock?.Text;
                values["VesselType"] = Properties.Settings.Default.SelectedVesselType.ToString();
                values["TemperatureValue"] = mainControl?.TemperatureValue?.Content?.ToString();
                values["TemperatureTarget"] = mainControl?.TemperatureTarget?.Text;
                values["StirrerValue"] = mainControl?.StirrerValue?.Content?.ToString();
                values["StirrerTarget"] = mainControl?.StirrerTarget?.Text;
                values["pHValue"] = mainControl?.pHValue?.Content?.ToString();
                values["pHTarget"] = mainControl?.pHTarget?.Text;
                values["pO2Value"] = mainControl?.pO2Value?.Content?.ToString();
                values["pO2Target"] = mainControl?.pO2Target?.Text;
                //values["Gas1Value"] = mainControl?.Gas1Value?.Content?.ToString();
                //values["Gas1Target"] = mainControl?.Gas1Target?.Text;
                //values["Gas2Value"] = mainControl?.Gas2Value?.Content?.ToString();
                //values["Gas2Target"] = mainControl?.Gas2Target?.Text;
                //values["Gas3Value"] = mainControl?.Gas3Value?.Content?.ToString();
                //values["Gas3Target"] = mainControl?.Gas3Target?.Text;
                //values["Gas4Value"] = mainControl?.Gas4Value?.Content?.ToString();
                //values["Gas4Target"] = mainControl?.Gas4Target?.Text;
                //values["FoamValue"] = mainControl?.FoamValue?.Content?.ToString();
                //values["FoamTarget"] = mainControl?.FoamTarget?.Text;
                values["RedoxValue"] = mainControl?.RedoxValue?.Content?.ToString();
                values["RedoxTarget"] = mainControl?.RedoxTarget?.Text;
                values["TurbidityValue"] = extendedControl?.TurbidityValue?.Content?.ToString();
                values["TurbidityTarget"] = extendedControl?.TurbidityTarget?.Text;
                values["BalanceValue"] = extendedControl?.BalanceValue?.Content?.ToString();
                values["BalanceTarget"] = extendedControl?.BalanceTarget?.Text;
                values["AirFlowValue"] = extendedControl?.AirFlowValue?.Content?.ToString();
                values["AirFlowTarget"] = extendedControl?.AirFlowTarget?.Text;
                values["Gas2FlowValue"] = extendedControl?.Gas2Value?.Content?.ToString();
                values["Gas2FlowTarget"] = extendedControl?.Gas2Target?.Text;
                //values["Gas3FlowValue"] = extendedControl?.Gas3FlowValue?.Content?.ToString();
                //values["Gas3FlowTarget"] = extendedControl?.Gas3FlowTarget?.Text;
                //values["Gas4FlowValue"] = extendedControl?.Gas4FlowValue?.Content?.ToString();
                //values["Gas4FlowTarget"] = extendedControl?.Gas4FlowTarget?.Text;
                //values["Gas5FlowValue"] = extendedControl?.Gas5FlowValue?.Content?.ToString();
                //values["Gas5FlowTarget"] = extendedControl?.Gas5FlowTarget?.Text;
                values["ExitTurbidityValue"] = exitGasControl?.ExitTurbidityValue?.Content?.ToString();
                values["ExitTurbidityTarget"] = exitGasControl?.ExitTurbidityTarget?.Text;
                values["ExitBalanceValue"] = exitGasControl?.ExitBalanceValue?.Content?.ToString();
                values["ExitBalanceTarget"] = exitGasControl?.ExitBalanceTarget?.Text;
                values["Pump1Value"] = pumpsControl?.Pump1Value?.Content?.ToString();
                values["Pump1Target"] = pumpsControl?.Pump1Target?.Text;
                values["Pump2Value"] = pumpsControl?.Pump2Value?.Content?.ToString();
                values["Pump2Target"] = pumpsControl?.Pump2Target?.Text;
                values["Pump3Value"] = pumpsControl?.Pump3Value?.Content?.ToString();
                values["Pump3Target"] = pumpsControl?.Pump3Target?.Text;
                values["Pump4Value"] = pumpsControl?.Pump4Value?.Content?.ToString();
                values["Pump4Target"] = pumpsControl?.Pump4Target?.Text;
                values["Pump5Value"] = pumpsControl?.Pump5Value?.Content?.ToString();
                values["Pump5Target"] = pumpsControl?.Pump5Target?.Text;
                values["Pump6Value"] = pumpsControl?.Pump6Value?.Content?.ToString();
                values["Pump6Target"] = pumpsControl?.Pump6Target?.Text;
                // Verileri kaydet
                DatabaseHelper.LogData(currentTableName, values);

                // Sample sayısını artır
                sampleCount++;

                // Butonun text'ini güncelle
                SampleButtonText.Text = $"{sampleCount}.Sample";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving sample: {ex.Message}");
            }
        }



        //DATABASE BAĞLANTISI GİRİŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞ
        private void LogTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(currentTableName))
                {
                    Console.WriteLine("Tablo adı boş!");
                    return;
                }

                var values = new Dictionary<string, string>();

                // Her alan için varsayılan boş değer atayalım
                void AddValue(string key, object value)
                {
                    values[key] = value?.ToString() ?? ""; // Null ise boş string ata
                }
                // Sample sütununu boş bırak
                values["Sample"] = "";  // Her log kaydında boş olacak
                // Tüm alanları ekle (null olsa bile)
                AddValue("Username", _currentUser?.Username);
                AddValue("DateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                AddValue("ElapsedTime", clockTextBlock?.Text);
                AddValue("VesselType", Properties.Settings.Default.SelectedVesselType);
                AddValue("TemperatureValue", mainControl?.TemperatureValue?.Content);
                AddValue("TemperatureTarget", mainControl?.TemperatureTarget?.Text);
                AddValue("StirrerValue", mainControl?.StirrerValue?.Content);
                AddValue("StirrerTarget", mainControl?.StirrerTarget?.Text);
                AddValue("pHValue", mainControl?.pHValue?.Content);
                AddValue("pHTarget", mainControl?.pHTarget?.Text);
                AddValue("pO2Value", mainControl?.pO2Value?.Content);
                AddValue("pO2Target", mainControl?.pO2Target?.Text);
                //AddValue("Gas1Value", mainControl?.Gas1Value?.Content);
                //AddValue("Gas1Target", mainControl?.Gas1Target?.Text);
                //AddValue("Gas2Value", mainControl?.Gas2Value?.Content);
                //AddValue("Gas2Target", mainControl?.Gas2Target?.Text);
                //AddValue("Gas3Value", mainControl?.Gas3Value?.Content);
                //AddValue("Gas3Target", mainControl?.Gas3Target?.Text);
                //AddValue("Gas4Value", mainControl?.Gas4Value?.Content);
                //AddValue("Gas4Target", mainControl?.Gas4Target?.Text);
                //AddValue("FoamValue", mainControl?.FoamValue?.Content);
                //AddValue("FoamTarget", mainControl?.FoamTarget?.Text);
                AddValue("RedoxValue", mainControl?.RedoxValue?.Content);
                AddValue("RedoxTarget", mainControl?.RedoxTarget?.Text);
                AddValue("TurbidityValue", extendedControl?.TurbidityValue?.Content);
                AddValue("TurbidityTarget", extendedControl?.TurbidityTarget?.Text);
                AddValue("BalanceValue", extendedControl?.BalanceValue?.Content);
                AddValue("BalanceTarget", extendedControl?.BalanceTarget?.Text);
                AddValue("AirFlowValue", extendedControl?.AirFlowValue?.Content);
                AddValue("AirFlowTarget", extendedControl?.AirFlowTarget?.Text);
                AddValue("Gas2FlowValue", extendedControl?.Gas2Value?.Content);
                AddValue("Gas2FlowTarget", extendedControl?.Gas2Target?.Text);
                //AddValue("Gas3FlowValue", extendedControl?.Gas3FlowValue?.Content);
                //AddValue("Gas3FlowTarget", extendedControl?.Gas3FlowTarget?.Text);
                //AddValue("Gas4FlowValue", extendedControl?.Gas4FlowValue?.Content);
                //AddValue("Gas4FlowTarget", extendedControl?.Gas4FlowTarget?.Text);
                //AddValue("Gas5FlowValue", extendedControl?.Gas5FlowValue?.Content);
                //AddValue("Gas5FlowTarget", extendedControl?.Gas5FlowTarget?.Text);
                AddValue("ExitTurbidityValue", exitGasControl?.ExitTurbidityValue?.Content);
                AddValue("ExitTurbidityTarget", exitGasControl?.ExitTurbidityTarget?.Text);
                AddValue("ExitBalanceValue", exitGasControl?.ExitBalanceValue?.Content);
                AddValue("ExitBalanceTarget", exitGasControl?.ExitBalanceTarget?.Text);
                AddValue("Pump1Value", pumpsControl?.Pump1Value?.Content);
                AddValue("Pump1Target", pumpsControl?.Pump1Target?.Text);
                AddValue("Pump2Value", pumpsControl?.Pump2Value?.Content);
                AddValue("Pump2Target", pumpsControl?.Pump2Target?.Text);
                AddValue("Pump3Value", pumpsControl?.Pump3Value?.Content);
                AddValue("Pump3Target", pumpsControl?.Pump3Target?.Text);
                AddValue("Pump4Value", pumpsControl?.Pump4Value?.Content);
                AddValue("Pump4Target", pumpsControl?.Pump4Target?.Text);
                AddValue("Pump5Value", pumpsControl?.Pump5Value?.Content);
                AddValue("Pump5Target", pumpsControl?.Pump5Target?.Text);
                AddValue("Pump6Value", pumpsControl?.Pump6Value?.Content);
                AddValue("Pump6Target", pumpsControl?.Pump6Target?.Text);

                Console.WriteLine($"Kayıt yapılacak tablo: {currentTableName}");
                Console.WriteLine($"Kayıt edilecek değer sayısı: {values.Count}");

                DatabaseHelper.LogData(currentTableName, values);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LogTimer_Tick'de hata: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                MessageBox.Show($"Veri kaydı sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        //DATABASE BAĞLANTISI ÇIKIŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞŞ

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
                mainControl.conditionalButtonTemperature,
                mainControl.conditionalButtonStirrer,
                mainControl.conditionalButtonpH,
                mainControl.conditionalButtonpO2,
                //mainControl.conditionalButtonGas1,
                //mainControl.conditionalButtonGas2,
                //mainControl.conditionalButtonGas3,
                //mainControl.conditionalButtonGas4,
                mainControl.conditionalButtonFoam,
                extendedControl.conditionalButtonTurbidity,
                extendedControl.conditionalButtonBalance,
                extendedControl.conditionalButtonAirFlow,
                extendedControl.conditionalButtonGas2,
                //extendedControl.conditionalButtonGas3Flow,
                //extendedControl.conditionalButtonGas4Flow,
                //extendedControl.conditionalButtonGas5Flow,
                exitGasControl.conditionalButtonTurbidity,
                exitGasControl.conditionalButtonBalance))
            {
                StartButton.Opacity = 1.0;
                StartButton.IsEnabled = true;
            }
        }



        private void Admin_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUser.Role == "admin")
            {
                adminPanel = new AdminPanel(this);
                adminPanel.Show();
            }
            else
            {
                MessageBox.Show("You do not have permission to access this page.");
            }
        }


        private void InitializeRoleBasedAccess()
        {
            // Örnek: Admin olmayan kullanıcılar için AdminPanel butonunu gizle
            if (_currentUser.Role != "admin")
            {
                //AdminPanelButton.Visibility = Visibility.Collapsed;
                SettingsButtonClk.Visibility = Visibility.Collapsed;
            }
        }
        public void UpdateCurrentUser(User user)
        {
            _currentUser = user;
            InitializeRoleBasedAccess();
        }

        private void Logout_Button_Click(object sender, RoutedEventArgs e)
        {
            // Yeni login penceresi oluştur
            LoginWindow loginWindow = new LoginWindow();

            // Mevcut pencereyi gizle
            this.Hide();

            // Login penceresini göster
            loginWindow.Show();

            // Login penceresinin Closed olayını dinle
            loginWindow.Closed += (s, args) =>
            {
                if (loginWindow.LoggedInUser != null) // Başarılı giriş yapıldıysa
                {
                    _currentUser = loginWindow.LoggedInUser;
                    InitializeRoleBasedAccess();
                    this.Show();
                }
            };
        }


        private void CloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to close the application?",
                "Close Application",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Properties.Settings.Default.StartButton = 0;
                Properties.Settings.Default.TemperatureConditionalButton = 0;
                Properties.Settings.Default.StirrerConditionalButton = 0;
                Properties.Settings.Default.pHConditionalButton = 0;
                Properties.Settings.Default.pO2ConditionalButton = 0;
                Properties.Settings.Default.FoamConditionalButton = 0;
                Properties.Settings.Default.RedoxConditionalButton = 0;
                Properties.Settings.Default.TurbidityConditionalButton = 0;
                Properties.Settings.Default.BalanceConditionalButton = 0;
                Properties.Settings.Default.AirFlowConditionalButton = 0;
                Properties.Settings.Default.Gas2ConditionalButton = 0;

                Properties.Settings.Default.TemperatureEllipse = 0;
                Properties.Settings.Default.StirrerEllipse = 0;
                Properties.Settings.Default.pHEllipse = 0;
                Properties.Settings.Default.pO2Ellipse = 0;
                Properties.Settings.Default.FoamEllipse = 0;
                Properties.Settings.Default.RedoxEllipse = 0;
                Properties.Settings.Default.TurbidityEllipse = 0;
                Properties.Settings.Default.BalanceEllipse = 0;
                Properties.Settings.Default.AirFlowEllipse = 0;
                Properties.Settings.Default.Gas2Ellipse = 0;
                Properties.Settings.Default.Save();
                CloseApplication();
            }
        }

        private void ShutdownButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to shutdown the computer?",
                "Shutdown Computer",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                // Önce uygulamayı düzgün şekilde kapatalım
                CloseApplication();

                // Bilgisayarı kapat
                try
                {
                    Process.Start("shutdown", "/s /t 0");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to shutdown computer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CloseApplication()
        {
            // Tüm açık portları kapat
            if (port != null && port.IsOpen)
            {
                port.Close();
            }

            // Timer'ları durdur
            if (timer != null)
                timer.Stop();
            if (checkButtonTimer != null)
                checkButtonTimer.Stop();
            if (logTimer != null)
                logTimer.Stop();

            // Uygulamayı kapat
            Application.Current.Shutdown();
        }


    }
}