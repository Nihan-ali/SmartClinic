using SmartClinic.View.UserControls;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartClinic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string TodayDate { get; set; }

        private Popup treatmentPlanPopup;
        public MainWindow()
        {
            InitializeComponent();
            DatabaseHelper init = new DatabaseHelper();
           
            DataContext = this;
            TodayDate = DateTime.Now.ToString("dd-MM-yyyy");
            contentControl.Content = new View.UserControls.RxUsercontrol();
        }
        // MainWindow.xaml.cs
        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (treatmentPlanPopup.IsOpen && !IsMouseOverPopup(e))
            {
                treatmentPlanPopup.IsOpen = false;
            }
        }

        private bool IsMouseOverPopup(MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(this);
            return position.X >= treatmentPlanPopup.HorizontalOffset &&
            position.X <= treatmentPlanPopup.HorizontalOffset + treatmentPlanPopup.ActualWidth &&
                   position.Y >= treatmentPlanPopup.VerticalOffset &&
            position.Y <= treatmentPlanPopup.VerticalOffset + treatmentPlanPopup.ActualHeight;
        }

        public static implicit operator MainWindow(MedicineSearchWindow v)
        {
            throw new NotImplementedException();
        }
        private void RxButton_Click(object sender, RoutedEventArgs e)
        {
            // Set content to RxUserControl
            contentControl.Content = new View.UserControls.RxUsercontrol();
        }

        private void PatientButton_Click(object sender, RoutedEventArgs e)
        {
            // Set content to PatientUserControl
            contentControl.Content = new View.UserControls.PatientsUserControl();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Set content to SettingsUserControl
            //contentControl.Content = new UserControls.SettingsUserControl();
        }

        private void StatButton_Click(object sender, RoutedEventArgs e)
        {
            // Set content to StatUserControl
            //contentControl.Content = new UserControls.StatUserControl();
        }

        private void searchMenu_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}