﻿#pragma checksum "..\..\..\..\..\..\Settings\SettingWindows\UsbWindows\ExportDataToUsb.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9F1A00302EFD020D73E8A68F485B519525E19495"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfApp1.Settings.SettingWindows.UsbWindows;


namespace WpfApp1.Settings.SettingWindows.UsbWindows {
    
    
    /// <summary>
    /// ExportDataToUsb
    /// </summary>
    public partial class ExportDataToUsb : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\..\..\..\Settings\SettingWindows\UsbWindows\ExportDataToUsb.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox DatabaseTablesListBox;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\..\..\Settings\SettingWindows\UsbWindows\ExportDataToUsb.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button TransferButton;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\..\Settings\SettingWindows\UsbWindows\ExportDataToUsb.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteTableButton;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\..\..\Settings\SettingWindows\UsbWindows\ExportDataToUsb.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox UsbDrivesComboBox;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\..\..\Settings\SettingWindows\UsbWindows\ExportDataToUsb.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RefreshButton;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\..\..\Settings\SettingWindows\UsbWindows\ExportDataToUsb.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox UsbContentsListBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp1;component/settings/settingwindows/usbwindows/exportdatatousb.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\Settings\SettingWindows\UsbWindows\ExportDataToUsb.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.DatabaseTablesListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 2:
            this.TransferButton = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\..\..\..\Settings\SettingWindows\UsbWindows\ExportDataToUsb.xaml"
            this.TransferButton.Click += new System.Windows.RoutedEventHandler(this.TransferButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.DeleteTableButton = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\..\..\..\Settings\SettingWindows\UsbWindows\ExportDataToUsb.xaml"
            this.DeleteTableButton.Click += new System.Windows.RoutedEventHandler(this.DeleteTableButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.UsbDrivesComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 50 "..\..\..\..\..\..\Settings\SettingWindows\UsbWindows\ExportDataToUsb.xaml"
            this.UsbDrivesComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.UsbDrivesComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.RefreshButton = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\..\..\..\..\Settings\SettingWindows\UsbWindows\ExportDataToUsb.xaml"
            this.RefreshButton.Click += new System.Windows.RoutedEventHandler(this.RefreshButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.UsbContentsListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 7:
            
            #line 66 "..\..\..\..\..\..\Settings\SettingWindows\UsbWindows\ExportDataToUsb.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Ok_Button_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 81 "..\..\..\..\..\..\Settings\SettingWindows\UsbWindows\ExportDataToUsb.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Cancel_Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

