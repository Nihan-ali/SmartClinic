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
    public partial class DiagnosisSearchWindow : Window
    {
        private List<Diagnosis> initialDiagnoses;
        private ObservableCollection<Diagnosis> displayedDiagnoses;
        private ObservableCollection<Diagnosis> selectedDiagnoses;

        public ObservableCollection<Diagnosis> SelectedDiagnoses => selectedDiagnoses;

        public DiagnosisSearchWindow()
        {
            InitializeComponent();
            initialDiagnoses = DatabaseHelper.GetInitialDiagnoses();
            displayedDiagnoses = new ObservableCollection<Diagnosis>(initialDiagnoses);
            selectedDiagnoses = new ObservableCollection<Diagnosis>();
            UpdateDiagnosisItems();
            Loaded += DiagnosisSearchWindow_Loaded;
        }

        private void DiagnosisSearchWindow_Loaded(object sender, RoutedEventArgs e)
        {
            searchTextBox.Focus();
            Keyboard.Focus(searchTextBox);
        }
        public void UpdateDiagnosisItems()
        {
            diagnosisItemsControl.ItemsSource = displayedDiagnoses;
        }

        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
            {
                string diag = searchTextBox.Text;
                Diagnosis selectedDiagnosis = new Diagnosis();
                selectedDiagnosis.Content = diag;
                SelectedDiagnoses.Add(selectedDiagnosis);
                UpdateSelectedDiagnosisListView();
                DatabaseHelper.AddDiagnosis(diag);
                searchTextBox.Text = "";
            }
        }

        private void SearchDiagnosis(string Content)
        {
            if (Content != "")
            {
                var searchedDiagnoses = DatabaseHelper.SearchDiagnoses(Content);
                displayedDiagnoses.Clear(); // Clear the existing items in displayedAdvices
                foreach (var diagnosis in searchedDiagnoses)
                {
                    displayedDiagnoses.Add(diagnosis); // Add each advice from the search result
                }
                UpdateDiagnosisItems();
            }
            else
            {
                displayedDiagnoses.Clear(); // Clear the existing items in displayedAdvices
                foreach (var diagnosis in initialDiagnoses)
                {
                    displayedDiagnoses.Add(diagnosis); // Add each advice from the initial advices
                }
                UpdateDiagnosisItems();
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
                    isFirstCharacterProcessed = true; // Mark as processed
                }
                else if (char.IsLower(search[0]))
                {
                    search = char.ToUpper(search[0]) + search.Substring(1);
                    isFirstCharacterProcessed = true; // Mark as processed
                }
            }
            else if (search == "")
            {
                isFirstCharacterProcessed = false;
            }

            searchTextBox.Text = search;

            // Restore the cursor position
            searchTextBox.SelectionStart = searchTextBox.Text.Length;

            SearchDiagnosis(search);

            searchTextBox.TextChanged += SearchTextBox_TextChanged; // Subscribe back to the event
        }


        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            Diagnosis selectedDiagnosis = toggleButton.DataContext as Diagnosis;

            if (toggleButton.IsChecked == true)
            {
                SelectedDiagnoses.Add(selectedDiagnosis);
            }
            else
            {
                // Remove the diagnosis if unchecked
                SelectedDiagnoses.Remove(selectedDiagnosis);
            }

            UpdateSelectedDiagnosisListView();
        }

        private void UpdateSelectedDiagnosisListView()
        {
            // Set the ItemsSource to the new collection
            selectedDiagnosisListView.ItemsSource = null;  // Set it to null first
            selectedDiagnosisListView.ItemsSource = selectedDiagnoses;
        }

        private void addToDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            DatabaseHelper.IncreaseDiagnosisOccurrence(selectedDiagnoses);
        }

        private void RemoveDiagnosisFromWindow(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Diagnosis selectedItem)
            {
                ToggleButton toggleButton = FindToggleButton(selectedItem);

                if (toggleButton != null)
                {
                    toggleButton.IsChecked = !toggleButton.IsChecked;
                }

                selectedDiagnoses.Remove(selectedItem);

                UpdateSelectedDiagnosisListView();
            }
        }

        private ToggleButton FindToggleButton(Diagnosis diagnosis)
        {
            foreach (var item in diagnosisItemsControl.Items)
            {
                if (diagnosisItemsControl.ItemContainerGenerator.ContainerFromItem(item) is FrameworkElement container)
                {
                    ToggleButton toggleButton = FindVisualChild<ToggleButton>(container);
                    if (toggleButton != null && toggleButton.DataContext == diagnosis)
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