using System;
namespace alexa_math_facts_functions.application
{
    public class Question
    {
        public string Problem { get; set; }
        public int Answer { get; set; }
    }

    public enum QuestionType
    {
        Addition,
        Multiplication,
        Subtraction,
        Division
    }
}
