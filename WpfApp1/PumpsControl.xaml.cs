﻿using System;
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
using System.Globalization;
using WpfApp1;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for PumpsControl.xaml
    /// </summary>
    public partial class PumpsControl : UserControl
    {
        private MainWindow mainWindow;
        private DateTime buttonPressStartTime;

        public PumpsControl(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            KeypadControl.ValueSelected += KeyPadControl_ValueSelected;
            comparisonTimer.Interval = TimeSpan.FromSeconds(1); // 1 saniyelik aralıklarla
            comparisonTimer.Tick += ComparisonTimer_Tick; // Zamanlayıcı olayı

            // TextBox'ları başlangıçta ayarla
            LoadTargetValues();

            // TextBox eventlerini bağla
            RegisterTextBoxEvents();
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

        private void AutoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenAutoWindow openAutoWindow = new OpenAutoWindow(mainWindow);
            openAutoWindow.Show();
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

        //ÇOK ÖNEMLİ
        private void EditPump1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var editPump1Window = new WpfApp1.EditPages.EditPump1();
            editPump1Window.Show();
        }
        private void EditPump2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var editPump2Window = new WpfApp1.EditPages.EditPump2();
            editPump2Window.Show();
        }

        private void EditPump3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var editPump3Window = new WpfApp1.EditPages.EditPump3();
            editPump3Window.Show();
        }

        private void EditPump4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var editPump4Window = new WpfApp1.EditPages.EditPump4();
            editPump4Window.Show();
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
                    CheckEllipsePositionAndSetButtonVisibility(ellipse20, conditionalButtonPump1);
                    CheckEllipsePositionAndSetButtonVisibility(ellipse21, conditionalButtonPump2);
                    CheckEllipsePositionAndSetButtonVisibility(ellipse22, conditionalButtonPump3);
                    CheckEllipsePositionAndSetButtonVisibility(ellipse23, conditionalButtonPump4);
                };

                clickedEllipse.BeginAnimation(Canvas.LeftProperty, animation);
            }
        }
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckEllipsePositionAndSetButtonVisibility(ellipse20, conditionalButtonPump1);
            CheckEllipsePositionAndSetButtonVisibility(ellipse21, conditionalButtonPump2);
            CheckEllipsePositionAndSetButtonVisibility(ellipse22, conditionalButtonPump3);
            CheckEllipsePositionAndSetButtonVisibility(ellipse23, conditionalButtonPump4);
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

        // Yardımcı metod: Belirli bir türdeki tüm görsel çocukları bulur
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

        private void Pump1FillClick_MouseDown(object sender, MouseButtonEventArgs e)
        {
            buttonPressStartTime = DateTime.Now;
        }

        private void Pump1FillClick_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DateTime buttonPressEndTime = DateTime.Now;
            TimeSpan pressDuration = buttonPressEndTime - buttonPressStartTime;
            MainWindow.Pump1Fill1ButtonPressDuration = pressDuration.TotalSeconds;
        }

        private void Pump2FillClick_MouseDown(object sender, MouseButtonEventArgs e)
        {
            buttonPressStartTime = DateTime.Now;
        }

        private void Pump2FillClick_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DateTime buttonPressEndTime = DateTime.Now;
            TimeSpan pressDuration = buttonPressEndTime - buttonPressStartTime;
            MainWindow.Pump2FillButtonPressDuration = pressDuration.TotalSeconds;
        }

        private void Pump3FillClick_MouseDown(object sender, MouseButtonEventArgs e)
        {
            buttonPressStartTime = DateTime.Now;
        }

        private void Pump3FillClick_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DateTime buttonPressEndTime = DateTime.Now;
            TimeSpan pressDuration = buttonPressEndTime - buttonPressStartTime;
            MainWindow.Pump3FillButtonPressDuration = pressDuration.TotalSeconds;
        }

        private void Pump4FillClick_MouseDown(object sender, MouseButtonEventArgs e)
        {
            buttonPressStartTime = DateTime.Now;
        }

        private void Pump4FillClick_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DateTime buttonPressEndTime = DateTime.Now;
            TimeSpan pressDuration = buttonPressEndTime - buttonPressStartTime;
            MainWindow.Pump4FillButtonPressDuration = pressDuration.TotalSeconds;
        }

        private void Pump1EmptyClick_MouseDown(object sender, MouseButtonEventArgs e)
        {
            buttonPressStartTime = DateTime.Now;
        }

        private void Pump1EmptyClick_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DateTime buttonPressEndTime = DateTime.Now;
            TimeSpan pressDuration = buttonPressEndTime - buttonPressStartTime;
            MainWindow.Pump1EmptyButtonPressDuration = pressDuration.TotalSeconds;
        }

        private void Pump2EmptyClick_MouseDown(object sender, MouseButtonEventArgs e)
        {
            buttonPressStartTime = DateTime.Now;
        }

        private void Pump2EmptyClick_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DateTime buttonPressEndTime = DateTime.Now;
            TimeSpan pressDuration = buttonPressEndTime - buttonPressStartTime;
            MainWindow.Pump2EmptyButtonPressDuration = pressDuration.TotalSeconds;
        }

        private void Pump3EmptyClick_MouseDown(object sender, MouseButtonEventArgs e)
        {
            buttonPressStartTime = DateTime.Now;
        }

        private void Pump3EmptyClick_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DateTime buttonPressEndTime = DateTime.Now;
            TimeSpan pressDuration = buttonPressEndTime - buttonPressStartTime;
            MainWindow.Pump3EmptyButtonPressDuration = pressDuration.TotalSeconds;
        }

        private void Pump4EmptyClick_MouseDown(object sender, MouseButtonEventArgs e)
        {
            buttonPressStartTime = DateTime.Now;
        }

        private void Pump4EmptyClick_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DateTime buttonPressEndTime = DateTime.Now;
            TimeSpan pressDuration = buttonPressEndTime - buttonPressStartTime;
            MainWindow.Pump4EmptyButtonPressDuration = pressDuration.TotalSeconds;
        }

        // Kayıtlı değerleri yüklemek için yeni bir metot ekleyin
        private void LoadTargetValues()
        {
            try
            {
                // Kaydedilmiş değerleri TextBox'lara yükle
                Pump1Target.Text = Properties.Settings.Default.Pump1TargetValue.ToString(CultureInfo.CurrentCulture);
                Pump2Target.Text = Properties.Settings.Default.Pump2TargetValue.ToString(CultureInfo.CurrentCulture);
                Pump3Target.Text = Properties.Settings.Default.Pump3TargetValue.ToString(CultureInfo.CurrentCulture);
                Pump4Target.Text = Properties.Settings.Default.Pump4TargetValue.ToString(CultureInfo.CurrentCulture);
                Pump5Target.Text = Properties.Settings.Default.Pump5TargetValue.ToString(CultureInfo.CurrentCulture);
                Pump6Target.Text = Properties.Settings.Default.Pump6TargetValue.ToString(CultureInfo.CurrentCulture);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading target values: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Değerleri kaydetmek için yeni bir metot ekleyin
        private void SaveTargetValues()
        {
            try
            {
                // TextBox değerlerini ayarlara kaydet
                if (double.TryParse(Pump1Target.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double pump1Value))
                    Properties.Settings.Default.Pump1TargetValue = pump1Value;

                if (double.TryParse(Pump2Target.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double pump2Value))
                    Properties.Settings.Default.Pump2TargetValue = pump2Value;

                if (double.TryParse(Pump3Target.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double pump3Value))
                    Properties.Settings.Default.Pump3TargetValue = pump3Value;

                if (double.TryParse(Pump4Target.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double pump4Value))
                    Properties.Settings.Default.Pump4TargetValue = pump4Value;

                if (double.TryParse(Pump5Target.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double pump5Value))
                    Properties.Settings.Default.Pump5TargetValue = pump5Value;

                if (double.TryParse(Pump6Target.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double pump6Value))
                    Properties.Settings.Default.Pump6TargetValue = pump6Value;

                // Değişiklikleri kaydet
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving target values: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // TextBox eventlerini bağlamak için yeni bir metot ekleyin
        private void RegisterTextBoxEvents()
        {
            // TextChanged olaylarını bağla
            Pump1Target.TextChanged += TextBox_TextChanged;
            Pump2Target.TextChanged += TextBox_TextChanged;
            Pump3Target.TextChanged += TextBox_TextChanged;
            Pump4Target.TextChanged += TextBox_TextChanged;
            Pump5Target.TextChanged += TextBox_TextChanged;
            Pump6Target.TextChanged += TextBox_TextChanged;

            // Ondalık sayı girişine izin veren TextBox_PreviewTextInput metodunu bağla
            Pump1Target.PreviewTextInput += TextBox_PreviewTextInput;
            Pump2Target.PreviewTextInput += TextBox_PreviewTextInput;
            Pump3Target.PreviewTextInput += TextBox_PreviewTextInput;
            Pump4Target.PreviewTextInput += TextBox_PreviewTextInput;
            Pump5Target.PreviewTextInput += TextBox_PreviewTextInput;
            Pump6Target.PreviewTextInput += TextBox_PreviewTextInput;
        }

        // KeyPad_ValueSelected metodunu değiştirin - ondalık sayı desteği ekleyin
        private void KeyPadControl_ValueSelected(object sender, string value)
        {
            if (activeTextBox != null)
            {
                // Nokta ve virgül desteği ekleyin
                string normalizedValue = value.Replace(',', '.');

                if (activeTextBox.Tag is string tag)
                {
                    string[] limits = tag.ToString().Split(',');

                    if (limits.Length == 2 &&
                        double.TryParse(limits[0], out double min) &&
                        double.TryParse(limits[1], out double max) &&
                        double.TryParse(normalizedValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double doubleValue))
                    {
                        if (doubleValue >= min && doubleValue <= max)
                        {
                            activeTextBox.Text = doubleValue.ToString(CultureInfo.CurrentCulture);
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
                        activeTextBox.Text = normalizedValue;
                    }
                }
                else
                {
                    // Tag yoksa direkt atama yap
                    activeTextBox.Text = normalizedValue;
                }

                // Her değişiklikte değerleri kaydet
                SaveTargetValues();
            }
        }

        // TextBox'lara sadece sayı ve ondalık ayraç girilmesine izin veren metot ekleyin
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Nokta veya virgül içeren değerleri kabul et
            bool isValid = e.Text.All(c => char.IsDigit(c) || c == '.' || c == ',');

            // TextBox'ta zaten nokta veya virgül varsa tekrar girilmesini engelle
            if (isValid && (e.Text == "." || e.Text == ","))
            {
                if (sender is TextBox textBox)
                {
                    isValid = !textBox.Text.Contains(".") && !textBox.Text.Contains(",");
                }
            }

            e.Handled = !isValid;
        }

        // TextBox değeri değiştiğinde çağrılan metot
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TextBox değeri değiştiğinde hemen kaydet
            SaveTargetValues();
        }
    }
}
