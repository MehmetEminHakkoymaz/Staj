using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfApp1.EditPages
{
    public partial class EditRedox : Window
    {
        private readonly DispatcherTimer clockTimer;
        private TextBox activeTextBox = null;
        public EditRedox()
        {
            InitializeComponent();

        }
        //    clockTimer = InitializeClock();
        //    contentComboBox.SelectionChanged += ContentComboBox_SelectionChanged;
        //    KeypadControl.ValueSelected += KeyPadControl_ValueSelected;
        //    LoadLastSelectedCascade();
        //    LoadPIDSettings();
        //    WindowState = WindowState.Maximized;
        //    WindowStyle = WindowStyle.None;
        //    ResizeMode = ResizeMode.NoResize;
        //    Topmost = true;
        //}
        //private void LoadPIDSettings()
        //{
        //    try
        //    {
        //        var culture = System.Globalization.CultureInfo.CurrentCulture;
        //        RedoxP.Text = Properties.Settings.Default.RedoxP.ToString(culture);
        //        RedoxI.Text = Properties.Settings.Default.RedoxI.ToString(culture);
        //        RedoxILimit.Text = Properties.Settings.Default.RedoxILimit.ToString(culture);
        //        RedoxDeadband.Text = Properties.Settings.Default.RedoxDeadband.ToString(culture);
        //        RedoxNegfactor.Text = Properties.Settings.Default.RedoxNegfactor.ToString(culture);
        //        RedoxEvalTime.Text = Properties.Settings.Default.RedoxEvalTime.ToString(culture);
        //        RegisterPIDTextBoxEvents();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error loading PID settings: {ex.Message}");
        //    }
        //}
        //private void RegisterPIDTextBoxEvents()
        //{
        //    RedoxP.PreviewTextInput += TextBox_PreviewTextInput;
        //    RedoxI.PreviewTextInput += TextBox_PreviewTextInput;
        //    RedoxILimit.PreviewTextInput += TextBox_PreviewTextInput;
        //    RedoxDeadband.PreviewTextInput += TextBox_PreviewTextInput;
        //    RedoxNegfactor.PreviewTextInput += TextBox_PreviewTextInput;
        //    RedoxEvalTime.PreviewTextInput += TextBox_PreviewTextInput;
        //    RedoxP.TextChanged += TextBox_TextChanged;
        //    RedoxI.TextChanged += TextBox_TextChanged;
        //    RedoxILimit.TextChanged += TextBox_TextChanged;
        //    RedoxDeadband.TextChanged += TextBox_TextChanged;
        //    RedoxNegfactor.TextChanged += TextBox_TextChanged;
        //    RedoxEvalTime.TextChanged += TextBox_TextChanged;
        //}
        //private DispatcherTimer InitializeClock()
        //{
        //    var timer = new DispatcherTimer
        //    {
        //        Interval = TimeSpan.FromSeconds(1)
        //    };
        //    timer.Tick += ClockTimer_Tick;
        //    timer.Start();
        //    return timer;
        //}
        //private void LoadLastSelectedCascade()
        //{
        //    try
        //    {
        //        string lastSelected = Properties.Settings.Default.LastSelectedRedoxCascadeItem;
        //        if (!string.IsNullOrEmpty(lastSelected))
        //        {
        //            foreach (ComboBoxItem item in contentComboBox.Items)
        //            {
        //                if (item.Content.ToString() == lastSelected)
        //                {
        //                    contentComboBox.SelectedItem = item;
        //                    return; 
        //                }
        //            }
        //        }
        //        contentComboBox.SelectedIndex = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error loading last selected cascade: {ex.Message}");
        //        contentComboBox.SelectedIndex = 0;
        //    }
        //}
        ////EVENT HANDLERS
        //private void ContentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (contentComboBox.SelectedItem is ComboBoxItem selectedItem)
        //    {
        //        string content = selectedItem.Content.ToString();
        //        Properties.Settings.Default.RedoxSelectedCascade = content;
        //        switch (content)
        //        {
        //            case "Stirrer":
        //                Properties.Settings.Default.HideStirrerBorder = true;
        //                Properties.Settings.Default.HideGas1Border = false;
        //                break;
        //            case "TotalFlow":
        //                Properties.Settings.Default.HideStirrerBorder = false;
        //                Properties.Settings.Default.HideGas1Border = true;
        //                break;
        //            case "Stirrer->TotalFlow":
        //                Properties.Settings.Default.HideStirrerBorder = true;
        //                Properties.Settings.Default.HideGas1Border = true;
        //                break;
        //            case "Stirrer->GasMix":
        //                Properties.Settings.Default.HideStirrerBorder = true;
        //                Properties.Settings.Default.HideGas1Border = false;
        //                break;
        //            default:
        //                Properties.Settings.Default.HideStirrerBorder = false;
        //                Properties.Settings.Default.HideGas1Border = false;
        //                break;
        //        }

        //        // Settings'i kaydet
        //        Properties.Settings.Default.Save();

        //        // Önce mevcut içeriği temizle
        //        contentArea.Content = null;
        //        contentArea.ContentTemplate = null;

        //        // Seçilen template'i yükle
        //        ApplyTemplate(content);
        //    }
        //}

































    }
}
