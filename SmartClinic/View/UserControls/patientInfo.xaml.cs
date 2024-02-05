using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static SmartClinic.Patient;

namespace SmartClinic.View.UserControls
{
    public partial class patientInfo : UserControl
    {
        private bool isPatientAdded = false;

        public patientInfo()
        {
            InitializeComponent();
        }

        private void AddPatientButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the AddPatient window
            AddPatient addPatientWindow = new AddPatient();
            addPatientWindow.PatientInfoSubmitted += AddPatientWindow_PatientInfoSubmitted;
            addPatientWindow.ShowDialog();
        }

        private void AddPatientWindow_PatientInfoSubmitted(object sender, PatientEventArgs e)
        {
            // Handle the submitted patient info
            string patientName = e.NewPatient.Name;
            string patientAge = e.NewPatient.Age;

            // Update UI with patient details
            UpdateUIWithPatientDetails(patientName, patientAge);
        }

        private void UpdateUIWithPatientDetails(string patientName, string patientAge)
        {
            if (!isPatientAdded)
            {
                // Remove the "+ Add Patient" button from the StackPanel
                addPatientPanel.Children.Remove(AddPatientButton);

                // Create TextBlocks for patient name and age
                TextBlock nameLabel = new TextBlock
                {
                    Text = patientName,
                    FontWeight = FontWeights.Bold,
                    VerticalAlignment = VerticalAlignment.Center
                };
                age.Text = patientAge;

                // Add the TextBlocks to the StackPanel
                addPatientPanel.Children.Add(nameLabel);

                isPatientAdded = true;
            }
        }


        private void OnSearchTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (searchPatientTextBox.Text == "Search Patient")
            {
                searchPatientTextBox.Text = "";
                searchPatientTextBox.Foreground = Brushes.Black;
            }
        }

        private void OnSearchTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchPatientTextBox.Text))
            {
                searchPatientTextBox.Text = "Search Patient";
                searchPatientTextBox.Foreground = Brushes.Gray;
            }
        }
    }
}
