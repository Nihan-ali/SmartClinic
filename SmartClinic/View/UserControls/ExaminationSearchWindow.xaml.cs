using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public partial class ExaminationSearchWindow : Window
    {
        private List<Examination> initialExaminations;
        private ObservableCollection<Examination> displayedExaminations;
        private ObservableCollection<Examination> selectedExaminations;

        public ObservableCollection<Examination> SelectedExaminations => selectedExaminations;

        public ExaminationSearchWindow()
        {
            InitializeComponent();
            initialExaminations = DatabaseHelper.GetInitialExaminations();
            displayedExaminations = new ObservableCollection<Examination>(initialExaminations);
            selectedExaminations = new ObservableCollection<Examination>();
            UpdateExaminationItems();
        }

        private void addToExamination_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            DatabaseHelper.IncreaseExaminationOccurrence(selectedExaminations);
        }


        private void AddToSelectedExaminations(Examination selectedExamination)
        {
            if (!selectedExaminations.Contains(selectedExamination))
            {
                selectedExaminations.Add(selectedExamination);
                UpdateSelectedExaminationListView();
            }
        }
        public void UpdateExaminationItems()
        {
            examinationItemsControl.ItemsSource = displayedExaminations;
        }

        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
            {
                string examination = searchTextBox.Text;
                Examination selectedExamination = new Examination();
                selectedExamination.Content = examination;
                SelectedExaminations.Add(selectedExamination);
                UpdateSelectedExaminationListView();
                DatabaseHelper.AddExamination(examination);
                searchTextBox.Text = "";
            }
        }

        private void SearchExamination(string keyword)
        {
            displayedExaminations = new ObservableCollection<Examination>(
                initialExaminations
                .Where(examination => examination.Content.ToLower().Contains(keyword.ToLower()))
                .OrderByDescending(examination => examination.Occurrence)
                .Take(20)
                .ToList());

            UpdateExaminationItems();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchExamination(searchTextBox.Text);
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            Examination selectedExamination = toggleButton.DataContext as Examination;

            if (toggleButton.IsChecked == true)
            {
                SelectedExaminations.Add(selectedExamination);
            }
            else
            {
                // Remove the examination if unchecked
                SelectedExaminations.Remove(selectedExamination);
            }

            UpdateSelectedExaminationListView();
        }

        private void UpdateSelectedExaminationListView()
        {
            // Set the ItemsSource to the new collection
            selectedExaminationListView.ItemsSource = null;  // Set it to null first
            selectedExaminationListView.ItemsSource = selectedExaminations;
        }

        private void RemoveExaminationFromWindow(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Examination selectedItem)
            {
                ToggleButton toggleButton = FindToggleButton(selectedItem);

                if (toggleButton != null)
                {
                    toggleButton.IsChecked = !toggleButton.IsChecked;
                }

                selectedExaminations.Remove(selectedItem);

                UpdateSelectedExaminationListView();
            }
        }

        private ToggleButton FindToggleButton(Examination examination)
        {
            foreach (var item in examinationItemsControl.Items)
            {
                if (examinationItemsControl.ItemContainerGenerator.ContainerFromItem(item) is FrameworkElement container)
                {
                    ToggleButton toggleButton = FindVisualChild<ToggleButton>(container);
                    if (toggleButton != null && toggleButton.DataContext == examination)
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
