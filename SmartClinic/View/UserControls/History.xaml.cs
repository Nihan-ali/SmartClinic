using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartClinic.View.UserControls
{
    public partial class History : UserControl
    {
        private List<Complaint> selectedComplaints = new List<Complaint>();

        public List<Complaint> SelectedComplaints => selectedComplaints;

        public History()
        {
            InitializeComponent();
        }

        public void AddToSelectedComplaints(Complaint newComplaint)
        {
            newComplaint.DisplayIndex = selectedComplaints.Count + 1;
            selectedComplaints.Add(newComplaint);
            UpdateSelectedComplaintListView();
        }
        private void UpdateSelectedComplaintListView()
        {
            selectedComplaintsListView.ItemsSource = null;
            selectedComplaintsListView.ItemsSource = selectedComplaints;
        }

        private void AddComplaint_Click(object sender, RoutedEventArgs e)
        {
            // Uncomment this block if you want to use it
            ComplaintSearchWindow searchWindow = new ComplaintSearchWindow();
            searchWindow.ShowDialog();

            foreach (var selectedComplaint in searchWindow.SelectedComplaints)
            {
                AddToSelectedComplaints(selectedComplaint);
            }
        }

        private void RemoveComplaint_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button deleteButton && deleteButton.DataContext is Complaint selectedItem)
            {
                // Remove the selected item from the collection
                selectedComplaints.Remove(selectedItem);

                // Update the DisplayIndex after removing the item
                UpdateDisplayIndex();

                // Update the ListView after removing the item
                UpdateSelectedComplaintListView();
            }
        }

        private void UpdateDisplayIndex()
        {
            // Update the DisplayIndex of each Complaint in the collection
            for (int i = 0; i < selectedComplaints.Count; i++)
            {
                selectedComplaints[i].DisplayIndex = i + 1;
            }
        }

        private void ListViewItem_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            if (item != null)
            {
                int index = selectedComplaintsListView.Items.IndexOf(item.DataContext) + 1;
                TextBlock serialNumberTextBlock = FindVisualChild<TextBlock>(item, "serialNumberTextBlock");
                if (serialNumberTextBlock != null)
                {
                    serialNumberTextBlock.Text = index.ToString();
                }
            }
        }

        private T FindVisualChild<T>(DependencyObject depObj, string name) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T && (child as FrameworkElement).Name == name)
                    return child as T;
                else
                {
                    T childItem = FindVisualChild<T>(child, name);
                    if (childItem != null)
                        return childItem;
                }
            }
            return null;
        }

        private void ComplaintItem_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem listViewItem && listViewItem.DataContext is Complaint selectedComplaint)
            {
                // Create an instance of the EditComplaintControl
                EditComplaintControl editControl = new EditComplaintControl();

                // Set the DataContext of the control to the selected complaint
                editControl.DataContext = selectedComplaint;

                // Create a Popup
                Popup popup = new Popup
                {
                    StaysOpen = false, // Close the Popup automatically when focus is lost
                    AllowsTransparency = true,
                    PlacementTarget = this,
                    Placement = PlacementMode.Center,
                    Child = editControl
                };

                // Show the Popup
                popup.IsOpen = true;

                // Handle the Closed event to restore the original content when the Popup is closed
                popup.Closed += (s, args) =>
                {
                    // Optionally handle any logic upon closing the popup
                    // For example, you might want to update the ListView after editing
                };
            }
        }




    }
}
