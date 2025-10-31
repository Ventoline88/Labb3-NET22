using Labb3_NET22.Constants;
using Labb3_NET22.DataModels;
using Microsoft.Win32;
using System;
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

            AddDefaultQuizIfNeeded();

            lstBoxAvailableQuizzes.DisplayMemberPath = "Title";
            lstBoxAvailableQuizzes.SelectedValuePath = "Questions";
            lstBoxAvailableQuizzes.ItemsSource = _quizzes;

            lstBoxQuizQuestions.DisplayMemberPath = "Statement";
            lstBoxQuizQuestions.ItemsSource = _questions;

            InitializeCategoriesCheckBoxes();
        }

        /// <summary>
        /// Plays the selected quiz by switching to a play view with the selected quiz.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event.</param>
        private void ButtonPlayClicked(object sender, RoutedEventArgs e)
        {
            Quiz selectedQuiz = (Quiz)lstBoxAvailableQuizzes.SelectedItem;

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

            foreach (var child in wrapPanelCategories.Children)
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
            var selectedQuiz = (Quiz)lstBoxAvailableQuizzes.SelectedItem;

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
            var selectedQuiz = (Quiz)lstBoxAvailableQuizzes.SelectedItem;

            if (selectedQuiz == null)
            {
                MessageBox.Show(MessageConstants.PROMPT_SELECT_QUIZ_TO_REMOVE);
            }
            else
            {
                lstBoxAvailableQuizzes.SelectedItem = null;
                lstBoxQuizQuestions.SelectedItem = null;
                _quizzes.Remove(selectedQuiz);
                _questions.Clear();
                lstBoxAvailableQuizzes.Items.Refresh();
                lstBoxQuizQuestions.Items.Refresh();
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
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            try
            {
                await File.WriteAllTextAsync(_fileName, JsonSerializer.Serialize(_quizzes, jsonOptions));
                MessageBox.Show(MessageConstants.MESSAGE_QUIZZES_SAVED);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MessageConstants.MESSAGE_ERROR_WHILE_SAVING_QUIZ_DATA);
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Asynchronously loads the quizzes from the specified file using JSON.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event</param>
        private async void ButtonLoadQuizzesClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    FileName = ConfigurationConstants.SaveFileName,
                    Filter = ConfigurationConstants.LOAD_DATA_DEFAULT_FILTER,
                    Title = ConfigurationConstants.LOAD_DATA_DEFAULT_TITLE,
                    InitialDirectory = ConfigurationConstants.DirectoryPath
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    var filePath = openFileDialog.FileName;

                    _quizzes.Clear(); // Clears the quizzes to avoid loading the same quizzes several times.
                    List<Quiz>? loadedQuizzes = new List<Quiz>();

                    using FileStream fileStream = File.OpenRead(filePath); // Ensures that the file stream is closed at the end.
                    loadedQuizzes = await JsonSerializer.DeserializeAsync<List<Quiz>>(fileStream);

                    if (loadedQuizzes != null)
                    {
                        foreach (Quiz q in loadedQuizzes)
                        {
                            _quizzes.Add(q);
                        }
                    }

                    AddDefaultQuizIfNeeded();

                    lstBoxAvailableQuizzes.Items.Refresh();
                    InitializeCategoriesCheckBoxes();
                    MessageBox.Show(MessageConstants.MESSAGE_QUIZZES_LOADED);
                }
                else
                {
                    MessageBox.Show(MessageConstants.MESSAGE_SAVE_FILE_NOT_FOUND);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(MessageConstants.MESSAGE_ERROR_WHILE_LOADING_QUIZ_DATA);
                MessageBox.Show(ex.Message);
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
            wrapPanelCategories.Children.Clear();  // Clears the checkboxes from the stack panel to ensure that
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
                wrapPanelCategories.Children.Add(checkBox);
            }
        }

        /// <summary>
        /// Updates the list of questions based on the selected quiz.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The evelnt.</param>
        private void QuizChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBoxAvailableQuizzes.SelectedItem != null)
            {
                _questions = _quizzes[lstBoxAvailableQuizzes.SelectedIndex].Questions.ToList();
                lstBoxQuizQuestions.ItemsSource = _questions;
            }
        }

        /// <summary>
        /// Adds the defaults quiz to the list of quizzes
        /// if it is not already in the list.
        /// Only adds the default quiz if there is no quiz with a title
        /// that contains the default quiz name.
        /// </summary>
        private void AddDefaultQuizIfNeeded()
        {
            if (_quizzes.Count == 0)
            {
                AddDefaultQuiz();
            }
            else if (_quizzes.Count > 0)
            {
                var defaultQuizList = _quizzes.Where(q => q.Title.Contains(ConfigurationConstants.DEFAULT_QUIZ_TITLE));

                if (defaultQuizList.Count() == 0)
                {
                    AddDefaultQuiz();
                }
            }
        }

        /// <summary>
        /// Adds the default quiz to the list of quizzes.
        /// </summary>
        private void AddDefaultQuiz()
        {
            Quiz defaultQuiz = new Quiz();
            defaultQuiz.Title = ConfigurationConstants.DEFAULT_QUIZ_TITLE;

            foreach (Question question in ConfigurationConstants.DEFAULT_QUIZ_QUESTIONS)
            {
                defaultQuiz.AddQuestion(question);
            }

            _quizzes.Add(defaultQuiz);
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
