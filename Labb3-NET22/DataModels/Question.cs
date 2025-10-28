namespace Labb3_NET22.DataModels;

/// <summary>
/// Class representing a question.
/// </summary>
public class Question
{
    /// <summary>
    /// The statement of the question.
    /// </summary>
    public string Statement { get; set; }
    /// <summary>
    /// The answers of the question.
    /// </summary>
    public string[] Answers { get; set; }
    /// <summary>
    /// The index of the correct answer.
    /// </summary>
    public int CorrectAnswerIndex { get; set; }
    /// <summary>
    /// The location of the associated image for this question.
    /// </summary>
    public string ImagePath { get; set; }
    /// <summary>
    /// The category of the question.
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// Creates a new Question with the specified values set.
    /// </summary>
    /// <param name="statement">The question statement.</param>
    /// <param name="answers">The answers for the question.</param>
    /// <param name="correctAnswerIndex">The index of the correct answer.</param>
    /// <param name="imagePath">The path to the associated image.</param>
    /// <param name="category">The category of the question.</param>
    public Question(string statement, string[] answers, int correctAnswerIndex, string imagePath, Category category)
    {
        Statement = statement;
        Answers = answers;
        CorrectAnswerIndex = correctAnswerIndex;
        ImagePath = imagePath;
        Category = category;
    }
}

/// <summary>
/// Enum representing the different categories a question can have.
/// </summary>
public enum Category
{
    Other,
    Geography,
    History,
    Math,
    Science,
    Psychology,
    Music,
    Botany,
    Biology,
    Food,
    Animal,
    Sport,
    Movie,
    Religion,
    Game,
    Chemistry
}