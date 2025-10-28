using Labb3_NET22.DataModels;
using System;

namespace Labb3_NET22.Constants
{
    /// <summary>
    /// Class for variables and constants related to configuration of the application.
    /// </summary>
    internal static class ConfigurationConstants
    {
        /// <summary>
        /// The name of the file where quizzes are saved.
        /// </summary>
        public static string SaveFileName = "Quizzes.json";
        /// <summary>
        /// The name of the directory where the save file exists.
        /// </summary>
        public static string DirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        /// <summary>
        /// Path to the file where quizzes are saved.
        /// </summary>
        public static string SAVE_FILE_PATH = @$"{DirectoryPath}\{SaveFileName}";

        /// <summary>
        /// The name of the quiz created when answering questions by categories.
        /// </summary>
        public const string QUIZ_CATEGORIES_NAME = "Questions by category";

        /// <summary>
        /// The available categories in the game.
        /// </summary>
        public static readonly Category[] AVAILABLE_CATEGORIES = new Category[]
        {
            Category.Other,
            Category.Geography,
            Category.History,
            Category.Math,
            Category.Science,
            Category.Psychology,
            Category.Music,
            Category.Botany,
            Category.Biology,
            Category.Food,
            Category.Animal,
            Category.Sport,
            Category.Movie,
            Category.Religion,
            Category.Game,
            Category.Chemistry
        };
    }
}
