﻿#pragma checksum "..\..\..\HomeWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4DB0F04B5E54E6AF280610E2DD6671C982385B3C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SmartClinic;
using SmartClinic.View.UserControls;
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


namespace SmartClinic {
    
    
    /// <summary>
    /// HomeWindow
    /// </summary>
    public partial class HomeWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\HomeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button rx;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\HomeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button patients;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\HomeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button settings;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\HomeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button stat;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\HomeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition searching;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\HomeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl contentControl;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SmartClinic;V1.0.0.0;component/homewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\HomeWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.rx = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\HomeWindow.xaml"
            this.rx.Click += new System.Windows.RoutedEventHandler(this.RxButton_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.patients = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\HomeWindow.xaml"
            this.patients.Click += new System.Windows.RoutedEventHandler(this.PatientButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.settings = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\HomeWindow.xaml"
            this.settings.Click += new System.Windows.RoutedEventHandler(this.SettingsButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.stat = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\..\HomeWindow.xaml"
            this.stat.Click += new System.Windows.RoutedEventHandler(this.StatButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.searching = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 6:
            this.contentControl = ((System.Windows.Controls.ContentControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

