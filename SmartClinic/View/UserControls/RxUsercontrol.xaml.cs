using System;
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


using static SmartClinic.Patient;

namespace SmartClinic.View.UserControls
{
    /// <summary>
    /// Interaction logic for RxUsercontrol.xaml
    /// </summary>
    public partial class RxUsercontrol : UserControl
    {
        //from Patientinfo
        private bool isPatientAdded = false;

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
        private List<Medicine> selectedMedicines = new List<Medicine>();
        private List<Advice> selectedAdvices = new List<Advice>();
        private List<FollowUp> selectedFollowUps = new List<FollowUp>();
        private List<SpecialNote> selectedSpecialNotes = new List<SpecialNote>();
        public List<Medicine> SelectedMedicines => selectedMedicines;
        public List<Advice> SelectedAdvices => selectedAdvices;
        public List<FollowUp> SelectedFollowUps => selectedFollowUps;
        public List<SpecialNote> SelectedSpecialNotes => selectedSpecialNotes;



        public RxUsercontrol()
        {
            InitializeComponent();
            docname.Content = variables.docname;
            docdegree.Content = variables.docdegree;
            docname_bangla.Content = variables.docname_bangla;
            docdegree_bangla.Content = variables.docdegree_bangla;
        }


        //patientInfo cs
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
            UpdateUIWithPatientDetails(patientName, patientAge);
        }
        private void UpdateUIWithPatientDetails(string patientName, string patientAge)
        {
            if (!isPatientAdded)
            {
                // Remove the "+ Add Patient" button from the StackPanel
                addPatientPanel.Children.Remove(AddPatientButton);

                // Create TextBlocks for patient name and age
                TextBlock nameLabel = new TextBlock
                {
                    Text = patientName,
                    FontWeight = FontWeights.Bold,
                    VerticalAlignment = VerticalAlignment.Center
                };
                age.Text = patientAge;

                // Add the TextBlocks to the StackPanel
                addPatientPanel.Children.Add(nameLabel);

                isPatientAdded = true;
            }
        }
        private void OnSearchTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (searchPatientTextBox.Text == "Search Patient")
            {
                searchPatientTextBox.Text = "";
                searchPatientTextBox.Foreground = Brushes.Black;
            }
        }
        private void OnSearchTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchPatientTextBox.Text))
            {
                searchPatientTextBox.Text = "Search Patient";
                searchPatientTextBox.Foreground = Brushes.Gray;
            }
        }


        //history cs
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
                EditComplaintControl editControl = new EditComplaintControl();

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
            selectedMedicines.Add(newMedicine);

            // Update the selectedMedicinesListView
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

        private void UpdateSelectedMedicinesListView()
        {
            selectedMedicinesListView.ItemsSource = null;
            selectedMedicinesListView.ItemsSource = selectedMedicines;
        }


        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button removeButton)
            {
                if (removeButton.DataContext is Medicine selectedMedicine)
                {
                    // Remove the selected medicine from the collection
                    selectedMedicines.Remove(selectedMedicine);

                    // Update the selectedMedicinesListView
                    UpdateSelectedMedicinesListView();
                }
                else if (removeButton.DataContext is Advice selectedAdvice)
                {
                    selectedAdvices.Remove(selectedAdvice);
                }

                else if (removeButton.DataContext is FollowUp selectedFollowUp)
                {
                    selectedFollowUps.Remove(selectedFollowUp);
                }

                else if (removeButton.DataContext is SpecialNote selectedNote)
                {
                    selectedSpecialNotes.Remove(selectedNote);
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


        private void SpecialNotes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FollowUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PatientSearchStringChanged(object sender, TextChangedEventArgs e)
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
