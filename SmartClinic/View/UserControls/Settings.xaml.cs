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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartClinic.View.UserControls
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
            List<DoctorInfo> doctorInfo1 = DatabaseHelper.GetDoctorInfos();
            DoctorInfo doctorInfo = doctorInfo1[0];
            DataContext = doctorInfo;
        }
        //implementing the update button
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorInfo updatedDoctorInfo = new DoctorInfo
            {
                docname = Docname.Text,
                docname_bangla = docname_bangla.Text,
                docdegree = docdegree.Text,
                docdegree_bangla = docdegree_bangla.Text,
                docdetail = docdetail.Text,
                docdetail_bangla = docdetail_bangla.Text,
                chamber = chamber.Text,
                chamber_location = chamber_location.Text,
                chamber_phone = chamber_phone.Text,
                visit_date = visit_date.Text,
                visit_time = visit_time.Text,
                outro = outro.Text,
                moredetail_bangla = moredetail_bangla.Text,
                // Set other properties accordingly
            };
            DatabaseHelper.UpdateDoctorInfo(updatedDoctorInfo);
        }
        //privacy button function
        private void PrivacyButton_Click(object sender, RoutedEventArgs e)
        {
            PrivacyQA privacy = new PrivacyQA();
            privacy.ShowDialog();
        }
    }
}
