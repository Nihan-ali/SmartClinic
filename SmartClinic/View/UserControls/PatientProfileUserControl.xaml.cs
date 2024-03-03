using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using static SmartClinic.Patient;

namespace SmartClinic.View.UserControls
{
    public partial class PatientProfileUserControl : UserControl
    {
        private readonly HomeWindow mainWindowInstance;

        public ObservableCollection<PatientVisit> Patients { get; set; }
        public PatientVisit SelectedPatientVisit { get; set; }
        public event EventHandler<PatientEventArgs> PatientInfoSubmitted;
        public Patient newPatient;
        public int patientCount = 0;

        public PatientProfileUserControl(Patient selectedPatient, HomeWindow mainWindow)
        {
            InitializeComponent();

            // Store the instance of MainWindow
            mainWindowInstance = mainWindow;
            MessageBox.Show("PatientProfileUserControl constructor called." + selectedPatient.Name);

            if (selectedPatient != null)
            {
                Patients = new ObservableCollection<PatientVisit>(DatabaseHelper.GetPatientVisitsById(selectedPatient.Id));

                // Set the ItemsSource of the ListBox to the Patients collection
                patientCount = Patients.Count;
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
            newPatient = e.NewPatient;
            PatientInfoSubmitted -= PatientProfileUserControl_PatientInfoSubmitted;

            if (mainWindowInstance != null && mainWindowInstance.contentControl.Content is RxUsercontrol rxUsercontrol)
            {
                rxUsercontrol.UpdatePatientInfo(newPatient);
            }
            else
            {
                // MessageBox.Show("Error: MainWindow or RxUsercontrol is null.");
            }
        }



        private void SetDataContext(Patient selectedPatient)
        {
            // Set data context for UI elements
            PatientNameTextBlock.Text = selectedPatient.Name;
            IDTextBlock.Text = $"ID: {selectedPatient.Id}";
            AgeTextBlock.Text = $"Age: {selectedPatient.Age}";
            PastVisitTextBlock.Text = $"Past Visits: {patientCount}"; // Replace [Data] with actual data
        }

        private void ShowPrescription_Click(object sender, RoutedEventArgs e)
        {
            Button showPrescriptionButton = sender as Button;

            if (showPrescriptionButton != null)
            {
                // Retrieve the DataContext of the button, which should be a PatientVisit
                PatientVisit patientVisit = showPrescriptionButton.DataContext as PatientVisit;

                if (patientVisit != null)
                {
                    // Invoke the event with both newPatient and patientVisit
                    PatientInfoSubmitted?.Invoke(this, new PatientEventArgs { NewPatient = newPatient, SelectedPatientVisit = patientVisit });

                    // Assuming you have an instance of MainWindow called 'mainWindowInstance'
                    if (mainWindowInstance != null)
                    {
                        RxUsercontrol rxUsercontrol = new RxUsercontrol(newPatient, patientVisit);

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
                else
                {
                    MessageBox.Show("Error: Unable to retrieve PatientVisit from the button's DataContext.");
                }
            }
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
                    bool deleted = DatabaseHelper.DeletePatientByPrescriptionId(SelectedPatientVisit.prescriptionId);

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