﻿#pragma checksum "..\..\..\..\..\View\UserControls\medicine.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E5FD6C5D431A256FA576C828FA1C844D8C9F0DFF"
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
    /// medicine
    /// </summary>
    public partial class medicine : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\..\..\View\UserControls\medicine.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Rx;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\..\View\UserControls\medicine.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Advices;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\..\View\UserControls\medicine.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button FollowUp;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\..\View\UserControls\medicine.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SpecialNotes;
        
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
            System.Uri resourceLocater = new System.Uri("/SmartClinic;component/view/usercontrols/medicine.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\UserControls\medicine.xaml"
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
            this.Rx = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\..\..\View\UserControls\medicine.xaml"
            this.Rx.Click += new System.Windows.RoutedEventHandler(this.Rx_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Advices = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\..\..\View\UserControls\medicine.xaml"
            this.Advices.Click += new System.Windows.RoutedEventHandler(this.Advices_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.FollowUp = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\..\..\View\UserControls\medicine.xaml"
            this.FollowUp.Click += new System.Windows.RoutedEventHandler(this.FollowUp_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SpecialNotes = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\..\..\View\UserControls\medicine.xaml"
            this.SpecialNotes.Click += new System.Windows.RoutedEventHandler(this.SpecialNotes_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

