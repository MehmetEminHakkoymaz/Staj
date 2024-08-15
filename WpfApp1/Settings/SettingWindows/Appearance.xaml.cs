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

namespace WpfApp1.Settings.SettingWindows
{
    /// <summary>
    /// Interaction logic for Appearance.xaml
    /// </summary>
    public partial class Appearance : UserControl
    {
        public Appearance()
        {
            InitializeComponent();
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
                };

                clickedEllipse.BeginAnimation(Canvas.LeftProperty, animation);
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
                        activeTextBox = sender as TextBox;
                        KeypadPopup.IsOpen = true;
                        KeypadControl.SetLabelContent(labelContent);
                    }
                }
            }
        }

        private void KeyPadControl_ValueSelected(object sender, string value)
        {
            if (activeTextBox != null)
            {
                activeTextBox.Text = value; // KeyPad'den gelen değeri aktif TextBox'a atayın
            }
        }

    }
}
