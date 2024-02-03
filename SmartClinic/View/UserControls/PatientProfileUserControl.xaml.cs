using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SmartClinic.View.UserControls
{
    public partial class PatientProfileUserControl : UserControl
    {
        public ObservableCollection<PatientVisit> Patients { get; set; }
        public PatientVisit SelectedPatientVisit { get; set; }

        public PatientProfileUserControl(Patient selectedPatient)
        {
            InitializeComponent();
            Patients = new ObservableCollection<PatientVisit>(DatabaseHelper.GetPatientVisitsById(selectedPatient.Id));

            // Set the ItemsSource of the ListBox to the Patients collection

            PrescriptionList.ItemsSource = Patients;

            // Set the data context for the UI elements based on the selected patient
            SetDataContext(selectedPatient);

        }

        private void SetDataContext(Patient selectedPatient)
        {
            if (selectedPatient != null)
            {
                // Set data context for UI elements
                PatientNameTextBlock.Text = selectedPatient.Name;
                IDTextBlock.Text = $"ID: {selectedPatient.Id}";
                AgeTextBlock.Text = $"Age: {selectedPatient.Age}";
                RxTextBlock.Text = "Rx+ Write Prescription";
                RxVisitTextBlock.Text = "Rx Visit: [Data]"; // Replace [Data] with actual data
                PastVisitTextBlock.Text = "Past Visit: [Data]"; // Replace [Data] with actual data

                // You may need to bind PrescriptionList.ItemsSource to a collection of prescription data
                // For simplicity, it's not done here. You need to replace [PrescriptionData] with actual prescription data.
                // PrescriptionList.ItemsSource = [PrescriptionData];
            }
        }
        public class PrescriptionItem
        {
            public DateTime Date { get; set; }
            public string PrescriptionText { get; set; }
        }


        private void ShowPrescription_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Handle show prescription button click
        }

        private void Print_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Handle print button click
        }

        // Add this method to your code-behind (PatientProfileUserControl.xaml.cs)


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Use the SelectedPatientVisit property instead of PrescriptionList.SelectedItem
            if (SelectedPatientVisit != null)
            {
                Patients.Remove(SelectedPatientVisit);
            }
            else
            {
                MessageBox.Show("Please select a visit to delete.");
            }
        }
        private void PrescriptionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update the SelectedPatientVisit property when selection changes
            SelectedPatientVisit = PrescriptionList.SelectedItem as PatientVisit;
        }


    }




}
