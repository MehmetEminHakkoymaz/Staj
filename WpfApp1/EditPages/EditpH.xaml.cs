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
using System.Windows.Threading;

namespace WpfApp1.EditPages
{
    /// <summary>
    /// Interaction logic for EditpH.xaml
    /// </summary>
    public partial class EditpH : Window
    {
        private DispatcherTimer clockTimer;
        public EditpH()
        {
            InitializeComponent();
            InitializeClock();
        }

        private void InitializeClock()
        {
            clockTimer = new DispatcherTimer();
            clockTimer.Interval = TimeSpan.FromSeconds(1);
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            // Sistem saatini "HH:mm:ss" formatında güncelle
            ClockTextBlock.Text = DateTime.Now.ToString("HH : mm : ss");
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private void contentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
        //    {
        //        var contentName = selectedItem.Content.ToString();
        //        UserControl contentTo
        //            = null;

        //        switch (contentName)
        //        {
        //            case "None":
        //                contentToLoad = new EditpHCascade.None();
        //                break;
        //            case "Base":
        //                contentToLoad = new EditpHCascade.Base();
        //                break;
        //            case "Acid":
        //                contentToLoad = new EditpHCascade.Acid();
        //                break;
        //            case "Base->Acid":
        //                contentToLoad = new EditpHCascade.BaseAcid();
        //                break;
        //        }

        //        contentArea.Content = contentToLoad;
        //    }
        //}
    }
}
