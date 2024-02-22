using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            // Load all patient visits initially
            LoadAllPatientVisits();

            // Set Todays values based on all patient visits initially
            TodaysVisit = Patients.Count(visit => visit.Visit.Date == DateTime.Today);
            TodaysPatient = Patients.Select(visit => visit.Id).Distinct().Count(id => Patients.Any(visit => visit.Id == id && visit.Visit.Date == DateTime.Today));

            MonthsVisit = Patients.Count(visit => visit.Visit.Month == DateTime.Today.Month);
            LastMonthsVisit = Patients.Count(visit => visit.Visit.Month == DateTime.Today.AddMonths(-1).Month);

            // Set This Month and Last Month values based on all patient visits initially
            ThisMonthsPatient = Patients.Select(visit => visit.Id).Distinct().Count(id => Patients.Any(visit => visit.Id == id && visit.Visit.Month == DateTime.Today.Month));
            LastMonthsPatient = Patients.Select(visit => visit.Id).Distinct().Count(id => Patients.Any(visit => visit.Id == id && visit.Visit.Month == DateTime.Today.AddMonths(-1).Month));

            // Convert DateTime to string with a specific format
            StartDatePicker.SelectedDate = DateTime.Today;
            EndDatePicker.SelectedDate = DateTime.Today;

            // Set event handlers for date picker selection changed events
            StartDatePicker.SelectedDateChanged += DateChanged;
            EndDatePicker.SelectedDateChanged += DateChanged;

            // Update total visit and patient counts
            UpdateCountsBasedOnVisits();
        }

        private void LoadAllPatientVisits()
        {
            // Load all patient visits initially
            Patients = new ObservableCollection<PatientVisit>(DatabaseHelper.GetPatientVisits());
        }

        private void DateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DatePicker datePicker)
            {
                string startDateString = StartDatePicker.SelectedDate?.ToString("yyyy-MM-dd");
                string endDateString = EndDatePicker.SelectedDate?.ToString("yyyy-MM-dd");

                // Update the statistics based on the selected dates
                //MessageBox.Show("for " + startDateString + " and " + endDateString);
                UpdateStatistics(startDateString, endDateString);
            }
        }

        private void UpdateStatistics(string startDate, string endDate)
        {
            var selectedVisits = Patients
                .Where(visit => visit.Visit >= DateTime.Parse(startDate) && visit.Visit <= DateTime.Parse(endDate))
                .ToList();

            // Update the counts based on the filtered visits
            UpdateVisitStatistics(selectedVisits);
            UpdatePatientStatistics(selectedVisits);
        }

        private void UpdateVisitStatistics(List<PatientVisit> selectedVisits)
        {
            TotalVisit = selectedVisits.Count;
            //MessageBox.Show("updating " + TotalPatient);
            UpdateBindings();
            //TodaysVisit = selectedVisits.Count(visit => visit.Visit.Date == DateTime.Today);
            //MonthsVisit = selectedVisits.Count(visit => visit.Visit.Month == DateTime.Today.Month);
            //LastMonthsVisit = selectedVisits.Count(visit => visit.Visit.Month == DateTime.Today.AddMonths(-1).Month);
        }

        private void UpdatePatientStatistics(List<PatientVisit> selectedVisits)
        {
            var uniquePatients = selectedVisits.Select(visit => visit.Id).Distinct().ToList();
            TotalPatient = uniquePatients.Count();
            //TodaysPatient = uniquePatients.Count(id => selectedVisits.Any(visit => visit.Id == id && visit.Visit.Date == DateTime.Today));
            //ThisMonthsPatient = uniquePatients.Count(id => selectedVisits.Any(visit => visit.Id == id && visit.Visit.Month == DateTime.Today.Month));
            //LastMonthsPatient = uniquePatients.Count(id => selectedVisits.Any(visit => visit.Id == id && visit.Visit.Month == DateTime.Today.AddMonths(-1).Month));

            // Update the bindings
            UpdateBindings();
        }

        private void UpdateCountsBasedOnVisits()
        {
            TotalVisit = Patients.Count;
            TotalPatient = Patients.Select(visit => visit.Id).Distinct().Count();

            // Update the bindings
            UpdateBindings();
        }

        private void UpdateBindings()
        {
            // Explicitly update the bindings to reflect the changes in the UI
            TotalVisitTextBlock.GetBindingExpression(TextBlock.TextProperty)?.UpdateTarget();
            TodaysVisitTextBlock.GetBindingExpression(TextBlock.TextProperty)?.UpdateTarget();
            MonthsVisitTextBlock.GetBindingExpression(TextBlock.TextProperty)?.UpdateTarget();
            LastMonthsVisitTextBlock.GetBindingExpression(TextBlock.TextProperty)?.UpdateTarget();

            TotalPatientTextBlock.GetBindingExpression(TextBlock.TextProperty)?.UpdateTarget();
            TodaysPatientTextBlock.GetBindingExpression(TextBlock.TextProperty)?.UpdateTarget();
            ThisMonthsPatientTextBlock.GetBindingExpression(TextBlock.TextProperty)?.UpdateTarget();
            LastMonthsPatientTextBlock.GetBindingExpression(TextBlock.TextProperty)?.UpdateTarget();
        }
    }
}
