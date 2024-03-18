using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
            Loaded += ComplaintSearchWindow_Loaded;
        }

        public void UpdateComplaintItems()
        {
            complaintItemsControl.ItemsSource = displayedComplaints;
        }

        private void ComplaintSearchWindow_Loaded(object sender, RoutedEventArgs e)
        {
            searchTextBox.Focus();
            Keyboard.Focus(searchTextBox);
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

        //complaint,history,examination,investigation,diagnosis,treatment, specialnote, followup
        private void SearchComplaint(string Content)
        {
            if (Content != "")
            {
                var searchedComplaints = DatabaseHelper.SearchComplaints(Content);
                displayedComplaints.Clear(); // Clear the existing items in displayedAdvices
                foreach (var advice in searchedComplaints)
                {
                    displayedComplaints.Add(advice); // Add each advice from the search result
                }
                UpdateComplaintItems();
            }
            else
            {
                displayedComplaints.Clear(); // Clear the existing items in displayedAdvices
                foreach (var advice in initialComplaints)
                {
                    displayedComplaints.Add(advice); // Add each advice from the initial advices
                }
                UpdateComplaintItems();
            }
        }

        private bool isFirstCharacterProcessed = false;
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchTextBox.TextChanged -= SearchTextBox_TextChanged; // Unsubscribe from the event

            string search = searchTextBox.Text;

            // Process the 0th index value only if it hasn't been processed before
            if (!isFirstCharacterProcessed && search != "")
            {
                if (char.IsUpper(search[0]))
                {
                    search = char.ToLower(search[0]) + search.Substring(1);
                    isFirstCharacterProcessed = true;
                }
                else if (char.IsLower(search[0]))
                {
                    search = char.ToUpper(search[0]) + search.Substring(1);
                    isFirstCharacterProcessed = true;
                }
            }
            else if (search == "")
            {
                isFirstCharacterProcessed = false;
            }

            searchTextBox.Text = search;

            // Restore the cursor position
            searchTextBox.SelectionStart = searchTextBox.Text.Length;

            SearchComplaint(search);

            searchTextBox.TextChanged += SearchTextBox_TextChanged; // Subscribe back to the event
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
