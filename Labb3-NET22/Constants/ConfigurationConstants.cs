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
        /// The default file types that the user is able to select
        /// when loading quiz data.
        /// </summary>
        public const string LOAD_DATA_DEFAULT_FILTER = "JSON files (*.json)|*.json";
        /// <summary>
        /// The default title of the dialog box when loading quiz data.
        /// </summary>
        public const string LOAD_DATA_DEFAULT_TITLE = "Open Quiz Data";
        /// <summary>
        /// The name of the default quiz.
        /// </summary>
        public const string DEFAULT_QUIZ_TITLE = "Default Quiz";

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
            Category.Chemistry,
            Category.Literature
        };

        /// <summary>
        /// The questions that the default quiz contains.
        /// </summary>
        public static readonly Question[] DEFAULT_QUIZ_QUESTIONS =
        {
            new Question(
                "In 1768, Captain James Cook set out to explore which ocean?",
                new string[]
                {
                    "Pacific Ocean",
                    "Atlantic Ocean",
                    "Indian Ocean"
                },
                0,
                "",
                Category.History),
            new Question(
                "What is actually electricity?",
                new string[]
                {
                    "A flow of water",
                    "A flow of air",
                    "A flow of electrons"
                },
                2,
                "",
                Category.Science),
            new Question(
                "Which of the following is not an international organization?",
                new string[]
                {
                    "FIFA",
                    "NATO",
                    "FBI"
                },
                2,
                "",
                Category.Other),
            new Question(
                "Which of the following disorders is the fear of being alone?",
                new string[]
                {
                    "Agoraphobia",
                    "Aerophobia",
                    "Monophobia"
                },
                2,
                "",
                Category.Psychology),
            new Question(
                "Which of the following is a song by the German heavy metal band “Scorpions”?",
                new string[]
                {
                    "Stairway to Heaven",
                    "Wind of Change",
                    "Don't stop me now"
                },
                1,
                "",
                Category.Music),
            new Question(
                "What is the speed of sound?",
                new string[]
                {
                    "120 km/h",
                    "1200 km/h",
                    "400 km/h"
                },
                1,
                "",
                Category.Science),
            new Question(
                "Which is the easiest way to tell the age of many trees?",
                new string[]
                {
                    "To measure the width of the tree",
                    "To count the rings on the trunk",
                    "To count the number of leaves"
                },
                1,
                "",
                Category.Botany),
            new Question(
                "What do we call a newly hatched butterfly?",
                new string[]
                {
                    "A moth",
                    "A butter",
                    "A caterpillar"
                },
                1,
                "",
                Category.Biology),
            new Question(
                "In total, how many novels were written by the Bronte sisters?",
                new string[]
                {
                    "5",
                    "6",
                    "7"
                },
                2,
                "",
                Category.Literature),
            new Question(
                "Which did Viking people use as money?",
                new string[]
                {
                    "Rune stones",
                    "Jewellery",
                    "Seal skins"
                },
                1,
                "",
                Category.History),
            new Question(
                "What was the first country to use tanks in combat during World War I?",
                new string[]
                {
                    "France",
                    "Britain",
                    "Germany"
                },
                1,
                "",
                Category.History),
            new Question(
                "What is the main component of the sun?",
                new string[]
                {
                    "Gas",
                    "Molten iron",
                    "Rock"
                },
                0,
                "",
                Category.Science),
            new Question(
                "Goulash is a type of beef soup in which country?",
                new string[]
                {
                    "Hungary",
                    "Czech Republic",
                    "Slovakia"
                },
                0,
                "",
                Category.Food),
            new Question(
                "Which two months are named after Emperors of the Roman Empire?",
                new string[]
                {
                    "March and April",
                    "May and June",
                    "July and August"
                },
                2,
                "",
                Category.History),
            new Question(
                "Which of the following animals can run the fastest?",
                new string[]
                {
                    "Cheetah",
                    "Leopard",
                    "Tiger"
                },
                0,
                "",
                Category.Animal),
            new Question(
                "Where did the powers of Spiderman come from?",
                new string[]
                {
                    "He was born with them",
                    "He was bitten by a radioactive spider",
                    "He went through a science experiment"
                },
                1,
                "",
                Category.Other),
            new Question(
                "What is the most points that a player can score with a single throw in darts?",
                new string[]
                {
                    "20",
                    "40",
                    "60"
                },
                2,
                "",
                Category.Sport),
            new Question(
                "In the United States, football is called soccer. So what is American football called in the United Kingdom?",
                new string[]
                {
                    "Rugby",
                    "American football",
                    "Handball"
                },
                1,
                "",
                Category.Sport),
            new Question(
                "Which of the following actors was the first one to play James Bond?",
                new string[]
                {
                    "Timothy Dalton",
                    "Roger Moore",
                    "Sean Connery"
                },
                2,
                "",
                Category.Movie),
            new Question(
                "Which of the following songs was the top-selling hit in 2019?",
                new string[]
                {
                    "Someone You Loved",
                    "Old Town Road",
                    "I Don't Care"
                },
                0,
                "",
                Category.Music),
            new Question(
                "In which country is Transylvania?",
                new string[]
                {
                    "Bulgaria",
                    "Romania",
                    "Croatia"
                },
                1,
                "",
                Category.Geography),
            new Question(
                "Which football club does Jordan Henderson play for?",
                new string[]
                {
                    "Liverpool",
                    "Manchester City",
                    "Tottenham Hotspur"
                },
                0,
                "",
                Category.Sport),
            new Question(
                "The two biggest exporters of beers in Europe are Germany and …",
                new string[]
                {
                    "Spain",
                    "France",
                    "Belgium"
                },
                2,
                "",
                Category.Food),
            new Question(
                "The phrase: ”I think, therefore I am” was coined by which philosopher?",
                new string[]
                {
                    "Socrates",
                    "Plato",
                    "Descartes"
                },
                2,
                "",
                Category.History),
            new Question(
                "In the series “Game of Thrones”, Winterfell is the ancestral home of which family?",
                new string[]
                {
                    "The Lannisters",
                    "The Starks",
                    "The Tully's"
                },
                1,
                "",
                Category.Other),
            new Question(
                "Who is known as the Patron Saint of Spain?",
                new string[]
                {
                    "St Patrick",
                    "St Benedict",
                    "St James"
                },
                2,
                "",
                Category.Religion),
            new Question(
                "What does the term “SOS” commonly stand for?",
                new string[]
                {
                    "Save Our Ship",
                    "Save Our Seal",
                    "Save Our Souls"
                },
                2,
                "",
                Category.Other),
            new Question(
                "Which company is known for publishing the Mario video game?",
                new string[]
                {
                    "Xbox",
                    "Nintendo",
                    "SEGA"
                },
                1,
                "",
                Category.Game),
            new Question(
                "We often use sodium bicarbonate in the kitchen. What is its other name?",
                new string[]
                {
                    "Sugar",
                    "Salt",
                    "Baking Soda"
                },
                2,
                "",
                Category.Chemistry),
            new Question(
                "Which was the first film by Disney to be produced in colour?",
                new string[]
                {
                    "Sleeping Beauty",
                    "Snow White and the Seven Dwarfs",
                    "Cinderella"
                },
                1,
                "",
                Category.Movie),
            new Question(
                "What is the name of the first book of the Old Testament in the Bible?",
                new string[]
                {
                    "Exodus",
                    "Genesis",
                    "Matthew"
                },
                1,
                "",
                Category.Religion),
            new Question(
                "Neil Armstrong was the first astronaut in the world to step foot on the moon. Who was the second?",
                new string[]
                {
                    "Yuri Gagarin",
                    "Alan Bean",
                    "Buzz Aldrin"
                },
                2,
                "",
                Category.History),
            new Question(
                "How many time zones are there in total in the world?",
                new string[]
                {
                    "8",
                    "16",
                    "24"
                },
                2,
                "",
                Category.Other),
            new Question(
                "What is the rarest type of blood in the human body?",
                new string[]
                {
                    "AB negative",
                    "O positive",
                    "B negative"
                },
                0,
                "",
                Category.Biology),
            new Question(
                "Cu is the chemical symbol for which element?",
                new string[]
                {
                    "Copper",
                    "Zinc",
                    "Helium"
                },
                0,
                "",
                Category.Chemistry)
        };
    }
}
