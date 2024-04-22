using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmartClinic.View.UserControls
{
    /// <summary>
    /// Interaction logic for Printer.xaml
    /// </summary>
    public partial class Printer : Window
    {
        private Patient newPatient;
        private List<Complaint> selectedComplaints = new List<Complaint>();
        public List<Complaint> SelectedComplaints => selectedComplaints;

        private List<history> selectedHistories = new List<history>();
        public List<history> SelectedHistories => selectedHistories;

        private List<Examination> selectedExaminations = new List<Examination>();
        public List<Examination> SelectedExaminations => selectedExaminations;

        private List<Investigation> selectedInvestigations = new List<Investigation>();
        public List<Investigation> SelectedInvestigations => selectedInvestigations;

        private List<Diagnosis> selectedDiagnosis = new List<Diagnosis>();
        public List<Diagnosis> SelectedDiagnosis => selectedDiagnosis;

        private List<Treatment> selectedTreatments = new List<Treatment>();
        public List<Treatment> SelectedTreatments => selectedTreatments;

        //from  medicine
        private List<DummyMedicine> selectedMedicines = new List<DummyMedicine>();
        private List<Advice> selectedAdvices = new List<Advice>();
        private List<FollowUp> selectedFollowUps = new List<FollowUp>();
        private List<SpecialNote> selectedSpecialNotes = new List<SpecialNote>();
        string dayyt;
        public List<DummyMedicine> SelectedMedicines => selectedMedicines;
        public List<Advice> SelectedAdvices => selectedAdvices;
        public List<FollowUp> SelectedFollowUps => selectedFollowUps;
        public List<SpecialNote> SelectedSpecialNotes => selectedSpecialNotes;

        //public event EventHandler<PatientEventArgs> PrescriptionDataAvailable;
        public string prescriptionId;


        public Printer()
        {
            InitializeComponent();
            UpdateDoctorInfo();

        }
        public Printer(Patient patient, Int64 presid, string todaydate, List<Complaint> complaints, List<history> histories, List<Examination> examinations, List<Investigation> investigations, List<Diagnosis> diagnoses, List<Treatment> treatments, List<DummyMedicine> medicines, List<Advice> advices, List<FollowUp> followUps, List<SpecialNote> specialNotes)
        {
            InitializeComponent();
            //docname.Content = "ghochu222";
            prescriptionId = presid.ToString();
            newPatient = patient;
            dayyt = todaydate;
            selectedComplaints = complaints;
            selectedHistories = histories;
            selectedExaminations = examinations;
            selectedInvestigations = investigations;
            selectedDiagnosis = diagnoses;
            selectedTreatments = treatments;
            selectedMedicines = medicines;
            selectedAdvices = advices;
            selectedFollowUps = followUps;
            selectedSpecialNotes = specialNotes;
            UpdateDoctorInfo();
            UpdateAllFields();
        }
        private void UpdateDoctorInfo()
        {
            List<DoctorInfo> doctorInfos = DatabaseHelper.GetDoctorInfos();
            // Check if the list is not empty before accessing its first element
            if (doctorInfos != null && doctorInfos.Count > 0)
            {
                DoctorInfo doctorInfo = doctorInfos[0];

                docdegree.Content = doctorInfo.docdegree;
                docname.Content = doctorInfo.docname;
                docname_bangla.Content = doctorInfo.docname_bangla;
                docdegree_bangla.Content = doctorInfo.docdegree_bangla;
                docdetail.Content = doctorInfo.docdetail;
                docdetail_bangla.Content = doctorInfo.docdetail_bangla;
                moredetail_bangla.Content = doctorInfo.moredetail_bangla;
                chambername.Content = doctorInfo.chamber;
                chamberlocation.Content = doctorInfo.chamber_location;
                chamberphone.Content = doctorInfo.chamber_phone;
                visit_date.Content = doctorInfo.visit_date;
                visit_time.Content = doctorInfo.visit_time;
                outro.Content = doctorInfo.outro;
            }
        }

        private void UpdateAllFields()
        {
            todaydate.Text = dayyt;
            UpdatePatientInformation();
            UpdateComplaintListViews();
            UpdateHistoryListViews();
            UpdateExaminationListViews();
            UpdateInvestigationListViews();
            UpdateDiagnosisListViews();
            UpdateTreatmentListViews();
            UpdateMedicineListViews();
            UpdateAdviceListViews();
            UpdateFollowUpListViews();
            UpdateSpecialNoteListViews();
        }

        private void UpdatePatientInformation()
        {
            if (newPatient != null)
            {
                PatientName.Text = newPatient.Name;
                age.Text = newPatient.Age;
                PrescriptionId.Text = prescriptionId;
            }
        }
        private void UpdateComplaintListViews()
        {
            if (selectedComplaints != null && selectedComplaints.Count > 0)
            {
                ComplaintsListView.ItemsSource = null;
                ComplaintsListView.ItemsSource = selectedComplaints;
            }
            else
            {
                complaintgrid.Visibility = Visibility.Collapsed;
                ComplaintsListView.ItemsSource = null;
            }
        }


        private void UpdateHistoryListViews()
        {
            if (selectedHistories != null && selectedHistories.Count > 0)
            {
                HistoryListView.ItemsSource = null;
                HistoryListView.ItemsSource = selectedHistories;

            }
            else
            {
                historygrid.Visibility = Visibility.Collapsed;
                HistoryListView.ItemsSource = null;
            }
        }

        private void UpdateExaminationListViews()
        {
            if (selectedExaminations != null && selectedExaminations.Count > 0)
            {
                ExaminationListView.ItemsSource = null;
                ExaminationListView.ItemsSource = selectedExaminations;
            }
            else
            {
                examinationgrid.Visibility = Visibility.Collapsed;
                ExaminationListView.ItemsSource = null;
            }
        }

        private void UpdateInvestigationListViews()
        {
            if (selectedInvestigations != null && selectedInvestigations.Count > 0)
            {
                InvestigationsListView.ItemsSource = null;
                InvestigationsListView.ItemsSource = selectedInvestigations;
            }
            else
            {
                investigationgrid.Visibility = Visibility.Collapsed;
                DiagnosisListView.ItemsSource = null;
            }
        }

        private void UpdateDiagnosisListViews()
        {
            if (selectedDiagnosis != null && selectedDiagnosis.Count > 0)
            {
                DiagnosisListView.ItemsSource = null;
                DiagnosisListView.ItemsSource = selectedDiagnosis;
            }
            else
            {
                diagnosisgrid.Visibility = Visibility.Collapsed;
                DiagnosisListView.ItemsSource = null;
            }
        }

        private void UpdateTreatmentListViews()
        {
            if (selectedTreatments != null && selectedTreatments.Count > 0)
            {
                TreatmentListView.ItemsSource = null;
                TreatmentListView.ItemsSource = selectedTreatments;
            }
            else
            {
                treatmentgrid.Visibility = Visibility.Collapsed;
                TreatmentListView.ItemsSource = null;
            }
        }

        private void UpdateMedicineListViews()
        {
            if (selectedMedicines != null && selectedMedicines.Count > 0)
            {
                selectedMedicinesListView.ItemsSource = null;
                selectedMedicinesListView.ItemsSource = selectedMedicines;
            }
            else
            {
                medicinegrid.Visibility = Visibility.Collapsed;
                selectedMedicinesListView.ItemsSource = null;
            }
        }

        private void UpdateAdviceListViews()
        {
            if (selectedAdvices != null && selectedAdvices.Count > 0)
            {
                selectedAdvicesListView.ItemsSource = null;
                selectedAdvicesListView.ItemsSource = selectedAdvices;
            }
            else
            {
                advicegrid.Visibility = Visibility.Collapsed;
                selectedAdvicesListView.ItemsSource = null;
            }
        }

        private void UpdateFollowUpListViews()
        {
            if (selectedFollowUps != null && selectedFollowUps.Count > 0)
            {
                selectedFollowUpListView.ItemsSource = null;
                selectedFollowUpListView.ItemsSource = selectedFollowUps;
            }
            else
            {
                followupgrid.Visibility = Visibility.Collapsed;
                selectedFollowUpListView.ItemsSource = null;
            }
        }

        private void UpdateSpecialNoteListViews()
        {
            if (selectedSpecialNotes != null && selectedSpecialNotes.Count > 0)
            {
                selectedSpecialNoteListView.ItemsSource = null;
                selectedSpecialNoteListView.ItemsSource = selectedSpecialNotes;
            }
            else
            {
                specialnotegrid.Visibility = Visibility.Collapsed;
                selectedSpecialNoteListView.ItemsSource = null;
            }
        }
        public int offsett = 0;
        public void PrintButton_Click(Int64 presid, Patient patient, string todaydate, List<Complaint> complaints, List<history> histories, List<Examination> examinations, List<Investigation> investigations, List<Diagnosis> diagnoses, List<Treatment> treatments, List<DummyMedicine> medicines, List<Advice> advices, List<FollowUp> followUps, List<SpecialNote> specialNotes, bool second, int offset)
        {
            newPatient = patient;
            dayyt = todaydate;
            prescriptionId = presid.ToString();
            selectedComplaints = complaints;
            selectedHistories = histories;
            selectedExaminations = examinations;
            selectedInvestigations = investigations;
            selectedDiagnosis = diagnoses;
            selectedTreatments = treatments;
            selectedMedicines = medicines;
            selectedAdvices = advices;
            selectedFollowUps = followUps;
            selectedSpecialNotes = specialNotes;
            offsett = offset;
            if (second)
            {
                continued.Content = "(Continued)";
            }
            UpdateAllFields();
            prescriptionscroll.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            PrintDialog printDialog = new PrintDialog();

            PrintTicket printTicket = printDialog.PrintTicket;
            printDialog.PrintVisual(this, "Prescription");
        }

        private void ListViewItem_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            if (item != null)
            {
                int index = selectedMedicinesListView.Items.IndexOf(item.DataContext) + 1 + offsett;
                TextBlock serialNumberTextBlock = FindVisualChild<TextBlock>(item, "serialNumberTextBlock");
                if (serialNumberTextBlock != null)
                {
                    serialNumberTextBlock.Text = index.ToString();
                }
            }
        }

        private T FindVisualChild<T>(DependencyObject depObj, string name) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T && (child as FrameworkElement).Name == name)
                    return child as T;
                else
                {
                    T childItem = FindVisualChild<T>(child, name);
                    if (childItem != null)
                        return childItem;
                }
            }
            return null;
        }

    }
    public class WrapInParenthesesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                return $" ({value})";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }



}