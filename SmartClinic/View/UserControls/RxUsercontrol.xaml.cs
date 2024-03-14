﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using System.Printing;
using static SmartClinic.Patient;
using Org.BouncyCastle.Asn1.Crmf;
using iTextSharp.text.pdf;

namespace SmartClinic.View.UserControls
{
    /// <summary>
    /// Interaction logic for RxUsercontrol.xaml
    /// </summary>
    public partial class RxUsercontrol : UserControl
    {
        //variable initialization
        //from Patientinfo
        private bool isPatientAdded = false;
        private Patient newPatient;
        private PatientVisit selectedPatientVisit;
        //from history
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
        public event EventHandler<PatientEventArgs> PrescriptionDataAvailable;




        public RxUsercontrol()
        {
            InitializeComponent();
            updatedocordetails();
            todaydate.Text = DateTime.Now.ToString("dd-MM-yyyy");

            // Subscribe to the Loaded event of the Popup
            //searchResultsPopup.Loaded += SearchResultsPopup_Loaded;
            //PrescriptionSearchResultsPopup.Loaded += PrescriptionSearchResultsPopup_Loaded;
        }
        public RxUsercontrol(Patient newPatient) : this()
        {
            this.newPatient = newPatient;
            todaydate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            updatedocordetails();
            UpdatePatientInfo(newPatient);
            OnPrescriptionDataAvailable(new PatientEventArgs { NewPatient = newPatient });
        }
        public RxUsercontrol(Patient newPatient, PatientVisit selectedPatientVisit) : this()
        {
            UpdateUI(newPatient, selectedPatientVisit);
            updatedocordetails();
        }
        private void updatedocordetails()
        {
            List<DoctorInfo> doctorInfos = DatabaseHelper.GetDoctorInfos();
            docname.Content = doctorInfos[0].docname;
            docdegree.Content = doctorInfos[0].docdegree;
            docname_bangla.Content = doctorInfos[0].docname_bangla;
            docdegree_bangla.Content = doctorInfos[0].docdegree_bangla;
            DoctorsName.Content = doctorInfos[0].docname;
            Mobile.Content = doctorInfos[0].chamber_phone;
        }

        private void UpdateUI(Patient newPatient, PatientVisit selectedPatientVisit)
        {
            this.newPatient = newPatient;
            todaydate.Text = selectedPatientVisit.visit.ToString("dd-MM-yyyy");
            selectedComplaints = DatabaseHelper.ExtractComplaint(selectedPatientVisit.complaint);
            selectedHistories = DatabaseHelper.ExtractHistory(selectedPatientVisit.hhistory);
            selectedExaminations = DatabaseHelper.ExtractExamination(selectedPatientVisit.onExamination);
            selectedInvestigations = DatabaseHelper.ExtractInvestigation(selectedPatientVisit.investigation);
            selectedDiagnosis = DatabaseHelper.ExtractDiagnosis(selectedPatientVisit.diagnosis);
            selectedTreatments = DatabaseHelper.ExtractTreatment(selectedPatientVisit.treatmentPlan);
            selectedMedicines = DatabaseHelper.ExtractMedicine(selectedPatientVisit.medicine);
            selectedAdvices = DatabaseHelper.ExtractAdvice(selectedPatientVisit.advice);
            selectedFollowUps = DatabaseHelper.ExtractFollowUp(selectedPatientVisit.followUp);
            selectedSpecialNotes = DatabaseHelper.ExtractSpecialNotes(selectedPatientVisit.notes);
            UpdatePatientInfo(newPatient);
            UpdateSelectedComplaintListView();
            UpdateSelectedHistoryListView();
            UpdateSelectedExaminationListView();
            UpdateSelectedInvestigationListView();
            UpdateSelectedDiagnosisListView();
            UpdateSelectedTreatmentListView();
            UpdateSelectedMedicinesListView();
            UpdateSelectedAdvicesListView();
            UpdateSelectedFollowUpsListView();
            UpdateSelectedSpecialNotesListView();
        }

        private void RefreshWholeWindow_Click(object sender, RoutedEventArgs e)
        {
            AddPatientButton.Content = "+ Add Patient";
            AddPatientButton.Foreground = Brushes.Blue;
            age.Text = "";
            selectedComplaintsListView.ItemsSource = null;
            historyListView.ItemsSource = null;
            examinationListView.ItemsSource = null;
            selectedInvestigationsListView.ItemsSource = null;
            selectedDiagnosisListView.ItemsSource = null;
            selectedTreatmentListView.ItemsSource = null;
            selectedMedicinesListView.ItemsSource = null;
            selectedAdvicesListView.ItemsSource = null;
            selectedFollowUpListView.ItemsSource = null;
            selectedSpecialNoteListView.ItemsSource = null;
            searchPatientTextBox.Text = "Search Patient";
            searchPatientTextBox.Foreground = Brushes.Gray;
            isPatientAdded = false;
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (menubarbox.Text == "Search Prescription")
            {
                menubarbox.Text = "";
                menubarbox.Opacity = 1;
                menubarbox.Foreground = Brushes.Black;
            }
        }
        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            menubarbox.Text = "Search Prescription";
            menubarbox.Opacity = 0.7;
            menubarbox.Foreground = Brushes.Gray;

        }

        private void PatientTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (searchPatientTextBox.Text == "Search Patient")
            {
                searchPatientTextBox.Text = "";
                searchPatientTextBox.Foreground = Brushes.Black;
            }
        }
        private void PatientTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            searchPatientTextBox.Text = "Search Patient";
            searchPatientTextBox.Opacity = 0.7;
            searchPatientTextBox.Foreground = Brushes.Gray;
        }

        private void searchResultsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                if (listBox.SelectedItem is Patient selectedPatient)
                {
                    this.newPatient = selectedPatient;
                    RefreshWholeWindow_Click(null, null);
                    UpdatePatientInfo(selectedPatient);
                    OnPrescriptionDataAvailable(new PatientEventArgs { NewPatient = selectedPatient });
                }
            }
        }
        private void PatientSearchStringChanged(object sender, TextChangedEventArgs e)
        {
            string searchterm = searchPatientTextBox.Text;
            List<Patient> searchresults = DatabaseHelper.SearchPatients(searchterm);
            if (searchterm != "Search Patient" && searchterm != "")
            {
                if (searchresults.Count > 0)
                {
                    // Handle the search results, e.g., update ListBox or other UI elements
                    searchResultsListBox.ItemsSource = searchresults;
                    searchResultsPopup.IsOpen = true;
                }
                else
                {
                    // Close the popup if there are no search results
                    searchResultsPopup.IsOpen = false;
                }
            }

        }
        private void searchResultsPopup_Opened(object sender, EventArgs e)
        {
            string searchterm = searchPatientTextBox.Text;
            List<Patient> searchresults = DatabaseHelper.SearchPatients(searchterm);

            if (searchresults.Count > 0)
            {
                // Handle the search results, e.g., update ListBox or other UI elements

                searchResultsListBox.ItemsSource = searchresults;
                searchResultsPopup.IsOpen = true;
            }
            else
            {
                // Close the popup if there are no search results
                searchResultsPopup.IsOpen = false;
            }
        }

        private void PrescriptionSearchStringChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = menubarbox.Text;

            // Check if the searchText is not empty or null before attempting to parse
            if (!string.IsNullOrEmpty(searchText))
            {
                if (searchText == "developers")
                {
                    Developer devs = new Developer();
                    devs.ShowDialog();
                }
                else if (Int64.TryParse(searchText, out Int64 prescriptionId))
                {

                    List<PatientVisit> filteredPrescriptions = DatabaseHelper.SearchPrescriptionByPrescriptionId(prescriptionId);
                    if (filteredPrescriptions.Count > 0)
                    {
                        PrescriptionSearchResultsListBox.ItemsSource = filteredPrescriptions;
                        PrescriptionSearchResultsPopup.IsOpen = true; // Show the popup
                    }
                    else
                    {
                        PrescriptionSearchResultsPopup.IsOpen = false; // Hide the popup if search text is empty
                    }
                }
                else
                {
                    // Handle the case where the searchText is not a valid long
                    // You might want to show a message to the user indicating that the input is not a valid prescription ID
                }
            }
            else
            {
                PrescriptionSearchResultsPopup.IsOpen = false; // Hide the popup if search text is empty
            }
        }
        private void PrescriptionSearchResultsPopup_Opened(object sender, EventArgs e)
        {
            // Ensure that PrescriptionSearchResultsListBox is found only when PrescriptionSearchResultsPopup is fully loaded
            if (PrescriptionSearchResultsPopup.IsLoaded)
            {
                ListBox PrescriptionSearchResultsListBox = FindChild<ListBox>(PrescriptionSearchResultsPopup.Child, "PrescriptionSearchResultsListBox");

                if (PrescriptionSearchResultsListBox != null)
                {
                    //PrescriptionSearchResultsListBox.SelectionChanged += PrescriptionSearchResultsListBox_SelectionChanged;

                    string searchText = menubarbox.Text;
                    List<PatientVisit> filteredPrescriptions = DatabaseHelper.SearchPrescriptionByPrescriptionId(Int64.Parse(searchText));
                    if (filteredPrescriptions.Count > 0)
                    {
                        //PrescriptionSearchResultsListBox.ItemsSource = null;
                        PrescriptionSearchResultsListBox.ItemsSource = filteredPrescriptions;
                        PrescriptionSearchResultsPopup.IsOpen = true; // Show the popup
                    }
                    else
                    {
                        PrescriptionSearchResultsPopup.IsOpen = false; // Hide the popup if search text is empty
                    }
                }
                else
                {
                    // Handle case when PrescriptionSearchResultsListBox is not found
                    MessageBox.Show("PrescriptionSearchResultsListBox not found!");
                }
            }
            else
            {
                // Handle case when PrescriptionSearchResultsPopup is not fully loaded
                MessageBox.Show("PrescriptionSearchResultsPopup is not fully loaded!");
            }
        }
        private void PrescriptionSearchResultsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                if (listBox.SelectedItem is PatientVisit selectedPatientVisit)
                {
                    this.selectedPatientVisit = selectedPatientVisit;
                    this.newPatient = DatabaseHelper.GetPatientById(selectedPatientVisit.Id);
                    PrescriptionSearchResultsPopup.IsOpen = false;
                    UpdateUI(newPatient, selectedPatientVisit);
                    textBox_LostFocus(null, null);
                }
            }
        }


        private void OnPrescriptionDataAvailable(PatientEventArgs e)
        {
            // Raise the event if there are subscribers
            PrescriptionDataAvailable?.Invoke(this, e);
        }

        private T FindChild<T>(DependencyObject parent, string name) where T : DependencyObject
        {
            if (parent == null) return null;
            if (parent is T element && (element as FrameworkElement)?.Name == name) return element;

            T result = null;
            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                result = FindChild<T>(child, name);
                if (result != null) break;
            }
            return result;
        }
        public void UpdatePatientInfo(Patient patient)
        {
            if (patient != null)
            {
                AddPatientButton.Content = patient.Name;
                AddPatientButton.Foreground = Brushes.Black;
                age.Text = patient.Age;
                PatientId.Text = patient.Id.ToString();
                searchPatientTextBox.Text = "";
                searchResultsPopup.IsOpen = false;
            }
            PatientTextBoxLostFocus(null, null);
        }

        private void AddPatientButton_Click(object sender, RoutedEventArgs e)
        {
            AddPatient addPatientWindow = new AddPatient();
            addPatientWindow.PatientInfoSubmitted += AddPatientWindow_PatientInfoSubmitted;
            addPatientWindow.ShowDialog();
        }
        private void AddPatientWindow_PatientInfoSubmitted(object sender, PatientEventArgs e)
        {

            string patientName = e.NewPatient.Name;
            string patientAge = e.NewPatient.Age;
            this.newPatient = e.NewPatient;
            UpdatePatientInfo(e.NewPatient);
        }


        private void AddComplaint_Click(object sender, RoutedEventArgs e)
        {
            // Uncomment this block if you want to use it
            ComplaintSearchWindow searchWindow = new ComplaintSearchWindow();
            searchWindow.ShowDialog();

            foreach (var selectedComplaint in searchWindow.SelectedComplaints)
            {
                AddToSelectedComplaints(selectedComplaint);
            }
        }
        private void AddHistory_Click(object sender, RoutedEventArgs e)
        {
            // Uncomment this block if you want to use it
            HistorySearchWindow searchWindow = new HistorySearchWindow();
            searchWindow.ShowDialog();

            foreach (var selectedHistory in searchWindow.SelectedHistoryItems)
            {
                AddToSelectedHistories(selectedHistory);
            }
        }
        private void AddExamination_Click(object sender, RoutedEventArgs e)
        {
            // Uncomment this block if you want to use it
            ExaminationSearchWindow searchWindow = new ExaminationSearchWindow();
            searchWindow.ShowDialog();

            foreach (var selectedExamination in searchWindow.SelectedExaminations)
            {
                AddToSelectedExaminations(selectedExamination);
            }
        }
        private void AddInvestigation_Click(object sender, RoutedEventArgs e)
        {
            // Uncomment this block if you want to use it
            InvestigationSearchWindow searchWindow = new InvestigationSearchWindow();
            searchWindow.ShowDialog();

            foreach (var selectedInvestigation in searchWindow.SelectedInvestigations)
            {
                AddToSelectedInvestigations(selectedInvestigation);
            }
        }
        private void AddDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            // Uncomment this block if you want to use it
            DiagnosisSearchWindow searchWindow = new DiagnosisSearchWindow();
            searchWindow.ShowDialog();

            foreach (var selectedDiagnosis in searchWindow.SelectedDiagnoses)
            {
                AddToSelectedDiagnosis(selectedDiagnosis);
            }
        }
        private void AddTreatment_Click(object sender, RoutedEventArgs e)
        {
            // Uncomment this block if you want to use it
            TreatmentSearchWindow searchWindow = new TreatmentSearchWindow();
            searchWindow.ShowDialog();

            foreach (var selectedTreatment in searchWindow.SelectedTreatments)
            {
                AddToSelectedTreatments(selectedTreatment);
            }
        }

        public void AddToSelectedComplaints(Complaint newComplaint)
        {
            selectedComplaints.Add(newComplaint);
            UpdateSelectedComplaintListView();
        }
        public void AddToSelectedHistories(history newHistory)
        {
            selectedHistories.Add(newHistory);
            UpdateSelectedHistoryListView();
        }
        public void AddToSelectedExaminations(Examination newExamination)
        {
            selectedExaminations.Add(newExamination);
            UpdateSelectedExaminationListView();
        }
        public void AddToSelectedInvestigations(Investigation newInvestigation)
        {
            // Uncomment this block if you want to use it
            selectedInvestigations.Add(newInvestigation);
            UpdateSelectedInvestigationListView();
        }
        public void AddToSelectedDiagnosis(Diagnosis newDiagnosis)
        {
            selectedDiagnosis.Add(newDiagnosis);
            UpdateSelectedDiagnosisListView();
        }
        public void AddToSelectedTreatments(Treatment newTreatment)
        {
            selectedTreatments.Add(newTreatment);
            UpdateSelectedTreatmentListView();
        }

        private void UpdateSelectedComplaintListView()
        {
            selectedComplaintsListView.ItemsSource = null;
            selectedComplaintsListView.ItemsSource = selectedComplaints;
        }
        private void UpdateSelectedHistoryListView()
        {
            historyListView.ItemsSource = null;
            historyListView.ItemsSource = selectedHistories;
        }
        private void UpdateSelectedExaminationListView()
        {
            examinationListView.ItemsSource = null;
            examinationListView.ItemsSource = selectedExaminations;
        }
        private void UpdateSelectedInvestigationListView()
        {
            selectedInvestigationsListView.ItemsSource = null;
            selectedInvestigationsListView.ItemsSource = selectedInvestigations;
        }
        private void UpdateSelectedDiagnosisListView()
        {
            selectedDiagnosisListView.ItemsSource = null;
            selectedDiagnosisListView.ItemsSource = selectedDiagnosis;
        }
        private void UpdateSelectedTreatmentListView()
        {
            selectedTreatmentListView.ItemsSource = null;
            selectedTreatmentListView.ItemsSource = selectedTreatments;
        }





        private void RemoveComplaint_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button deleteButton && deleteButton.DataContext is Complaint selectedItem)
            {
                // Assuming your data context is set to a List<Complaint>
                if (selectedComplaints is List<Complaint> complaintList)
                {
                    // Remove the selected item from the list
                    complaintList.Remove(selectedItem);

                    // Update the ListView after removing the item
                    UpdateSelectedComplaintListView();
                }
            }
        }
        private void RemoveHistory_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button deleteButton && deleteButton.DataContext is history selectedItem)
            {
                // Assuming your data context is set to a List<History>
                if (selectedHistories is List<history> historyList)
                {
                    // Remove the selected item from the list
                    historyList.Remove(selectedItem);

                    // Update the ListView after removing the item
                    UpdateSelectedHistoryListView();
                }
            }
        }
        private void RemoveExamination_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button deleteButton && deleteButton.DataContext is Examination selectedItem)
            {
                // Assuming your data context is set to a List<Examination>
                if (selectedExaminations is List<Examination> examinationList)
                {
                    // Remove the selected item from the list
                    examinationList.Remove(selectedItem);

                    // Update the ListView after removing the item
                    UpdateSelectedExaminationListView();
                }
            }
        }
        private void RemoveInvestigation_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button deleteButton && deleteButton.DataContext is Investigation selectedItem)
            {
                if (selectedInvestigations is List<Investigation> investigationList)
                {
                    investigationList.Remove(selectedItem);
                    UpdateSelectedInvestigationListView();
                }
            }
        }
        private void RemoveDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button deleteButton && deleteButton.DataContext is Diagnosis selectedItem)
            {
                if (selectedDiagnosis is List<Diagnosis> diagnosisList)
                {
                    diagnosisList.Remove(selectedItem);
                    UpdateSelectedDiagnosisListView();
                }
            }
        }
        private void RemoveTreatment_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button deleteButton && deleteButton.DataContext is Treatment selectedItem)
            {
                if (selectedTreatments is List<Treatment> treatmentList)
                {
                    treatmentList.Remove(selectedItem);
                    UpdateSelectedTreatmentListView();
                }
            }
        }

        private void TextBlock_MouseLeftButtonDownComplaint(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock && textBlock.DataContext is Complaint complaintData)
            {
                EditMainComplaintControl editControl = new EditMainComplaintControl();

                Point position = PointToScreen(e.GetPosition(this));
                editControl.Left = position.X;
                editControl.Top = position.Y;

                editControl.DataContext = complaintData;

                editControl.ShowDialog();
            }
        }
        private void TextBlock_MouseLeftButtonDownHistory(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock && textBlock.DataContext is history historyData)
            {
                EditComplaintControl editControl = new EditComplaintControl();

                Point position = PointToScreen(e.GetPosition(this));
                editControl.Left = position.X;
                editControl.Top = position.Y;

                editControl.DataContext = historyData;

                editControl.ShowDialog();
            }
        }
        private void TextBlock_MouseLeftButtonDownExamination(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock && textBlock.DataContext is Examination examinationData)
            {
                EditComplaintControl editControl = new EditComplaintControl();

                Point position = PointToScreen(e.GetPosition(this));
                editControl.Left = position.X;
                editControl.Top = position.Y;

                editControl.DataContext = examinationData;

                editControl.ShowDialog();
            }
        }
        private void TextBlock_MouseLeftButtonDownInvestigation(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock && textBlock.DataContext is Investigation investigationData)
            {
                EditComplaintControl editControl = new EditComplaintControl();

                Point position = PointToScreen(e.GetPosition(this));
                editControl.Left = position.X;
                editControl.Top = position.Y;

                editControl.DataContext = investigationData;

                editControl.ShowDialog();
            }
        }
        private void TextBlock_MouseLeftButtonDownDiagnosis(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock && textBlock.DataContext is Diagnosis diagnosisData)
            {
                EditComplaintControl editControl = new EditComplaintControl();

                Point position = PointToScreen(e.GetPosition(this));
                editControl.Left = position.X;
                editControl.Top = position.Y;

                editControl.DataContext = diagnosisData;

                editControl.ShowDialog();
            }
        }
        private void TextBlock_MouseLeftButtonDownTreatment(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock && textBlock.DataContext is Treatment treatmentData)
            {
                EditComplaintControl editControl = new EditComplaintControl();

                Point position = PointToScreen(e.GetPosition(this));
                editControl.Left = position.X;
                editControl.Top = position.Y;

                editControl.DataContext = treatmentData;

                editControl.ShowDialog();
            }
        }


        //medicine cs
        public void AddToSelectedMedicines(Medicine newMedicine)
        {
            // Add the new medicine to the selectedMedicines collection
            DummyMedicine dummy = new DummyMedicine();
            dummy.MedicineName = newMedicine.MedicineName;
            dummy.formatedDose = newMedicine.formatedDose;
            dummy.MakeNote = newMedicine.MakeNote;
            selectedMedicines.Add(dummy);

            UpdateSelectedMedicinesListView();
        }
        public void AddToSelectedAdvices(Advice newAdvice)
        {
            selectedAdvices.Add(newAdvice);
        }
        public void AddToSelectedFollowUps(FollowUp newFollowUp)
        {
            selectedFollowUps.Add(newFollowUp);
        }
        public void AddToSelectedSpecialNotes(SpecialNote newSpecialNote)
        {
            selectedSpecialNotes.Add(newSpecialNote);
        }

        private void UpdateSelectedAdvicesListView()
        {
            selectedAdvicesListView.ItemsSource = null;
            selectedAdvicesListView.ItemsSource = selectedAdvices;
        }
        private void UpdateSelectedFollowUpsListView()
        {
            selectedFollowUpListView.ItemsSource = null;
            selectedFollowUpListView.ItemsSource = selectedFollowUps;
        }
        private void UpdateSelectedSpecialNotesListView()
        {
            selectedSpecialNoteListView.ItemsSource = null;
            selectedSpecialNoteListView.ItemsSource = selectedSpecialNotes;
        }
        private void UpdateSelectedMedicinesListView()
        {
            selectedMedicinesListView.ItemsSource = null;
            selectedMedicinesListView.ItemsSource = selectedMedicines;
        }


        private void Rx_Click(object sender, RoutedEventArgs e)
        {
            MedicineSearchWindow searchWindow = new MedicineSearchWindow();
            searchWindow.ShowDialog();

            // Handle the selected medicine from the search window as needed
            foreach (var selectedMedicine in searchWindow.SelectedMedicines)
            {
                AddToSelectedMedicines(selectedMedicine);
            }
        }
        private void Advices_Click(object sender, RoutedEventArgs e)
        {
            AdviceSearchWindow searchWindow = new AdviceSearchWindow();
            searchWindow.ShowDialog();

            // Handle the selected advices from the search window as needed
            foreach (var selectedAdvice in searchWindow.SelectedAdvices)
            {
                AddToSelectedAdvices(selectedAdvice);
            }
            UpdateSelectedAdvicesListView();
        }
        private void AddFollowUp_Click(object sender, RoutedEventArgs e)
        {
            FollowUpSearchWindow followUpWindow = new FollowUpSearchWindow();
            followUpWindow.ShowDialog();
            foreach (var selectedFollowUp in followUpWindow.SelectedFollowUps)
            {
                AddToSelectedFollowUps(selectedFollowUp);
            }
            UpdateSelectedFollowUpsListView();

        }
        private void AddSpecialNote_Click(object sender, RoutedEventArgs e)
        {
            SpecialNoteSearchWindow notewindow = new SpecialNoteSearchWindow();
            notewindow.ShowDialog();
            foreach (var selectedNote in notewindow.SelectedSpecialNotes)
            {
                AddToSelectedSpecialNotes(selectedNote);
            }
            UpdateSelectedSpecialNotesListView();
        }



        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button removeButton)
            {
                if (removeButton.DataContext is DummyMedicine selectedMedicine)
                {
                    // Remove the selected medicine from the collection
                    selectedMedicines.Remove(selectedMedicine);

                    // Update the selectedMedicinesListView
                    UpdateSelectedMedicinesListView();
                }
                else if (removeButton.DataContext is Advice selectedAdvice)
                {
                    selectedAdvices.Remove(selectedAdvice);
                    UpdateSelectedAdvicesListView();
                }

                else if (removeButton.DataContext is FollowUp selectedFollowUp)
                {
                    selectedFollowUps.Remove(selectedFollowUp);
                    UpdateSelectedFollowUpsListView();
                }

                else if (removeButton.DataContext is SpecialNote selectedNote)
                {
                    selectedSpecialNotes.Remove(selectedNote);
                    UpdateSelectedSpecialNotesListView();
                }
            }
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


        private void Translate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMedicinesListView != null)
            {
                for (int i = 0; i < selectedMedicines.Count; i++)
                {
                    string note = selectedMedicines[i].MakeNote;
                    //MessageBox.Show(note);
                    string translatedNote = DatabaseHelper.Translate(note);
                    selectedMedicines[i].MakeNote = translatedNote;
                }
                UpdateSelectedMedicinesListView();
            }
            MessageBox.Show(leftgrid.ActualHeight.ToString());
            MessageBox.Show(rightgrid.ActualHeight.ToString());

        }

        private void SavePrescription_Click(object sender, RoutedEventArgs e)
        {
            int id = newPatient.Id;
            string name = newPatient.Name;
            string combinedComplaint = string.Join("$$", selectedComplaints.Select(c => $"{c.Content}@{c.PeriodTime}@{c.SelectedComboBoxItemContent}@{c.Note}"));
            string combinedHistory = string.Join("$$", selectedHistories.Select(h => $"{h.Content}@{h.Note}"));
            string combinedExamination = string.Join("$$", selectedExaminations.Select(e => $"{e.Content}@{e.Note}"));
            string combinedInvestigation = string.Join("$$", selectedInvestigations.Select(i => $"{i.Content}@{i.Note}"));
            string combinedDiagnosis = string.Join("$$", selectedDiagnosis.Select(d => $"{d.Content}@{d.Note}"));
            string combinedTreatment = string.Join("$$", selectedTreatments.Select(t => $"{t.Content}@{t.Note}"));
            string combinedMedicine = string.Join("$$", selectedMedicines.Select(m => $"{m.MedicineName}@{m.formatedDose}&{m.MakeNote}"));
            string combinedAdvice = string.Join("$$", selectedAdvices.Select(a => $"{a.Content}"));
            string combinedFollowUp = string.Join("$$", selectedFollowUps.Select(f => $"{f.Content}"));
            string combinedSpecialNote = string.Join("$$", selectedSpecialNotes.Select(s => $"{s.Content}"));
            string prescriptionId = id.ToString() + DateTime.Today.ToString("ddMMyyyy");


            PatientVisit newpres = new PatientVisit
            {
                Id = id,
                visit = DateTime.Now,
                Name = name,
                prescriptionId = Int64.Parse(prescriptionId),
                complaint = combinedComplaint,
                hhistory = combinedHistory,
                onExamination = combinedExamination,
                investigation = combinedInvestigation,
                diagnosis = combinedDiagnosis,
                treatmentPlan = combinedTreatment,
                medicine = combinedMedicine,
                advice = combinedAdvice,
                followUp = combinedFollowUp,
                notes = combinedSpecialNote
            };
            ;
            if (DatabaseHelper.SavePrescription(newpres))
            {
                MessageBox.Show("Prescription Stored Successfully");
                todaydate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
            else
            {
                MessageBox.Show("Prescription Alreadys Exists");
            }
        }

        private Task PrintPage(Patient pat, string date, List<Complaint> complaints, List<history> histories, List<Examination> examinations, List<Investigation> investigations, List<Diagnosis> diagnoses, List<Treatment> treatments, List<DummyMedicine> medicines, List<Advice> advices, List<FollowUp> followUps, List<SpecialNote> specialNotes, int offset)
        {
            Printer printDialog = new Printer();
            printDialog.ContentRendered += (s, args) =>
            {
                printDialog.PrintButton_Click(pat, date, complaints, histories, examinations, investigations, diagnoses, treatments, medicines, advices, followUps, specialNotes, false, offset);
                printDialog.Close();
            };
            printDialog.Visibility = Visibility.Hidden;
            printDialog.Show();

            return Task.CompletedTask;
        }


        async private void PrintPrescription_Click(object sender, RoutedEventArgs e)
        {
            SavePrescription_Click(null, null);
            Patient pat = newPatient;
            string date = todaydate.Text;
            List<Complaint> complaints = null;
            List<history> histories = null;
            List<Examination> examinations = null;
            List<Investigation> investigations = null;
            List<Diagnosis> diagnoses = null;
            List<Treatment> treatments = null;
            List<DummyMedicine> medicines = null;
            List<Advice> advices = null;
            List<FollowUp> followUps = null;
            List<SpecialNote> specialNotes = null;
            string missedleft = "";
            string missedright = "";

            int left = 16, right = 16;

            // all complaints taken
            if ((left - selectedComplaints.Count) >= 0)
            {
                complaints = selectedComplaints;
                left = left - 1 - selectedComplaints.Count;
                if ((left - selectedHistories.Count) >= 0)
                {
                    histories = selectedHistories;
                    left = left - 1 - selectedHistories.Count;
                    if ((left - selectedExaminations.Count) >= 0)
                    {
                        examinations = selectedExaminations;
                        left = left - 1 - selectedExaminations.Count;
                        if ((left - selectedInvestigations.Count) >= 0)
                        {
                            investigations = selectedInvestigations;
                            left = left - 1 - selectedInvestigations.Count;
                            if ((left - selectedDiagnosis.Count) >= 0)
                            {
                                diagnoses = selectedDiagnosis;
                                left = left - 1 - selectedDiagnosis.Count;
                                if ((left - selectedTreatments.Count) >= 0)
                                {
                                    treatments = selectedTreatments;
                                    left = left - 1 - selectedTreatments.Count;
                                }
                                else
                                {
                                    treatments = null;
                                    treatments = selectedTreatments.Take(left).ToList();
                                    missedleft = "treatments";
                                }
                            }
                            else
                            {
                                diagnoses = null;
                                diagnoses = selectedDiagnosis.Take(left).ToList();
                                missedleft = "diagnoses";
                            }
                        }
                        else
                        {
                            investigations = null;
                            investigations = selectedInvestigations.Take(left).ToList();
                            missedleft = "investigations";
                        }
                    }
                    else
                    {
                        examinations = null;
                        examinations = selectedExaminations.Take(left).ToList();
                        missedleft = "examinations";
                    }
                }
                else
                {
                    histories = null;
                    histories = selectedHistories.Take(left).ToList();
                    missedleft = "histories";
                }
            }
            else
            {
                complaints = null;
                complaints = selectedComplaints.Take(left).ToList();
                missedleft = "complaints";
            }

            if (right - 1 - (selectedMedicines.Count * 2) >= 0)
            {
                medicines = selectedMedicines;
                right = right - 1 - (selectedMedicines.Count * 2);
                if ((right - 1 - selectedAdvices.Count) >= 0)
                {
                    advices = selectedAdvices;
                    right = right - 1 - selectedAdvices.Count;
                    if ((right - 1 - selectedFollowUps.Count) >= 0)
                    {
                        followUps = selectedFollowUps;
                        right = right - 1 - selectedFollowUps.Count;
                        if ((right - 1 - selectedSpecialNotes.Count) >= 0)
                        {
                            specialNotes = selectedSpecialNotes;
                            right = right - 1 - selectedSpecialNotes.Count;
                        }
                        else
                        {
                            specialNotes = null;
                            specialNotes = selectedSpecialNotes.Take(right).ToList();
                            missedright = "special notes";
                        }
                    }
                    else
                    {
                        followUps = null;
                        followUps = selectedFollowUps.Take(right).ToList();
                        missedright = "follow ups";
                    }
                }
                else
                {
                    advices = null;
                    advices = selectedAdvices.Take(right).ToList();
                    missedright = "advices";
                }
            }
            else
            {
                medicines = null;
                medicines = selectedMedicines.Take(right / 2).ToList();
                missedright = "medicines";
            }

            await PrintPage(pat, date, complaints, histories, examinations, investigations, diagnoses, treatments, medicines, advices, followUps, specialNotes,0);
            int offset = medicines.Count;

            //// 2nd page
            complaints = null;
            histories = null;
            examinations = null;
            investigations = null;
            diagnoses = null;
            treatments = null;
            medicines = null;
            advices = null;
            followUps = null;
            specialNotes = null;

            //take all values from position left
            if (missedleft == "complaints")
            {
                complaints = selectedComplaints.Skip(left).Take(selectedComplaints.Count).ToList();
                histories = selectedHistories;
                examinations = selectedExaminations;
                investigations = selectedInvestigations;
                diagnoses = selectedDiagnosis;
                treatments = selectedTreatments;
            }
            else if (missedleft == "histories")
            {
                complaints = null;
                histories = selectedHistories.Skip(left).Take(selectedHistories.Count).ToList();
                examinations = selectedExaminations;
                investigations = selectedInvestigations;
                diagnoses = selectedDiagnosis;
                treatments = selectedTreatments;
            }
            else if (missedleft == "examinations")
            {
                complaints = null;
                histories = null;
                examinations = selectedExaminations.Skip(left).Take(selectedExaminations.Count).ToList();
                investigations = selectedInvestigations;
                diagnoses = selectedDiagnosis;
                treatments = selectedTreatments;
            }
            else if (missedleft == "investigations")
            {
                complaints = null;
                histories = null;
                examinations = null;
                investigations = selectedInvestigations.Skip(left).Take(selectedInvestigations.Count).ToList();
                diagnoses = selectedDiagnosis;
                treatments = selectedTreatments;
            }
            else if (missedleft == "diagnoses")
            {
                complaints = null;
                histories = null;
                examinations = null;
                investigations = null;
                diagnoses = selectedDiagnosis.Skip(left).Take(selectedDiagnosis.Count).ToList();
                treatments = selectedTreatments;
            }
            else if (missedleft == "treatments")
            {
                complaints = null;
                histories = null;
                examinations = null;
                investigations = null;
                diagnoses = null;
                treatments = selectedTreatments.Skip(left).Take(selectedTreatments.Count).ToList();
            }

            if (missedright == "medicines")
            {
                medicines = selectedMedicines.Skip(right / 2).Take(selectedMedicines.Count).ToList();
                advices = selectedAdvices;
                followUps = selectedFollowUps;
                specialNotes = selectedSpecialNotes;
            }
            else if (missedright == "advices")
            {
                medicines = null;
                advices = selectedAdvices.Skip(right).Take(selectedAdvices.Count).ToList();
                followUps = selectedFollowUps;
                specialNotes = selectedSpecialNotes;
            }
            else if (missedright == "follow ups")
            {
                medicines = null;
                advices = null;
                followUps = selectedFollowUps.Skip(right).Take(selectedFollowUps.Count).ToList();
                specialNotes = selectedSpecialNotes;
            }
            else if (missedright == "special notes")
            {
                medicines = null;
                advices = null;
                followUps = null;
                specialNotes = selectedSpecialNotes.Skip(right).Take(selectedSpecialNotes.Count).ToList();
            }

            if ((complaints != null && complaints.Count > 0) || (histories != null && histories.Count > 0) || (examinations != null && examinations.Count > 0) || (investigations != null && investigations.Count > 0) || (diagnoses != null && diagnoses.Count > 0) || (treatments != null && treatments.Count > 0) || (medicines != null && medicines.Count > 0) || (advices != null && advices.Count > 0) || (followUps != null && followUps.Count > 0) || (specialNotes != null && specialNotes.Count > 0))
            {
                await PrintPage(pat, date, complaints, histories, examinations, investigations, diagnoses, treatments, medicines, advices, followUps, specialNotes, offset);
            }

        }

        private void PrintPreview_Click(object sender, RoutedEventArgs e)
        {
            Printer printDialog = new Printer(newPatient, todaydate.Text, selectedComplaints, selectedHistories, selectedExaminations, selectedInvestigations, selectedDiagnosis, selectedTreatments, selectedMedicines, selectedAdvices, selectedFollowUps, selectedSpecialNotes);
            printDialog.ShowDialog();
        }




    }


    public class IsNullOrEmptyConverter : IValueConverter
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