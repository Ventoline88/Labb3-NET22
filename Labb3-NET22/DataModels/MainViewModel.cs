using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Labb3_NET22.DataModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The number of questions available in the categories that are checked.
        /// </summary>
        public int QuestionsInCheckedCategories { get; set; } = 0;

        // The event that is raised when a property changes.
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method to raise the property changed event.
        /// </summary>
        /// <param name="name">The name of the property that changed.</param>
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Goes through all the questions in the list of quizzes and increments the QuestionsInCheckedCategories variable
        /// if the question category is included in the list of categories provided.
        /// </summary>
        /// <param name="quizzes">The list of quizzes to go through.</param>
        /// <param name="categories">The categories to match.</param>
        public void UpdateQuestionsInCategories(List<Quiz> quizzes, List<Category> categories)
        {
            // Reset variable.
            QuestionsInCheckedCategories = 0;

            // Checks all the questions in all the quizzes if they match any of the checked categories.
            // If they do, increment the number of questions in the checked categories.
            foreach (Quiz quiz in quizzes)
            {
                foreach (Question question in quiz.Questions)
                {
                    if (categories.Contains(question.Category))
                    {
                        QuestionsInCheckedCategories++;
                    }
                }
            }

            UpdateProperties();
        }

        /// <summary>
        /// Specifies the properties that will update when they are changed.
        /// </summary>
        private void UpdateProperties()
        {
            OnPropertyChanged("QuestionsInCheckedCategories");
        }
    }
}
