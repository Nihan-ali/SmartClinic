using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmartClinic.View.UserControls
{
    /// <summary>
    /// Interaction logic for CreateMedicineGroupWindow.xaml
    /// </summary>
    public partial class CreateMedicineGroupWindow : Window
    {
        private List<Medicine> selectedMedicines = new List<Medicine>();
        public CreateMedicineGroupWindow()
        {
            InitializeComponent();
        }
        private void addListView_Click(object sender, RoutedEventArgs e)
        {
            MedicineSearchWindow medicineSearchWindow = new MedicineSearchWindow();
            medicineSearchWindow.ShowDialog();
            if (medicineSearchWindow.SelectedMedicines != null)
            {
                foreach (Medicine medicine in medicineSearchWindow.SelectedMedicines)
                {
                    selectedMedicines.Add(medicine);
                }
            }
        }
    }
}
