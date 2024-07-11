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

        }
        private void TogglePopupButton_Click(object sender, RoutedEventArgs e)
        {
            // Animasyonun hedef Margin değerini ve okun yönünü belirle
            Thickness targetMargin;
            string arrowDirection;

            if (TopGrid.Margin.Top == 0)
            {
                targetMargin = new Thickness(0, -30, 0, 0);
                arrowDirection = "↓"; // Grid yukarı çıkınca buton aşağı ok göstersin
            }
            else
            {
                targetMargin = new Thickness(0, 0, 0, 0);
                arrowDirection = "↑"; // Grid aşağı inince buton yukarı ok göstersin
            }

            // Margin animasyonu oluştur
            ThicknessAnimation marginAnimation = new ThicknessAnimation
            {
                To = targetMargin,
                Duration = TimeSpan.FromSeconds(0.5),
                FillBehavior = FillBehavior.Stop
            };

            // Animasyon tamamlandığında Margin'i ve okun yönünü manuel olarak ayarla
            marginAnimation.Completed += (s, args) =>
            {
                TopGrid.Margin = targetMargin;
                ((Button)sender).Content = new TextBlock { Text = arrowDirection, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            };

            // Animasyonu Grid'e uygula
            TopGrid.BeginAnimation(MarginProperty, marginAnimation);
        }

        private void ToggleRightGridButton_Click(object sender, RoutedEventArgs e)
        {
            // Animasyonun hedef Margin değerini belirle
            Thickness targetMargin;
            string arrowDirection;

            // Grid'in mevcut sağ Margin değerini kontrol et
            if (RightGrid.Margin.Right < 0) // Grid tamamen veya kısmen gizli
            {
                targetMargin = new Thickness(0, 54, 0, 0); // Grid'i görünür yap
                arrowDirection = "→"; // Ok yönünü sağa çevir
            }
            else
            {
                targetMargin = new Thickness(0, 54, -325, 0); // Grid'i gizle
                arrowDirection = "←"; // Ok yönünü sola çevir
            }

            // Margin animasyonu oluştur
            ThicknessAnimation marginAnimation = new ThicknessAnimation
            {
                From = RightGrid.Margin,
                To = targetMargin,
                Duration = TimeSpan.FromSeconds(0.5),
                FillBehavior = FillBehavior.Stop
            };

            // Animasyon tamamlandığında Margin'i ve okun yönünü manuel olarak ayarla
            marginAnimation.Completed += (s, args) =>
            {
                RightGrid.Margin = targetMargin;
                ((Button)sender).Content = new TextBlock { Text = arrowDirection, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            };

            // Animasyonu Grid'e uygula
            RightGrid.BeginAnimation(MarginProperty, marginAnimation);
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

    }
}
