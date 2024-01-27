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
    /// Interaction logic for medicine.xaml
    /// </summary>
    public partial class medicine : UserControl
    {
        public medicine()
        {
            InitializeComponent();
        }



        // MainWindow.xaml.cs
        private void Rx_Click(object sender, RoutedEventArgs e)
        {
            MedicineSearchWindow searchWindow = new MedicineSearchWindow();
            searchWindow.ShowDialog();
            // Handle the selected medicine from the search window as needed
        }


        private void Advices_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FollowUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SpecialNotes_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
