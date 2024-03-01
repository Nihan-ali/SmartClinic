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
        private RxUsercontrol rxUserControl;
        public MainWindow()
        {
            InitializeComponent();
            DatabaseHelper init = new DatabaseHelper();

            DataContext = this;
            TodayDate = DateTime.Now.ToString("dd-MM-yyyy");
            rxUserControl = new RxUsercontrol();
            rx.Background = Brushes.Red;
            
            contentControl.Content = rxUserControl;
        }
        private void checking(object sender, RoutedEventArgs e)
        {
            rx.Foreground = Brushes.Purple;
            rx.Background = Brushes.Red;

        }

        // MainWindow.xaml.cs
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
            contentControl.Content = new StatUserControl();
        }

        private void searchMenu_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}