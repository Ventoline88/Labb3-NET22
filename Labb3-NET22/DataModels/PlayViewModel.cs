using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace Labb3_NET22.DataModels
{
    /// <summary>
    /// The model used when the quiz game is played.
    /// </summary>
    internal class PlayViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The quiz being played.
        /// </summary>
        public Quiz Quiz { get; private set; }
        /// <summary>
        /// The current question being displayed.
        /// </summary>
        public Question CurrentQuestion { get; private set; }
        /// <summary>
        /// The percentage of correct answers.
        /// </summary>
        public double PercentageCorrect { get; private set; } = 0;
        /// <summary>
        /// The number of correct answers.
        /// </summary>
        public int NumberCorrectAnswers { get; private set; } = 0;
        /// <summary>
        /// The number of questions answered.
        /// </summary>
        public int QuestionsAnswered { get; private set; } = 0;
        /// <summary>
        /// The image of the question.
        /// </summary>
        public BitmapImage CurrentImage { get; private set; }

        /// <summary>
        /// Creates a new view model based on the specified Quiz.
        /// </summary>
        /// <param name="quiz">The quiz to base the view model on.</param>
        public PlayViewModel(Quiz quiz)
        {
            Quiz = quiz;
            SetUpQuestion();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Sets up the next question to be answered.
        /// Gets a new random available question from the quiz.
        /// Sets the image to the image of the question if it can be found,
        /// otherwise a default image will be used.
        /// Sets up all the properties that will change.
        /// </summary>
        public void SetUpQuestion()
        {
            if (Quiz.GetRandomQuestionsRemaining() > 0)
            {
                CurrentQuestion = Quiz.GetRandomQuestion();

                if (!File.Exists(CurrentQuestion.ImagePath))
                {
                    CurrentImage = new BitmapImage(new Uri("pack://application:,,,/Images/QuestionMark.jpg"));
                }
                else
                {
                    CurrentImage = new BitmapImage(new Uri(CurrentQuestion.ImagePath));
                }

                UpdateProperties();
            }
        }

        /// <summary>
        /// Specifies the properties that will update when they are changed.
        /// </summary>
        public void UpdateProperties()
        {
            OnPropertyChanged("CurrentQuestion");
            OnPropertyChanged("PercentageCorrect");
            OnPropertyChanged("NumberCorrectAnswers");
            OnPropertyChanged("QuestionsAnswered");
            OnPropertyChanged("CurrentImage");
        }

        /// <summary>
        /// Updates the percentage of correct answers.
        /// </summary>
        public void UpdateScoreData()
        {
            if (QuestionsAnswered == 0)
            {
                PercentageCorrect = 0;
            }
            else
            {
                PercentageCorrect =
                    Math.Round(((double)NumberCorrectAnswers / QuestionsAnswered) * 100, 2);
            }
        }

        /// <summary>
        /// Increments the count of correct answers by one.
        /// </summary>
        public void IncrementCorrectAnswers()
        {
            NumberCorrectAnswers++;
        }

        /// <summary>
        /// Increments the count of questions answered by one.
        /// </summary>
        public void IncrementQuestionsAnswered()
        {
            QuestionsAnswered++;
        }
    }
}
