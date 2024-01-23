using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SmartClinic.View.UserControls
{
    public partial class History : UserControl
    {
        public ObservableCollection<string> ChiefComplaints { get; set; }

        public History()
        {
            InitializeComponent();
            ChiefComplaints = new ObservableCollection<string>();
            DataContext = this;  // Set UserControl's DataContext to itself to allow binding
        }

        private void ChiefComplaintButton_Click(object sender, RoutedEventArgs e)
        {
            chiefComplaintPopup.IsOpen = true;
        }

        private void ChiefComplaintOkButton_Click(object sender, RoutedEventArgs e)
        {
            string chiefComplaintText = chiefComplaintTextBox.Text;
            ChiefComplaints.Add(chiefComplaintText);
            chiefComplaintPopup.IsOpen = false;
        }

        private void RemoveChiefComplaint_Click(object sender, RoutedEventArgs e)
        {
            Button removeButton = (Button)sender;
            string selectedChiefComplaint = removeButton.DataContext as string;
            ChiefComplaints.Remove(selectedChiefComplaint);
        }

        private void ChiefComplaintCancel_Click(object sender, RoutedEventArgs e)
        {
            chiefComplaintPopup.IsOpen = false;
        }
        private void TreatmentPlanButton_Click(object sender, RoutedEventArgs e)
        {
            treatmentPlanPopup.IsOpen = true;
        }

    }
}
