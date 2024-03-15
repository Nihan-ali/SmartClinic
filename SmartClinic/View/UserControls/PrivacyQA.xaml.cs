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
    /// Interaction logic for PrivacyQA.xaml
    /// </summary>
    public partial class PrivacyQA : Window
    {
        List<Question> questions = DatabaseHelper.GetQuestions();
        QuesAns quesAns = new QuesAns();
        public PrivacyQA()
        {
            InitializeComponent();
            quesAns.qus1 = questions[0].Ques;
            quesAns.ans1= questions[0].Answer;
            quesAns.qus2 = questions[1].Ques;
            quesAns.ans2 = questions[1].Answer;
            quesAns.qus3 = questions[2].Ques;
            quesAns.ans3 = questions[2].Answer;

            DataContext = quesAns;
        }
        //cancel button function
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //submit button function
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHelper.UpdateQuestion(1,qs1.Text,ans1.Text);
            DatabaseHelper.UpdateQuestion(2, qs2.Text, ans2.Text);
            DatabaseHelper.UpdateQuestion(3, qs3.Text, ans3.Text);
            MessageBox.Show("Updated Successfully");
            this.Close();
        }
    }
}
