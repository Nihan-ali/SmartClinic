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

            // Subscribe to the Loaded event
            Loaded += PatientsUserControl_Loaded;
        }

        private void PatientsUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Set focus to the SearchPatient TextBox
            SearchPatient.Focus();
            Keyboard.Focus(SearchPatient);
        }



        private void UpdateListView(int n)
        {
            Patients = new ObservableCollection<Patient>(DatabaseHelper.GetPatientsByLastVisitedDate(n));
            PatientsListBox.ItemsSource = Patients;

        }
        private void PatientsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedPatient = (Patient)e.AddedItems[0];

                // Update the MainWindow content with the PatientProfileUserControl
                //show the selected patient name in messagebox
                //MessageBox.Show(selectedPatient.Name);
                UpdateMainWindowContent(selectedPatient);
            }
        }


        private void UpdateMainWindowContent(Patient selectedPatient)
        {
            if (Application.Current.MainWindow is HomeWindow homeWindow)
            {
                if (homeWindow.FindName("contentControl") is ContentControl contentControl)
                {
                    contentControl.Content = new View.UserControls.PatientProfileUserControl(selectedPatient, homeWindow);
                }
            }
            else
            {
                // Handle the case where the main window is not of type HomeWindow
                MessageBox.Show("Unexpected main window type.");
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
            if (n > 1)
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
            if (e.Delta > 0)
            {
                PatientListScrollviewer.LineDown();
            }
            else
            {
                PatientListScrollviewer.LineUp();
            }
        }

        private void SearchPatient_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchPatient.Text == "")
            {
                Patients = new ObservableCollection<Patient>(DatabaseHelper.GetPatientsByLastVisitedDate(n));
                PatientsListBox.ItemsSource = Patients;
            }
            else if (SearchPatient.Text != "search Patient")
            {

                Patients = new ObservableCollection<Patient>(DatabaseHelper.GetPatientsByName(SearchPatient.Text));
                if (Patients.Count > 0)
                {
                    PatientsListBox.ItemsSource = Patients;
                }
                else if (Patients.Count == 0)
                {
                    if (int.TryParse(SearchPatient.Text, out int searchId))
                    {
                        Patients.Clear();
                        Patients = new ObservableCollection<Patient>(DatabaseHelper.GetPatientByIdNear(searchId));
                        PatientsListBox.ItemsSource = Patients;
                    }
                }
            }
        }


    }


}