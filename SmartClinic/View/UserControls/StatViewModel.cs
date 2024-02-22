// StatViewModel.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace SmartClinic.View.UserControls
{
    public class StatViewModel : INotifyPropertyChanged
    {
        private List<PatientVisit> _patientVisits;
        private DateTime _startDate;
        private DateTime _endDate;

        private int _totalVisit;
        private int _todaysVisit;
        private int _monthsVisit;
        private int _lastMonthsVisit;
        private int _totalPatient;
        private int _todaysPatient;
        private int _thisMonthsPatient;
        private int _lastMonthsPatient;

        public StatViewModel()
        {
            // Initialize your properties or perform any additional setup if needed
            LoadData();
            SetDefaultDates();
            UpdateStatistics();
        }

        public List<PatientVisit> PatientVisits
        {
            get { return _patientVisits; }
            set { _patientVisits = value; OnPropertyChanged(nameof(PatientVisits)); }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                    UpdateStatistics();
                }
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                    UpdateStatistics();
                }
            }
        }

        public int TotalVisit
        {
            get { return _totalVisit; }
            set { _totalVisit = value; OnPropertyChanged(nameof(TotalVisit)); }
        }

        public int TodaysVisit
        {
            get { return _todaysVisit; }
            set { _todaysVisit = value; OnPropertyChanged(nameof(TodaysVisit)); }
        }

        public int MonthsVisit
        {
            get { return _monthsVisit; }
            set { _monthsVisit = value; OnPropertyChanged(nameof(MonthsVisit)); }
        }

        public int LastMonthsVisit
        {
            get { return _lastMonthsVisit; }
            set { _lastMonthsVisit = value; OnPropertyChanged(nameof(LastMonthsVisit)); }
        }

        public int TotalPatient
        {
            get { return _totalPatient; }
            set { _totalPatient = value; OnPropertyChanged(nameof(TotalPatient)); }
        }

        public int TodaysPatient
        {
            get { return _todaysPatient; }
            set { _todaysPatient = value; OnPropertyChanged(nameof(TodaysPatient)); }
        }

        public int ThisMonthsPatient
        {
            get { return _thisMonthsPatient; }
            set { _thisMonthsPatient = value; OnPropertyChanged(nameof(ThisMonthsPatient)); }
        }

        public int LastMonthsPatient
        {
            get { return _lastMonthsPatient; }
            set { _lastMonthsPatient = value; OnPropertyChanged(nameof(LastMonthsPatient)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadData()
        {
            // Load patient visits from the database
            PatientVisits = DatabaseHelper.GetPatientVisits();

            // Check the count
            if (PatientVisits != null)
            {
                MessageBox.Show($"Loaded {PatientVisits.Count} patient visits.");
            }
            else
            {
                MessageBox.Show("No patient visits loaded.");
            }
        }

        private void SetDefaultDates()
        {
            // Set default dates
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
        }

        private void UpdateStatistics()
        {
            if (PatientVisits == null)
                return;

            // Filter patient visits based on selected date range
            var selectedVisits = PatientVisits
                .Where(visit => visit.Visit >= StartDate && visit.Visit <= EndDate)
                .ToList();

            // Update the statistics based on the filtered visits
            UpdateVisitStatistics(selectedVisits);
            UpdatePatientStatistics(selectedVisits);
        }

        private void UpdateVisitStatistics(List<PatientVisit> selectedVisits)
        {
            TotalVisit = selectedVisits.Count;
            TodaysVisit = selectedVisits.Count(visit => visit.Visit.Date == DateTime.Today);
            MonthsVisit = selectedVisits.Count(visit => visit.Visit.Month == DateTime.Today.Month);
            LastMonthsVisit = selectedVisits.Count(visit => visit.Visit.Month == DateTime.Today.AddMonths(-1).Month);
        }

        private void UpdatePatientStatistics(List<PatientVisit> selectedVisits)
        {
            var uniquePatients = selectedVisits.Select(visit => visit.Id).Distinct().ToList();

            TotalPatient = uniquePatients.Count;
            TodaysPatient = uniquePatients.Count(id => selectedVisits.Any(visit => visit.Id == id && visit.Visit.Date == DateTime.Today));
            ThisMonthsPatient = uniquePatients.Count(id => selectedVisits.Any(visit => visit.Id == id && visit.Visit.Month == DateTime.Today.Month));
            LastMonthsPatient = uniquePatients.Count(id => selectedVisits.Any(visit => visit.Id == id && visit.Visit.Month == DateTime.Today.AddMonths(-1).Month));
        }
    }
}
