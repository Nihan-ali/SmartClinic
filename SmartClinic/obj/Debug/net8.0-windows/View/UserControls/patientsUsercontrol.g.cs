﻿#pragma checksum "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1A214D9CB04081F55610186E7AAE93EE9615E858"
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
    public partial class PatientsUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddPatient;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox searchPatient;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox PatientsListBox;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup PatientPopup;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock PopupText;
        
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
            System.Uri resourceLocater = new System.Uri("/SmartClinic;component/view/usercontrols/patientsusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
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
            
            #line 11 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.OnGridMouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.AddPatient = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
            this.AddPatient.Click += new System.Windows.RoutedEventHandler(this.AddPatientButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.searchPatient = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.PatientsListBox = ((System.Windows.Controls.ListBox)(target));
            
            #line 33 "..\..\..\..\..\View\UserControls\patientsUsercontrol.xaml"
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
            }
            this._contentLoaded = true;
        }
    }
}

