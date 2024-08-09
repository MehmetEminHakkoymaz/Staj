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
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfApp1.EditPages.EditpHCascade;

namespace WpfApp1.EditPages
{
    /// <summary>
    /// Interaction logic for EditPump2.xaml
    /// </summary>
    public partial class EditPump2 : Window
    {
        public EditPump2()
        {
            InitializeComponent();
            InitializeClock();
        }

        private DispatcherTimer clockTimer;

        private void InitializeClock()
        {
            clockTimer = new DispatcherTimer();
            clockTimer.Interval = TimeSpan.FromSeconds(1);
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
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

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton clickedButton = sender as ToggleButton;

            if (clickedButton == Di05 && Di1 != null && Di25 != null)
            {
                Di1.IsChecked = false;
                Di25.IsChecked = false;
            }
            else if (clickedButton == Di1 && Di05 != null && Di25 != null)
            {
                Di05.IsChecked = false;
                Di25.IsChecked = false;
            }
            else if (clickedButton == Di25 && Di05 != null && Di1 != null)
            {
                Di05.IsChecked = false;
                Di1.IsChecked = false;
            }
            else if (clickedButton == Base&& Feed != null)
            {
                Feed.IsChecked = false;
            }
            else if (clickedButton == Feed && Base != null)
            {
                Base.IsChecked = false;
            }
            else if (clickedButton == Count && ml != null)
            {
                Count.IsChecked = true;
                ml.IsChecked = false;
            }
            else if (clickedButton == ml && Count != null)
            {
                ml.IsChecked = true;
                Count.IsChecked = false;
            }

        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton clickedButton = sender as ToggleButton;

            if (!Di05.IsChecked.Value && !Di1.IsChecked.Value && !Di25.IsChecked.Value)
            {
                clickedButton.IsChecked = true;
            }
            else if (!Base.IsChecked.Value && !Feed.IsChecked.Value)
            {
                clickedButton.IsChecked = true;
            }
            else if (!Count.IsChecked.Value && !ml.IsChecked.Value)
            {
                clickedButton.IsChecked = true;
            }

        }
    }
}
