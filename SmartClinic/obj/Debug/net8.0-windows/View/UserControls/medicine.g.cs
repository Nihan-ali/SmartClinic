﻿#pragma checksum "..\..\..\..\..\View\UserControls\medicine.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E219F1BB82F27EFA3538B493153C6E94EE95D9FE"
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
    public partial class medicine : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 22 "..\..\..\..\..\View\UserControls\medicine.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Rx;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\..\View\UserControls\medicine.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView selectedMedicinesListView;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\..\View\UserControls\medicine.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Advices;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\..\View\UserControls\medicine.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView selectedAdvicesListView;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\..\..\View\UserControls\medicine.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddFollowUpButton;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\..\..\..\View\UserControls\medicine.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView selectedFollowUpListView;
        
        #line default
        #line hidden
        
        
        #line 153 "..\..\..\..\..\View\UserControls\medicine.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddSpecialNoteButton;
        
        #line default
        #line hidden
        
        
        #line 155 "..\..\..\..\..\View\UserControls\medicine.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView selectedSpecialNoteListView;
        
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
            System.Uri resourceLocater = new System.Uri("/SmartClinic;component/view/usercontrols/medicine.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\UserControls\medicine.xaml"
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
            this.Rx = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\..\..\View\UserControls\medicine.xaml"
            this.Rx.Click += new System.Windows.RoutedEventHandler(this.Rx_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.selectedMedicinesListView = ((System.Windows.Controls.ListView)(target));
            return;
            case 5:
            this.Advices = ((System.Windows.Controls.Button)(target));
            
            #line 71 "..\..\..\..\..\View\UserControls\medicine.xaml"
            this.Advices.Click += new System.Windows.RoutedEventHandler(this.Advices_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.selectedAdvicesListView = ((System.Windows.Controls.ListView)(target));
            return;
            case 9:
            this.AddFollowUpButton = ((System.Windows.Controls.Button)(target));
            
            #line 112 "..\..\..\..\..\View\UserControls\medicine.xaml"
            this.AddFollowUpButton.Click += new System.Windows.RoutedEventHandler(this.AddFollowUp_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.selectedFollowUpListView = ((System.Windows.Controls.ListView)(target));
            return;
            case 12:
            this.AddSpecialNoteButton = ((System.Windows.Controls.Button)(target));
            
            #line 153 "..\..\..\..\..\View\UserControls\medicine.xaml"
            this.AddSpecialNoteButton.Click += new System.Windows.RoutedEventHandler(this.AddSpecialNote_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.selectedSpecialNoteListView = ((System.Windows.Controls.ListView)(target));
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
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 3:
            
            #line 46 "..\..\..\..\..\View\UserControls\medicine.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveButton_Click);
            
            #line default
            #line hidden
            break;
            case 4:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.FrameworkElement.LoadedEvent;
            
            #line 62 "..\..\..\..\..\View\UserControls\medicine.xaml"
            eventSetter.Handler = new System.Windows.RoutedEventHandler(this.ListViewItem_Loaded);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            case 7:
            
            #line 87 "..\..\..\..\..\View\UserControls\medicine.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveButton_Click);
            
            #line default
            #line hidden
            break;
            case 8:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.FrameworkElement.LoadedEvent;
            
            #line 101 "..\..\..\..\..\View\UserControls\medicine.xaml"
            eventSetter.Handler = new System.Windows.RoutedEventHandler(this.ListViewItem_Loaded);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            case 11:
            
            #line 132 "..\..\..\..\..\View\UserControls\medicine.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveButton_Click);
            
            #line default
            #line hidden
            break;
            case 14:
            
            #line 173 "..\..\..\..\..\View\UserControls\medicine.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveButton_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}
