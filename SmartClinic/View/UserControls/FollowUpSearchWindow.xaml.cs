using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

using static SmartClinic.DatabaseHelper;

namespace SmartClinic.View.UserControls
{
    public partial class FollowUpSearchWindow : Window
    {
        private List<FollowUp> initialFollowUps;
        private ObservableCollection<FollowUp> displayedFollowUps;
        private ObservableCollection<FollowUp> selectedFollowUps;

        public ObservableCollection<FollowUp> SelectedFollowUps => selectedFollowUps;

        public FollowUpSearchWindow()
        {
            InitializeComponent();

            // Load initial follow-ups
            initialFollowUps = DatabaseHelper.GetInitialFollowUps();
            displayedFollowUps = new ObservableCollection<FollowUp>(initialFollowUps);
            selectedFollowUps = new ObservableCollection<FollowUp>();

            UpdateFollowUpItems();
        }

        private void UpdateFollowUpItems()
        {
            followUpItemsControl.ItemsSource = displayedFollowUps;
        }

        private void SearchFollowUps(string keyword)
        {
            displayedFollowUps = new ObservableCollection<FollowUp>(
                initialFollowUps
                .Where(followUp => followUp.Content.ToLower().Contains(keyword.ToLower()))
                .OrderByDescending(followUp => followUp.Occurrence)
                .Take(20)
                .ToList());

            UpdateFollowUpItems();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchFollowUps(searchTextBox.Text);
        }

        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
            {
                string followUp = searchTextBox.Text;
                FollowUp selectedFollowUp = new FollowUp();
                selectedFollowUp.Content = followUp;
                SelectedFollowUps.Add(selectedFollowUp);
                DatabaseHelper.AddFollowUp(followUp);
                searchTextBox.Text = "";
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            FollowUp selectedFollowUp = toggleButton.DataContext as FollowUp;

            if (toggleButton.IsChecked == true)
            {
                selectedFollowUps.Add(selectedFollowUp);
            }
            else
            {
                selectedFollowUps.Remove(selectedFollowUp);
            }

        }

        public void AddToSelectedFollowUps(FollowUp newFollowUp)
        {
            selectedFollowUps.Add(newFollowUp);
        }

        private void addToFollowUp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            DatabaseHelper.IncreaseFollowUpOccurrence(selectedFollowUps);
        }

    }
}