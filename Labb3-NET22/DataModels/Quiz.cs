using System;
using System.Collections.Generic;
using System.Linq;

namespace Labb3_NET22.DataModels;

public class Quiz
{
    /// <summary>
    /// Backing field for the list of questions in this quiz.
    /// </summary>
    private IEnumerable<Question> _questions;
    /// <summary>
    /// Backing field for the title of the quiz.
    /// </summary>
    private string _title = string.Empty;
    /// <summary>
    /// Backing field for the list of random questions available.
    /// </summary>
    private IEnumerable<Question> _availableRandomQuestions = new List<Question>();
    /// <summary>
    /// Random generator for getting questions in a random order.
    /// </summary>
    private Random _random = new Random();

    /// <summary>
    /// The questions in the quiz.
    /// </summary>
    public IEnumerable<Question> Questions
    {
        get => _questions;
        set => _questions = value;
    }
    /// <summary>
    /// The title of the quiz.
    /// </summary>
    public string Title
    {
        get => _title;
        set => _title = value;
    }

    /// <summary>
    /// Creates a new quiz with an empty list of questions.
    /// </summary>
    public Quiz()
    {
        _questions = new List<Question>();
        InitializeRandomQuestions();
    }

    /// <summary>
    /// Creates a new quiz with the specified values.
    /// </summary>
    /// <param name="title">The title of the quiz.</param>
    /// <param name="questions">The questions of the quiz.</param>
    public Quiz(string title, IEnumerable<Question> questions)
    {
        Title = title;
        Questions = questions;
        InitializeRandomQuestions();
    }

    /// <summary>
    /// Gets a new random question from the list of available random questions.
    /// </summary>
    /// <returns>A random question from the available random questions.</returns>
    public Question GetRandomQuestion()
    {
        int randomIndex = _random.Next(_availableRandomQuestions.Count());
        Question randomQuestion = _availableRandomQuestions.ElementAt(randomIndex);
        ((List<Question>)_availableRandomQuestions).RemoveAt(randomIndex);
        return randomQuestion;
    }

    /// <summary>
    /// Adds a question to the list of questions in the quiz.
    /// </summary>
    /// <param name="statement">The statement of the question.</param>
    /// <param name="correctAnswerIndex">The index of the correct answer.</param>
    /// <param name="imagePath">The path to the image associated with the question.</param>
    /// <param name="category">The category of the question.</param>
    /// <param name="answers">The answers for the question.</param>
    public void AddQuestion(string statement, int correctAnswerIndex, string imagePath,
        Category category, params string[] answers)
    {
        ((List<Question>)_questions).Add(new Question(statement, answers, correctAnswerIndex, imagePath, category));
    }

    /// <summary>
    /// Adds a question to the list of questions in the quiz.
    /// </summary>
    /// <param name="question">The question to add.</param>
    public void AddQuestion(Question question)
    {
        ((List<Question>)_questions).Add(question);
    }

    /// <summary>
    /// Removes a question from the list of questions in the quiz.
    /// </summary>
    /// <param name="index">The index of the question to remove.</param>
    public void RemoveQuestion(int index)
    {
        ((List<Question>)_questions).RemoveAt(index);
    }

    /// <summary>
    /// Initializes the list of available random questions.
    /// Fills the list of available random questions with copies of the questions
    /// found in the list of questions for this quiz.
    /// </summary>
    public void InitializeRandomQuestions()
    {
        foreach (Question question in _questions)
        {
            Question newQuestion = new Question(question.Statement, question.Answers,
                question.CorrectAnswerIndex, question.ImagePath, question.Category);
            ((List<Question>)_availableRandomQuestions).Add(newQuestion);
        }
    }

    /// <summary>
    /// Gets the number of random available questions remaining.
    /// </summary>
    /// <returns></returns>
    public int GetRandomQuestionsRemaining()
    {
        return _availableRandomQuestions.Count();
    }
}