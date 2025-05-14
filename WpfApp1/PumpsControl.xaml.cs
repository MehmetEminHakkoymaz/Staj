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
using System.Globalization;
using WpfApp1;
using System.ComponentModel;

namespace WpfApp1
{
    public partial class PumpsControl : UserControl
    {
        private MainWindow mainWindow;
        private DateTime buttonPressStartTime;

        public PumpsControl(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            ellipse20.MouseLeftButtonDown += ellipse20_MouseLeftButtonDown;
            ellipse21.MouseLeftButtonDown += ellipse21_MouseLeftButtonDown;
            ellipse22.MouseLeftButtonDown += ellipse22_MouseLeftButtonDown;
            KeypadControl.ValueSelected += KeyPadControl_ValueSelected;
            comparisonTimer.Interval = TimeSpan.FromSeconds(1); // 1 saniyelik aralıklarla
            comparisonTimer.Tick += ComparisonTimer_Tick; // Zamanlayıcı olayı

            // TextBox'ları başlangıçta ayarla
            LoadTargetValues();

            // TextBox eventlerini bağla
            RegisterTextBoxEvents();
            Properties.Settings.Default.PropertyChanged += Settings_PropertyChanged;
            UpdateBorderVisibilities();

            // Check ellipse10 state in ExtendedControl and update Pump4 accordingly
            Loaded += PumpsControl_Loaded;
        }
        private void PumpsControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Update Pump4 content and visibility based on ellipse10 state
            UpdatePump4BasedOnTurbidity();
        }
        private void UpdatePump4BasedOnTurbidity()
        {
            try
            {
                // Get the ellipse10 from ExtendedControl
                if (mainWindow?.extendedControl != null)
                {
                    Ellipse ellipse10 = mainWindow.extendedControl.FindName("ellipse10") as Ellipse;
                    if (ellipse10 != null)
                    {
                        Canvas canvas10 = ellipse10.Parent as Canvas;
                        if (canvas10 != null)
                        {
                            double canvasWidth = canvas10.ActualWidth;
                            double ellipseRightPosition = Canvas.GetLeft(ellipse10) + ellipse10.Width;

                            // Check if ellipse10 is active (positioned on the right side)
                            if (ellipseRightPosition > canvasWidth / 2)
                            {
                                // Update Pump4Content label
                                Pump4Content.Content = "Pump4 ← Turbidity";

                                // Set Pump4TargetBorder visibility based on HidePump4Border setting
                                //Pump4TargetBorder.Visibility = Properties.Settings.Default.HidePump4Border ?
                                //    Visibility.Collapsed : Visibility.Visible;

                                Pump4TargetBorder.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                // If ellipse10 is not active, keep the default content
                                Pump4Content.Content = "Pump4← ";
                                Pump4TargetBorder.Visibility = Visibility.Visible;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating Pump4 based on Turbidity: {ex.Message}",
                               "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Ellipse20_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
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
        private void EditPump5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var editPump5Window = new WpfApp1.EditPages.EditPump5();
            editPump5Window.Show();
        }
        private void EditPump6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var editPump6Window = new WpfApp1.EditPages.EditPump6();
            editPump6Window.Show();
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
                    CheckEllipsePositionAndSetButtonVisibility(ellipse24, conditionalButtonPump5);
                    CheckEllipsePositionAndSetButtonVisibility(ellipse25, conditionalButtonPump6);

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
            CheckEllipsePositionAndSetButtonVisibility(ellipse24, conditionalButtonPump5);
            CheckEllipsePositionAndSetButtonVisibility(ellipse25, conditionalButtonPump6);

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
            if (Properties.Settings.Default.StartButton ==2)
            {
                conditionalButtonPump1.Visibility = Visibility.Collapsed;
                conditionalButtonPump2.Visibility = Visibility.Collapsed;
                conditionalButtonPump3.Visibility = Visibility.Collapsed;
                conditionalButtonPump4.Visibility = Visibility.Collapsed;
                conditionalButtonPump5.Visibility = Visibility.Collapsed;
                conditionalButtonPump6.Visibility = Visibility.Collapsed;
            }

            UpdatePump4BasedOnTurbidity();
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

        // Change the LoadTargetValues method to use PumpXTarget instead of PumpXTargetValue
        private void LoadTargetValues()
        {
            try
            {
                // Kaydedilmiş değerleri TextBox'lara yükle
                Pump1Target.Text = Properties.Settings.Default.Pump1Target.ToString(CultureInfo.CurrentCulture);
                Pump2Target.Text = Properties.Settings.Default.Pump2Target.ToString(CultureInfo.CurrentCulture);
                Pump3Target.Text = Properties.Settings.Default.Pump3Target.ToString(CultureInfo.CurrentCulture);
                Pump4Target.Text = Properties.Settings.Default.Pump4Target.ToString(CultureInfo.CurrentCulture);
                Pump5Target.Text = Properties.Settings.Default.Pump5Target.ToString(CultureInfo.CurrentCulture);
                Pump6Target.Text = Properties.Settings.Default.Pump6Target.ToString(CultureInfo.CurrentCulture);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading target values: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Change the SaveTargetValues method to use PumpXTarget instead of PumpXTargetValue
        private void SaveTargetValues()
        {
            try
            {
                // TextBox değerlerini ayarlara kaydet
                if (double.TryParse(Pump1Target.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double pump1Value))
                    Properties.Settings.Default.Pump1Target = pump1Value;

                if (double.TryParse(Pump2Target.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double pump2Value))
                    Properties.Settings.Default.Pump2Target = pump2Value;

                if (double.TryParse(Pump3Target.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double pump3Value))
                    Properties.Settings.Default.Pump3Target = pump3Value;

                if (double.TryParse(Pump4Target.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double pump4Value))
                    Properties.Settings.Default.Pump4Target = pump4Value;

                if (double.TryParse(Pump5Target.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double pump5Value))
                    Properties.Settings.Default.Pump5Target = pump5Value;

                if (double.TryParse(Pump6Target.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double pump6Value))
                    Properties.Settings.Default.Pump6Target = pump6Value;

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

        private void ellipse24_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Peristaltic pump must be installed to activate these features.",
              "Configuration Required",
              MessageBoxButton.OK,
              MessageBoxImage.Warning);
            return;
        }

        private void ellipse25_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Peristaltic pump must be installed to activate these features.",
              "Configuration Required",
              MessageBoxButton.OK,
              MessageBoxImage.Warning);
            return;
        }

        private void ellipse20_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Properties.Settings.Default.EditPump1Feature == 0)
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("Redox selection is required. Please go to EditRedox settings and select an option.",
                              "Configuration Required",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return; // Ellipse4'ün durumunu değiştirmeden fonksiyonu sonlandır
            }


            // Normal ellipse tıklama olayını çağır
            Ellipse_MouseLeftButtonDown(sender, e);
        }

        private void ellipse21_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Properties.Settings.Default.EditPump2Feature == 0)
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("Redox selection is required. Please go to EditRedox settings and select an option.",
                              "Configuration Required",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return; // Ellipse4'ün durumunu değiştirmeden fonksiyonu sonlandır
            }

            // Normal ellipse tıklama olayını çağır
            Ellipse_MouseLeftButtonDown(sender, e);
        }

        private void ellipse22_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Properties.Settings.Default.EditPump3Feature == 0)
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("Redox selection is required. Please go to EditRedox settings and select an option.",
                              "Configuration Required",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return; // Ellipse4'ün durumunu değiştirmeden fonksiyonu sonlandır
            }

            // Normal ellipse tıklama olayını çağır
            Ellipse_MouseLeftButtonDown(sender, e);
        }

        private void UpdateBorderVisibilities()
        {
            if (FindName("Pump1TargetBorder") is Border pump1Border)
            {
                pump1Border.Visibility = Properties.Settings.Default.Pump1TargetBorder == 1 ?
                    Visibility.Collapsed : Visibility.Visible;
            }
            if (FindName("Pump2TargetBorder") is Border pump2Border)
            {
                pump2Border.Visibility = Properties.Settings.Default.Pump2TargetBorder == 1 ?
                    Visibility.Collapsed : Visibility.Visible;
            }
            if (FindName("Pump3TargetBorder") is Border pump3Border)
            {
                pump3Border.Visibility = Properties.Settings.Default.Pump3TargetBorder == 1 ?
                    Visibility.Collapsed : Visibility.Visible;
            }
            if (FindName("Pump4TargetBorder") is Border pump4Border)
            {
                pump4Border.Visibility = Properties.Settings.Default.Pump4TargetBorder == 1 ?
                    Visibility.Collapsed : Visibility.Visible;
            }

            // Also update Pump4 based on turbidity
            UpdatePump4BasedOnTurbidity();
        }

        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Eğer ilgili ayarlar değiştiyse border görünürlüklerini güncelle
            if (e.PropertyName == "HidePump1Border" ||
                  e.PropertyName == "HidePump2Border" ||
                  e.PropertyName == "HidePump3Border" ||
                  e.PropertyName == "HidePump4Border" ||
                  e.PropertyName == "EditPump1Feature" ||
                  e.PropertyName == "EditPump2Feature" ||
                  e.PropertyName == "EditPump3Feature" ||
                  e.PropertyName == "EditPump4Feature")
            {
                UpdateBorderVisibilities();
            }
        }

        private void ellipse23_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // After handling the ellipse click, update the Pump4 content
            UpdatePump4BasedOnTurbidity();
            if (Pump4TargetBorder.Visibility == Visibility.Collapsed)
            {
                // Eğer None seçiliyse, kullanıcıya bir mesaj gösterin
                MessageBox.Show("Redox selection is required. Please go to EditRedox settings and select an option.",
                              "Configuration Required",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return; // Ellipse4'ün durumunu değiştirmeden fonksiyonu sonlandır
            }
            else
            {
                Ellipse_MouseLeftButtonDown(sender, e);
            }

        }
    }
}
