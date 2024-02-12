using System;
using System.Windows;
using System.Windows.Controls;
using static SmartClinic.Patient;

namespace SmartClinic.View.UserControls
{
    public partial class RxUsercontrol : UserControl
    {
        private Patient newPatient;
        public event EventHandler<PatientEventArgs> PatientInfoSubmitted;
        public event EventHandler<PatientEventArgs> PatientInfoRequested;
        public event EventHandler<PatientEventArgs> PatientInfoUpdated;

        // Add a flag to prevent reentry and stack overflow
        private bool isUpdatingPatientInfo = false;

        public RxUsercontrol()
        {
            InitializeComponent();
            PatientInfoSubmitted += RxUsercontrol_PatientInfoSubmitted;
            PatientInfoRequested += RxUsercontrol_PatientInfoRequested;
        }

        public RxUsercontrol(Patient newPatient)
        {
            InitializeComponent(); // Add this line to initialize the XAML content
            this.newPatient = newPatient;
            PatientInfoSubmitted?.Invoke(this, new PatientEventArgs { NewPatient = newPatient });
        }

        private void UpdatePatientInfo()
        {
            // Check if already updating to prevent stack overflow
            if (isUpdatingPatientInfo)
            {
                return;
            }

            isUpdatingPatientInfo = true;

            // Update patient information in the patientInfo UserControl
            // You can get patient information from this.newPatient or any other source
            // For simplicity, let's assume you have a method UpdatePatientInfo in patientInfo UserControl
            patientInfoControl.UpdatePatientInfo(this.newPatient);

            // Raise event to notify MainWindow about the update
            PatientInfoUpdated?.Invoke(this, new PatientEventArgs { NewPatient = this.newPatient });

            isUpdatingPatientInfo = false;
        }

        // Handle the requested patient info
        private void RxUsercontrol_PatientInfoRequested(object sender, PatientEventArgs e)
        {
            if (e != null && e.NewPatient != null)
            {
                // Update UI with patient details
                // Call a method or update UI elements here
            }
        }

        private void RxUsercontrol_PatientInfoSubmitted(object sender, PatientEventArgs e)
        {
            // Handle the patient information
            MessageBox.Show($"Received patient information in RxUsercontrol: {e.NewPatient.Name}, {e.NewPatient.Age}");
        }
    }
}
