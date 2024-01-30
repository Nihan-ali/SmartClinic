﻿#pragma checksum "..\..\..\..\..\View\UserControls\MedicineSearchWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "41ABE86E86EE578A114F17430CA50F69DE7F79F0"
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


namespace SmartClinic {
    
    
    /// <summary>
    /// MedicineSearchWindow
    /// </summary>
    public partial class MedicineSearchWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 18 "..\..\..\..\..\View\UserControls\MedicineSearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox searchTextBox;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\..\View\UserControls\MedicineSearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup searchResultsPopup;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\..\View\UserControls\MedicineSearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView searchResultsListBox;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\..\View\UserControls\MedicineSearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox searchComboBox;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\..\..\..\View\UserControls\MedicineSearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl medicineItemsControl;
        
        #line default
        #line hidden
        
        
        #line 145 "..\..\..\..\..\View\UserControls\MedicineSearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView selectedMedicinesListView;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\..\..\..\View\UserControls\MedicineSearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addToRx;
        
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
            System.Uri resourceLocater = new System.Uri("/SmartClinic;component/view/usercontrols/medicinesearchwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\UserControls\MedicineSearchWindow.xaml"
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
            this.searchTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 18 "..\..\..\..\..\View\UserControls\MedicineSearchWindow.xaml"
            this.searchTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SearchTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.searchResultsPopup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 3:
            this.searchResultsListBox = ((System.Windows.Controls.ListView)(target));
            
            #line 39 "..\..\..\..\..\View\UserControls\MedicineSearchWindow.xaml"
            this.searchResultsListBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SearchResultsListBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.searchComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            
            #line 118 "..\..\..\..\..\View\UserControls\MedicineSearchWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.MedicineButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 119 "..\..\..\..\..\View\UserControls\MedicineSearchWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.MedicineGroupButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.medicineItemsControl = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 9:
            this.selectedMedicinesListView = ((System.Windows.Controls.ListView)(target));
            return;
            case 10:
            this.addToRx = ((System.Windows.Controls.Button)(target));
            
            #line 159 "..\..\..\..\..\View\UserControls\MedicineSearchWindow.xaml"
            this.addToRx.Click += new System.Windows.RoutedEventHandler(this.addToRx_Click);
            
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
            case 8:
            
            #line 135 "..\..\..\..\..\View\UserControls\MedicineSearchWindow.xaml"
            ((System.Windows.Controls.Primitives.ToggleButton)(target)).Click += new System.Windows.RoutedEventHandler(this.ToggleButton_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

