using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for editPatient.xaml
    /// </summary>
    public partial class editPatient : Window
    {
        Patient newPatient;
        public editPatient()
        {
            InitializeComponent();
        }
        //make a constructor that takes a patient object
        public editPatient(Patient patient)
        {
            InitializeComponent();
            newPatient = patient;
            //set the data context of the UI elements to the patient object
           // this.DataContext = newPatient;
            nameTextBox.Text = newPatient.Name;
            addressTextBox.Text = newPatient.Address;
            phoneTextBox.Text = newPatient.Phone;
            ageTextBox.Text = newPatient.Age.ToString();
        }
        //implement cancel button click event
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //implement save button click event
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string name = string.Empty;
            string age = string.Empty;
            string phone = string.Empty;
            string address = string.Empty;
            string bloodGroup = string.Empty;

            if (nameTextBox.Text != null)
            {

                newPatient.Name = nameTextBox.Text;

                // Create a TextInfo object for the current culture
                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

                // Capitalize the first letter of each word
                name = textInfo.ToTitleCase(name);
            }
            if (ageTextBox.Text != null)
            {
                newPatient.Age = ageTextBox.Text;
            }
            if (phoneTextBox.Text != null)
            {
                newPatient.Phone = phoneTextBox.Text;
            }
            if (addressTextBox.Text != null)
            {
                newPatient.Address = addressTextBox.Text;
            }
            if ((ComboBoxItem)bloodGroupComboBox.SelectedItem != null)
            {
                newPatient.Blood = (string)((ComboBoxItem)bloodGroupComboBox.SelectedItem).Content;
            }
            DatabaseHelper.UpdatePatientInfo(newPatient);
            MessageBox.Show("Patient info updated successfully.");
            this.Close();
        }
    }
}
