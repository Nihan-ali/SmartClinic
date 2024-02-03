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
using System.Windows.Shapes;

namespace SmartClinic.View.UserControls
{
    /// <summary>
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Window
    {
        private patientInfo _patientInfoUserControl;

        public AddPatient() { 
        InitializeComponent();
        }
        public AddPatient(patientInfo patientInfoUserControl)
        {
            InitializeComponent();
            _patientInfoUserControl = patientInfoUserControl;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            string age = ageTextBox.Text;
            string phone = phoneTextBox.Text;
            string address = addressTextBox.Text;
            string bloodGroup = (string)((ComboBoxItem)bloodGroupComboBox.SelectedItem).Content;

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(age) && !string.IsNullOrWhiteSpace(phone)
                && !string.IsNullOrWhiteSpace(address) && !string.IsNullOrWhiteSpace(bloodGroup))
            {
                DatabaseHelper.InsertPatientInfo(name, age, phone, address, bloodGroup);

                // Update the patientInfo UserControl
                _patientInfoUserControl.UpdatePatientInfo(name, age);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please fill in all fields.");
            }
        }
    }
}
