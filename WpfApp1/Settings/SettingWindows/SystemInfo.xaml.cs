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
//    public partial class SystemInfo : UserControl
//    {
//        private DispatcherTimer updateTimer;

//        public SystemInfo()
//        {
//            InitializeComponent();
//            LoadTotalWorkTime();

//            updateTimer = new DispatcherTimer();
//            updateTimer.Interval = TimeSpan.FromSeconds(1);
//            updateTimer.Tick += UpdateTimer_Tick;
//            updateTimer.Start();
//        }

//        private void LoadTotalWorkTime()
//        {
//            TimeSpan totalTime = Properties.Settings.Default.TotalWorkTime;
//            UpdateTotalWorkHoursDisplay(totalTime);
//            UpdateServiceTimeDisplay(totalTime);
//        }

//        private void UpdateTimer_Tick(object sender, EventArgs e)
//        {
//            TimeSpan totalTime = Properties.Settings.Default.TotalWorkTime;
//            UpdateTotalWorkHoursDisplay(totalTime);
//            UpdateServiceTimeDisplay(totalTime);
//        }

//        private void UpdateTotalWorkHoursDisplay(TimeSpan totalTime)
//        {
//            TotalWorkHours.Text = $"{(int)totalTime.TotalHours}h {totalTime.Minutes}min {totalTime.Seconds}s";
//        }

//        private void UpdateServiceTimeDisplay(TimeSpan totalTime)
//        {
//            double totalHours = totalTime.TotalHours;
//            int nextMaintenance = GetNextMaintenanceHour(totalHours);

//            // Kalan süreyi hesapla
//            double remainingHours = nextMaintenance - totalHours;

//            // TimeSpan'e çevir
//            TimeSpan remainingTime = TimeSpan.FromHours(remainingHours);

//            // Saat, dakika ve saniye olarak formatla
//            int hours = (int)remainingTime.TotalHours;
//            int minutes = remainingTime.Minutes;
//            int seconds = remainingTime.Seconds;

//            ServiceTm.Text = $"{hours}h {minutes}min {seconds}s";
//        }

//        private int GetNextMaintenanceHour(double currentHours)
//        {
//            if (currentHours < 1000)
//            {
//                return 1000; // İlk bakım 1000 saatte
//            }
//            else if (currentHours < 3000)
//            {
//                return 3000; // İkinci bakım 3000 saatte
//            }
//            else
//            {
//                // 3000 saatten sonra her 4000 saatte bir bakım
//                int maintenanceCount = (int)((currentHours - 3000) / 4000);
//                int nextMaintenanceCount = maintenanceCount + 1;
//                return 3000 + (nextMaintenanceCount * 4000);
//            }
//        }

//        public void UpdateTotalWorkTime()
//        {
//            var totalTime = Properties.Settings.Default.TotalWorkTime;
//            TotalWorkHours.Text = $"{totalTime.Days * 24 + totalTime.Hours}h {totalTime.Minutes}min {totalTime.Seconds}s";
//        }


//    }
//}

namespace WpfApp1.Settings.SettingWindows
{
    public partial class SystemInfo : UserControl
    {
        private DispatcherTimer updateTimer;
        private const int FIRST_MAINTENANCE_HOUR = 1000;
        private const int SECOND_MAINTENANCE_HOUR = 3000;
        private const int REGULAR_MAINTENANCE_INTERVAL = 4000;

        public SystemInfo()
        {
            InitializeComponent();
            //InitializeTimer();
            LoadTotalWorkTime();
        }

        //private void InitializeTimer()
        //{
        //    updateTimer = new DispatcherTimer
        //    {
        //        Interval = TimeSpan.FromSeconds(1)
        //    };
        //    updateTimer.Tick += UpdateTimer_Tick;
        //    updateTimer.Start();
        //}

        private void LoadTotalWorkTime()
        {
            try
            {
                TimeSpan totalTime = Properties.Settings.Default.TotalWorkTime;
                UpdateDisplays(totalTime);
            }
            catch (Exception ex)
            {
                HandleError("Error loading total work time", ex);
            }
        }

        //private void UpdateTimer_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        TimeSpan totalTime = Properties.Settings.Default.TotalWorkTime;
        //        UpdateDisplays(totalTime);
        //    }
        //    catch (Exception ex)
        //    {
        //        HandleError("Error updating displays", ex);
        //    }
        //}

        public void UpdateDisplays(TimeSpan totalTime)
        {
            try
            {
                UpdateTotalWorkHoursDisplay(totalTime);
                UpdateServiceTimeDisplay(totalTime);
            }
            catch (Exception ex)
            {
                HandleError("Error updating displays", ex);
            }
        }
        private void UpdateTotalWorkHoursDisplay(TimeSpan totalTime)
        {
            int days = totalTime.Days;
            int hours = totalTime.Hours;
            int minutes = totalTime.Minutes;
            int seconds = totalTime.Seconds;

            TotalWorkHours.Text = FormatTimeDisplay(days, hours, minutes, seconds);
        }

        private void UpdateServiceTimeDisplay(TimeSpan totalTime)
        {
            double currentHours = totalTime.TotalHours;
            int nextMaintenance = CalculateNextMaintenanceHour(currentHours);

            double remainingHours = nextMaintenance - currentHours;
            TimeSpan remainingTime = TimeSpan.FromHours(remainingHours);

            int days = remainingTime.Days;
            int hours = remainingTime.Hours;
            int minutes = remainingTime.Minutes;
            int seconds = remainingTime.Seconds;

            ServiceTm.Text = FormatTimeDisplay(days, hours, minutes, seconds);
        }

        private int CalculateNextMaintenanceHour(double currentHours)
        {
            if (currentHours < FIRST_MAINTENANCE_HOUR)
                return FIRST_MAINTENANCE_HOUR;

            if (currentHours < SECOND_MAINTENANCE_HOUR)
                return SECOND_MAINTENANCE_HOUR;

            int maintenanceCount = (int)((currentHours - SECOND_MAINTENANCE_HOUR) / REGULAR_MAINTENANCE_INTERVAL);
            int nextMaintenanceCount = maintenanceCount + 1;
            return SECOND_MAINTENANCE_HOUR + (nextMaintenanceCount * REGULAR_MAINTENANCE_INTERVAL);
        }

        private string FormatTimeDisplay(int days, int hours, int minutes, int seconds)
        {
            return $"{days}d {hours}h {minutes}m {seconds}s";
        }

        private void HandleError(string message, Exception ex)
        {
            System.Windows.MessageBox.Show(
                $"{message}: {ex.Message}",
                "Error",
                System.Windows.MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Error
            );
        }

        public void RefreshDisplays()
        {
            LoadTotalWorkTime();
        }

        //public void Cleanup()
        //{
        //    updateTimer?.Stop();
        //}
    }
}
