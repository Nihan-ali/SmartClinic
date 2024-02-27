﻿#pragma checksum "..\..\..\..\..\View\UserControls\PatientList.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "CDC9E92FD0E014E9911F9E987B30C1644805D504"
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
    /// PatientList
    /// </summary>
    public partial class PatientList : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 51 "..\..\..\..\..\View\UserControls\PatientList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox searchPatientTextBox;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\..\View\UserControls\PatientList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView PatientListView;
        
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
            System.Uri resourceLocater = new System.Uri("/SmartClinic;component/view/usercontrols/patientlist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\UserControls\PatientList.xaml"
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
            
            #line 11 "..\..\..\..\..\View\UserControls\PatientList.xaml"
            ((SmartClinic.PatientList)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.PatientList_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 42 "..\..\..\..\..\View\UserControls\PatientList.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnclickAddPatient);
            
            #line default
            #line hidden
            return;
            case 3:
            this.searchPatientTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 51 "..\..\..\..\..\View\UserControls\PatientList.xaml"
            this.searchPatientTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.OnSearchTextBoxGotFocus);
            
            #line default
            #line hidden
            
            #line 51 "..\..\..\..\..\View\UserControls\PatientList.xaml"
            this.searchPatientTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.OnSearchTextBoxLostFocus);
            
            #line default
            #line hidden
            
            #line 51 "..\..\..\..\..\View\UserControls\PatientList.xaml"
            this.searchPatientTextBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.SearchBox_KeyDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.PatientListView = ((System.Windows.Controls.ListView)(target));
            
            #line 55 "..\..\..\..\..\View\UserControls\PatientList.xaml"
            this.PatientListView.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.PatientListView_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 5:
            
            #line 67 "..\..\..\..\..\View\UserControls\PatientList.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnDeleteButtonClicked);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

