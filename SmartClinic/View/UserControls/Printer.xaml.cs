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

        public Printer()
        {
            InitializeComponent();
            docdegree.Content = variables.docdegree;
            docname.Content = variables.docname;
            docname_bangla.Content = variables.docname_bangla;
            docdegree_bangla.Content = variables.docdegree_bangla;
            docdetail.Content = variables.docdetail;
            docdetail_bangla.Content = variables.docdetail_bangla;
            moredetail_bangla.Content = variables.moredetail_bangla;
            chambername.Content = variables.chamber;
            chamberlocation.Content = variables.chamber_location;
            chamberphone.Content = variables.chamber_phone;
            visit_date.Content = variables.visit_date;
            visit_time.Content = variables.visit_time;
            outro.Content = variables.outro;

    }
        public Printer(Patient patient, string todaydate, List<Complaint> complaints, List<history> histories, List<Examination> examinations, List<Investigation> investigations, List<Diagnosis> diagnoses, List<Treatment> treatments, List<DummyMedicine> medicines, List<Advice> advices, List<FollowUp> followUps, List<SpecialNote> specialNotes)
        {
            InitializeComponent();
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

            UpdateAllFields();
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
                PrescriptionId.Text = newPatient.Id.ToString();
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
                historyListView.ItemsSource = null;
                historyListView.ItemsSource = selectedHistories;

            }
            else
            {
                historygrid.Visibility = Visibility.Collapsed;
                historyListView.ItemsSource = null;
            }
        }

        private void UpdateExaminationListViews()
        {
            if (selectedExaminations != null && selectedExaminations.Count > 0)
            {
                examinationListView.ItemsSource = null;
                examinationListView.ItemsSource = selectedExaminations;
            }
            else
            {
                examinationgrid.Visibility = Visibility.Collapsed;
                examinationListView.ItemsSource = null;
            }
        }

        private void UpdateInvestigationListViews()
        {
            if (selectedInvestigations != null && selectedInvestigations.Count > 0)
            {
                selectedInvestigationsListView.ItemsSource = null;
                selectedInvestigationsListView.ItemsSource = selectedInvestigations;
            }
            else
            {
                investigationgrid.Visibility = Visibility.Collapsed;
                selectedDiagnosisListView.ItemsSource = null;
            }
        }

        private void UpdateDiagnosisListViews()
        {
            if(selectedDiagnosis != null && selectedDiagnosis.Count > 0)
            {
                selectedDiagnosisListView.ItemsSource = null;
                selectedDiagnosisListView.ItemsSource = selectedDiagnosis;
            }
            else
            {
                diagnosisgrid.Visibility = Visibility.Collapsed;
                selectedDiagnosisListView.ItemsSource = null;
            }
        }

        private void UpdateTreatmentListViews()
        {
            if (selectedTreatments != null && selectedTreatments.Count > 0)
            {
                selectedTreatmentListView.ItemsSource = null;
                selectedTreatmentListView.ItemsSource = selectedTreatments;
            }
            else
            {
                treatmentgrid.Visibility = Visibility.Collapsed;
                selectedTreatmentListView.ItemsSource = null;
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


        public void PrintButton_Click(Patient patient, string todaydate, List<Complaint> complaints, List<history> histories, List<Examination> examinations, List<Investigation> investigations, List<Diagnosis> diagnoses, List<Treatment> treatments, List<DummyMedicine> medicines, List<Advice> advices, List<FollowUp> followUps, List<SpecialNote> specialNotes)
        {
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

            UpdateAllFields();

            PrintDialog printDialog = new PrintDialog();

            PrintTicket printTicket = printDialog.PrintTicket;
            printTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);

            printDialog.PrintVisual(this, "Prescription");
        }

        private void ListViewItem_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            if (item != null)
            {
                int index = selectedMedicinesListView.Items.IndexOf(item.DataContext) + 1;
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

    public class IsNullOrEmptyConverter1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return string.IsNullOrEmpty(stringValue);
            }

            return true; // Default to true if not a string
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
