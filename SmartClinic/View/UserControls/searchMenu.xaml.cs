using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartClinic.View.UserControls
{
    /// <summary>
    /// Interaction logic for searchMenu.xaml
    /// </summary>
    public partial class searchMenu : UserControl
    {
        public searchMenu()
        {
            InitializeComponent();
        }
        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (menubarbox.Text == "Search Here")
            {
                menubarbox.Text = "";
                menubarbox.Opacity = 0.8;
                menubarbox.Foreground = Brushes.Black;
            }
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (menubarbox.Text == "")
            {
                menubarbox.Text = "Search Here";
                menubarbox.Opacity = 0.5;
                menubarbox.Foreground = Brushes.Gray;

            }
        }
    }
}
