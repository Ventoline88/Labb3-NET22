using Labb3_NET22.Constants;
using Labb3_NET22.DataModels;
using System.Windows;
using System.Windows.Controls;

namespace Labb3_NET22
{
    /// <summary>
    /// Interaction logic for PlayView.xaml
    /// </summary>
    public partial class PlayView : UserControl
    {
        /// <summary>
        /// The model to use when playing the game.
        /// </summary>
        private PlayViewModel _model;

        /// <summary>
        /// Creates a new play view using the specified quiz.
        /// </summary>
        /// <param name="quiz">The quiz to use.</param>
        public PlayView(Quiz quiz)
        {
            InitializeComponent();

            _model = new PlayViewModel(quiz);
            _model.Quiz.InitializeRandomQuestions();
            DataContext = _model;

            if (_model.Quiz.GetRandomQuestionsRemaining() > 0)
            {
                _model.SetUpQuestion();
                _model.UpdateScoreData();
                InitializeQuestionButtons();
            }
            else
            {
                ButtonReturnToMainMenu.IsEnabled = true;
            }
        }

        /// <summary>
        /// Initializes the buttons used to answer the question based on the answers of the question.
        /// </summary>
        private void InitializeQuestionButtons()
        {
            StackPanelAnswerButtons.Children.Clear();

            Question currentQuestion = _model.CurrentQuestion;

            for (int i = 0; i < currentQuestion.Answers.Length; i++)
            {
                Button answerButton = new Button();
                answerButton.Content = currentQuestion.Answers[i];
                answerButton.Tag = i;
                answerButton.Click += ButtonAnswerQuestionClicked;

                StackPanelAnswerButtons.Children.Add(answerButton);
            }
        }


        /// <summary>
        /// Compares the tag of the answer button pressed to the index of the correct answer of the question.
        /// If it is the same the number of correct answers is incremented.
        /// If there are questions left to answer the next question is set up.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event</param>
        private void ButtonAnswerQuestionClicked(object sender, RoutedEventArgs e)
        {
            int selectedAnswerIndex = (int)((Button)sender).Tag;

            if (selectedAnswerIndex == _model.CurrentQuestion.CorrectAnswerIndex)
            {
                MessageBox.Show(MessageConstants.MESSAGE_ANSWER_CORRECT);
                _model.IncrementCorrectAnswers();
            }
            else
            {
                MessageBox.Show(MessageConstants.MESSAGE_ANSWER_INCORRECT);
            }

            if (_model.Quiz.GetRandomQuestionsRemaining() == 0)
            {
                LabelQuestionStatement.Visibility = Visibility.Hidden;
                LabelQuizComplete.Visibility = Visibility.Visible;
                ImageQuestion.Visibility = Visibility.Hidden;
                StackPanelAnswerButtons.Children.Clear();
                ButtonReturnToMainMenu.IsEnabled = true;
            }
            else
            {
                _model.SetUpQuestion();
                InitializeQuestionButtons();
            }

            _model.IncrementQuestionsAnswered();
            _model.UpdateScoreData();
            _model.UpdateProperties();
        }


        /// <summary>
        /// Switches the content of the window to the main view.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event</param>
        private void ButtonReturnToMainMenuClicked(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Content = new MainView();
        }
    }
}
