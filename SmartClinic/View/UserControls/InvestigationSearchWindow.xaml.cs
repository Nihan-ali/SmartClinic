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
    public partial class InvestigationSearchWindow : Window
    {
        private List<Investigation> initialInvestigations;
        private ObservableCollection<Investigation> displayedInvestigations;
        private ObservableCollection<Investigation> selectedInvestigations;

        public ObservableCollection<Investigation> SelectedInvestigations => selectedInvestigations;

        public InvestigationSearchWindow()
        {
            InitializeComponent();
            initialInvestigations = DatabaseHelper.GetInitialInvestigations();
            displayedInvestigations = new ObservableCollection<Investigation>(initialInvestigations);
            selectedInvestigations = new ObservableCollection<Investigation>();
            UpdateInvestigationItems();
        }

        public void UpdateInvestigationItems()
        {
            investigationItemsControl.ItemsSource = displayedInvestigations;
        }

        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
            {
                string investigation = searchTextBox.Text;
                Investigation selectedInvestigation = new Investigation();
                selectedInvestigation.Content = investigation;
                SelectedInvestigations.Add(selectedInvestigation);
                UpdateSelectedInvestigationListView();
                DatabaseHelper.AddInvestigation(investigation);
                searchTextBox.Text = "";
            }
        }

        private void SearchInvestigation(string keyword)
        {
            displayedInvestigations = new ObservableCollection<Investigation>(
                initialInvestigations
                .Where(investigation => investigation.Content.ToLower().Contains(keyword.ToLower()))
                .OrderByDescending(investigation => investigation.Occurrence)
                .Take(20)
                .ToList());

            UpdateInvestigationItems();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchInvestigation(searchTextBox.Text);
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            Investigation selectedInvestigation = toggleButton.DataContext as Investigation;

            if (toggleButton.IsChecked == true)
            {
                SelectedInvestigations.Add(selectedInvestigation);
            }
            else
            {
                SelectedInvestigations.Remove(selectedInvestigation);
            }

            UpdateSelectedInvestigationListView();
        }

        private void UpdateSelectedInvestigationListView()
        {
            selectedInvestigationListView.ItemsSource = null;
            selectedInvestigationListView.ItemsSource = selectedInvestigations;
        }

        private void addToInvestigation_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            DatabaseHelper.IncreaseInvestigationOccurrence(selectedInvestigations);
        }

        private void RemoveInvestigationFromWindow(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Investigation selectedItem)
            {
                ToggleButton toggleButton = FindToggleButton(selectedItem);

                if (toggleButton != null)
                {
                    toggleButton.IsChecked = !toggleButton.IsChecked;
                }

                selectedInvestigations.Remove(selectedItem);
                UpdateSelectedInvestigationListView();
            }
        }

        private ToggleButton FindToggleButton(Investigation investigation)
        {
            foreach (var item in investigationItemsControl.Items)
            {
                if (investigationItemsControl.ItemContainerGenerator.ContainerFromItem(item) is FrameworkElement container)
                {
                    ToggleButton toggleButton = FindVisualChild<ToggleButton>(container);
                    if (toggleButton != null && toggleButton.DataContext == investigation)
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
