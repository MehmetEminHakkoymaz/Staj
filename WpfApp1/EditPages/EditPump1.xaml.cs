﻿using System;
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

namespace WpfApp1.EditPages
{
    /// <summary>
    /// Interaction logic for EditPump1.xaml
    /// </summary>
    public partial class EditPump1 : Window
    {
        private List<ToggleButton> allToggleButtons;
        private DispatcherTimer clockTimer;

        public EditPump1()
        {
            // Önce component'leri initialize et
            InitializeComponent();

            // Clock'u başlat
            InitializeClock();

            // Window ayarlarını yap
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
            this.Topmost = true;

            // Bu kısmı Loaded event'ine taşıyalım
            this.Loaded += EditPump1_Loaded;
        }

        private void EditPump1_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Toggle butonları sadece window yüklendikten sonra initialize et
                InitializeToggleButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing toggle buttons: {ex.Message}");
            }
        }
        private void InitializeToggleButtons()
        {
            // Liste oluştur
            allToggleButtons = new List<ToggleButton>();

            // Butonları bulmaya çalış ve varsa listeye ekle
            var buttons = new[] { "Button13", "Button14", "Button19", "Button16", "Button25", "Button17", "Button18" };
            foreach (var buttonName in buttons)
            {
                var button = this.FindName(buttonName) as ToggleButton;
                if (button != null)
                {
                    allToggleButtons.Add(button);
                }
            }

            if (allToggleButtons.Count == 0)
            {
                MessageBox.Show("No toggle buttons were found. Please check XAML names.");
            }
        }
        private void HandleButtonToggle(object sender, RoutedEventArgs e)
        {
            try
            {
                var clickedButton = sender as ToggleButton;
                if (clickedButton == null) return;

                // Önce allToggleButtons'ın initialize edildiğinden emin ol
                if (allToggleButtons == null)
                {
                    InitializeToggleButtons();
                }

                // Özellik butonları için kontrol (Acid/Feed)
                if (clickedButton == Acid && Feed != null)
                {
                    Feed.IsChecked = false;
                }
                else if (clickedButton == Feed && Acid != null)
                {
                    Acid.IsChecked = false;
                }
                // Birim butonları için kontrol (Count/ml)
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
                // Yeni eklenen numaralı butonlar için kontrol (#13-#18)
                else if (allToggleButtons?.Contains(clickedButton) == true)
                {
                    foreach (var button in allToggleButtons)
                    {
                        if (button != clickedButton && button != null)
                        {
                            button.IsChecked = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in HandleButtonToggle: {ex.Message}");
            }
        }
        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            var clickedButton = sender as ToggleButton;
            if (clickedButton == null) return;

            // Acid/Feed kontrolü
            if ((clickedButton == Acid || clickedButton == Feed) &&
                !Acid.IsChecked.Value && !Feed.IsChecked.Value)
            {
                clickedButton.IsChecked = true;
            }
            // Count/ml kontrolü
            else if ((clickedButton == Count || clickedButton == ml) &&
                     !Count.IsChecked.Value && !ml.IsChecked.Value)
            {
                clickedButton.IsChecked = true;
            }
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




    }
}
