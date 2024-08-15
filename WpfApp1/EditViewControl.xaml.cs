using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class EditViewControl : UserControl
    {
        private MainWindow mainWindow;
        private int checkedCount = 0;
        private const int MaxCheckedCount = 10;

        public EditViewControl(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            InitializeCheckBoxEvents();
            UpdateSelectedCountText();
            //CalculateGreenCheckBoxSum();

        }


        private void InitializeCheckBoxEvents()
        {
            foreach (var child in LogicalTreeHelper.GetChildren(this).OfType<CheckBox>())
            {
                child.Checked += CheckBox_Checked;
                child.Unchecked += CheckBox_Unchecked;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            checkedCount++;
            //CalculateGreenCheckBoxSum();
            UpdateSelectedCountText();
            if (checkedCount >= MaxCheckedCount)
            {
                DisableUncheckedCheckBoxes();
            }
            if (sender is CheckBox checkBox)
            {
                if (checkBox.Name == "Temperature")
                {
                    int value = 1;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.AddToTotal(value);
                }
                else if (checkBox.Name == "Stirrer")
                {
                    int value = 2;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.AddToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "pH")
                {
                    int value = 4;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.AddToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "pO2")
                {
                    int value = 8;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.AddToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "Gas1")
                {
                    int value = 16;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.AddToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "Gas2")
                {
                    int value = 32;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.AddToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "Gas3")
                {
                    int value = 64;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.AddToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "Gas4")
                {
                    int value = 128;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.AddToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "Foam")
                {
                    int value = 256;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.AddToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "Turbidity")
                {
                    int value = 512;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.AddToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "Balance")
                {
                    int value = 1024;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.AddToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "AirFlow")
                {
                    int value = 2048;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.AddToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "Gas2Flow")
                {
                    int value = 4096;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.AddToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "ExitTurbidity")
                {
                    int value = 8192;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.AddToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "ExitBalance")
                {
                    int value = 16384;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.AddToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }


            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            checkedCount--;
            //CalculateGreenCheckBoxSum();
            UpdateSelectedCountText();
            if (checkedCount < MaxCheckedCount)
            {
                EnableAllCheckBoxes();
            }
            if (sender is CheckBox checkBox)
            {
                if (checkBox.Name == "Temperature")
                {
                    int value = 1;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.SubToTotal(value);
                }
                else if (checkBox.Name == "Stirrer")
                {
                    int value = 2;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.SubToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "pH")
                {
                    int value = 4;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.SubToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "pO2")
                {
                    int value = 8;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.SubToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "Gas1")
                {
                    int value = 16;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.SubToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "Gas2")
                {
                    int value = 32;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.SubToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "Gas3")
                {
                    int value = 64;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.SubToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "Gas4")
                {
                    int value = 128;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.SubToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "Foam")
                {
                    int value = 256;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.SubToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "Turbidity")
                {
                    int value = 512;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.SubToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "Balance")
                {
                    int value = 1024;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.SubToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "AirFlow")
                {
                    int value = 2048;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.SubToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "Gas2Flow")
                {
                    int value = 4096;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.SubToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "ExitTurbidity")
                {
                    int value = 8192;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.SubToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }
                else if (checkBox.Name == "ExitBalance")
                {
                    int value = 16384;
                    //Debug.WriteLine($"CheckBox Checked: {value}");
                    //MessageBox.Show($"CheckBox Checked: {value}");
                    TotalManager.Instance.SubToTotal(value);
                    //CalculateGreenCheckBoxSum();
                }


            }

        }


        private void DisableUncheckedCheckBoxes()
        {
            foreach (var child in CheckBoxContainer.Children)
            {
                if (child is CheckBox checkBox && !checkBox.IsChecked.GetValueOrDefault())
                {
                    checkBox.IsEnabled = false;
                }
            }
        }

        private void EnableAllCheckBoxes()
        {
            foreach (var child in CheckBoxContainer.Children)
            {
                if (child is CheckBox checkBox)
                {
                    checkBox.IsEnabled = true;
                }
            }
        }

        private void UpdateSelectedCountText()
        {
            SelectedCountTextBlock.Text = $"Selected Parameters: {checkedCount}/10";
        }
    }
}
