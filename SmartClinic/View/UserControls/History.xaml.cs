using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartClinic.View.UserControls
{
    public partial class History : UserControl
    {
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


        public History()
        {
            InitializeComponent();
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

 

        

    }
}
