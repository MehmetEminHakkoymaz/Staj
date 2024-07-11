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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for _00000000000.xaml
    /// </summary>
    public partial class _00000000000 : Window
    {
     
        private bool isAtRight = false; // Yuvarlağın mevcut pozisyonunu takip etmek için bir bayrak

        private bool isEllipseMovedRight = false;

        public _00000000000()
        {
            InitializeComponent();
            ellipse1.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            //Saat İçin Gerekli Yapı
            StartClock();

        }


        //YUKARI AŞAĞI MENÜ İÇİN GEREKLİ YAPI


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
                    targetMargin = new Thickness(0, 54, -325, 0); // Margin değerlerini projenize göre ayarlayın
                    targetImage = "pack://application:,,,/WpfApp1;component/images/Left.png";
                }
                else
                {
                    // RightGrid'i göster
                    targetMargin = new Thickness(0, 54, 0, 0); // Margin değerlerini projenize göre ayarlayın
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
            double canvasWidth = 66.87;
            double ellipseWidth = 20;
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
            };

            ellipse1.BeginAnimation(Canvas.LeftProperty, animation);
        }

        //SAAT İÇİN GEREKLİ YAPI
        private void StartClock()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ClockTextBlock.Text = DateTime.Now.ToString("HH : mm : ss");
        }
    }
}
