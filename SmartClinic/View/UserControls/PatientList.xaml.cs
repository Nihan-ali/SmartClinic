using System.Collections.ObjectModel;
using System.Windows;

namespace SmartClinic
{
    public partial class PatientList : Window
    {
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Medicine> Medicines { get; set; }


        public PatientList()
        {
            InitializeComponent();
            DataContext = this;

            // Initialize Patients collection by fetching from the database
            Patients = new ObservableCollection<Patient>(DatabaseHelper.GetAllPatients());

            // Initialize Medicines collection by fetching from the database
            Medicines = new ObservableCollection<Medicine>(DatabaseHelper.GetInitialMedicines());

            // Set the ItemsSource of the ListView to the Patients collection
            PatientListView.ItemsSource = Patients;

            // Set the ItemsSource of the MedicineListView to the Medicines collection
            //PatientListView.ItemsSource = Medicines;  // Fix here
        }

        private void OnclickAddPatient(object sender, RoutedEventArgs e)
        {
            // Handle Add Patient button click
            // You might want to open a new window to add patient information
        }

        private void OnSearchTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            // Handle search text box got focus
        }

        private void OnSearchTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            // Handle search text box lost focus
        }

        private void SearchBox_KeyDown(object sender, RoutedEventArgs e)
        {
            // Handle search box key down event
            // You might want to filter the Patients collection based on the search term
        }

        private void PatientListView_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // Handle patient list view selection changed event
        }

        private void OnDeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            // Handle delete button click
            // You might want to prompt the user for confirmation before deleting
        }

        private void PatientList_MouseDown(object sender, RoutedEventArgs e)
        {
            // Handle mouse down event for the entire window
        }
    }
}
