using System;
using System.Windows;
using System.Windows.Controls;
using static SmartClinic.Patient;

namespace SmartClinic.View.UserControls
{
    public partial class AddPatient : Window
    {
        // Define an event to signal when patient info is submitted
        public event EventHandler<PatientEventArgs> PatientInfoSubmitted;

        public AddPatient()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Get patient info from form elements
            string name = nameTextBox.Text;
            string age = ageTextBox.Text;
            string phone = phoneTextBox.Text;
            string address = addressTextBox.Text;
            string bloodGroup = (string)((ComboBoxItem)bloodGroupComboBox.SelectedItem).Content;

            // Validate input (you may add more validation logic)

            // Create a new Patient object
            Patient newPatient = new Patient
            {
                Name = name,
                Age = age,
                Phone = phone,
                Address = address,
                Blood = bloodGroup
            };

            // Insert patient info into the database
            DatabaseHelper.InsertPatientInfo(name, age, phone, address, bloodGroup);

            // Raise the event to notify subscribers (e.g., patientInfo UserControl)
            PatientInfoSubmitted?.Invoke(this, new PatientEventArgs { NewPatient = newPatient });

            // Close the form window
            Close();
        }
    }
}