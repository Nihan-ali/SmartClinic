﻿#pragma checksum "..\..\..\..\..\View\UserControls\PatientProfileUserControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8E875D38EED3AA5F1539E86BCE9D4293FB7774D9"
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


namespace SmartClinic.View.UserControls {
    
    
    /// <summary>
    /// PatientProfileUserControl
    /// </summary>
    public partial class PatientProfileUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 27 "..\..\..\..\..\View\UserControls\PatientProfileUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock PatientNameTextBlock;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\..\View\UserControls\PatientProfileUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock IDTextBlock;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\..\View\UserControls\PatientProfileUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock AgeTextBlock;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\..\View\UserControls\PatientProfileUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button writePrescription;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\..\View\UserControls\PatientProfileUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock PastVisitTextBlock;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\..\View\UserControls\PatientProfileUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox PrescriptionList;
        
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
            System.Uri resourceLocater = new System.Uri("/SmartClinic;component/view/usercontrols/patientprofileusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\UserControls\PatientProfileUserControl.xaml"
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
            this.PatientNameTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.IDTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.AgeTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.writePrescription = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\..\..\View\UserControls\PatientProfileUserControl.xaml"
            this.writePrescription.Click += new System.Windows.RoutedEventHandler(this.writePrescription_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PastVisitTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.PrescriptionList = ((System.Windows.Controls.ListBox)(target));
            
            #line 58 "..\..\..\..\..\View\UserControls\PatientProfileUserControl.xaml"
            this.PrescriptionList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.PrescriptionList_SelectionChanged);
            
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
            case 7:
            
            #line 83 "..\..\..\..\..\View\UserControls\PatientProfileUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowPrescription_Click);
            
            #line default
            #line hidden
            break;
            case 8:
            
            #line 86 "..\..\..\..\..\View\UserControls\PatientProfileUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Print_Click);
            
            #line default
            #line hidden
            break;
            case 9:
            
            #line 89 "..\..\..\..\..\View\UserControls\PatientProfileUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Delete_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

