﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmartClinic.View.UserControls
{
    /// <summary>
    /// Interaction logic for EditComplaintControl.xaml
    /// </summary>
    public partial class EditComplaintControl : Window
    {
        public EditComplaintControl()
        {
            InitializeComponent();
        }

        private void CloseWindow(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Close();
            }
        }
    }
}
