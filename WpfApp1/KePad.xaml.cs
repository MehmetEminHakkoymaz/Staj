using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for KePad.xaml
    /// </summary>
    public partial class KePad : UserControl
    {
        public KePad()
        {
            InitializeComponent();
            TextBox numbersTextBox = this.FindName("numbers") as TextBox;
            if (numbersTextBox != null)
            {
                // numbersTextBox ile işlem yap
            }
        }


        private void KeyButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var value = button.Content.ToString(); // Veya seçilen değeri temsil eden başka bir özellik
            OnValueSelected(value); // Olayı tetikle
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            // Burada tuşa basıldığında yapılacak işlemleri ekleyin.
            // Örneğin, bir TextBox'a sayı eklemek.
        }
        public void SetLabelContent(string content)
        {
            InfoLabel.Content = content;
        }

        private void Button_Click_(object sender, RoutedEventArgs e)
        {
            numbers.Text += ".";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            numbers.Text += "1";
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (numbers.Text.Length > 0)
            {
                numbers.Text = numbers.Text.Substring(0, numbers.Text.Length - 1);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            numbers.Text += "2";
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            numbers.Text += "3";
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            numbers.Text += "4";
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            numbers.Text += "5";
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            numbers.Text += "6";
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            numbers.Text += "7";
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            numbers.Text += "8";
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            numbers.Text += "9";
        }

        private void Button_Click_0(object sender, RoutedEventArgs e)
        {
            numbers.Text += "0";
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            var parentPopup = this.Parent as Popup;
            if (parentPopup != null)
            {
                parentPopup.IsOpen = false;
            }
            numbers.Text = string.Empty; // TextBox'ı boşalt
        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            // numbers TextBox'ın mevcut değerini kullanarak ValueSelected olayını tetikle
            ValueSelected?.Invoke(this, numbers.Text);
            var parentPopup = this.Parent as Popup;
            if (parentPopup != null)
            {
                parentPopup.IsOpen = false;
            }
            numbers.Text = string.Empty; // TextBox'ı boşalt
        }
        public event EventHandler<string> ValueSelected;
        protected virtual void OnValueSelected(string value)
        {
            ValueSelected?.Invoke(this, value);
        }
    }
}
