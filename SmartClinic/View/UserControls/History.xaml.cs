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
            ChiefComplaintPopup.IsOpen = true;
        }

        private void ChiefComplaintOkButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void RemoveChiefComplaint_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ChiefComplaintCancel_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void TreatmentPlanButton_Click(object sender, RoutedEventArgs e)
        {
            treatmentPlanPopup.IsOpen = true;
        }

    }
}
