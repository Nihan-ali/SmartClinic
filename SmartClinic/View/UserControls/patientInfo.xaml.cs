﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SmartClinic.View.UserControls
{
    public partial class patientInfo : UserControl, INotifyPropertyChanged
    {
        private string _todayDate;
        private string _age;

        public patientInfo()
        {
            InitializeComponent();
            // Set default values or perform any initialization
            TodayDate = "YourDefaultTodayDate";
            Age = "YourDefaultAge";
        }

        public string TodayDate
        {
            get { return _todayDate; }
            set
            {
                if (_todayDate != value)
                {
                    _todayDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Age
        {
            get { return _age; }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged();
                }
            }
        }

        public void UpdatePatientInfo(string name, string age)
        {
            MessageBox.Show(age);
            Age = age;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnSearchTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (searchPatientTextBox.Text == "Search Patient")
            {
                searchPatientTextBox.Text = "";
                searchPatientTextBox.Foreground = Brushes.Black; // Change the color to the regular text color
            }
        }

        private void OnSearchTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchPatientTextBox.Text))
            {
                searchPatientTextBox.Text = "Search Patient";
                searchPatientTextBox.Foreground = Brushes.Gray; // Change the color to the placeholder color
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Pass this instance to the constructor of AddPatient
            AddPatient AddPatientWindow = new AddPatient(this);
            AddPatientWindow.Left = (SystemParameters.PrimaryScreenWidth - AddPatientWindow.Width) / 2;
            AddPatientWindow.Top = (SystemParameters.PrimaryScreenHeight - AddPatientWindow.Height) / 2;
            AddPatientWindow.Show();
        }

        private void searchPatientTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
