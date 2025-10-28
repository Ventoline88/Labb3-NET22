namespace Labb3_NET22.Constants
{
    /// <summary>
    /// Class for variables and constants related to messages and prompts.
    /// </summary>
    internal static class MessageConstants
    {
        /// <summary>
        /// Message to display to indicate what a quiz needs to be created.
        /// </summary>
        public const string MESSAGE_QUIZ_REQUIREMENT =
            "The quiz needs to have at least one question and a name.";
        /// <summary>
        /// Message to display to indicate what a question needs to be created.
        /// </summary>
        public const string MESSAGE_QUESTION_REQUIREMENTS =
            "A question needs to at least have a question statement and three answers.";
        /// <summary>
        /// Message to display when a question is added to a quiz.
        /// </summary>
        public const string MESSAGE_QUESTION_ADDED = "Question Added";
        /// <summary>
        /// Message to display when a question is modified.
        /// </summary>
        public const string MESSAGE_QUESTION_MODIFIED = "Question Modified";
        /// <summary>
        /// Message to display when no question is selected.
        /// </summary>
        public const string MESSAGE_NO_QUESTION_SELECTED = "No question selected.";
        /// <summary>
        /// Message to display when a question is removed from a quiz.
        /// </summary>
        public const string MESSAGE_QUESTION_REMOVED = "Question Removed";
        /// <summary>
        /// Message to display when a quiz is removed.
        /// </summary>
        public const string MESSAGE_QUIZ_REMOVED = "Quiz removed.";
        /// <summary>
        /// Message to display when a correct answer is given.
        /// </summary>
        public const string MESSAGE_ANSWER_CORRECT = "Correct";
        /// <summary>
        /// Message to display when an incorrect answer is given.
        /// </summary>
        public const string MESSAGE_ANSWER_INCORRECT = "Incorrect";
        /// <summary>
        /// Message to display when the save file was not found.
        /// </summary>
        public const string MESSAGE_SAVE_FILE_NOT_FOUND = "Could not find any quiz data to load";

        /// <summary>
        /// Message to display when saving quizzes.
        /// </summary>
        public static string MESSAGE_QUIZZES_SAVED = $"Quizzes saved to {ConfigurationConstants.SAVE_FILE_PATH}.";
        /// <summary>
        /// Message to display when loading quizzes.
        /// </summary>
        public static string MESSAGE_QUIZZES_LOADED = $"Quizzes loaded from {ConfigurationConstants.SAVE_FILE_PATH}.";

        /// <summary>
        /// Prompt to display getting the user to select a quiz to play.
        /// </summary>
        public const string PROMPT_SELECT_QUIZ_TO_PLAY = "Select a quiz to play.";
        /// <summary>
        /// Prompt to display when getting the user to select one or more categories.
        /// </summary>
        public const string PROMPT_SELECT_CATEGORIES = "Select at least one category from the list.";
        /// <summary>
        /// Prompt to display when getting the user to select a quiz to edit.
        /// </summary>
        public const string PROMPT_SELECT_QUIZ_TO_EDIT = "Select a quiz to edit.";
        /// <summary>
        /// Prompt to display when getting the user to select a quiz to remove.
        /// </summary>
        public const string PROMPT_SELECT_QUIZ_TO_REMOVE = "Select a quiz to remove.";
    }
}
