using SmartClinic.View.UserControls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;


namespace SmartClinic
{
    /// <summary>
    /// Interaction logic for PatientList.xaml
    /// </summary>
    /// 
    public class Patient : INotifyPropertyChanged
    {
        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private DateTime _lastVisited;
        public DateTime LastVisited
        {
            get { return _lastVisited; }
            set
            {
                _lastVisited = value;
                OnPropertyChanged();
            }
        }

        private string _phone = string.Empty;
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        private string _serialNumber = string.Empty;
        public string SerialNumber
        {
            get { return _serialNumber; }
            set
            {
                _serialNumber = value;
                OnPropertyChanged();
            }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        private string _address = string.Empty;
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public partial class PatientList : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private List<Patient> _patients = new List<Patient>();

        public List<Patient> Patients
        {
            get { return _patients; }
            set
            {
                _patients = value;
                OnPropertyChanged();
            }
        }


        public PatientList()
        {
            InitializeComponent();
            DataContext = this;

            // Initialize the Patients collection with sample data
            //Patients.Add(new Patient { Name = "John Doe", LastVisited = DateTime.Now.AddDays(-10), Phone = "123-456-7890", SerialNumber = "001", Age = 30, Address = "123 Main St" });
            //Patients.Add(new Patient { Name = "Jane Doe", LastVisited = DateTime.Now.AddDays(-5), Phone = "987-654-3210", SerialNumber = "002", Age = 25, Address = "456 Oak St" });
            // Add more patients as needed
        }
        private void PatientList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ;
        }

        private void OnDeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            // Handle the delete button click event here
            // You can access the selected patient or perform any other action
            if (sender is FrameworkElement button && button.DataContext is Patient patient)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {patient.Name}?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Patients.Remove(patient);
                    MessageBox.Show($"{patient.Name} has been deleted.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        private void OnclickAddPatient(object sender, RoutedEventArgs e)
        {
            AddPatient AddPatientWindow = new AddPatient();
           // AddPatientWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
             AddPatientWindow.Left = (SystemParameters.PrimaryScreenWidth - AddPatientWindow.Width) / 2;
            AddPatientWindow.Top = (SystemParameters.PrimaryScreenHeight - AddPatientWindow.Height) / 2;
            AddPatientWindow.Show();

        }

      


        // Add input fields and submit button to the popup content


        private void OnSearchTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (searchPatientTextBox != null && searchPatientTextBox.Text == "Search Patient")
            {
                searchPatientTextBox.Text = "";
                searchPatientTextBox.Foreground = Brushes.Black; // Change the color to the regular text color
            }
        }

        private void OnSearchTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (searchPatientTextBox != null && string.IsNullOrWhiteSpace(searchPatientTextBox.Text))
            {
                searchPatientTextBox.Text = "Search Patient";
                searchPatientTextBox.Foreground = Brushes.Gray; // Change the color to the placeholder color
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void PatientListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle the selection changed event here
            // You can access the selected item or perform any other action
            if (PatientListView.SelectedItem is Patient selectedPatient)
            {
                MessageBox.Show($"Selected Patient: {selectedPatient.Name}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            ;
        }





       
    }
}
