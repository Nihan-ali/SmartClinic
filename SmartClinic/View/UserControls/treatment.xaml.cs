using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace SmartClinic.View.UserControls
{
    /// <summary>
    /// Interaction logic for treatment.xaml
    /// </summary>
    public partial class treatment : UserControl
    {
        public ObservableCollection<string> ChiefComplaints { get; set; }
        public treatment()
        {
            InitializeComponent();
            ChiefComplaints = new ObservableCollection<string>();
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
    }
}
