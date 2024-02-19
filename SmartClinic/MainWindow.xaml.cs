using SmartClinic.View.UserControls;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using System.Collections.ObjectModel;
using static SmartClinic.Patient;
using System.Windows.Controls;

namespace SmartClinic
{
    public partial class MainWindow : Window
    {
        public string TodayDate { get; set; }

        private Popup treatmentPlanPopup;
        private Patient newPatient;
        private RxUsercontrol rxUserControl;
        private patientInfo patientInfoControl;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize patientInfo and RxUsercontrol
            patientInfoControl = new patientInfo();
            //rxUserControl = new RxUsercontrol(newPatient, patientInfoControl);
            rxUserControl = new RxUsercontrol();

            DataContext = this;
            TodayDate = DateTime.Now.ToString("dd-MM-yyyy");
            treatmentPlanPopup = new Popup();

            // Set the content to RxUsercontrol
            contentControl.Content = rxUserControl;
        }

        //private void RxUserControl_PrescriptionDataAvailable(object sender, PatientEventArgs e)
        //{
        //    // Handle prescription data available event
        //    newPatient = e.NewPatient;

        //    // Update PatientInfoControl with the provided patient data
        //    MessageBox.Show("came in main " + newPatient.Name);
        //    patientInfoControl.UpdatePatientInfo(newPatient);
        //}

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
            contentControl.Content = rxUserControl;
        }

        private void PatientButton_Click(object sender, RoutedEventArgs e)
        {
            // Set content to PatientUserControl
            contentControl.Content = new View.UserControls.PatientsUserControl();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Set content to SettingsUserControl
            //contentControl.Content = new UserControls.SettingsUserControl();
        }

        private void StatButton_Click(object sender, RoutedEventArgs e)
        {
            // Set content to StatUserControl
            //contentControl.Content = new UserControls.StatUserControl();
        }
    }
}
