using System;
using System.Collections.Generic;
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
    public partial class ExitGasControl : UserControl
    {
        private MainWindow mainWindow;

        public ExitGasControl(MainWindow mainWindow)
        {
            InitializeComponent();
            ellipse14.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            this.mainWindow = mainWindow;
            KeypadControl.ValueSelected += KeyPadControl_ValueSelected;
            comparisonTimer.Interval = TimeSpan.FromSeconds(1); // 1 saniyelik aralıklarla
            comparisonTimer.Tick += ComparisonTimer_Tick; // Zamanlayıcı olayı
        }

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

        private TextBox activeTextBox = null;

        private TextBox currentTextBox = null;

        private DispatcherTimer comparisonTimer = new DispatcherTimer();

        private void KeyPadControl_ValueSelected(object sender, string value)
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
                    CheckEllipsePositionAndSetButtonVisibility(ellipse14, conditionalButtonTurbidity);
                    CheckEllipsePositionAndSetButtonVisibility(ellipse15, conditionalButtonBalance);
                };

                clickedEllipse.BeginAnimation(Canvas.LeftProperty, animation);
            }
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckEllipsePositionAndSetButtonVisibility(ellipse14, conditionalButtonTurbidity);
            CheckEllipsePositionAndSetButtonVisibility(ellipse15, conditionalButtonBalance);
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


    }
}
