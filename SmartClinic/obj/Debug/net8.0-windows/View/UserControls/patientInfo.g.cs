﻿#pragma checksum "..\..\..\..\..\View\UserControls\patientInfo.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BD0F79BC5A4303B77A4D7CF27BE96B71C2118048"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace SmartClinic.View.UserControls {
    
    
    /// <summary>
    /// patientInfo
    /// </summary>
    public partial class patientInfo : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\..\..\View\UserControls\patientInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel addPatientPanel;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\..\View\UserControls\patientInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddPatientButton;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\View\UserControls\patientInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel patientDetailsPanel;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\..\View\UserControls\patientInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock age;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\..\View\UserControls\patientInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button search;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\..\View\UserControls\patientInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox searchPatientTextBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SmartClinic;component/view/usercontrols/patientinfo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\UserControls\patientInfo.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.addPatientPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            this.AddPatientButton = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\..\..\View\UserControls\patientInfo.xaml"
            this.AddPatientButton.Click += new System.Windows.RoutedEventHandler(this.AddPatientButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.patientDetailsPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.age = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.search = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.searchPatientTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 40 "..\..\..\..\..\View\UserControls\patientInfo.xaml"
            this.searchPatientTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.OnSearchTextBoxGotFocus);
            
            #line default
            #line hidden
            
            #line 40 "..\..\..\..\..\View\UserControls\patientInfo.xaml"
            this.searchPatientTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.OnSearchTextBoxLostFocus);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
