using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartClinic.View.UserControls
{
    public partial class PatientsUserControl : UserControl
    {
        public ObservableCollection<Patient> Patients { get; set; }
        int n = 1;
        public PatientsUserControl()
        {
            InitializeComponent();
            SearchPatient.Text = "search Patient";
            SearchPatient.Opacity = 0.5;
            UpdateListView(n);

        }
        

        private void UpdateListView(int n)
        {
            MessageBox.Show("UpdateListView "+n.ToString());
            Patients = new ObservableCollection<Patient>(DatabaseHelper.GetPatientsByLastVisitedDate(n));
            PatientsListBox.ItemsSource = Patients;

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
                    // Provide both parameters when creating the PatientProfileUserControl instance
                    contentControl.Content = new View.UserControls.PatientProfileUserControl(selectedPatient, mainWindow);
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
                    Patients.Remove(patientToDelete);
                }
                else
                {
                    MessageBox.Show("Failed to delete the patient.");
                }
            }
        }

        private void OnPreviousButtonClick(object sender, RoutedEventArgs e)
        {
            if(n>1)
            {
                n--;
                UpdateListView(n);
            }
        }

        private void OnNextButtonClick(object sender, RoutedEventArgs e)
        {
            n++;
            UpdateListView(n);
        }
        private void PatientsListBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if(e.Delta > 0)
            {
                //MessageBox.Show("MouseWheel Up");
                PatientListScrollviewer.LineDown();
            }
            else
            {
                //MessageBox.Show("MouseWheel Down");
                PatientListScrollviewer.LineUp();
            }
        }



    }


}