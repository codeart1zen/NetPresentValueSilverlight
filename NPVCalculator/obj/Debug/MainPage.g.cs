﻿#pragma checksum "C:\src\VisualRisk\NPVCalculator\Solution\NPVCalculator\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "40AE032C5304D351EFF49C011D393AAA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace NPVCalculator {
    
    
    public partial class MainPage : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ErrorDialog;
        
        internal System.Windows.Controls.TabItem tabItem2;
        
        internal System.Windows.Controls.Button AddCashflowButton;
        
        internal System.Windows.Controls.TabItem tabItem1;
        
        internal System.Windows.Controls.DataVisualization.Charting.LinearAxis DRAxis;
        
        internal System.Windows.Controls.DataVisualization.Charting.LinearAxis NPVAxis;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/NPVCalculator;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ErrorDialog = ((System.Windows.Controls.Grid)(this.FindName("ErrorDialog")));
            this.tabItem2 = ((System.Windows.Controls.TabItem)(this.FindName("tabItem2")));
            this.AddCashflowButton = ((System.Windows.Controls.Button)(this.FindName("AddCashflowButton")));
            this.tabItem1 = ((System.Windows.Controls.TabItem)(this.FindName("tabItem1")));
            this.DRAxis = ((System.Windows.Controls.DataVisualization.Charting.LinearAxis)(this.FindName("DRAxis")));
            this.NPVAxis = ((System.Windows.Controls.DataVisualization.Charting.LinearAxis)(this.FindName("NPVAxis")));
        }
    }
}

