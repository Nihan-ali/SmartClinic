using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmartClinic.View.UserControls
{
    
    public partial class ComplaintSearchWindow : Window
    {
        private List<Complaint> initialComplaints;
        private ObservableCollection<Complaint> displayedComplaints;
        private ObservableCollection<Complaint> selectedComplaints;

        public ObservableCollection<Complaint> SelectedComplaints => selectedComplaints;

        
        public ComplaintSearchWindow()
        {
            InitializeComponent();
            initialComplaints = DatabaseHelper.GetInitialComplaint();
            displayedComplaints = new ObservableCollection<Complaint>(initialComplaints);
            selectedComplaints = new ObservableCollection<Complaint>();
            UpdateComplaintItems();
        }
        public void UpdateComplaintItems()
        {
            complaintItemsControl.ItemsSource = displayedComplaints;
        }

        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if(Key.Enter == e.Key)
            {
                string comp = searchTextBox.Text;
                Complaint selectedComplaint = new Complaint();
                selectedComplaint.Content = comp;
                SelectedComplaints.Add(selectedComplaint);
                UpdateSelectedComplaintListView();
                DatabaseHelper.AddComplaint(comp);
                searchTextBox.Text = "";
            }
        }
        private void SearchComplaint(string keyword)
        {
            displayedComplaints = new ObservableCollection<Complaint>(
                initialComplaints
                .Where(complaint => complaint.Content.ToLower().Contains(keyword.ToLower()))
                .OrderByDescending(complaint => complaint.Occurrence)
                .Take(20)
                .ToList());

            UpdateComplaintItems();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchComplaint(searchTextBox.Text);
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            Complaint selectedComplaint = toggleButton.DataContext as Complaint;

            if (toggleButton.IsChecked == true)
            {
                SelectedComplaints.Add(selectedComplaint);
            }
            else
            {
                // Remove the advice if unchecked
                SelectedComplaints.Remove(selectedComplaint);
            }

            UpdateSelectedComplaintListView();
        }

        private void UpdateSelectedComplaintListView()
        {
            // Set the ItemsSource to the new collection
            selectedComplaintListView.ItemsSource = null;  // Set it to null first
            selectedComplaintListView.ItemsSource = selectedComplaints;
        }


        private void addToComplaint_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            DatabaseHelper.IncreaseComplaintOccurrence(selectedComplaints);           
        }


        private void RemoveComplaintFromWindow(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Complaint selectedItem)
            {
                ToggleButton toggleButton = FindToggleButton(selectedItem);

                if (toggleButton != null)
                {
                    toggleButton.IsChecked = !toggleButton.IsChecked;
                }

                selectedComplaints.Remove(selectedItem);

                UpdateSelectedComplaintListView();
            }
        }

        private ToggleButton FindToggleButton(Complaint complaint)
        {
            foreach (var item in complaintItemsControl.Items)
            {
                if (complaintItemsControl.ItemContainerGenerator.ContainerFromItem(item) is FrameworkElement container)
                {
                    ToggleButton toggleButton = FindVisualChild<ToggleButton>(container);
                    if (toggleButton != null && toggleButton.DataContext == complaint)
                    {
                        return toggleButton;
                    }
                }
            }

            return null;
        }

        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            int childCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is T foundChild)
                {
                    return foundChild;
                }

                T childOfChild = FindVisualChild<T>(child);

                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }

            return null;
        }



    }
}
