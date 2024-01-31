using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace SmartClinic.View.UserControls
{
    /// <summary>
    /// Interaction logic for OnExaminationPopup.xaml
    /// </summary>
    public partial class OnExaminationPopup : UserControl
    {
        public ObservableCollection<string> CommonlyUsedExaminations { get; set; }
        public ObservableCollection<string> SelectedExaminations { get; set; }

        public OnExaminationPopup()
        {
            InitializeComponent();
            DataContext = this; // Set DataContext to enable data binding

            // Dummy data for commonly used examinations
            CommonlyUsedExaminations = new ObservableCollection<string>
            {
                "Examination A", "Examination B", "Examination C", "Examination D", "Examination E"
            };

            SelectedExaminations = new ObservableCollection<string>();
        }

        private void AddToSelectedExaminations_Click(object sender, RoutedEventArgs e)
        {
            Button addButton = (Button)sender;
            string selectedExamination = addButton.DataContext as string;

            if (!SelectedExaminations.Contains(selectedExamination))
                SelectedExaminations.Add(selectedExamination);
        }

        private void RemoveFromSelectedExaminations_Click(object sender, RoutedEventArgs e)
        {
            Button removeButton = (Button)sender;
            string selectedExamination = removeButton.DataContext as string;

            SelectedExaminations.Remove(selectedExamination);
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
