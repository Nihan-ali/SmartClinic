using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace SmartClinic.View.UserControls
{
    /// <summary>
    /// Interaction logic for HistoryPopup.xaml
    /// </summary>
    public partial class HistoryPopup : UserControl
    {
        public ObservableCollection<string> CommonlyUsedHistory { get; set; }
        public ObservableCollection<string> SelectedHistory { get; set; }

        public HistoryPopup()
        {
            InitializeComponent();
            DataContext = this; // Set DataContext to enable data binding

            // Dummy data for commonly used history
            CommonlyUsedHistory = new ObservableCollection<string>
            {
                "History A", "History B", "History C", "History D", "History E"
            };

            SelectedHistory = new ObservableCollection<string>();
        }

        private void AddToSelectedHistory_Click(object sender, RoutedEventArgs e)
        {
            Button addButton = (Button)sender;
            string selectedHistory = addButton.DataContext as string;

            if (!SelectedHistory.Contains(selectedHistory))
                SelectedHistory.Add(selectedHistory);
        }

        private void RemoveFromSelectedHistory_Click(object sender, RoutedEventArgs e)
        {
            Button removeButton = (Button)sender;
            string selectedHistory = removeButton.DataContext as string;

            SelectedHistory.Remove(selectedHistory);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Handle OK button click, close the popup, and update the main UI
            // For now, let's just close the popup
            ClosePopup();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Handle Cancel button click, close the popup without updating the main UI
            // For now, let's just close the popup
            ClosePopup();
        }

        private void ClosePopup()
        {
            // Close the popup
            var parentPopup = this.Parent as Popup;
            if (parentPopup != null)
            {
                parentPopup.IsOpen = false;
            }
        }

        private void addCreate_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Handle add/create button click
        }
    }
}
