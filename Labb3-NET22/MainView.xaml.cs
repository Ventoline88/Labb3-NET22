using Labb3_NET22.Constants;
using Labb3_NET22.DataModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace Labb3_NET22
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        /// <summary>
        /// The list of quizzes currently available.
        /// </summary>
        private static List<Quiz> _quizzes = new List<Quiz>();
        /// <summary>
        /// The list of questions in a selected quiz.
        /// </summary>
        private List<Question> _questions = new List<Question>();
        /// <summary>
        /// The path to the file where the data is saved.
        /// </summary>
        private string _fileName = ConfigurationConstants.SAVE_FILE_PATH;
        /// <summary>
        /// List of categories that the user has checked.
        /// </summary>
        private List<Category> _checkedCategories = new List<Category>();

        /// <summary>
        /// Creates a new main view.
        /// </summary>
        public MainView()
        {
            InitializeComponent();

            LstBoxAvailableQuizzes.DisplayMemberPath = "Title";
            LstBoxAvailableQuizzes.SelectedValuePath = "Questions";
            LstBoxAvailableQuizzes.ItemsSource = _quizzes;

            LstBoxQuizQuestions.DisplayMemberPath = "Statement";
            LstBoxQuizQuestions.ItemsSource = _questions;

            InitializeCategoriesCheckBoxes();
        }

        /// <summary>
        /// Plays the selected quiz by switching to a play view with the selected quiz.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event.</param>
        private void ButtonPlayClicked(object sender, RoutedEventArgs e)
        {
            Quiz selectedQuiz = (Quiz)LstBoxAvailableQuizzes.SelectedItem;

            if (selectedQuiz == null)
            {
                MessageBox.Show(MessageConstants.PROMPT_SELECT_QUIZ_TO_PLAY);
            }
            else
            {
                Window window = Window.GetWindow(this);
                window.Content = new PlayView(selectedQuiz);
            }
        }

        /// <summary>
        /// Creates a temporary quiz based on the selected categories and plays this
        /// by switching to a play view with the created quiz.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event</param>
        private void ButtonQuestionsByCategoryClicked(object sender, RoutedEventArgs e)
        {
            GetCheckedCategories();

            if (_checkedCategories.Count <= 0)
            {
                MessageBox.Show(MessageConstants.PROMPT_SELECT_CATEGORIES);
            }
            else
            {
                Quiz categoryQuiz = GetCategoryQuiz();

                Window window = Window.GetWindow(this);
                window.Content = new PlayView(categoryQuiz);
            }
        }

        /// <summary>
        /// Gets all the categories that are checked.
        /// </summary>
        private void GetCheckedCategories()
        {
            List<CheckBox> checkBoxes = new List<CheckBox>();

            foreach (var child in StackPanelCategories.Children)
            {
                CheckBox checkBox = (CheckBox)child;
                if (checkBox.IsChecked == true)
                {
                    _checkedCategories.Add((Category)checkBox.Content);
                }
            }
        }

        /// <summary>
        /// Creates and returns a new quiz based on the checked categories.
        /// </summary>
        /// <returns>
        /// A temporary quiz with questions from all other quizzes that match the 
        /// checked categories.
        /// </returns>
        private Quiz GetCategoryQuiz()
        {
            string quizName = ConfigurationConstants.QUIZ_CATEGORIES_NAME;
            Quiz categoryQuiz = new Quiz(quizName, new List<Question>());

            foreach (Quiz quiz in _quizzes)
            {
                foreach (Question question in quiz.Questions)
                {
                    if (_checkedCategories.Contains(question.Category))
                    {
                        categoryQuiz.AddQuestion(question);
                    }
                }
            }

            return categoryQuiz;
        }


        /// <summary>
        /// Edits the selected quiz by switching to a edit view with the 
        /// specified quiz.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event</param>
        private void ButtonEditQuizClicked(object sender, RoutedEventArgs e)
        {
            var selectedQuiz = (Quiz)LstBoxAvailableQuizzes.SelectedItem;

            if (selectedQuiz == null)
            {
                MessageBox.Show(MessageConstants.PROMPT_SELECT_QUIZ_TO_EDIT);
            }
            else
            {
                Window window = Window.GetWindow(this);
                window.Content = new CreateEditQuizView(selectedQuiz);
            }
        }

        /// <summary>
        /// Removes the selected quiz.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event.</param>
        private void ButtonRemoveQuizClicked(object sender, RoutedEventArgs e)
        {
            var selectedQuiz = (Quiz)LstBoxAvailableQuizzes.SelectedItem;

            if (selectedQuiz == null)
            {
                MessageBox.Show(MessageConstants.PROMPT_SELECT_QUIZ_TO_REMOVE);
            }
            else
            {
                LstBoxAvailableQuizzes.SelectedItem = null;
                LstBoxQuizQuestions.SelectedItem = null;
                _quizzes.Remove(selectedQuiz);
                _questions.Clear();
                LstBoxAvailableQuizzes.Items.Refresh();
                LstBoxQuizQuestions.Items.Refresh();
                InitializeCategoriesCheckBoxes();
                MessageBox.Show(MessageConstants.MESSAGE_QUIZ_REMOVED);
            }
        }

        /// <summary>
        /// Creates a new quiz by switching to a create view.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event</param>
        private void ButtonCreateNewQuizClicked(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Content = new CreateEditQuizView(null);
        }


        /// <summary>
        /// Asynchronously saves the quizzes to the specified file using JSON.
        /// The previous data in the file is overwritten.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event</param>
        private async void ButtonSaveQuizzesClicked(object sender, RoutedEventArgs e)
        {
            Directory.CreateDirectory(@"C:\QuizData");
            await File.WriteAllTextAsync(_fileName, JsonSerializer.Serialize(_quizzes));
            MessageBox.Show(MessageConstants.MESSAGE_QUIZZES_SAVED);
        }


        /// <summary>
        /// Asynchronously loads the quizzes from the specified file using JSON.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event</param>
        private async void ButtonLoadQuizzesClicked(object sender, RoutedEventArgs e)
        {
            if (File.Exists(_fileName))
            {
                _quizzes.Clear(); // Clears the quizzes to avoid loading the same quizzes several times.
                using FileStream fileStream = File.OpenRead(_fileName); // Ensures that the file stream is closed at the end.
                var loadedQuizzes = await JsonSerializer.DeserializeAsync<List<Quiz>>(fileStream);

                foreach (Quiz q in loadedQuizzes)
                {
                    _quizzes.Add(q);
                }

                LstBoxAvailableQuizzes.Items.Refresh();
                InitializeCategoriesCheckBoxes();
                MessageBox.Show(MessageConstants.MESSAGE_QUIZZES_LOADED);
            }
            else
            {
                MessageBox.Show(MessageConstants.MESSAGE_SAVE_FILE_NOT_FOUND);
            }
        }


        /// <summary>
        /// Shuts down the application.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event</param>
        private void ButtonExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Initializes the categories checkboxes based on the categories of the questions
        /// in all of the created quizzes.
        /// </summary>
        private void InitializeCategoriesCheckBoxes()
        {
            StackPanelCategories.Children.Clear();  // Clears the checkboxes from the stack panel to ensure that
                                                    // the same categories are not loaded several times.
            List<Category> availableCategories = new List<Category>();

            // Go through all the questions in all the quizzes and add
            // the catagories to the available categories.
            // Does not add the same category twice.
            foreach (Quiz quiz in _quizzes)
            {
                foreach (Question question in quiz.Questions)
                {
                    if (!availableCategories.Contains(question.Category))
                    {
                        availableCategories.Add(question.Category);
                    }
                }
            }

            // Display all the categories as checkboxes.
            foreach (Category category in availableCategories)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = category;
                StackPanelCategories.Children.Add(checkBox);
            }
        }

        /// <summary>
        /// Updates the list of questions based on the selected quiz.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The evelnt.</param>
        private void QuizChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LstBoxAvailableQuizzes.SelectedItem != null)
            {
                _questions = _quizzes[LstBoxAvailableQuizzes.SelectedIndex].Questions.ToList();
                LstBoxQuizQuestions.ItemsSource = _questions;
            }
        }

        /// <summary>
        /// Adds a quiz to the list of quizzes.
        /// </summary>
        /// <param name="quiz">The quiz to add.</param>
        public static void AddQuiz(Quiz quiz)
        {
            _quizzes.Add(quiz);
        }

        /// <summary>
        /// Gets the list of available quizzes.
        /// </summary>
        /// <returns>The list of the available quizzes.</returns>
        public static List<Quiz> GetQuizzes()
        {
            return _quizzes;
        }
    }
}
