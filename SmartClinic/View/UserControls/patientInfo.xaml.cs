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

        public event EventHandler<PatientEventArgs> PatientInfoSubmitted;

        public patientInfo()
        {
            InitializeComponent();
        }

        // This method will be called when RxUsercontrol wants to update patient info
        public void UpdatePatientInfo(Patient patient)
        {
            //MessageBox.Show("message from patientinfo " + patient.Name);
            if (patient != null)
            {
                // Update UI with patient details
                UpdateUIWithPatientDetails(patient.Name, patient.Age);
            }
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
            PatientInfoSubmitted?.Invoke(this, e);

            // Update UI with patient details
            UpdateUIWithPatientDetails(e.NewPatient.Name, e.NewPatient.Age);
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

                TextBlock ageLabel = new TextBlock
                {
                    Text = "Age: " + patientAge,
                    FontWeight = FontWeights.Bold,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Add the TextBlocks to the StackPanel
                addPatientPanel.Children.Add(nameLabel);
                //addPatientPanel.Children.Add(ageLabel);
                age.Text=patientAge;
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
