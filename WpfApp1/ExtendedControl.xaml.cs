using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
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
using WpfApp1;
using Label = System.Windows.Controls.Label;

namespace WpfApp1
{
    public partial class ExtendedControl : UserControl, INotifyPropertyChanged
    {
        private MainWindow mainWindow;

        public ExtendedControl(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            //conditionalButtonTurbidity düğmesinin başlangıç görünürlüğünü ve rengini ayarla
            if (Properties.Settings.Default.TurbidityEllipse == 1 &&
                Properties.Settings.Default.StartButton == 1 &&
                Properties.Settings.Default.TurbidityConditionalButtonVisibility == 1)
            {
                conditionalButtonTurbidity.Visibility = Visibility.Visible;

                // Butonun rengini Settings'e göre ayarla
                switch (Properties.Settings.Default.TurbidityConditionalButton)
                {
                    case 0: // Kırmızı
                        conditionalButtonTurbidity.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1: // Sarı
                        conditionalButtonTurbidity.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2: // Yeşil
                        conditionalButtonTurbidity.Background = new SolidColorBrush(Colors.Green);
                        break;
                    default:
                        conditionalButtonTurbidity.Background = new SolidColorBrush(Colors.Red);
                        break;
                }
            }
            else
            {
                conditionalButtonTurbidity.Visibility = Visibility.Collapsed;
            }

            // Click event handler'ı conditionalButtonpO2'ye bağla
            conditionalButtonTurbidity.Click -= conditionalButtonTurbidity_Click; // Önce eski bağlantıyı kaldır
            conditionalButtonTurbidity.Click += conditionalButtonTurbidity_Click; // Yeni bağlantıyı ekle


            //conditionalButtonBalance düğmesinin başlangıç görünürlüğünü ve rengini ayarla
            if (Properties.Settings.Default.BalanceEllipse == 1 &&
                Properties.Settings.Default.StartButton == 1 &&
                Properties.Settings.Default.BalanceConditionalButtonVisibility == 1)
            {
                conditionalButtonBalance.Visibility = Visibility.Visible;

                // Butonun rengini Settings'e göre ayarla
                switch (Properties.Settings.Default.BalanceConditionalButton)
                {
                    case 0: // Kırmızı
                        conditionalButtonBalance.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1: // Sarı
                        conditionalButtonBalance.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2: // Yeşil
                        conditionalButtonBalance.Background = new SolidColorBrush(Colors.Green);
                        break;
                    default:
                        conditionalButtonBalance.Background = new SolidColorBrush(Colors.Red);
                        break;
                }
            }
            else
            {
                conditionalButtonBalance.Visibility = Visibility.Collapsed;
            }

            // Click event handler'ı conditionalButtonpO2'ye bağla
            conditionalButtonBalance.Click -= conditionalButtonBalance_Click; // Önce eski bağlantıyı kaldır
            conditionalButtonBalance.Click += conditionalButtonBalance_Click; // Yeni bağlantıyı ekle


            //conditionalButtonAirFlow düğmesinin başlangıç görünürlüğünü ve rengini ayarla
            if (Properties.Settings.Default.AirFlowEllipse == 1 &&
                Properties.Settings.Default.StartButton == 1 &&
                Properties.Settings.Default.AirFlowConditionalButtonVisibility == 1)
            {
                conditionalButtonAirFlow.Visibility = Visibility.Visible;

                // Butonun rengini Settings'e göre ayarla
                switch (Properties.Settings.Default.AirFlowConditionalButton)
                {
                    case 0: // Kırmızı
                        conditionalButtonAirFlow.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1: // Sarı
                        conditionalButtonAirFlow.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2: // Yeşil
                        conditionalButtonAirFlow.Background = new SolidColorBrush(Colors.Green);
                        break;
                    default:
                        conditionalButtonAirFlow.Background = new SolidColorBrush(Colors.Red);
                        break;
                }
            }
            else
            {
                conditionalButtonAirFlow.Visibility = Visibility.Collapsed;
            }

            // Click event handler'ı conditionalButtonpO2'ye bağla
            conditionalButtonAirFlow.Click -= conditionalButtonAirFlow_Click; // Önce eski bağlantıyı kaldır
            conditionalButtonAirFlow.Click += conditionalButtonAirFlow_Click; // Yeni bağlantıyı ekle

            //conditionalButtonGas2 düğmesinin başlangıç görünürlüğünü ve rengini ayarla
            if (Properties.Settings.Default.Gas2Ellipse == 1 &&
                Properties.Settings.Default.StartButton == 1 &&
                Properties.Settings.Default.Gas2ConditionalButtonVisibility == 1)
            {
                conditionalButtonGas2.Visibility = Visibility.Visible;

                // Butonun rengini Settings'e göre ayarla
                switch (Properties.Settings.Default.Gas2ConditionalButton)
                {
                    case 0: // Kırmızı
                        conditionalButtonGas2.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1: // Sarı
                        conditionalButtonGas2.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2: // Yeşil
                        conditionalButtonGas2.Background = new SolidColorBrush(Colors.Green);
                        break;
                    default:
                        conditionalButtonGas2.Background = new SolidColorBrush(Colors.Red);
                        break;
                }
            }
            else
            {
                conditionalButtonGas2.Visibility = Visibility.Collapsed;
            }

            // Click event handler'ı conditionalButtonpO2'ye bağla
            conditionalButtonGas2.Click -= conditionalButtonGas2_Click; // Önce eski bağlantıyı kaldır
            conditionalButtonGas2.Click += conditionalButtonGas2_Click; // Yeni bağlantıyı ekle

            // Load saved text box values
            LoadTextBoxValues();

            // Register TextChanged events
            RegisterTextChangedEvents();


            ellipse10.MouseLeftButtonDown += ellipse10_MouseLeftButtonDown;
            ellipse11.MouseLeftButtonDown += ellipse11_MouseLeftButtonDown;
            ellipse12.MouseLeftButtonDown += ellipse12_MouseLeftButtonDown;
            ellipse13.MouseLeftButtonDown += ellipse13_MouseLeftButtonDown; 
            //ellipse16.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            //ellipse17.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            //ellipse18.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            KeypadControl.ValueSelected += KeyPadControl_ValueSelected;
            comparisonTimer.Interval = TimeSpan.FromSeconds(1); // 1 saniyelik aralıklarla
            comparisonTimer.Tick += ComparisonTimer_Tick; // Zamanlayıcı olayı
            DataContext = this;
            InitializeBalanceMonitoring();
            Properties.Settings.Default.PropertyChanged += Settings_PropertyChanged;
            UpdateBorderVisibilities();
            CompareGas2Values();

        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Make sure text boxes are loaded with saved values
            LoadTextBoxValues();

            // Set ellipse10 position based on BalanceEllipse setting
            Canvas canvas10 = ellipse10.Parent as Canvas;
            if (canvas10 != null)
            {
                double canvasWidth = canvas10.ActualWidth;
                double ellipseWidth = ellipse10.ActualWidth;
                double maxRight = canvasWidth - ellipseWidth - 12;

                // If BalanceEllipse is 10, position ellipse to the right
                if (Properties.Settings.Default.BalanceEllipse == 10)
                {
                    Canvas.SetLeft(ellipse10, maxRight);
                    ellipse10.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101"));
                }
                else
                {
                    Canvas.SetLeft(ellipse10, 6);
                    ellipse10.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF"));
                }
            }
            //conditionalButtonTurbidity düğmesinin başlangıç görünürlüğünü ve rengini ayarla
            if (Properties.Settings.Default.TurbidityEllipse == 1 &&
                Properties.Settings.Default.StartButton == 1 &&
                Properties.Settings.Default.TurbidityConditionalButtonVisibility == 1)
            {
                conditionalButtonTurbidity.Visibility = Visibility.Visible;

                // Butonun rengini Settings'e göre ayarla
                switch (Properties.Settings.Default.TurbidityConditionalButton)
                {
                    case 0: // Kırmızı
                        conditionalButtonTurbidity.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1: // Sarı
                        conditionalButtonTurbidity.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2: // Yeşil
                        conditionalButtonTurbidity.Background = new SolidColorBrush(Colors.Green);
                        break;
                    default:
                        conditionalButtonTurbidity.Background = new SolidColorBrush(Colors.Red);
                        break;
                }
            }
            else
            {
                conditionalButtonTurbidity.Visibility = Visibility.Collapsed;
            }

            // Click event handler'ı conditionalButtonpO2'ye bağla
            conditionalButtonTurbidity.Click -= conditionalButtonTurbidity_Click; // Önce eski bağlantıyı kaldır
            conditionalButtonTurbidity.Click += conditionalButtonTurbidity_Click; // Yeni bağlantıyı ekle

            // Set ellipse11 position based on BalanceEllipse setting
            Canvas canvas11 = ellipse11.Parent as Canvas;
            if (canvas11 != null)
            {
                double canvasWidth = canvas11.ActualWidth;
                double ellipseWidth = ellipse11.ActualWidth;
                double maxRight = canvasWidth - ellipseWidth - 12;

                // If BalanceEllipse is 11, position ellipse to the right
                if (Properties.Settings.Default.BalanceEllipse == 11)
                {
                    Canvas.SetLeft(ellipse11, maxRight);
                    ellipse11.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101"));
                }
                else
                {
                    Canvas.SetLeft(ellipse11, 6);
                    ellipse11.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF"));
                }
            }
            //conditionalButtonBalance düğmesinin başlangıç görünürlüğünü ve rengini ayarla
            if (Properties.Settings.Default.BalanceEllipse == 1 &&
                Properties.Settings.Default.StartButton == 1 &&
                Properties.Settings.Default.BalanceConditionalButtonVisibility == 1)
            {
                conditionalButtonBalance.Visibility = Visibility.Visible;

                // Butonun rengini Settings'e göre ayarla
                switch (Properties.Settings.Default.BalanceConditionalButton)
                {
                    case 0: // Kırmızı
                        conditionalButtonBalance.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1: // Sarı
                        conditionalButtonBalance.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2: // Yeşil
                        conditionalButtonBalance.Background = new SolidColorBrush(Colors.Green);
                        break;
                    default:
                        conditionalButtonBalance.Background = new SolidColorBrush(Colors.Red);
                        break;
                }
            }
            else
            {
                conditionalButtonBalance.Visibility = Visibility.Collapsed;
            }

            // Click event handler'ı conditionalButtonpO2'ye bağla
            conditionalButtonBalance.Click -= conditionalButtonBalance_Click; // Önce eski bağlantıyı kaldır
            conditionalButtonBalance.Click += conditionalButtonBalance_Click; // Yeni bağlantıyı ekle

            // Set ellipse12 position based on BalanceEllipse setting
            Canvas canvas12 = ellipse12.Parent as Canvas;
            if (canvas12 != null)
            {
                double canvasWidth = canvas12.ActualWidth;
                double ellipseWidth = ellipse12.ActualWidth;
                double maxRight = canvasWidth - ellipseWidth - 12;

                // If BalanceEllipse is 12, position ellipse to the right
                if (Properties.Settings.Default.BalanceEllipse == 12)
                {
                    Canvas.SetLeft(ellipse12, maxRight);
                    ellipse12.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101"));
                }
                else
                {
                    Canvas.SetLeft(ellipse12, 6);
                    ellipse12.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF"));
                }
            }
            //conditionalButtonAirFlow düğmesinin başlangıç görünürlüğünü ve rengini ayarla
            if (Properties.Settings.Default.AirFlowEllipse == 1 &&
                Properties.Settings.Default.StartButton == 1 &&
                Properties.Settings.Default.AirFlowConditionalButtonVisibility == 1)
            {
                conditionalButtonAirFlow.Visibility = Visibility.Visible;

                // Butonun rengini Settings'e göre ayarla
                switch (Properties.Settings.Default.AirFlowConditionalButton)
                {
                    case 0: // Kırmızı
                        conditionalButtonAirFlow.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1: // Sarı
                        conditionalButtonAirFlow.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2: // Yeşil
                        conditionalButtonAirFlow.Background = new SolidColorBrush(Colors.Green);
                        break;
                    default:
                        conditionalButtonAirFlow.Background = new SolidColorBrush(Colors.Red);
                        break;
                }
            }
            else
            {
                conditionalButtonAirFlow.Visibility = Visibility.Collapsed;
            }

            // Click event handler'ı conditionalButtonpO2'ye bağla
            conditionalButtonAirFlow.Click -= conditionalButtonAirFlow_Click; // Önce eski bağlantıyı kaldır
            conditionalButtonAirFlow.Click += conditionalButtonAirFlow_Click; // Yeni bağlantıyı ekle

            // Set ellipse13 position based on BalanceEllipse setting
            Canvas canvas13 = ellipse13.Parent as Canvas;
            if (canvas13 != null)
            {
                double canvasWidth = canvas13.ActualWidth;
                double ellipseWidth = ellipse13.ActualWidth;
                double maxRight = canvasWidth - ellipseWidth - 12;

                // If BalanceEllipse is 13, position ellipse to the right
                if (Properties.Settings.Default.BalanceEllipse == 13)
                {
                    Canvas.SetLeft(ellipse13, maxRight);
                    ellipse13.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101"));
                }
                else
                {
                    Canvas.SetLeft(ellipse13, 6);
                    ellipse13.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF"));
                }
            }
            //conditionalButtonGas2 düğmesinin başlangıç görünürlüğünü ve rengini ayarla
            if (Properties.Settings.Default.Gas2Ellipse == 1 &&
                Properties.Settings.Default.StartButton == 1 &&
                Properties.Settings.Default.Gas2ConditionalButtonVisibility == 1)
            {
                conditionalButtonGas2.Visibility = Visibility.Visible;

                // Butonun rengini Settings'e göre ayarla
                switch (Properties.Settings.Default.Gas2ConditionalButton)
                {
                    case 0: // Kırmızı
                        conditionalButtonGas2.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1: // Sarı
                        conditionalButtonGas2.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2: // Yeşil
                        conditionalButtonGas2.Background = new SolidColorBrush(Colors.Green);
                        break;
                    default:
                        conditionalButtonGas2.Background = new SolidColorBrush(Colors.Red);
                        break;
                }
            }
            else
            {
                conditionalButtonGas2.Visibility = Visibility.Collapsed;
            }

            // Click event handler'ı conditionalButtonpO2'ye bağla
            conditionalButtonGas2.Click -= conditionalButtonGas2_Click; // Önce eski bağlantıyı kaldır
            conditionalButtonGas2.Click += conditionalButtonGas2_Click; // Yeni bağlantıyı ekle




            CheckEllipsePositionAndSetButtonVisibility(ellipse10, conditionalButtonTurbidity);
            CheckEllipsePositionAndSetButtonVisibility(ellipse11, conditionalButtonBalance);
            CheckEllipsePositionAndSetButtonVisibility(ellipse12, conditionalButtonAirFlow);
            CheckEllipsePositionAndSetButtonVisibility(ellipse13, conditionalButtonGas2);
            //CheckEllipsePositionAndSetButtonVisibility(ellipse16, conditionalButtonGas3Flow);
            //CheckEllipsePositionAndSetButtonVisibility(ellipse17, conditionalButtonGas4Flow);
            //CheckEllipsePositionAndSetButtonVisibility(ellipse18, conditionalButtonGas5Flow);

            //CompareGas2Values();

        }
        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Eğer ilgili ayarlar değiştiyse border görünürlüklerini güncelle
            if (e.PropertyName == "AirFlowTargetBorder" || e.PropertyName == "Gas2TargetBorder")
            {
                UpdateBorderVisibilities();
            }





            // When TurbidityTarget changes in Settings, update the TextBox
            if (e.PropertyName == "TurbidityTarget" && TurbidityTarget != null)
            {
                // Only update if the TextBox doesn't have focus to avoid triggering a text changed event loop
                if (!TurbidityTarget.IsFocused)
                {
                    TurbidityTarget.Text = Properties.Settings.Default.TurbidityTarget.ToString();
                }
            }
            // When BalanceTarget changes in Settings, update the TextBox
            if (e.PropertyName == "BalanceTarget" && BalanceTarget != null)
            {
                // Only update if the TextBox doesn't have focus to avoid triggering a text changed event loop
                if (!BalanceTarget.IsFocused)
                {
                    BalanceTarget.Text = Properties.Settings.Default.BalanceTarget.ToString();
                }
            }
            // When AirFlowTarget changes in Settings, update the TextBox
            if (e.PropertyName == "AirFlowTarget" && AirFlowTarget != null)
            {
                // Only update if the TextBox doesn't have focus to avoid triggering a text changed event loop
                if (!AirFlowTarget.IsFocused)
                {
                    AirFlowTarget.Text = Properties.Settings.Default.AirFlowTarget.ToString();
                }
            }
            // When Gas2Target changes in Settings, update the TextBox
            if (e.PropertyName == "Gas2Target" && Gas2Target != null)
            {
                // Only update if the TextBox doesn't have focus to avoid triggering a text changed event loop
                if (!Gas2Target.IsFocused)
                {
                    Gas2Target.Text = Properties.Settings.Default.Gas2Target.ToString();
                }
            }

            // Eğer ilgili ayarlar değiştiyse border görünürlüklerini güncelle
            if (e.PropertyName == "TurbidityTargetBorder" ||
                e.PropertyName == "BalanceTargetBorder" ||
                e.PropertyName == "AirFlowTargetBorder" ||
                e.PropertyName == "Gas2TargetBorder" ||
                e.PropertyName == "TurbidityValue" ||
                e.PropertyName == "BalanceValue" ||
                e.PropertyName == "AirFlowValue" ||
                e.PropertyName == "Gas2Value" ||
                e.PropertyName == "TurbidityEllipse" ||
                e.PropertyName == "BalanceEllipse" ||
                e.PropertyName == "AirFlowEllipse" ||
                e.PropertyName == "Gas2Ellipse" ||
                e.PropertyName == "StartButton" ||
                e.PropertyName == "TurbidityConditionalButtonVisibility" ||
                e.PropertyName == "BalanceConditionalButtonVisibility" ||
                e.PropertyName == "AirFlowConditionalButtonVisibility" ||
                e.PropertyName == "Gas2ConditionalButtonVisibility" ||
                e.PropertyName == "EditpHCascade" ||
                e.PropertyName == "EditpO2Cascade" ||
                e.PropertyName == "EditFoamCascade" ||
                e.PropertyName == "EditRedoxCascade" ||
                e.PropertyName == "EditTurbidityCascade")  // Bu satırları ekledik
            {
                LoadTextBoxValues();
                CheckComparisonTimer();
                UpdateBorderVisibilities();
                CheckEllipsePositionAndSetButtonVisibility(ellipse10, conditionalButtonTurbidity);
                CheckEllipsePositionAndSetButtonVisibility(ellipse11, conditionalButtonBalance);
                CheckEllipsePositionAndSetButtonVisibility(ellipse12, conditionalButtonAirFlow);
                CheckEllipsePositionAndSetButtonVisibility(ellipse13, conditionalButtonGas2);


                // If TurbidityEllipse, StartButton or TurbidityConditionalButtonVisibility changed, update conditionalButtonTurbidity visibility
                if (e.PropertyName == "TurbidityEllipse" || e.PropertyName == "StartButton" || e.PropertyName == "TurbidityConditionalButtonVisibility")
                {
                    // conditionalButtonTurbidity düğmesinin görünürlüğünü güncelle
                    if (Properties.Settings.Default.TurbidityEllipse == 1 &&
                        Properties.Settings.Default.StartButton == 1 &&
                        Properties.Settings.Default.TurbidityConditionalButtonVisibility == 1)
                    {
                        conditionalButtonTurbidity.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        conditionalButtonTurbidity.Visibility = Visibility.Collapsed;
                    }

                    Canvas canvas10 = ellipse10.Parent as Canvas;
                    if (canvas10 != null && e.PropertyName == "TurbidityEllipse")
                    {
                        double canvasWidth = canvas10.ActualWidth;
                        double ellipseWidth = ellipse10.ActualWidth;
                        double maxRight = canvasWidth - ellipseWidth - 12;

                        double targetLeft = Properties.Settings.Default.TurbidityEllipse == 10 ? maxRight : 6;
                        Canvas.SetLeft(ellipse10, targetLeft);

                        if (Properties.Settings.Default.TurbidityEllipse == 10)
                        {
                            ellipse10.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101"));
                        }
                        else
                        {
                            ellipse10.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF"));
                        }
                    }
                }

                //FFAF0101

                // If BalanceEllipse, StartButton or BalanceConditionalButtonVisibility changed, update conditionalButtonBalance visibility
                if (e.PropertyName == "BalanceEllipse" || e.PropertyName == "StartButton" || e.PropertyName == "BalanceConditionalButtonVisibility")
                {
                    // conditionalButtonBalance düğmesinin görünürlüğünü güncelle
                    if (Properties.Settings.Default.BalanceEllipse == 1 &&
                        Properties.Settings.Default.StartButton == 1 &&
                        Properties.Settings.Default.BalanceConditionalButtonVisibility == 1)
                    {
                        conditionalButtonBalance.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        conditionalButtonBalance.Visibility = Visibility.Collapsed;
                    }

                    Canvas canvas11 = ellipse11.Parent as Canvas;
                    if (canvas11 != null && e.PropertyName == "BalanceEllipse")
                    {
                        double canvasWidth = canvas11.ActualWidth;
                        double ellipseWidth = ellipse11.ActualWidth;
                        double maxRight = canvasWidth - ellipseWidth - 12;

                        double targetLeft = Properties.Settings.Default.BalanceEllipse == 1 ? maxRight : 6;
                        Canvas.SetLeft(ellipse11, targetLeft);

                        if (Properties.Settings.Default.BalanceEllipse == 1)
                        {
                            ellipse11.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101"));
                        }
                        else
                        {
                            ellipse11.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF"));
                        }
                    }
                }

                // If AirFlowEllipse, StartButton or AirFlowConditionalButtonVisibility changed, update conditionalButtonAirFlow visibility
                if (e.PropertyName == "AirFlowEllipse" || e.PropertyName == "StartButton" || e.PropertyName == "AirFlowConditionalButtonVisibility")
                {
                    // conditionalButtonAirFlow düğmesinin görünürlüğünü güncelle
                    if (Properties.Settings.Default.AirFlowEllipse == 1 &&
                        Properties.Settings.Default.StartButton == 1 &&
                        Properties.Settings.Default.AirFlowConditionalButtonVisibility == 1)
                    {
                        conditionalButtonAirFlow.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        conditionalButtonAirFlow.Visibility = Visibility.Collapsed;
                    }

                    Canvas canvas12 = ellipse12.Parent as Canvas;
                    if (canvas12 != null && e.PropertyName == "AirFlowEllipse")
                    {
                        double canvasWidth = canvas12.ActualWidth;
                        double ellipseWidth = ellipse12.ActualWidth;
                        double maxRight = canvasWidth - ellipseWidth - 12;

                        double targetLeft = Properties.Settings.Default.AirFlowEllipse == 1 ? maxRight : 6;
                        Canvas.SetLeft(ellipse12, targetLeft);

                        if (Properties.Settings.Default.AirFlowEllipse == 1)
                        {
                            ellipse12.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101"));
                        }
                        else
                        {
                            ellipse12.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF"));
                        }
                    }
                }

                // If Gas2Ellipse, StartButton or Gas2ConditionalButtonVisibility changed, update conditionalButtonGas2 visibility
                if (e.PropertyName == "Gas2Ellipse" || e.PropertyName == "StartButton" || e.PropertyName == "Gas2ConditionalButtonVisibility")
                {
                    // conditionalButtonGas2 düğmesinin görünürlüğünü güncelle
                    if (Properties.Settings.Default.Gas2Ellipse == 1 &&
                        Properties.Settings.Default.StartButton == 1 &&
                        Properties.Settings.Default.Gas2ConditionalButtonVisibility == 1)
                    {
                        conditionalButtonGas2.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        conditionalButtonGas2.Visibility = Visibility.Collapsed;
                    }

                    Canvas canvas13 = ellipse13.Parent as Canvas;
                    if (canvas13 != null && e.PropertyName == "Gas2Ellipse")
                    {
                        double canvasWidth = canvas13.ActualWidth;
                        double ellipseWidth = ellipse13.ActualWidth;
                        double maxRight = canvasWidth - ellipseWidth - 12;

                        double targetLeft = Properties.Settings.Default.Gas2Ellipse == 1 ? maxRight : 6;
                        Canvas.SetLeft(ellipse13, targetLeft);

                        if (Properties.Settings.Default.Gas2Ellipse == 1)
                        {
                            ellipse13.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101"));
                        }
                        else
                        {
                            ellipse13.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF"));
                        }
                    }
                }
            }

            // TurbidityConditionalButton, TurbidityValue veya TurbidityTarget değiştiğinde butonun rengini güncelle
            if (e.PropertyName == "TurbidityConditionalButton" || e.PropertyName == "TurbidityValue" || e.PropertyName == "TurbidityTarget")
            {
                if (conditionalButtonTurbidity != null)
                {
                    if (e.PropertyName == "TurbidityValue" || e.PropertyName == "TurbidityTarget")
                    {
                        // TurbidityValue veya TurbidityTarget değiştiğinde, değerleri karşılaştır ve butonun rengini ayarla
                        double TurbidityValue = Properties.Settings.Default.TurbidityValue;
                        double TurbidityTarget = Properties.Settings.Default.TurbidityTarget;
                        double difference = Math.Abs(TurbidityValue - TurbidityTarget);

                        if (difference < 1)
                        {
                            // Değerler arasındaki fark 1'den az ise butonun arka planını yeşil yap
                            conditionalButtonTurbidity.Background = new SolidColorBrush(Colors.Green);
                            Properties.Settings.Default.TurbidityConditionalButton = 2; // Yeşil için 2
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            // Değerler farklıysa butonun arka planını sarı yap
                            conditionalButtonTurbidity.Background = new SolidColorBrush(Colors.Yellow);
                            Properties.Settings.Default.TurbidityConditionalButton = 1; // Sarı için 1
                            Properties.Settings.Default.Save();
                        }
                    }
                    else // TurbidityConditionalButton değiştiğinde
                    {
                        switch (Properties.Settings.Default.TurbidityConditionalButton)
                        {
                            case 0: // Kırmızı
                                conditionalButtonTurbidity.Background = new SolidColorBrush(Colors.Red);
                                break;
                            case 1: // Sarı
                                conditionalButtonTurbidity.Background = new SolidColorBrush(Colors.Yellow);
                                break;
                            case 2: // Yeşil
                                conditionalButtonTurbidity.Background = new SolidColorBrush(Colors.Green);
                                break;
                        }
                    }
                }
            }
            // BalanceConditionalButton, BalanceValue veya BalanceTarget değiştiğinde butonun rengini güncelle
            if (e.PropertyName == "BalanceConditionalButton" || e.PropertyName == "BalanceValue" || e.PropertyName == "BalanceTarget")
            {
                if (conditionalButtonBalance != null)
                {
                    if (e.PropertyName == "BalanceValue" || e.PropertyName == "BalanceTarget")
                    {
                        // BalanceValue veya BalanceTarget değiştiğinde, değerleri karşılaştır ve butonun rengini ayarla
                        double BalanceValue = Properties.Settings.Default.BalanceValue;
                        double BalanceTarget = Properties.Settings.Default.BalanceTarget;
                        double difference = Math.Abs(BalanceValue - BalanceTarget);

                        if (difference < 1)
                        {
                            // Değerler arasındaki fark 1'den az ise butonun arka planını yeşil yap
                            conditionalButtonBalance.Background = new SolidColorBrush(Colors.Green);
                            Properties.Settings.Default.BalanceConditionalButton = 2; // Yeşil için 2
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            // Değerler farklıysa butonun arka planını sarı yap
                            conditionalButtonBalance.Background = new SolidColorBrush(Colors.Yellow);
                            Properties.Settings.Default.BalanceConditionalButton = 1; // Sarı için 1
                            Properties.Settings.Default.Save();
                        }
                    }
                    else // BalanceConditionalButton değiştiğinde
                    {
                        switch (Properties.Settings.Default.BalanceConditionalButton)
                        {
                            case 0: // Kırmızı
                                conditionalButtonBalance.Background = new SolidColorBrush(Colors.Red);
                                break;
                            case 1: // Sarı
                                conditionalButtonBalance.Background = new SolidColorBrush(Colors.Yellow);
                                break;
                            case 2: // Yeşil
                                conditionalButtonBalance.Background = new SolidColorBrush(Colors.Green);
                                break;
                        }
                    }
                }
            }
            // AirFlowConditionalButton, AirFlowValue veya AirFlowTarget değiştiğinde butonun rengini güncelle
            if (e.PropertyName == "AirFlowConditionalButton" || e.PropertyName == "AirFlowValue" || e.PropertyName == "AirFlowTarget")
            {
                if (conditionalButtonAirFlow != null)
                {
                    if (e.PropertyName == "AirFlowValue" || e.PropertyName == "AirFlowTarget")
                    {
                        // AirFlowValue veya AirFlowTarget değiştiğinde, değerleri karşılaştır ve butonun rengini ayarla
                        double AirFlowValue = Properties.Settings.Default.AirFlowValue;
                        double AirFlowTarget = Properties.Settings.Default.AirFlowTarget;
                        double difference = Math.Abs(AirFlowValue - AirFlowTarget);

                        if (difference < 1)
                        {
                            // Değerler arasındaki fark 1'den az ise butonun arka planını yeşil yap
                            conditionalButtonAirFlow.Background = new SolidColorBrush(Colors.Green);
                            Properties.Settings.Default.AirFlowConditionalButton = 2; // Yeşil için 2
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            // Değerler farklıysa butonun arka planını sarı yap
                            conditionalButtonAirFlow.Background = new SolidColorBrush(Colors.Yellow);
                            Properties.Settings.Default.AirFlowConditionalButton = 1; // Sarı için 1
                            Properties.Settings.Default.Save();
                        }
                    }
                    else // AirFlowConditionalButton değiştiğinde
                    {
                        switch (Properties.Settings.Default.AirFlowConditionalButton)
                        {
                            case 0: // Kırmızı
                                conditionalButtonAirFlow.Background = new SolidColorBrush(Colors.Red);
                                break;
                            case 1: // Sarı
                                conditionalButtonAirFlow.Background = new SolidColorBrush(Colors.Yellow);
                                break;
                            case 2: // Yeşil
                                conditionalButtonAirFlow.Background = new SolidColorBrush(Colors.Green);
                                break;
                        }
                    }
                }
            }
            // Gas2ConditionalButton, Gas2Value veya Gas2Target değiştiğinde butonun rengini güncelle
            if (e.PropertyName == "Gas2ConditionalButton" || e.PropertyName == "Gas2Value" || e.PropertyName == "Gas2Target")
            {
                if (conditionalButtonGas2 != null)
                {
                    if (e.PropertyName == "Gas2Value" || e.PropertyName == "Gas2Target")
                    {
                        // Gas2Value veya Gas2Target değiştiğinde, değerleri karşılaştır ve butonun rengini ayarla
                        double Gas2Value = Properties.Settings.Default.Gas2Value;
                        double Gas2Target = Properties.Settings.Default.Gas2Target;
                        double difference = Math.Abs(Gas2Value - Gas2Target);

                        if (difference < 1)
                        {
                            // Değerler arasındaki fark 1'den az ise butonun arka planını yeşil yap
                            conditionalButtonGas2.Background = new SolidColorBrush(Colors.Green);
                            Properties.Settings.Default.Gas2ConditionalButton = 2; // Yeşil için 2
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            // Değerler farklıysa butonun arka planını sarı yap
                            conditionalButtonGas2.Background = new SolidColorBrush(Colors.Yellow);
                            Properties.Settings.Default.Gas2ConditionalButton = 1; // Sarı için 1
                            Properties.Settings.Default.Save();
                        }
                    }
                    else // Gas2ConditionalButton değiştiğinde
                    {
                        switch (Properties.Settings.Default.Gas2ConditionalButton)
                        {
                            case 0: // Kırmızı
                                conditionalButtonGas2.Background = new SolidColorBrush(Colors.Red);
                                break;
                            case 1: // Sarı
                                conditionalButtonGas2.Background = new SolidColorBrush(Colors.Yellow);
                                break;
                            case 2: // Yeşil
                                conditionalButtonGas2.Background = new SolidColorBrush(Colors.Green);
                                break;
                        }
                    }
                }
            }
        }
        private void ellipse10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Properties.Settings.Default.EditTurbidityCascade == 0)
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("Turbidity selection is required. Please go to EditTurbidity settings and select an option.",
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
                Canvas parentCanvas = ellipse10.Parent as Canvas;
                if (parentCanvas == null) return;

                double canvasWidth = parentCanvas.ActualWidth;
                double ellipseWidth = ellipse10.ActualWidth;
                double maxRight = canvasWidth - ellipseWidth - 12; // 12 = 6 (sol boşluk) + 6 (sağ boşluk)
                double targetLeft = Canvas.GetLeft(ellipse10) == 6 ? maxRight : 6; // Yuvarlağın hedef pozisyonu

                DoubleAnimation animation = new DoubleAnimation
                {
                    To = targetLeft,
                    Duration = TimeSpan.FromSeconds(0.2),
                    FillBehavior = FillBehavior.Stop
                };

                animation.Completed += (s, a) =>
                {
                    Canvas.SetLeft(ellipse10, targetLeft); // Animasyon tamamlandığında yuvarlağın pozisyonunu güncelle

                    if (targetLeft == maxRight)
                    {
                        ellipse10.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101")); // Sağdaysa kırmızı yap
                                                                                                                   // Ellipse açıldığında pO2Ellipse'i 1 yap
                        Properties.Settings.Default.TurbidityEllipse = 1;
                        Properties.Settings.Default.TurbidityConditionalButtonVisibility = 1;
                    }
                    else
                    {
                        ellipse10.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF")); // Soldaysa gri yap
                                                                                                                   // Ellipse kapandığında pO2Ellipse'i 0 yap
                        Properties.Settings.Default.TurbidityEllipse = 0;
                        Properties.Settings.Default.TurbidityConditionalButtonVisibility = 0;
                    }

                    // Ayarları kaydet
                    Properties.Settings.Default.Save();

                    // Ellipse'in rengi değiştikten sonra butonun görünürlüğünü kontrol et
                    CheckEllipsePositionAndSetButtonVisibility(ellipse10, conditionalButtonTurbidity);

                    // FavouritesControl'daki ilgili ellipse'i güncelle
                    mainWindow.favouritesControl.UpdateEllipsePosition(ellipse10.Name, targetLeft);
                    // FavouritesControl'daki ilgili conditionalButton'ı güncelle
                    mainWindow.favouritesControl.UpdateConditionalButtonVisibility(ellipse10.Name, targetLeft);
                };
                ellipse10.BeginAnimation(Canvas.LeftProperty, animation);
            }

            // Normal ellipse tıklama olayını çağır
            Ellipse_MouseLeftButtonDown(sender, e);
        }
        private void ellipse11_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Properties.Settings.Default.EditTurbidityCascade == 0)
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("Turbidity selection is required. Please go to EditTurbidity settings and select an option.",
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
                Canvas parentCanvas = ellipse11.Parent as Canvas;
                if (parentCanvas == null) return;

                double canvasWidth = parentCanvas.ActualWidth;
                double ellipseWidth = ellipse11.ActualWidth;
                double maxRight = canvasWidth - ellipseWidth - 12; // 12 = 6 (sol boşluk) + 6 (sağ boşluk)
                double targetLeft = Canvas.GetLeft(ellipse11) == 6 ? maxRight : 6; // Yuvarlağın hedef pozisyonu

                DoubleAnimation animation = new DoubleAnimation
                {
                    To = targetLeft,
                    Duration = TimeSpan.FromSeconds(0.2),
                    FillBehavior = FillBehavior.Stop
                };

                animation.Completed += (s, a) =>
                {
                    Canvas.SetLeft(ellipse11, targetLeft); // Animasyon tamamlandığında yuvarlağın pozisyonunu güncelle

                    if (targetLeft == maxRight)
                    {
                        ellipse11.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101")); // Sağdaysa kırmızı yap
                                                                                                                    // Ellipse açıldığında pO2Ellipse'i 1 yap
                        Properties.Settings.Default.BalanceEllipse = 1;
                        Properties.Settings.Default.BalanceConditionalButtonVisibility = 1;
                    }
                    else
                    {
                        ellipse11.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF")); // Soldaysa gri yap
                                                                                                                    // Ellipse kapandığında pO2Ellipse'i 0 yap
                        Properties.Settings.Default.BalanceEllipse = 0;
                        Properties.Settings.Default.BalanceConditionalButtonVisibility = 0;
                    }

                    // Ayarları kaydet
                    Properties.Settings.Default.Save();

                    // Ellipse'in rengi değiştikten sonra butonun görünürlüğünü kontrol et
                    CheckEllipsePositionAndSetButtonVisibility(ellipse11, conditionalButtonBalance);

                    // FavouritesControl'daki ilgili ellipse'i güncelle
                    mainWindow.favouritesControl.UpdateEllipsePosition(ellipse11.Name, targetLeft);
                    // FavouritesControl'daki ilgili conditionalButton'ı güncelle
                    mainWindow.favouritesControl.UpdateConditionalButtonVisibility(ellipse11.Name, targetLeft);
                };
                ellipse11.BeginAnimation(Canvas.LeftProperty, animation);
            }


            // Normal ellipse tıklama olayını çağır
            Ellipse_MouseLeftButtonDown(sender, e);
        }
        private void ellipse12_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Önce Properties.Settings.Default'tan cascade değerini kontrol edin
            if (Properties.Settings.Default.EditRedoxCascade == 2)
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
                return; // Ellipse1'ün durumunu değiştirmeden fonksiyonu sonlandır
            }
            else
            {
                // Canvas'ı bul
                Canvas parentCanvas = ellipse12.Parent as Canvas;
                if (parentCanvas == null) return;

                double canvasWidth = parentCanvas.ActualWidth;
                double ellipseWidth = ellipse12.ActualWidth;
                double maxRight = canvasWidth - ellipseWidth - 12; // 12 = 6 (sol boşluk) + 6 (sağ boşluk)
                double targetLeft = Canvas.GetLeft(ellipse12) == 6 ? maxRight : 6; // Yuvarlağın hedef pozisyonu

                DoubleAnimation animation = new DoubleAnimation
                {
                    To = targetLeft,
                    Duration = TimeSpan.FromSeconds(0.2),
                    FillBehavior = FillBehavior.Stop
                };

                animation.Completed += (s, a) =>
                {
                    Canvas.SetLeft(ellipse12, targetLeft); // Animasyon tamamlandığında yuvarlağın pozisyonunu güncelle

                    if (targetLeft == maxRight)
                    {
                        ellipse12.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101")); // Sağdaysa kırmızı yap
                                                                                                                   // Ellipse açıldığında pO2Ellipse'i 1 yap
                        Properties.Settings.Default.AirFlowEllipse = 1;
                        Properties.Settings.Default.AirFlowConditionalButtonVisibility = 1;
                    }
                    else
                    {
                        ellipse12.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF")); // Soldaysa gri yap
                                                                                                                   // Ellipse kapandığında pO2Ellipse'i 0 yap
                        Properties.Settings.Default.AirFlowEllipse = 0;
                        Properties.Settings.Default.AirFlowConditionalButtonVisibility = 0;
                    }

                    // Ayarları kaydet
                    Properties.Settings.Default.Save();

                    // Ellipse'in rengi değiştikten sonra butonun görünürlüğünü kontrol et
                    CheckEllipsePositionAndSetButtonVisibility(ellipse12, conditionalButtonAirFlow);

                    // FavouritesControl'daki ilgili ellipse'i güncelle
                    mainWindow.favouritesControl.UpdateEllipsePosition(ellipse12.Name, targetLeft);
                    // FavouritesControl'daki ilgili conditionalButton'ı güncelle
                    mainWindow.favouritesControl.UpdateConditionalButtonVisibility(ellipse12.Name, targetLeft);
                };
                ellipse12.BeginAnimation(Canvas.LeftProperty, animation);
            }

            // Normal ellipse tıklama olayını çağır
            Ellipse_MouseLeftButtonDown(sender, e);
        }
        private void ellipse13_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Önce Properties.Settings.Default'tan cascade değerini kontrol edin
            if (Properties.Settings.Default.EditRedoxCascade == 1)
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("pO2 cascade selection is required. Please go to EditpO2 settings and select a cascade option.",
                              "Configuration Required",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return; // Ellipse4'ün durumunu değiştirmeden fonksiyonu sonlandır
            }

            if (Properties.Settings.Default.EditRedoxCascade == 3)
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
                return; // Ellipse1'ün durumunu değiştirmeden fonksiyonu sonlandır
            }
            else
            {
                // Canvas'ı bul
                Canvas parentCanvas = ellipse13.Parent as Canvas;
                if (parentCanvas == null) return;

                double canvasWidth = parentCanvas.ActualWidth;
                double ellipseWidth = ellipse13.ActualWidth;
                double maxRight = canvasWidth - ellipseWidth - 12; // 12 = 6 (sol boşluk) + 6 (sağ boşluk)
                double targetLeft = Canvas.GetLeft(ellipse13) == 6 ? maxRight : 6; // Yuvarlağın hedef pozisyonu

                DoubleAnimation animation = new DoubleAnimation
                {
                    To = targetLeft,
                    Duration = TimeSpan.FromSeconds(0.2),
                    FillBehavior = FillBehavior.Stop
                };

                animation.Completed += (s, a) =>
                {
                    Canvas.SetLeft(ellipse13, targetLeft); // Animasyon tamamlandığında yuvarlağın pozisyonunu güncelle

                    if (targetLeft == maxRight)
                    {
                        ellipse13.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAF0101")); // Sağdaysa kırmızı yap
                                                                                                                   // Ellipse açıldığında pO2Ellipse'i 1 yap
                        Properties.Settings.Default.Gas2Ellipse = 1;
                        Properties.Settings.Default.Gas2ConditionalButtonVisibility = 1;
                    }
                    else
                    {
                        ellipse13.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7ECEF")); // Soldaysa gri yap
                                                                                                                   // Ellipse kapandığında pO2Ellipse'i 0 yap
                        Properties.Settings.Default.Gas2Ellipse = 0;
                        Properties.Settings.Default.Gas2ConditionalButtonVisibility = 0;
                    }

                    // Ayarları kaydet
                    Properties.Settings.Default.Save();

                    // Ellipse'in rengi değiştikten sonra butonun görünürlüğünü kontrol et
                    CheckEllipsePositionAndSetButtonVisibility(ellipse13, conditionalButtonGas2);

                    // FavouritesControl'daki ilgili ellipse'i güncelle
                    mainWindow.favouritesControl.UpdateEllipsePosition(ellipse13.Name, targetLeft);
                    // FavouritesControl'daki ilgili conditionalButton'ı güncelle
                    mainWindow.favouritesControl.UpdateConditionalButtonVisibility(ellipse13.Name, targetLeft);
                };
                ellipse13.BeginAnimation(Canvas.LeftProperty, animation);
            }

            // Normal ellipse tıklama olayını çağır
            Ellipse_MouseLeftButtonDown(sender, e);
        }
        public void CheckEllipsePositionAndSetButtonVisibility(Ellipse ellipse, Button button)
        {
            // Ellipse'in parent'ını Canvas olarak al
            Canvas parentCanvas = ellipse.Parent as Canvas;
            if (parentCanvas == null) return;

            double canvasWidth = parentCanvas.ActualWidth; // Canvas'ın gerçek genişliğini kullan
            double ellipseRightPosition = Canvas.GetLeft(ellipse) + ellipse.Width; // Ellipse'in sağ kenarının konumu

            // Eğer bu conditional Button Turbidity için ise özel kontrol uygula
            if (button == conditionalButtonTurbidity)
            {
                // Sadece TurbidityEllipse == 1 ve StartButton == 1 ise görünür yap
                if (Properties.Settings.Default.TurbidityEllipse == 1 &&
                    Properties.Settings.Default.StartButton == 1 &&
                    Properties.Settings.Default.TurbidityConditionalButtonVisibility == 1)
                {
                    button.Visibility = Visibility.Visible;
                }
                else
                {
                    button.Visibility = Visibility.Collapsed;
                }
            }

            // Eğer bu conditional Button Balance için ise özel kontrol uygula
            if (button == conditionalButtonBalance)
            {
                // Sadece BalanceEllipse == 1 ve StartButton == 1 ise görünür yap
                if (Properties.Settings.Default.BalanceEllipse == 1 &&
                    Properties.Settings.Default.StartButton == 1 &&
                    Properties.Settings.Default.BalanceConditionalButtonVisibility == 1)
                {
                    button.Visibility = Visibility.Visible;
                }
                else
                {
                    button.Visibility = Visibility.Collapsed;
                }
            }


            // Eğer bu conditional Button AirFlow için ise özel kontrol uygula
            if (button == conditionalButtonAirFlow)
            {
                // Sadece AirFlowEllipse == 1 ve StartButton == 1 ise görünür yap
                if (Properties.Settings.Default.AirFlowEllipse == 1 &&
                    Properties.Settings.Default.StartButton == 1 &&
                    Properties.Settings.Default.AirFlowConditionalButtonVisibility == 1)
                {
                    button.Visibility = Visibility.Visible;
                }
                else
                {
                    button.Visibility = Visibility.Collapsed;
                }
            }

            // Eğer bu conditional Button Gas2 için ise özel kontrol uygula
            if (button == conditionalButtonGas2)
            {
                // Sadece Gas2Ellipse == 1 ve StartButton == 1 ise görünür yap
                if (Properties.Settings.Default.Gas2Ellipse == 1 &&
                    Properties.Settings.Default.StartButton == 1 &&
                    Properties.Settings.Default.Gas2ConditionalButtonVisibility == 1)
                {
                    button.Visibility = Visibility.Visible;
                }
                else
                {
                    button.Visibility = Visibility.Collapsed;
                }
            }
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
        private void LoadTextBoxValues()
        {
            try
            {
                // Load values from Settings.settings for each TextBox
                if (TurbidityTarget != null)
                    TurbidityTarget.Text = Properties.Settings.Default.TurbidityTarget.ToString();

                if (TurbidityValue != null)
                    TurbidityValue.Content = Properties.Settings.Default.TurbidityValue.ToString();

                if (BalanceTarget != null)
                    BalanceTarget.Text = Properties.Settings.Default.BalanceTarget.ToString();

                if (BalanceValue != null)
                    BalanceValue.Content = Properties.Settings.Default.BalanceValue.ToString();

                if (AirFlowTarget != null)
                    AirFlowTarget.Text = Properties.Settings.Default.AirFlowTarget.ToString();

                if (AirFlowValue != null)
                    AirFlowValue.Content = Properties.Settings.Default.AirFlowValue.ToString();

                if (Gas2Target != null)
                    Gas2Target.Text = Properties.Settings.Default.Gas2Target.ToString();

                if (Gas2Value != null)
                    Gas2Value.Content = Properties.Settings.Default.Gas2Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading text box values: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void SaveTextBoxValue(TextBox textBox)
        {
            try
            {
                if (textBox == null) return;

                // Handle different TextBoxes
                if (textBox == TurbidityTarget)
                {
                    if (double.TryParse(textBox.Text, out double value))
                    {
                        Properties.Settings.Default.TurbidityTarget = value;
                        Properties.Settings.Default.Save();
                    }
                }
                else if (textBox == BalanceTarget)
                {
                    if (double.TryParse(textBox.Text, out double value))
                    {
                        Properties.Settings.Default.BalanceTarget = value;
                        Properties.Settings.Default.Save();
                    }
                }
                else if (textBox == AirFlowTarget)
                {
                    if (double.TryParse(textBox.Text, out double value))
                    {
                        Properties.Settings.Default.AirFlowTarget = value;
                        Properties.Settings.Default.Save();
                    }
                }
                else if (textBox == Gas2Target)
                {
                    if (double.TryParse(textBox.Text, out double value))
                    {
                        Properties.Settings.Default.Gas2Target = value;
                        Properties.Settings.Default.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving text box value: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void RegisterTextChangedEvents()
        {
            if (TurbidityTarget != null)
                TurbidityTarget.TextChanged += TextBox_TextChanged;

            if (BalanceTarget != null)
                BalanceTarget.TextChanged += TextBox_TextChanged;

            if (AirFlowTarget != null)
                AirFlowTarget.TextChanged += TextBox_TextChanged;

            if (Gas2Target != null)
                Gas2Target.TextChanged += TextBox_TextChanged;
        }
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
                if (activeTextBox.Tag is string tag && ParseRange(tag, out double min, out double max))
                {
                    string normalizedValue = value.Replace(',', '.');
                    if (double.TryParse(normalizedValue, System.Globalization.NumberStyles.Any,
                                       System.Globalization.CultureInfo.InvariantCulture, out double doubleValue))
                    {
                        if (doubleValue >= min && doubleValue <= max)
                        {
                            // Set the value
                            activeTextBox.Text = doubleValue.ToString(System.Globalization.CultureInfo.CurrentCulture);
                        }
                        else
                        {
                            KeypadPopup.IsOpen = true;
                            MessageBox.Show($"Please enter a value between {min} and {max}.",
                                           "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        KeypadPopup.IsOpen = true;
                        MessageBox.Show("Please enter a valid number.",
                                       "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    activeTextBox.Text = value;
                }
            }
        }
        private bool ParseRange(string tag, out double min, out double max)
        {
            min = max = 0;
            if (string.IsNullOrEmpty(tag)) return false;

            var parts = tag.Split(',');
            if (parts.Length == 2 &&
                double.TryParse(parts[0], System.Globalization.NumberStyles.Any,
                              System.Globalization.CultureInfo.InvariantCulture, out min) &&
                double.TryParse(parts[1], System.Globalization.NumberStyles.Any,
                              System.Globalization.CultureInfo.InvariantCulture, out max))
            {
                return true;
            }

            return false;
        }
        private void conditionalButtonTurbidity_Click(object sender, RoutedEventArgs e)
        {
            // Tıklanan butonu belirle
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Butonun Tag'ine Temperature Grid'ini atayın
                clickedButton.Tag = Turbidity;

                // Butonu kırmızı yap (varsayılan renk)
                clickedButton.Background = new SolidColorBrush(Colors.Red);
                Properties.Settings.Default.TurbidityConditionalButton = 0; // Kırmızı için 0
                Properties.Settings.Default.Save();
            }

            // Zamanlayıcıyı başlat veya devam ettir
            comparisonTimer.Start();
        }
        private void conditionalButtonBalance_Click(object sender, RoutedEventArgs e)
        {
            // Tıklanan butonu belirle
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Butonun Tag'ine Temperature Grid'ini atayın
                clickedButton.Tag = Balance;

                // Butonu kırmızı yap (varsayılan renk)
                clickedButton.Background = new SolidColorBrush(Colors.Red);
                Properties.Settings.Default.BalanceConditionalButton = 0; // Kırmızı için 0
                Properties.Settings.Default.Save();
            }

            // Zamanlayıcıyı başlat veya devam ettir
            comparisonTimer.Start();
        }
        private void conditionalButtonAirFlow_Click(object sender, RoutedEventArgs e)
        {
            // Tıklanan butonu belirle
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Butonun Tag'ine Temperature Grid'ini atayın
                clickedButton.Tag = AirFlow;

                // Butonu kırmızı yap (varsayılan renk)
                clickedButton.Background = new SolidColorBrush(Colors.Red);
                Properties.Settings.Default.AirFlowConditionalButton = 0; // Kırmızı için 0
                Properties.Settings.Default.Save();
            }

            // Zamanlayıcıyı başlat veya devam ettir
            comparisonTimer.Start();
        }
        private void conditionalButtonGas2_Click(object sender, RoutedEventArgs e)
        {
            // Tıklanan butonu belirle
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Butonun Tag'ine Temperature Grid'ini atayın
                clickedButton.Tag = Gas2;

                // Butonu kırmızı yap (varsayılan renk)
                clickedButton.Background = new SolidColorBrush(Colors.Red);
                Properties.Settings.Default.Gas2ConditionalButton = 0; // Kırmızı için 0
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
                    // Özel olarak conditionalButtonTurbidity düğmesini kontrol et
                    if (button == conditionalButtonTurbidity)
                    {
                        // TurbidityValue ve pO2Target değerlerini Settings.settings'den al
                        double TurbidityValue = Properties.Settings.Default.TurbidityValue;
                        double TurbidityTarget = Properties.Settings.Default.TurbidityTarget;

                        // Değerleri karşılaştır
                        double difference = Math.Abs(TurbidityValue - TurbidityTarget);

                        if (difference < 1)
                        {
                            // Değerler arasındaki fark 1'den az ise butonun arka planını yeşil yap
                            button.Background = new SolidColorBrush(Colors.Green);
                            Properties.Settings.Default.TurbidityConditionalButton = 2; // Yeşil için 2
                        }
                        else
                        {
                            // Değerler farklıysa butonun arka planını sarı yap
                            button.Background = new SolidColorBrush(Colors.Yellow);
                            Properties.Settings.Default.TurbidityConditionalButton = 1; // Sarı için 1
                        }

                        // Settings'i kaydet
                        Properties.Settings.Default.Save();
                    }



                    // Özel olarak conditionalButtonBalance düğmesini kontrol et
                    if (button == conditionalButtonBalance)
                    {
                        // BalanceValue ve BalanceTarget değerlerini Settings.settings'den al
                        double BalanceValue = Properties.Settings.Default.BalanceValue;
                        double BalanceTarget = Properties.Settings.Default.BalanceTarget;

                        // Değerleri karşılaştır
                        double difference = Math.Abs(BalanceValue - BalanceTarget);

                        if (difference < 1)
                        {
                            // Değerler arasındaki fark 1'den az ise butonun arka planını yeşil yap
                            button.Background = new SolidColorBrush(Colors.Green);
                            Properties.Settings.Default.BalanceConditionalButton = 2; // Yeşil için 2
                        }
                        else
                        {
                            // Değerler farklıysa butonun arka planını sarı yap
                            button.Background = new SolidColorBrush(Colors.Yellow);
                            Properties.Settings.Default.BalanceConditionalButton = 1; // Sarı için 1
                        }

                        // Settings'i kaydet
                        Properties.Settings.Default.Save();
                    }




                    // Özel olarak conditionalButtonAirFlow düğmesini kontrol et
                    if (button == conditionalButtonAirFlow)
                    {
                        // AirFlowValue ve AirFlowTarget değerlerini Settings.settings'den al
                        double AirFlowValue = Properties.Settings.Default.AirFlowValue;
                        double AirFlowTarget = Properties.Settings.Default.AirFlowTarget;

                        // Değerleri karşılaştır
                        double difference = Math.Abs(AirFlowValue - AirFlowTarget);

                        if (difference < 1)
                        {
                            // Değerler arasındaki fark 1'den az ise butonun arka planını yeşil yap
                            button.Background = new SolidColorBrush(Colors.Green);
                            Properties.Settings.Default.AirFlowConditionalButton = 2; // Yeşil için 2
                        }
                        else
                        {
                            // Değerler farklıysa butonun arka planını sarı yap
                            button.Background = new SolidColorBrush(Colors.Yellow);
                            Properties.Settings.Default.AirFlowConditionalButton = 1; // Sarı için 1
                        }

                        // Settings'i kaydet
                        Properties.Settings.Default.Save();
                    }






                    if (button == conditionalButtonGas2)
                    {
                        // Gas2Value ve Gas2Target değerlerini Settings.settings'den al
                        double Gas2Value = Properties.Settings.Default.Gas2Value;
                        double Gas2Target = Properties.Settings.Default.Gas2Target;

                        // Değerleri karşılaştır
                        double difference = Math.Abs(Gas2Value - Gas2Target);

                        if (difference < 1)
                        {
                            // Değerler arasındaki fark 1'den az ise butonun arka planını yeşil yap
                            button.Background = new SolidColorBrush(Colors.Green);
                            Properties.Settings.Default.Gas2ConditionalButton = 2; // Yeşil için 2
                        }
                        else
                        {
                            // Değerler farklıysa butonun arka planını sarı yap
                            button.Background = new SolidColorBrush(Colors.Yellow);
                            Properties.Settings.Default.Gas2ConditionalButton = 1; // Sarı için 1
                        }

                        // Settings'i kaydet
                        Properties.Settings.Default.Save();
                    }


                }
            }
            TurbidityValue.Content = Properties.Settings.Default.TurbidityValue.ToString();
            BalanceValue.Content = Properties.Settings.Default.BalanceValue.ToString();
            AirFlowValue.Content = Properties.Settings.Default.AirFlowValue.ToString();
            Gas2Value.Content = Properties.Settings.Default.Gas2Value.ToString();
            if (Properties.Settings.Default.StartButton == 0)
            {
                comparisonTimer.Stop();

            }
            CompareGas2Values();

        }
        //private void ComparisonTimer_Tick(object sender, EventArgs e)
        //{
        //    // Tüm butonları kontrol et
        //    foreach (Button button in FindVisualChildren<Button>(this))
        //    {
        //        if (button.Tag is Grid parentGrid)
        //        {
        //            // Grid içindeki ikinci Label'ı bul
        //            var secondLabel = parentGrid.Children.OfType<Label>().ElementAtOrDefault(1);
        //            // Grid içindeki TextBox'ı bul
        //            var textBox = parentGrid.Children.OfType<Border>().FirstOrDefault()?.Child as TextBox;

        //            if (secondLabel != null && textBox != null)
        //            {
        //                // Label ve TextBox içindeki değerleri karşılaştır
        //                if (secondLabel.Content.ToString() == textBox.Text)
        //                {
        //                    // Değerler aynıysa butonun arka planını yeşil yap
        //                    button.Background = new SolidColorBrush(Colors.Green);
        //                }
        //                else
        //                {
        //                    // Değerler farklıysa butonun arka planını sarı yap
        //                    button.Background = new SolidColorBrush(Colors.Yellow);
        //                }
        //            }
        //        }
        //    }
        //    CompareGas2Values();

        //}













































































































































































































































































        private double turbidityCurrentBalance;
        private double maxBalance = 45.0;
        private double warningOffset = 10.0;
        private DispatcherTimer BalanceTimer;

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
        private bool isTurbidityHighBalance;
        public bool IsTurbidityHighBalance
        {
            get => isTurbidityHighBalance;
            set
            {
                isTurbidityHighBalance = value;
                OnPropertyChanged(nameof(IsTurbidityHighBalance));
            }
        }

        private bool isTurbidityWarningBalance;
        public bool IsTurbidityWarningBalance
        {
            get => isTurbidityWarningBalance;
            set
            {
                isTurbidityWarningBalance = value;
                OnPropertyChanged(nameof(IsTurbidityWarningBalance));
            }
        }

        private bool isTurbidityNormalBalance;
        public bool IsTurbidityNormalBalance
        {
            get => isTurbidityNormalBalance;
            set
            {
                isTurbidityNormalBalance = value;
                OnPropertyChanged(nameof(IsTurbidityNormalBalance));
            }
        }

        private void InitializeBalanceMonitoring()
        {
            BalanceTimer = new DispatcherTimer();
            BalanceTimer.Interval = TimeSpan.FromSeconds(1);
            BalanceTimer.Tick += BalanceTimer_Tick;
            BalanceTimer.Start();
        }

        private void BalanceTimer_Tick(object sender, EventArgs e)
        {
            // Turbidity için sıcaklık değerini al ve güncelle
            turbidityCurrentBalance = GetBalanceFromSensor();
            TurbidityTemperatureIndicator.Text = turbidityCurrentBalance.ToString("F1");
            UpdateTurbidityBalanceStatus();
        }

        private double GetBalanceFromSensor()
        {
            // Gerçek sensör kodunuz buraya gelecek
            // Şimdilik test için rastgele değer üretiyoruz
            Random rand = new Random();
            return 38.85 + rand.NextDouble() * 0.25; // 20-50 derece arası
        }

        private void UpdateTurbidityBalanceStatus()
        {
            if (turbidityCurrentBalance >= maxBalance)
            {
                IsTurbidityHighBalance = true;
                IsTurbidityWarningBalance = false;
                IsTurbidityNormalBalance = false;
            }
            else if (turbidityCurrentBalance >= (maxBalance - warningOffset))
            {
                IsTurbidityHighBalance = false;
                IsTurbidityWarningBalance = true;
                IsTurbidityNormalBalance = false;
            }
            else
            {
                IsTurbidityHighBalance = false;
                IsTurbidityWarningBalance = false;
                IsTurbidityNormalBalance = true;
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
                "ellipse13" => conditionalButtonGas2,
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



        private void UpdateBorderVisibilities()
        {
            if (FindName("AirFlowTargetBorder") is Border airFlowBorder)
            {
                // AirFlowTargetBorder=0 ise visible, AirFlowTargetBorder=1 ise collapsed
                airFlowBorder.Visibility = Properties.Settings.Default.AirFlowTargetBorder == 0 ?
                    Visibility.Visible : Visibility.Collapsed;
            }

            if (FindName("Gas2TargetBorder") is Border gas2Border)
            {
                // Gas2TargetBorder=0 ise visible, Gas2TargetBorder=1 ise collapsed
                gas2Border.Visibility = Properties.Settings.Default.Gas2TargetBorder == 0 ?
                    Visibility.Visible : Visibility.Collapsed;
            }
        }


        private void CompareGas2Values()
        {
            if (double.TryParse(Gas2Value.Content?.ToString(), out double value) &&
                double.TryParse(Gas2Target.Text, out double target))
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


        private void EditTurbidity_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var editTurbidityWindow = new WpfApp1.EditPages.EditTurbidity();
            editTurbidityWindow.Show();
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

    }
}
