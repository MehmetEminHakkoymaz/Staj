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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1.Settings.SettingWindows
{
    /// <summary>
    /// Interaction logic for Usb.xaml
    /// </summary>
    public partial class Usb : UserControl
    {
        public Usb()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Export_Data_Button_Click(object sender, RoutedEventArgs e)
        {
            var exportDataToUsb = new WpfApp1.Settings.SettingWindows.UsbWindows.ExportDataToUsb();
            exportDataToUsb.Show();
        }

        private void Load_Config_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Update_Firmware_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Update_User_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Add_Language_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
