using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static SmartClinic.Patient;

namespace SmartClinic.View.UserControls
{
    public partial class RxUsercontrol : UserControl
    {
        private Patient newPatient;

        public event EventHandler<PatientEventArgs> PrescriptionDataAvailable;

        public RxUsercontrol()
        {
            InitializeComponent();
        }
        public RxUsercontrol(Patient newPatient) : this()
        {
            this.newPatient = newPatient;

            // Initialize the UI or update it with the new patient data
            // ...

            //MessageBox.Show("also found in rx " + newPatient.Name);
            patientInfoControl.UpdatePatientInfo(newPatient);
            // Raise the PrescriptionDataAvailable event
            OnPrescriptionDataAvailable(new PatientEventArgs { NewPatient = newPatient });
        }

        //public RxUsercontrol(Patient newPatient, patientInfo patientInfoControl) : this()
        //{
        //    this.newPatient = newPatient;

        //    // Update patient information in the patientInfo UserControl
        //    //MessageBox.Show("Does it work");
        //    patientInfoControl.UpdatePatientInfo(newPatient);
            
        //    // Raise the PrescriptionDataAvailable event
        //    OnPrescriptionDataAvailable(new PatientEventArgs { NewPatient = newPatient });
        //}


        private void OnPrescriptionDataAvailable(PatientEventArgs e)
        {
            // Raise the event if there are subscribers
            PrescriptionDataAvailable?.Invoke(this, e);
        }
    }
}
