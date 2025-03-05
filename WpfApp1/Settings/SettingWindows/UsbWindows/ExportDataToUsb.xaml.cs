using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using OfficeOpenXml;
using System.Data.SQLite;
using System.IO; // IO işlemleri için
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Management;


namespace WpfApp1.Settings.SettingWindows.UsbWindows
{
    /// <summary>
    /// Interaction logic for ExportDataToUsb.xaml
    /// </summary>
    public partial class ExportDataToUsb : Window
    {
        private string selectedUsbPath;
        private static string dbPath = "UserDatabase.db";
        private static string connectionString = "Data Source=UserDatabase.db;Version=3;";

        static ExportDataToUsb()
        {
            // Static constructor'da lisans ayarı
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        public ExportDataToUsb()
        {
            InitializeComponent();
            LoadDatabaseTables();
            LoadUsbDrives();
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
            this.Topmost = true;
        }

        private void LoadDatabaseTables()
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    var tables = connection.Query<string>(
                        "SELECT name FROM sqlite_master WHERE type='table' AND name != 'sqlite_sequence'");
                    DatabaseTablesListBox.ItemsSource = tables;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading database tables: {ex.Message}");
            }
        }

        private void LoadUsbDrives()
        {
            try
            {
                UsbDrivesComboBox.Items.Clear();
                foreach (DriveInfo drive in DriveInfo.GetDrives().Where(d => d.IsReady))
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        string label = string.IsNullOrEmpty(drive.VolumeLabel)
                            ? "USB Drive"
                            : drive.VolumeLabel;

                        UsbDrivesComboBox.Items.Add(new ComboBoxItem
                        {
                            Content = $"{drive.Name} ({label})",
                            Tag = drive.Name
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading USB drives: {ex.Message}");
            }
        }


        private void RefreshUsbContents()
        {
            if (string.IsNullOrEmpty(selectedUsbPath)) return;

            try
            {
                var files = Directory.GetFiles(selectedUsbPath, "*.xlsx")
                                   .Select(System.IO.Path.GetFileName); // Path'i açıkça belirt
                UsbContentsListBox.ItemsSource = files;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading USB contents: {ex.Message}");
            }
        }
        private async void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            if (DatabaseTablesListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a table to export.");
                return;
            }

            if (string.IsNullOrEmpty(selectedUsbPath))
            {
                MessageBox.Show("Please select a USB drive.");
                return;
            }

            try
            {
                TransferButton.IsEnabled = false;
                Mouse.OverrideCursor = Cursors.Wait;

                string tableName = DatabaseTablesListBox.SelectedItem.ToString();
                string fileName = $"{tableName}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                string filePath = System.IO.Path.Combine(selectedUsbPath, fileName); // Path'i açıkça belirt

                await Task.Run(() => ExportTableToExcel(tableName, filePath));
                RefreshUsbContents();
                MessageBox.Show("Export completed successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during export: {ex.Message}");
            }
            finally
            {
                TransferButton.IsEnabled = true;
                Mouse.OverrideCursor = null;
            }
        }

        private void ExportTableToExcel(string tableName, string filePath)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var data = connection.Query($"SELECT * FROM [{tableName}]").ToList();

                if (!data.Any())
                {
                    throw new Exception("No data found in the selected table.");
                }

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets.Add(tableName);
                    var properties = ((IDictionary<string, object>)data.First()).Keys.ToList();

                    // Başlıkları formatla
                    for (int i = 0; i < properties.Count; i++)
                    {
                        var cell = worksheet.Cells[1, i + 1];
                        cell.Value = properties[i];
                        cell.Style.Font.Bold = true;
                        cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    }

                    // Verileri yaz - LoadFromArray yerine doğrudan hücrelere yazma
                    for (int row = 0; row < data.Count; row++)
                    {
                        var item = (IDictionary<string, object>)data[row];
                        for (int col = 0; col < properties.Count; col++)
                        {
                            worksheet.Cells[row + 2, col + 1].Value = item[properties[col]]?.ToString();
                        }
                    }

                    worksheet.Cells.AutoFitColumns();

                    try
                    {
                        package.Save();
                    }
                    catch (IOException)
                    {
                        throw new Exception("The file is being used by another program. Please close it and try again.");
                    }
                }
            }
        }
        private void UsbDrivesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsbDrivesComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                selectedUsbPath = selectedItem.Tag.ToString();
                RefreshUsbContents();
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadUsbDrives();
            RefreshUsbContents();
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            GC.Collect(); // Excel işlemleri için memory cleanup
            GC.WaitForPendingFinalizers();
        }
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
