using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
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
            RxUsercontrol rxUsercontrol = new RxUsercontrol(); // Create an instance of RxUsercontrol
            rxUsercontrol.PatientInfoSubmitted += RxUsercontrol_PatientInfoSubmitted;
        }

        private void RxUsercontrol_PatientInfoSubmitted(object sender, PatientEventArgs e)
        {
            // Handle the requested patient info
            if (e != null && e.NewPatient != null)
            {
                // Update UI with patient details
                UpdateUIWithPatientDetails(e.NewPatient.Name, e.NewPatient.Age);
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
            if (e != null && e.NewPatient != null)
            {
                // Handle the submitted patient info
                string patientName = e.NewPatient.Name;
                string patientAge = e.NewPatient.Age;
                MessageBox.Show("Patient added: " + patientName);

                // Update UI with patient details
                UpdateUIWithPatientDetails(patientName, patientAge);

                // Raise the PatientInfoRequested event when patient information is requested
                PatientInfoSubmitted?.Invoke(this, new PatientEventArgs { NewPatient = e.NewPatient });
            }
        }
        public void UpdatePatientInfo(Patient patient)
        {
            // Update the name and age fields in your patientInfo UserControl
            string patientName = patient.Name;
            string patientAge = patient.Age;
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
                    Text = "Name: " + patientName,
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
                age.Text = patientAge;
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
