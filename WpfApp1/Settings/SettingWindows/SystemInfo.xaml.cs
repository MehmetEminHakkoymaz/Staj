using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//namespace WpfApp1.Settings.SettingWindows
//{
//    /// <summary>
//    /// Interaction logic for SystemInfo.xaml
//    /// </summary>
//    public partial class SystemInfo : UserControl
//    {
//        private DispatcherTimer updateTimer;

//        public SystemInfo()
//        {
//            InitializeComponent();
//            LoadTotalWorkTime();

//            // Timer'ı başlat (her saniye güncelle)
//            updateTimer = new DispatcherTimer();
//            updateTimer.Interval = TimeSpan.FromSeconds(1);
//            updateTimer.Tick += UpdateTimer_Tick;
//            updateTimer.Start();
//        }

//        private void LoadTotalWorkTime()
//        {
//            UpdateTotalWorkHoursDisplay(Properties.Settings.Default.TotalWorkTime);
//        }

//        private void UpdateTimer_Tick(object sender, EventArgs e)
//        {
//            UpdateTotalWorkHoursDisplay(Properties.Settings.Default.TotalWorkTime);
//        }

//        private void UpdateTotalWorkHoursDisplay(TimeSpan totalTime)
//        {
//            TotalWorkHours.Text = $"{(int)totalTime.TotalHours}h {totalTime.Minutes}min {totalTime.Seconds}s";
//        }
//    }
//}

namespace WpfApp1.Settings.SettingWindows
{
    public partial class SystemInfo : UserControl
    {
        private DispatcherTimer updateTimer;

        public SystemInfo()
        {
            InitializeComponent();
            LoadTotalWorkTime();

            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromSeconds(1);
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
        }

        private void LoadTotalWorkTime()
        {
            TimeSpan totalTime = Properties.Settings.Default.TotalWorkTime;
            UpdateTotalWorkHoursDisplay(totalTime);
            UpdateServiceTimeDisplay(totalTime);
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan totalTime = Properties.Settings.Default.TotalWorkTime;
            UpdateTotalWorkHoursDisplay(totalTime);
            UpdateServiceTimeDisplay(totalTime);
        }

        private void UpdateTotalWorkHoursDisplay(TimeSpan totalTime)
        {
            TotalWorkHours.Text = $"{(int)totalTime.TotalHours}h {totalTime.Minutes}min {totalTime.Seconds}s";
        }

        private void UpdateServiceTimeDisplay(TimeSpan totalTime)
        {
            double totalHours = totalTime.TotalHours;
            int nextMaintenance = GetNextMaintenanceHour(totalHours);

            // Kalan süreyi hesapla
            double remainingHours = nextMaintenance - totalHours;

            // TimeSpan'e çevir
            TimeSpan remainingTime = TimeSpan.FromHours(remainingHours);

            // Saat, dakika ve saniye olarak formatla
            int hours = (int)remainingTime.TotalHours;
            int minutes = remainingTime.Minutes;
            int seconds = remainingTime.Seconds;

            ServiceTm.Text = $"{hours}h {minutes}min {seconds}s";
        }

        private int GetNextMaintenanceHour(double currentHours)
        {
            if (currentHours < 1000)
            {
                return 1000; // İlk bakım 1000 saatte
            }
            else if (currentHours < 3000)
            {
                return 3000; // İkinci bakım 3000 saatte
            }
            else
            {
                // 3000 saatten sonra her 4000 saatte bir bakım
                int maintenanceCount = (int)((currentHours - 3000) / 4000);
                int nextMaintenanceCount = maintenanceCount + 1;
                return 3000 + (nextMaintenanceCount * 4000);
            }
        }
    }
}
