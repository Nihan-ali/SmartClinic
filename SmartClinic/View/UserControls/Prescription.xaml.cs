using PdfSharp.Pdf.Content.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace SmartClinic.View.UserControls
{
    /// <summary>
    /// Interaction logic for Prescription.xaml
    /// </summary>
    /// 
    public partial class Prescription : UserControl
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
        public List<DummyMedicine> SelectedMedicines => selectedMedicines;
        public List<Advice> SelectedAdvices => selectedAdvices;
        public List<FollowUp> SelectedFollowUps => selectedFollowUps;
        public List<SpecialNote> SelectedSpecialNotes => selectedSpecialNotes;

        public Prescription()
        {
            InitializeComponent();
        }
        public Prescription(Patient patient, List<Complaint> complaints, List<history> histories, List<Examination> examinations, List<Investigation> investigations, List<Diagnosis> diagnoses, List<Treatment> treatments, List<DummyMedicine> medicines, List<Advice> advices, List<FollowUp> followUps, List<SpecialNote> specialNotes)
        {
            InitializeComponent();
            newPatient = patient;
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
                PatientId.Text = newPatient.Id.ToString();
            }
        }
        private void UpdateComplaintListViews()
        {
            if (selectedComplaints != null && selectedComplaints.Count > 0)
            {
                selectedComplaintsListView.ItemsSource = null;
                selectedComplaintsListView.ItemsSource = selectedComplaints;
            }
            else
            {
                complaintgrid.Visibility = Visibility.Collapsed;
                selectedComplaintsListView.ItemsSource = null;
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
                selectedComplaintsListView.ItemsSource = null;
            }
        }

        private void UpdateDiagnosisListViews()
        {
            if (selectedDiagnosis != null && selectedDiagnosis.Count > 0)
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
        public void PrintButton_Click(Patient patient, List<Complaint> complaints, List<history> histories, List<Examination> examinations, List<Investigation> investigations, List<Diagnosis> diagnoses, List<Treatment> treatments, List<DummyMedicine> medicines, List<Advice> advices, List<FollowUp> followUps, List<SpecialNote> specialNotes)
        {
            newPatient = patient;
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
    }


}
