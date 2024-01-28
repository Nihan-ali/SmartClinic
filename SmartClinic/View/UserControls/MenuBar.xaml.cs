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
    /// Interaction logic for MenuBar.xaml
    /// </summary>
    public partial class MenuBar : UserControl
    {
        public MenuBar()
        {
            InitializeComponent();
        }
        private void RxButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle the click event for the Rx button
            MessageBox.Show("Rx Button Clicked!");
        }

        private void PatientsButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle the click event for the Patients button
            PatientList patientListWindow = new PatientList();
            patientListWindow.Show();
            //   MessageBox.Show("Patients Button Clicked!");
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle the click event for the Settings button
            MessageBox.Show("Settings Button Clicked!");
        }

        private void StatButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle the click event for the Stat button
            MessageBox.Show("Stat Button Clicked!");
        }
    }
}
