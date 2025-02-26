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

namespace WpfApp1.Settings.SettingWindows
{
    /// <summary>
    /// Interaction logic for VesselType.xaml
    /// </summary>
    public partial class VesselType : UserControl
    {
        public VesselType()
        {
            InitializeComponent();
            LoadSavedSelection();
        }

        private void LoadSavedSelection()
        {
            // Kaydedilen seçimi yükle
            string savedVessel = Properties.Settings.Default.SelectedVesselType;

            // Tüm butonları önce false yap
            ml500.IsChecked = false;
            L1.IsChecked = false;
            L2.IsChecked = false;
            L3.IsChecked = false;

            // Kaydedilen seçimi uygula
            switch (savedVessel)
            {
                case "500ml":
                    ml500.IsChecked = true;
                    break;
                case "1L":
                    L1.IsChecked = true;
                    break;
                case "2L":
                    L2.IsChecked = true;
                    break;
                case "3L":
                    L3.IsChecked = true;
                    break;
                default:
                    // Varsayılan olarak 500ml seçili olsun
                    L3.IsChecked = true;
                    break;
            }
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            var checkedButton = sender as ToggleButton;
            if (checkedButton == null) return;

            // Diğer tüm butonları uncheck yap
            foreach (var child in ((Grid)this.Content).Children)
            {
                if (child is StackPanel panel)
                {
                    foreach (var button in panel.Children)
                    {
                        if (button is ToggleButton toggleButton && toggleButton != checkedButton)
                        {
                            toggleButton.IsChecked = false;
                        }
                    }
                }
            }

            // Seçilen değeri kaydet
            string selectedValue = "";
            if (checkedButton == ml500) selectedValue = "500ml";
            else if (checkedButton == L1) selectedValue = "1L";
            else if (checkedButton == L2) selectedValue = "2L";
            else if (checkedButton == L3) selectedValue = "3L";

            Properties.Settings.Default.SelectedVesselType = selectedValue;
            Properties.Settings.Default.Save();
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            // En az bir buton seçili olmalı
            if (!ml500.IsChecked.Value && !L1.IsChecked.Value &&
                !L2.IsChecked.Value && !L3.IsChecked.Value)
            {
                ((ToggleButton)sender).IsChecked = true;
            }
        }
    }
}
