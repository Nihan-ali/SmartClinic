using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using static SmartClinic.Patient;

namespace SmartClinic.View.UserControls
{
    public partial class PatientProfileUserControl : UserControl
    {
        private readonly MainWindow mainWindowInstance;

        public ObservableCollection<PatientVisit> Patients { get; set; }
        public PatientVisit SelectedPatientVisit { get; set; }
        public event EventHandler<PatientEventArgs> PatientInfoSubmitted;
        public Patient newPatient;

        public PatientProfileUserControl(Patient selectedPatient, MainWindow mainWindow)
        {
            InitializeComponent();

            // Store the instance of MainWindow
            mainWindowInstance = mainWindow;

            if (selectedPatient != null)
            {
                Patients = new ObservableCollection<PatientVisit>(DatabaseHelper.GetPatientVisitsById(selectedPatient.Id));

                // Set the ItemsSource of the ListBox to the Patients collection
                newPatient = selectedPatient;
                PrescriptionList.ItemsSource = Patients;

                // Set the data context for the UI elements based on the selected patient
                SetDataContext(selectedPatient);
            }
            else
            {
                // Handle the case where selectedPatient is null, log an error, or show a message.
                MessageBox.Show("Error: selectedPatient is null.");
            }

            // Subscribe to the event when the control is created
            PatientInfoSubmitted += PatientProfileUserControl_PatientInfoSubmitted;
        }


        private void PatientProfileUserControl_PatientInfoSubmitted(object sender, PatientEventArgs e)
        {
            // Handle the submitted patient info
            newPatient = e.NewPatient;

            // Log information to identify the issue
            //Console.WriteLine("Handling PatientInfoSubmitted event. Patient: " + newPatient.Name);

            // Show MessageBox with additional information
           // MessageBox.Show("Handling PatientInfoSubmitted event. Patient: " + newPatient.Name, "Debug Information");

            // Unsubscribe from the event if needed to avoid multiple invocations
            PatientInfoSubmitted -= PatientProfileUserControl_PatientInfoSubmitted;

            // Update the patientInfoControl with the new patient data
            if (mainWindowInstance != null && mainWindowInstance.contentControl.Content is RxUsercontrol rxUsercontrol)
            {
                if (rxUsercontrol.Content is patientInfo patientInfoControl)
                {
                    // Update patientInfoControl with the new patient data
                    patientInfoControl.UpdatePatientInfo(newPatient);
                }
                else
                {
                    // Log an error or display a message indicating an issue with patientInfoControl
                    //MessageBox.Show("Error: patientInfoControl is not found in RxUsercontrol.");
                }
            }
            else
            {
                // Log an error or display a message indicating an issue with MainWindow or RxUsercontrol
                //MessageBox.Show("Error: MainWindow or RxUsercontrol is null.");
            }
        }



        private void SetDataContext(Patient selectedPatient)
        {
            // Set data context for UI elements
            PatientNameTextBlock.Text = selectedPatient.Name;
            IDTextBlock.Text = $"ID: {selectedPatient.Id}";
            AgeTextBlock.Text = $"Age: {selectedPatient.Age}";
            RxVisitTextBlock.Text = "Rx Visit: [Data]"; // Replace [Data] with actual data
            PastVisitTextBlock.Text = "Past Visit: [Data]"; // Replace [Data] with actual data

            // You may need to bind PrescriptionList.ItemsSource to a collection of prescription data
            // For simplicity, it's not done here. You need to replace [PrescriptionData] with actual prescription data.
            // PrescriptionList.ItemsSource = [PrescriptionData];
        }

        private void ShowPrescription_Click(object sender, RoutedEventArgs e)
        {
            // Handle show prescription button click
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            // Handle print button click
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Use the SelectedPatientVisit property instead of PrescriptionList.SelectedItem
                if (SelectedPatientVisit != null)
                {
                    // Perform the deletion from the database based on ID
                    bool deleted = DatabaseHelper.DeletePatientVisitByVisit(SelectedPatientVisit.Id, SelectedPatientVisit.Visit);

                    if (deleted)
                    {
                        // Remove the item from the ObservableCollection
                        Patients.Remove(SelectedPatientVisit);

                        // Set the SelectedItem to null to clear the selection
                        PrescriptionList.SelectedItem = null;
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the visit.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a visit to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting visit: {ex.Message}");
            }
        }

        private void PrescriptionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update the SelectedPatientVisit property when selection changes
            SelectedPatientVisit = PrescriptionList.SelectedItem as PatientVisit;
        }

        private void writePrescription_Click(object sender, RoutedEventArgs e)
        {
            // Invoke the event when the "write prescription" button is clicked
            PatientInfoSubmitted?.Invoke(this, new PatientEventArgs { NewPatient = newPatient });

            // Assuming you have an instance of MainWindow called 'mainWindowInstance'
            if (mainWindowInstance != null)
            {
                RxUsercontrol rxUsercontrol = new RxUsercontrol(newPatient);

                if (rxUsercontrol != null && rxUsercontrol.Content != null)
                {
                    mainWindowInstance.contentControl.Content = rxUsercontrol;
                }
                else
                {
                    // Log an error or display a message indicating an issue with RxUsercontrol or its Content
                    MessageBox.Show("Error: RxUsercontrol or its Content is null.");
                }
            }
            else
            {
                // Log an error or display a message indicating an issue with MainWindow
                MessageBox.Show("Error: MainWindow is null.");
            }
        }
    }
}
