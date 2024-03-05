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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ObservableCollection<PatientVisit> Patients { get; set; }

        public StatUserControl()
        {
            InitializeComponent();
            SetDefaultValues();
            SetCalendarDisplayDate();
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
            StartDateCalendar.SelectedDate = DateTime.Today;

            // Initialize EndDate with today's date
            EndDate = DateTime.Today;
            EndDateCalendar.SelectedDate = DateTime.Today;

            // Set event handlers for date picker selection changed events
            StartDateCalendar.SelectedDatesChanged += DateChanged;
            EndDateCalendar.SelectedDatesChanged += EndDateCalendar_SelectedDatesChanged;

            // Update total visit and patient counts
            UpdateCountsBasedOnVisits();
        }

        private void SetCalendarDisplayDate()
        {
            // Set the display date of both calendars to the first day of the current year
            StartDateCalendar.DisplayDate = new DateTime(DateTime.Today.Year, 1, 1);
            EndDateCalendar.DisplayDate = new DateTime(DateTime.Today.Year, 1, 1);
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
                string startDateString = StartDateCalendar.SelectedDate?.ToString("yyyy-MM-dd");
                string endDateString = EndDateCalendar.SelectedDate?.ToString("yyyy-MM-dd");

                // Update the statistics based on the selected dates
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
            UpdateBindings();
        }

        private void UpdatePatientStatistics(List<PatientVisit> selectedVisits)
        {
            var uniquePatients = selectedVisits.Select(visit => visit.Id).Distinct().ToList();
            TotalPatient = uniquePatients.Count();
            UpdateBindings();
        }

        private void UpdateCountsBasedOnVisits()
        {
            TotalVisit = Patients.Count;
            TotalPatient = Patients.Select(visit => visit.Id).Distinct().Count();
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

        private void EndDateCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EndDateCalendar.SelectedDate < StartDateCalendar.SelectedDate)
            {
                MessageBox.Show("Please select an end date that is greater than or equal to the start date.");
            }
        }
    }
}
