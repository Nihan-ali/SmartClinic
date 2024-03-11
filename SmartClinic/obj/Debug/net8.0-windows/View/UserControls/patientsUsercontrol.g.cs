﻿#pragma checksum "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C517366F9EBA0A13341F56B12A0A2582A22EE46E"
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
    /// PatientsUserControl
    /// </summary>
    public partial class PatientsUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 26 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddPatient;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchPatient;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView PatientsListBox;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup PatientPopup;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock PopupText;
        
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
            System.Uri resourceLocater = new System.Uri("/SmartClinic;component/view/usercontrols/patientsusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
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
            this.AddPatient = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
            this.AddPatient.Click += new System.Windows.RoutedEventHandler(this.AddPatientButton_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.SearchPatient = ((System.Windows.Controls.TextBox)(target));
            
            #line 35 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
            this.SearchPatient.Loaded += new System.Windows.RoutedEventHandler(this.PatientsUserControl_Loaded);
            
            #line default
            #line hidden
            
            #line 35 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
            this.SearchPatient.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SearchPatient_TextChanged);
            
            #line default
            #line hidden
            
            #line 35 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
            this.SearchPatient.GotFocus += new System.Windows.RoutedEventHandler(this.OnSearchTextBoxGotFocus);
            
            #line default
            #line hidden
            
            #line 35 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
            this.SearchPatient.LostFocus += new System.Windows.RoutedEventHandler(this.OnSearchTextBoxLostFocus);
            
            #line default
            #line hidden
            return;
            case 3:
            this.PatientsListBox = ((System.Windows.Controls.ListView)(target));
            
            #line 43 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
            this.PatientsListBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.PatientsListBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PatientPopup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 6:
            this.PopupText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            
            #line 113 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnPreviousButtonClick);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 129 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnNextButtonClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 4:
            
            #line 97 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Delete_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

