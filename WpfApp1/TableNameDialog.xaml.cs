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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for TableNameDialog.xaml
    /// </summary>
    public partial class TableNameDialog : Window
    {
        public string TableName => TableNameTextBox.Text;

        public TableNameDialog()
        {
            InitializeComponent();


            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
            this.Topmost = true;
        }
        private bool IsValidTableName(string tableName)
        {
            // Boş veya null kontrolü
            if (string.IsNullOrWhiteSpace(tableName))
                return false;

            // İlk karakter harf olmalı
            if (!char.IsLetter(tableName[0]))
                return false;

            // Uzunluk kontrolü (1-30 karakter)
            if (tableName.Length > 30)
                return false;

            // Sadece harf, rakam ve alt çizgi içermeli
            return tableName.All(c => char.IsLetterOrDigit(c) || c == '_');
        }

        // TextBox'a her karakter girildiğinde kontrol et
        private void KeyboardButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                TableNameTextBox.Text += button.Content.ToString();
                // Her karakter girişinde uyarıyı gizle
                WarningText.Visibility = Visibility.Collapsed;
            }
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (TableNameTextBox.Text.Length > 0)
            {
                TableNameTextBox.Text = TableNameTextBox.Text.Substring(0, TableNameTextBox.Text.Length - 1);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TableNameTextBox.Text))
            {
                WarningText.Text = "Please enter a table name.";
                WarningText.Visibility = Visibility.Visible;
                return;
            }

            if (!IsValidTableName(TableNameTextBox.Text))
            {
                WarningText.Visibility = Visibility.Visible;
                return;
            }

            WarningText.Visibility = Visibility.Collapsed;
            DialogResult = true;
        }

    }
}
