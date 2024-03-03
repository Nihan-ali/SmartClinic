using System;
using System.Diagnostics.Eventing.Reader;
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
            string name = string.Empty;
            string age = string.Empty;
            string phone = string.Empty;
            string address = string.Empty;
            string bloodGroup = string.Empty;

            if (nameTextBox.Text != null)
            {
                name = nameTextBox.Text;
            }
            if(ageTextBox.Text != null)
            {
                age = ageTextBox.Text;
            }
            if(phoneTextBox.Text != null)
            {
                phone = phoneTextBox.Text;
            }
            if(addressTextBox.Text != null)
            {
                address = addressTextBox.Text;
            }
            if((ComboBoxItem)bloodGroupComboBox.SelectedItem != null)
            {
                bloodGroup = (string)((ComboBoxItem)bloodGroupComboBox.SelectedItem).Content;
            }

            if (name != null && name.Length > 0)
            {
                int insertedId = DatabaseHelper.InsertPatientInfo(name, age, phone, address, bloodGroup);

                // Create a new Patient object with the retrieved ID
                Patient newPatient = new Patient
                {
                    Id = insertedId,
                    Name = name,
                    Age = age,
                    Phone = phone,
                    Address = address,
                    Blood = bloodGroup
                };

                // Raise the event to notify subscribers (e.g., patientInfo UserControl)
                PatientInfoSubmitted?.Invoke(this, new PatientEventArgs { NewPatient = newPatient });

                // Close the form window
                Close();

            }
            else
            {
                MessageBox.Show("Provide A Patient's Name");
            }

        }
    }
}