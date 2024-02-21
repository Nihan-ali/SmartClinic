// Assuming DatabaseHelper class exists with GetPatientVisitsByVisit method
using System;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;

namespace SmartClinic.View.UserControls
{
    public partial class StatUserControl : UserControl
    {
        public int TotalVisit { get; set; }
        public int TodaysVisit { get; set; }
        public int MonthsVisit { get; set; }
        public int LastMonthsVisit { get; set; }

        public int TotalPatient { get; set; }
        public int TodaysPatient { get; set; }
        public int ThisMonthsPatient { get; set; }
        public int LastMonthsPatient { get; set; }

        public ObservableCollection<PatientVisit> Patients { get; set; }

        public StatUserControl()
        {
            InitializeComponent();

            // Convert DateTime to string with a specific format
            StartDatePicker.SelectedDate = DateTime.Today;
            EndDatePicker.SelectedDate = DateTime.Today;
            string startDateString = DateTime.Today.ToString("yyyy-MM-dd");
            string endDateString = DateTime.Today.ToString("yyyy-MM-dd");
            // Set event handlers for date picker selection changed events
            StartDatePicker.SelectedDateChanged += DateChanged;
            EndDatePicker.SelectedDateChanged += DateChanged;

            // Set other properties accordingly
            TotalVisit = 300;
            //TodaysVisit = 150;
            MonthsVisit = 100;
            LastMonthsVisit = 200;
            ThisMonthsPatient = 300;
            LastMonthsPatient = 200;
            TodaysVisit = DatabaseHelper.GetPatientVisitsByVisit(startDateString, endDateString);
            //TodaysPatient = 45;
            // MessageBox.Show(TodaysPatient.ToString());
            // Set the data context of the UserControl to itself
            DataContext = this;

            // Load patients data
            LoadPatientsData();
        }
        private void DateChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the DatePicker instance from the sender object
            if (sender is DatePicker datePicker)
            {
                // Convert selected dates to string with a specific format
                string startDateString = StartDatePicker.SelectedDate?.ToString("yyyy-MM-dd");
                string endDateString = EndDatePicker.SelectedDate?.ToString("yyyy-MM-dd");

                // Call the method to get the total patient count within the specified date range
                TotalPatient = DatabaseHelper.GetPatientVisitsByVisit(startDateString, endDateString);

                // Call the method to get today's patient count
                TodaysPatient = DatabaseHelper.GetPatientVisitsByVisit(startDateString, endDateString);



            }
        }


        private void LoadPatientsData()
        {
            // Load patients data if needed
        }
    }
}