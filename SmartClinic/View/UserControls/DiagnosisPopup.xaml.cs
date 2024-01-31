using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace SmartClinic.View.UserControls
{
    /// <summary>
    /// Interaction logic for DiagnosisPopup.xaml
    /// </summary>
    public partial class DiagnosisPopup : UserControl
    {
        public ObservableCollection<string> CommonlyUsedDiagnoses { get; set; }
        public ObservableCollection<string> SelectedDiagnoses { get; set; }

        public DiagnosisPopup()
        {
            InitializeComponent();
            DataContext = this; // Set DataContext to enable data binding

            // Dummy data for commonly used diagnoses
            CommonlyUsedDiagnoses = new ObservableCollection<string>
            {
                "Diagnosis A", "Diagnosis B", "Diagnosis C", "Diagnosis D", "Diagnosis E"
            };

            SelectedDiagnoses = new ObservableCollection<string>();
        }

        private void AddToSelectedDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            Button addButton = (Button)sender;
            string selectedDiagnosis = addButton.DataContext as string;

            if (!SelectedDiagnoses.Contains(selectedDiagnosis))
                SelectedDiagnoses.Add(selectedDiagnosis);
        }

        private void RemoveFromSelectedDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            Button removeButton = (Button)sender;
            string selectedDiagnosis = removeButton.DataContext as string;

            SelectedDiagnoses.Remove(selectedDiagnosis);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Handle OK button click, close the popup, and update the main UI
            // For now, let's just close the popup
            ClosePopup();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Handle Cancel button click, close the popup without updating the main UI
            // For now, let's just close the popup
            ClosePopup();
        }

        private void ClosePopup()
        {
            // Close the popup
            var parentPopup = this.Parent as Popup;
            if (parentPopup != null)
            {
                parentPopup.IsOpen = false;
            }
        }

        private void addCreate_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Handle add/create button click
        }
    }
}
