using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace SmartClinic.View.UserControls
{
    /// <summary>
    /// Interaction logic for ChiefComplaintPopup.xaml
    /// </summary>
    public partial class ChiefComplaintPopup : UserControl
    {
        public ObservableCollection<string> CommonlyUsedComplaints { get; set; }
        public ObservableCollection<string> SelectedComplaints { get; set; }

        public ChiefComplaintPopup()
        {
            InitializeComponent();
            DataContext = this; // Set DataContext to enable data binding

            // Dummy data for commonly used complaints
            CommonlyUsedComplaints = new ObservableCollection<string>
            {
                "Complaint A", "Complaint B", "Complaint C", "Complaint D", "Complaint E"
            };

            SelectedComplaints = new ObservableCollection<string>();
        }

        private void AddToSelectedComplaints_Click(object sender, RoutedEventArgs e)
        {
            Button addButton = (Button)sender;
            string selectedComplaint = addButton.DataContext as string;

            if (!SelectedComplaints.Contains(selectedComplaint))
                SelectedComplaints.Add(selectedComplaint);
        }

        private void RemoveFromSelectedComplaints_Click(object sender, RoutedEventArgs e)
        {
            Button removeButton = (Button)sender;
            string selectedComplaint = removeButton.DataContext as string;

            SelectedComplaints.Remove(selectedComplaint);
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
