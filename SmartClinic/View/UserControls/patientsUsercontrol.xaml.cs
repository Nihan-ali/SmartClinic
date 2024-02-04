using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartClinic.View.UserControls
{
    public partial class PatientsUserControl : UserControl
    {
        public ObservableCollection<Patient> Patients { get; set; }

        public PatientsUserControl()
        {
            InitializeComponent();

            // Initialize Patients collection by fetching from the database
            Patients = new ObservableCollection<Patient>(DatabaseHelper.GetAllPatients());

            // Set the ItemsSource of the ListBox to the Patients collection
            PatientsListBox.ItemsSource = Patients;
            SearchPatient.Text = "search Patient";
            SearchPatient.Opacity = 0.5;
            Console.WriteLine(PatientsListBox.ItemsSource);
            Console.WriteLine("mostahid");
        }

        private void PatientsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedPatient = (Patient)e.AddedItems[0];

                // Update the MainWindow content with the PatientProfileUserControl
                UpdateMainWindowContent(selectedPatient);
            }
        }

        private void UpdateMainWindowContent(Patient selectedPatient)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                if (mainWindow.FindName("contentControl") is ContentControl contentControl)
                {
                    contentControl.Content = new View.UserControls.PatientProfileUserControl(selectedPatient);
                }
            }
        }



        private void AddPatientButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle add patient button click
            AddPatient AddPatientWindow = new AddPatient();
            // AddPatientWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            AddPatientWindow.Left = (SystemParameters.PrimaryScreenWidth - AddPatientWindow.Width) / 2;
            AddPatientWindow.Top = (SystemParameters.PrimaryScreenHeight - AddPatientWindow.Height) / 2;
            AddPatientWindow.Show();

        }

        private void OnSearchTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            // Handle search text box got focus
            if (SearchPatient.Text == "search Patient")
            {
                SearchPatient.Text = "";
                SearchPatient.Opacity = 1.0;
            }
        }

        private void OnSearchTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            // Handle search text box lost focus
            if (string.IsNullOrWhiteSpace(SearchPatient.Text))
            {
                SearchPatient.Text = "search Patient";
                SearchPatient.Opacity = 0.5;
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Get the DataContext of the clicked button, which is a Patient object
            if (sender is FrameworkElement element && element.DataContext is Patient patientToDelete)
            {
                // Perform the deletion from the database
                bool deleted = DatabaseHelper.DeletePatient(patientToDelete.Id);

                if (deleted)
                {
                    // Remove the item from the ObservableCollection
                    Patients.Remove(patientToDelete);
                }
                else
                {
                    MessageBox.Show("Failed to delete the patient.");
                }
            }
        }


      
    }

    /*private void SearchBox_KeyDown(object sender, RoutedEventArgs e)
    {
        // Handle search box key down event
        // You might want to filter the Patients collection based on the search term
    }*/
   /* private void OnGridMouseDown(object sender, MouseButtonEventArgs e)
    {
        // Handle mouse down on the grid or UserControl
        // This can help maintain focus and close the popup
      *//*  PatientPopup.IsOpen = false;
    }*/
  /*  private void PatientList_MouseDown(object sender, RoutedEventArgs e)
    {
        // Handle mouse down event for the entire window';
        ;
    }
*/
    // Add other methods as needed...


}
