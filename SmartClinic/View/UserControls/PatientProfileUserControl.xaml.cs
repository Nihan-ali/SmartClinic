using System.Windows.Controls;

namespace SmartClinic.View.UserControls
{
    public partial class PatientProfileUserControl : UserControl
    {
        // You can extend this class with additional logic or properties if needed

        public PatientProfileUserControl(Patient selectedPatient)
        {
            InitializeComponent();

            // Set the DataContext to the selected patient for data binding
            DataContext = selectedPatient;
        }
    }
}
