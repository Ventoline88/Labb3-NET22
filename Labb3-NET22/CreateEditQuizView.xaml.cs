using Labb3_NET22.Constants;
using Labb3_NET22.DataModels;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Labb3_NET22
{
    /// <summary>
    /// Interaction logic for CreateEditQuizView.xaml
    /// </summary>
    public partial class CreateEditQuizView : UserControl
    {
        /// <summary>
        /// The quiz to create or edit.
        /// </summary>
        private Quiz _quiz;
        /// <summary>
        /// The current question being processed.
        /// </summary>
        private Question? _currentQuestion;
        /// <summary>
        /// Field to show if a quiz is being created or edited.
        /// </summary>
        private QuizModificationMode _mode;

        /// <summary>
        /// Creates a new view where a quiz can be created or edited depending
        /// on the specified value.
        /// </summary>
        /// <param name="quiz">The quiz to edit, null if a new quiz is created.</param>
        public CreateEditQuizView(Quiz? quiz)
        {
            InitializeComponent();

            if (quiz != null) // Quiz exists, we want to edit it.
            {
                _quiz = quiz;
                txtBoxQuizName.Text = _quiz.Title;
                _mode = QuizModificationMode.Edit;
                labelEditQuiz.Visibility = Visibility.Visible;

                // There is no functionality for discarding changes,
                // so the cancel button should not be showing or be interactable.
                buttonCancel.IsEnabled = false;
                buttonCancel.Visibility = Visibility.Hidden;
            }
            else // Quiz doesn't exist, we want to create it.
            {
                _quiz = new Quiz();
                _mode = QuizModificationMode.Create;
                labelCreateNewQuiz.Visibility = Visibility.Visible;
            }

            InitializeCategoryComboBox();
            InitializeQuestionListBox();
        }

        /// <summary>
        /// Initializes the questions in the quiz, if they exist.
        /// </summary>
        private void InitializeQuestionListBox()
        {
            lstBoxQuizQuestions.DisplayMemberPath = "Statement";
            lstBoxQuizQuestions.SelectedValuePath = "Statement";
            lstBoxQuizQuestions.ItemsSource = _quiz.Questions;
        }

        /// <summary>
        /// Initializes the categories combobox with all the available categories.
        /// </summary>
        private void InitializeCategoryComboBox()
        {
            foreach (Category category in ConfigurationConstants.AVAILABLE_CATEGORIES)
            {
                comboBoxCategories.Items.Add(category);
            }
            comboBoxCategories.SelectedItem = Category.Other;
        }

        /// <summary>
        /// Validates that the quiz is well formed.
        /// If it is, the quiz is added to the list of quizzes in the main view.
        /// The content of the window is then switched to the main view.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event.</param>
        private void ButtonConfirmQuizClicked(object sender, RoutedEventArgs e)
        {
            if (!ValidateQuiz())
            {
                MessageBox.Show(MessageConstants.MESSAGE_QUIZ_REQUIREMENT);
            }
            else
            {
                _quiz.Title = txtBoxQuizName.Text;

                if (_mode == QuizModificationMode.Create)
                {
                    MainView.AddQuiz(_quiz);
                }

                Window window = Window.GetWindow(this);
                window.Content = new MainView();
            }
        }

        /// <summary>
        /// Validates that the quiz has all the required properties set.
        /// </summary>
        /// <returns>True if the quiz is well formed, false otherwise.</returns>
        private bool ValidateQuiz()
        {
            return
                ValidateTitle() &&
                ValidateQuestions();
        }

        /// <summary>
        /// Validates that a title is set to something that is not null or empty.
        /// </summary>
        /// <returns>True if the title text field is not null or empty, false otherwise.</returns>
        private bool ValidateTitle()
        {
            return !string.IsNullOrEmpty(txtBoxQuizName.Text);
        }

        /// <summary>
        /// Validates that at least one question exists.
        /// </summary>
        /// <returns>True if there is at least on question in the quiz, false otherwise.</returns>
        private bool ValidateQuestions()
        {
            return lstBoxQuizQuestions.Items.Count > 0;
        }

        /// <summary>
        /// Switches the content of the window to a main view.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event.</param>
        private void ButtonCancelClicked(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Content = new MainView();
        }

        /// <summary>
        /// Validates that a question is well formed and adds it to the list of 
        /// questions for the current quiz.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event.</param>
        private void ButtonAddQuestionClicked(object sender, RoutedEventArgs e)
        {
            if (!ValidateQuestion())
            {
                MessageBox.Show(MessageConstants.MESSAGE_QUESTION_REQUIREMENTS);
            }
            else
            {
                int correctAnswerIndex = GetCorrectAnswerIndex();

                _quiz.AddQuestion(
                    txtBoxQuestionText.Text,
                    correctAnswerIndex,
                    txtBoxImagePath.Text,
                    (Category)comboBoxCategories.SelectedItem,
                    txtBoxAnswer1.Text,
                    txtBoxAnswer2.Text,
                    txtBoxAnswer3.Text);

                MessageBox.Show(MessageConstants.MESSAGE_QUESTION_ADDED);
                ResetView();
            }
        }

        /// <summary>
        /// Modifies a selected question in the current quiz.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event.</param>
        private void ButtonModifyQuestionClicked(object sender, RoutedEventArgs e)
        {
            if (_currentQuestion != null)
            {
                ModifyQuestion(_currentQuestion);
                _currentQuestion = null;
                lstBoxQuizQuestions.SelectedItem = null;

                MessageBox.Show(MessageConstants.MESSAGE_QUESTION_MODIFIED);
                ResetView();
            }
            else
            {
                MessageBox.Show(MessageConstants.MESSAGE_NO_QUESTION_SELECTED);
            }
        }

        /// <summary>
        /// Validates that a question is well formed and updates 
        /// its properties to the new values.
        /// </summary>
        /// <param name="currentQuestion">The question to modify.</param>
        private void ModifyQuestion(Question currentQuestion)
        {
            if (!ValidateQuestion())
            {
                MessageBox.Show(MessageConstants.MESSAGE_QUESTION_REQUIREMENTS);
            }
            else
            {
                int correctAnswerIndex = GetCorrectAnswerIndex();
                Category category = (Category)comboBoxCategories.SelectedItem;

                currentQuestion.Statement = txtBoxQuestionText.Text;
                currentQuestion.Answers = new string[]
                {
                    txtBoxAnswer1.Text,
                    txtBoxAnswer2.Text,
                    txtBoxAnswer3.Text
                };
                currentQuestion.CorrectAnswerIndex = correctAnswerIndex;
                currentQuestion.Category = category;
                currentQuestion.ImagePath = txtBoxImagePath.Text;
            }
        }

        /// <summary>
        /// Removes a selected question from the quiz.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event.</param>
        private void ButtonRemoveQuestionClicked(object sender, RoutedEventArgs e)
        {
            if (_currentQuestion != null)
            {
                RemoveQuestion(_currentQuestion);
                _currentQuestion = null;
                lstBoxQuizQuestions.SelectedItem = null;

                MessageBox.Show(MessageConstants.MESSAGE_QUESTION_REMOVED);
                ResetView();
            }
            else
            {
                MessageBox.Show(MessageConstants.MESSAGE_NO_QUESTION_SELECTED);
            }
        }

        /// <summary>
        /// Removes the current question from the quiz.
        /// </summary>
        /// <param name="currentQuestion">The question to remove.</param>
        private void RemoveQuestion(Question currentQuestion)
        {
            _quiz.RemoveQuestion(GetIndexOfQuestion(_currentQuestion));
        }

        /// <summary>
        /// Gets the index of the current question from the list of questions in the quiz.
        /// </summary>
        /// <param name="currentQuestion">The question to find the index of.</param>
        /// <returns>The index of the specified question in the list of questions in the quiz.</returns>
        private int GetIndexOfQuestion(Question? currentQuestion)
        {
            if (currentQuestion != null)
            {
                for (int i = 0; i < _quiz.Questions.Count(); i++)
                {
                    if (currentQuestion.Statement == _quiz.Questions.ElementAt(i).Statement)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Resets the view by clearing all the text fields,
        /// setting the correct answer radio button and the
        /// categories combobox value to default values.
        /// </summary>
        private void ResetView()
        {
            ResetTextFields();
            ResetAnswerRadioButtons();
            ResetCategoryComboBox();
            ResetButtons();
            lstBoxQuizQuestions.Items.Refresh();
        }

        /// <summary>
        /// Resets all text fileds by clearing them.
        /// </summary>
        private void ResetTextFields()
        {
            txtBoxQuestionText.Text = string.Empty;
            txtBoxAnswer1.Text = string.Empty;
            txtBoxAnswer2.Text = string.Empty;
            txtBoxAnswer3.Text = string.Empty;
            txtBoxImagePath.Text = string.Empty;
        }

        /// <summary>
        /// Resets the correct answer radio buttons by
        /// setting the first one to checked and the rest
        /// to unchecked.
        /// </summary>
        private void ResetAnswerRadioButtons()
        {
            radioBtnAnswer1.IsChecked = true;
            radioBtnAnswer2.IsChecked = false;
            radioBtnAnswer3.IsChecked = false;
        }

        /// <summary>
        /// Resets the categories combobox by setting the value
        /// to unknown.
        /// </summary>
        private void ResetCategoryComboBox()
        {
            comboBoxCategories.SelectedItem = Category.Other;
        }

        /// <summary>
        /// Resets buttons that should not be available when a question is not selected
        /// by disabling them.
        /// </summary>
        private void ResetButtons()
        {
            buttonModifyQuestion.IsEnabled = false;
            buttonRemoveQuestion.IsEnabled = false;
        }

        /// <summary>
        /// Displays the properties of the selected question
        /// in the relevant fields.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event.</param>
        private void QuestionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentQuestion = (Question)lstBoxQuizQuestions.SelectedItem;

            if (_currentQuestion != null)
            {
                txtBoxQuestionText.Text = _currentQuestion.Statement;
                txtBoxAnswer1.Text = _currentQuestion.Answers[0];
                txtBoxAnswer2.Text = _currentQuestion.Answers[1];
                txtBoxAnswer3.Text = _currentQuestion.Answers[2];

                switch (_currentQuestion.CorrectAnswerIndex)
                {
                    case 0:
                        radioBtnAnswer1.IsChecked = true;
                        break;

                    case 1:
                        radioBtnAnswer2.IsChecked = true;
                        break;

                    case 2:
                        radioBtnAnswer3.IsChecked = true;
                        break;

                    default:
                        radioBtnAnswer1.IsChecked = true;
                        break;
                }

                comboBoxCategories.SelectedItem = _currentQuestion.Category;
                txtBoxImagePath.Text = _currentQuestion.ImagePath;

                buttonModifyQuestion.IsEnabled = true;
                buttonRemoveQuestion.IsEnabled = true;
            }
        }

        /// <summary>
        /// Validates a question by checking thatiit has a 
        /// statment and answers.
        /// </summary>
        /// <returns>True if the question is well formed, false otherwise.</returns>
        private bool ValidateQuestion()
        {
            return
                ValidateQuestionText() &&
                ValidateAnswerFields();
        }

        /// <summary>
        /// Validates that the question text is not null or empty.
        /// </summary>
        /// <returns></returns>
        private bool ValidateQuestionText()
        {
            return !string.IsNullOrEmpty(txtBoxQuestionText.Text);
        }

        /// <summary>
        /// Validates that the answers are not null or empty.
        /// </summary>
        /// <returns>True if all the answer text fields are not null or empty.</returns>
        private bool ValidateAnswerFields()
        {
            return
                !string.IsNullOrEmpty(txtBoxAnswer1.Text) &&
                !string.IsNullOrEmpty(txtBoxAnswer2.Text) &&
                !string.IsNullOrEmpty(txtBoxAnswer3.Text);
        }

        /// <summary>
        /// Gets the index of the correct answer for the question by
        /// checking which radio button is checked.
        /// </summary>
        /// <returns>The index corresponding to the checked radio button.</returns>
        private int GetCorrectAnswerIndex()
        {
            if (radioBtnAnswer1.IsChecked == true)
            {
                return 0;
            }
            else if (radioBtnAnswer2.IsChecked == true)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }

    /// <summary>
    /// Enum representing the mode of the view.
    /// </summary>
    public enum QuizModificationMode
    {
        Create,
        Edit
    }
}
