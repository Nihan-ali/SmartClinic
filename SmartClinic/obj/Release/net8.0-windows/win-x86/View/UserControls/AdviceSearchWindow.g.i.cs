﻿#pragma checksum "..\..\..\..\..\..\View\UserControls\AdviceSearchWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1924A8FD43FDB894E610B4B227DAF649E43BD3C1"
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
    /// AdviceSearchWindow
    /// </summary>
    public partial class AdviceSearchWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 22 "..\..\..\..\..\..\View\UserControls\AdviceSearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox searchTextBox;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\..\..\View\UserControls\AdviceSearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl adviceItemsControl;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\..\..\View\UserControls\AdviceSearchWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addToAdvices;
        
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
            System.Uri resourceLocater = new System.Uri("/SmartClinic;component/view/usercontrols/advicesearchwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\View\UserControls\AdviceSearchWindow.xaml"
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
            
            #line 22 "..\..\..\..\..\..\View\UserControls\AdviceSearchWindow.xaml"
            this.searchTextBox.Loaded += new System.Windows.RoutedEventHandler(this.AdviceSearchWindow_Loaded);
            
            #line default
            #line hidden
            
            #line 22 "..\..\..\..\..\..\View\UserControls\AdviceSearchWindow.xaml"
            this.searchTextBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.EnterPressed);
            
            #line default
            #line hidden
            
            #line 22 "..\..\..\..\..\..\View\UserControls\AdviceSearchWindow.xaml"
            this.searchTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SearchTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.adviceItemsControl = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 4:
            this.addToAdvices = ((System.Windows.Controls.Button)(target));
            
            #line 60 "..\..\..\..\..\..\View\UserControls\AdviceSearchWindow.xaml"
            this.addToAdvices.Click += new System.Windows.RoutedEventHandler(this.addToAdvice_Click);
            
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
            case 3:
            
            #line 53 "..\..\..\..\..\..\View\UserControls\AdviceSearchWindow.xaml"
            ((System.Windows.Controls.Primitives.ToggleButton)(target)).Click += new System.Windows.RoutedEventHandler(this.ToggleButton_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}
