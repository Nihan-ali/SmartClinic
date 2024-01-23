using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace SmartClinic.View.UserControls
{
    /// <summary>
    /// Interaction logic for TreatmentPlanPopup.xaml
    /// </summary>
    public partial class TreatmentPlanPopup : UserControl
    {
        public ObservableCollection<string> CommonlyUsedPlans { get; set; }
        public ObservableCollection<string> SelectedPlans { get; set; }

        public TreatmentPlanPopup()
        {
            InitializeComponent();
            DataContext = this; // Set DataContext to enable data binding

            // Dummy data for commonly used plans
            CommonlyUsedPlans = new ObservableCollection<string>
            {
                "Plan A", "Plan B", "Plan C", "Plan D", "Plan E"
            };

            SelectedPlans = new ObservableCollection<string>();
        }

        private void AddToSelectedPlans_Click(object sender, RoutedEventArgs e)
        {
            Button addButton = (Button)sender;
            string selectedPlan = addButton.DataContext as string;

            if (!SelectedPlans.Contains(selectedPlan))
                SelectedPlans.Add(selectedPlan);
        }

        private void RemoveFromSelectedPlans_Click(object sender, RoutedEventArgs e)
        {
            Button removeButton = (Button)sender;
            string selectedPlan = removeButton.DataContext as string;

            SelectedPlans.Remove(selectedPlan);
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

        }
    }
}