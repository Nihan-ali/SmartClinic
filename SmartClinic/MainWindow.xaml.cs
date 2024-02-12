using SmartClinic.View.UserControls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using static SmartClinic.Patient;

namespace SmartClinic
{
    public partial class MainWindow : Window
    {
        private Patient newPatient;
        private PatientProfileUserControl patientProfileUserControl;

        public string TodayDate { get; set; }
        private Popup treatmentPlanPopup;

        public event EventHandler<PatientEventArgs> PatientInfoSubmitted;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            TodayDate = DateTime.Now.ToString("dd-MM-yyyy");
            treatmentPlanPopup = new Popup();

            // Set a default content for contentControl
            patientProfileUserControl = new PatientProfileUserControl(null, this);

            // Subscribe to the event inside the constructor of MainWindow
            patientProfileUserControl.PatientInfoSubmitted += PatientProfileUserControl_PatientInfoSubmitted;

            // Set the content to RxUsercontrol initially
            contentControl.Content = new View.UserControls.RxUsercontrol();
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (treatmentPlanPopup.IsOpen && !IsMouseOverPopup(e))
            {
                treatmentPlanPopup.IsOpen = false;
            }
        }

        private bool IsMouseOverPopup(MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(this);
            return position.X >= treatmentPlanPopup.HorizontalOffset &&
                   position.X <= treatmentPlanPopup.HorizontalOffset + treatmentPlanPopup.ActualWidth &&
                   position.Y >= treatmentPlanPopup.VerticalOffset &&
                   position.Y <= treatmentPlanPopup.VerticalOffset + treatmentPlanPopup.ActualHeight;
        }

        private void RxButton_Click(object sender, RoutedEventArgs e)
        {
            // Set content to RxUserControl
            contentControl.Content = new View.UserControls.RxUsercontrol();
        }

        private void PatientButton_Click(object sender, RoutedEventArgs e)
        {
            // Set content to PatientUserControl
            contentControl.Content = new View.UserControls.PatientsUserControl();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Set content to SettingsUserControl
            // contentControl.Content = new UserControls.SettingsUserControl();
        }

        private void StatButton_Click(object sender, RoutedEventArgs e)
        {
            // Set content to StatUserControl
            // contentControl.Content = new UserControls.StatUserControl();
        }

        private void writePrescription_Click(object sender, RoutedEventArgs e)
        {
            // Create a new patient for demonstration purposes
            newPatient = new Patient
            {
                Name = "John Doe",
                Age = "30"
            };
            MessageBox.Show("This is being called in main");

            // Invoke the event and pass the new patient information
            PatientInfoSubmitted?.Invoke(this, new PatientEventArgs { NewPatient = newPatient });

            // Open the RxUserControl with the selected patient
            contentControl.Content = new View.UserControls.RxUsercontrol(newPatient);
        }

        // This method is called when PatientProfileUserControl fires the event
        private void PatientProfileUserControl_PatientInfoSubmitted(object sender, PatientEventArgs e)
        {
            // Handle the submitted patient info
            newPatient = e.NewPatient;
            MessageBox.Show("Also came here " + newPatient.Name);
            PatientInfoSubmitted?.Invoke(this, new PatientEventArgs { NewPatient = newPatient });

            // Open the PatientProfileUserControl with the selected patient
            // contentControl.Content = new PatientProfileUserControl(newPatient, this);
        }

        private void MainWindow_PatientInfoSubmitted(object sender, PatientEventArgs e)
        {
            // Handle the patient information submitted event
            MessageBox.Show($"Received patient information: {e.NewPatient.Name}, {e.NewPatient.Age}");
        }
    }
}
