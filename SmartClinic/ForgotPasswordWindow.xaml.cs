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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartClinic
{
    /// <summary>
    /// Interaction logic for ForgotPasswordWindow.xaml
    /// </summary>
    public partial class ForgotPasswordWindow : Window
    {
        private int currentQuestionNumber = 1;
        List<Question> questions = DatabaseHelper.GetQuestions();
        private int correctAnswer = 0;
        public ForgotPasswordWindow()
        {
            InitializeComponent();
            
            DisplayNextQuestion();
        }

        private async Task DisplayNextQuestion()
        {
            // Retrieve and display the security question
            string question = questions[currentQuestionNumber - 1].Ques;
            SecurityQuestionText.Text = question;
            QuestionNo.Text = "Question No : " + currentQuestionNumber.ToString();
            SecurityAnswerTextBox.Text = string.Empty;
            SecurityAnswerTextBox.Focus();
            await FadeInContent();
        }


        private Task FadeInContent()
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(1)
            };

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(fadeInAnimation);
            Storyboard.SetTarget(fadeInAnimation, ContentGrid);
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath("Opacity"));

            storyboard.Completed += (s, e) => tcs.SetResult(null);
            storyboard.Begin();

            return tcs.Task;
        }



        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to verify the answer against the database
            string userAnswer = SecurityAnswerTextBox.Text;
            bool isAnswerCorrect = VerifySecurityAnswerFromDatabase(currentQuestionNumber, userAnswer);
            currentQuestionNumber++;
            if (isAnswerCorrect)
            {
                // If the answer is correct, proceed to the next question or allow password reset
                
                correctAnswer++;
                
            }
            if (currentQuestionNumber <= 3)
            {
                DisplayNextQuestion();
            }
            else
            {
                if (correctAnswer >=2)
                {
                    //MessageBox.Show("You are ready to set your password");
                    // Navigate to the password reset window
                    //PasswordResetWindow passwordResetWindow = new PasswordResetWindow();
                    //passwordResetWindow.Show();
                    this.Close();
                    //open change password window
                    ChangePassword changePassword = new ChangePassword();
                    changePassword.Show();

                }
                else
                {
                    // message to show that the user has failed to answer the security questions
                    MessageBox.Show("You have failed to answer the security questions correctly. Please try again.");
                    this.Close();
                }
            }
        }

        private bool VerifySecurityAnswerFromDatabase(int questionNumber, string userAnswer)
        {
            // Implement logic to verify the answer against the database
            // You can use the questionNumber to fetch the correct answer and compare it with user's answer
            // Replace this with your database logic
            string correctAnswer = questions[questionNumber-1].Answer; // Replace with your actual database query
            return userAnswer.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase);
        }
        //implement homeClick button
        private void home_Click(object sender, RoutedEventArgs e)
        {
            // Handle home button click
            //open login window
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
