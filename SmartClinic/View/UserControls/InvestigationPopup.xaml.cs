using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace SmartClinic.View.UserControls
{
    /// <summary>
    /// Interaction logic for InvestigationPopup.xaml
    /// </summary>
    public partial class InvestigationPopup : UserControl
    {
        public ObservableCollection<string> CommonlyUsedInvestigations { get; set; }
        public ObservableCollection<string> SelectedInvestigations { get; set; }

        public InvestigationPopup()
        {
            InitializeComponent();
            DataContext = this; // Set DataContext to enable data binding

            // Dummy data for commonly used investigations
            CommonlyUsedInvestigations = new ObservableCollection<string>
            {
                "Investigation A", "Investigation B", "Investigation C", "Investigation D", "Investigation E"
            };

            SelectedInvestigations = new ObservableCollection<string>();
        }

        private void AddToSelectedInvestigations_Click(object sender, RoutedEventArgs e)
        {
            Button addButton = (Button)sender;
            string selectedInvestigation = addButton.DataContext as string;

            if (!SelectedInvestigations.Contains(selectedInvestigation))
                SelectedInvestigations.Add(selectedInvestigation);
        }

        private void RemoveFromSelectedInvestigations_Click(object sender, RoutedEventArgs e)
        {
            Button removeButton = (Button)sender;
            string selectedInvestigation = removeButton.DataContext as string;

            SelectedInvestigations.Remove(selectedInvestigation);
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
