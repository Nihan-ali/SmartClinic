using SmartClinic.View.UserControls;
using System;
using System.Collections.Generic;
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

namespace SmartClinic
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public string TodayDate { get; set; }

        private Popup treatmentPlanPopup;
        private RxUsercontrol rxUserControl;
        private Button selectedButton;

        public HomeWindow()
        {
            InitializeComponent();
            DatabaseHelper init = new DatabaseHelper();
            variables variables = new variables();
            DataContext = this;
            TodayDate = DateTime.Now.ToString("dd-MM-yyyy");
            rxUserControl = new RxUsercontrol();

            contentControl.Content = rxUserControl;
            // Set the selectedButton to RxButton
            string docname = variables.docname;
            if (!string.IsNullOrEmpty(docname))
            {
                // Convert the string to lowercase
                docname = docname.ToLower();

                // Split the string into words
                string[] words = docname.Split(' ');

                // Capitalize the first letter of each word
                for (int i = 0; i < words.Length; i++)
                {
                    if (!string.IsNullOrEmpty(words[i]))
                    {
                        char[] letters = words[i].ToCharArray();
                        letters[0] = char.ToUpper(letters[0]);
                        words[i] = new string(letters);
                    }
                }

                // Join the words back into a single string
                docname = string.Join(" ", words);
            }

            DrName.Content = docname;
            selectedButton = rx;
            selectedButton.Background = Brushes.White;
        }
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of MainWindow
            MainWindow mainWindow = new MainWindow();

            // Set the MainWindow as the main window
            Application.Current.MainWindow = mainWindow;

            // Show the MainWindow
            mainWindow.Show();

            // Close the current window
            Window.GetWindow(this).Close();
            DatabaseHelper.ResetStatus("AbuNoyim");
        }
        private void RxButton_Click(object sender, RoutedEventArgs e)
        {
            // Set content to RxUserControl
            UpdateButtonSelection(rx);
            contentControl.Content = new View.UserControls.RxUsercontrol();
        }

        private void PatientButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateButtonSelection(patients);
            contentControl.Content = new View.UserControls.PatientsUserControl();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Set content to SettingsUserControl
            UpdateButtonSelection(settings);
            contentControl.Content = new View.UserControls.Settings();

        }


        private void StatButton_Click(object sender, RoutedEventArgs e)
        {
            // Set content to StatUserControl
            UpdateButtonSelection(stat);
            contentControl.Content = new StatUserControl();
        }

        private void searchMenu_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void UpdateButtonSelection(Button clickedButton)
        {
            if (selectedButton != null)
            {
                // Reset the background of the previously selected button
                selectedButton.Background = System.Windows.Media.Brushes.Transparent;
            }

            // Set the background of the clicked button to red
            clickedButton.Background = System.Windows.Media.Brushes.White;

            // Update the selectedButton reference
            selectedButton = clickedButton;
        }
    }
}
