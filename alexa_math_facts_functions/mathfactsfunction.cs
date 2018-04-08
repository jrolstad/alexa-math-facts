using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using alexa_math_facts_functions.application;

namespace alexa_math_facts_functions
{
    public class mathfactsfunction
    {
        [FunctionName("AlexaMathFacts")]

        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "AlexaMathFacts")]
            HttpRequestMessage req,
            TraceWriter log)
        {

            var requestData = req.Content.ReadAsAsync<AlexaAPI.Request.SkillRequest>().Result;
            var intentName = requestData.GetIntentName();

            switch (intentName)
            {
                case "addition":
                    {
                        var outputSpeech = "Welcome to addition math facts.  Let's start with the first question.";

                        var question = GetNextQuestion(QuestionType.Addition);
                        outputSpeech += question.Problem;

                        var response = MyFirstAlexaSkill.Application.AlexaServiceResponse.CreateQuestionResponse(intentName, outputSpeech, false);
                        response.SetExpectedAnswer(question.Answer);
                        response.SetQuestionType(QuestionType.Addition);

                        return req.CreateResponse(HttpStatusCode.OK, response); 
                    } 
                case "answer":
                    {
                        var expectedAnswer = requestData.GetExpectedAnswer();
                        var actualAnswer = requestData.GetActualAnswer();

                        string outputSpeech = null;
                        if(expectedAnswer == actualAnswer)
                        {
                            outputSpeech = "Correct.";
                        }
                        else
                        {
                            outputSpeech = $"Incorrect.  The correct answer is {expectedAnswer}.";
                        }

                        var questionType = requestData.GetQuestionType();
                        var nextQuestion = GetNextQuestion(questionType);
                        outputSpeech += nextQuestion.Problem;

                        var response = MyFirstAlexaSkill.Application.AlexaServiceResponse.CreateSpeechResponse(intentName, outputSpeech, true);
                        response.SetExpectedAnswer(nextQuestion.Answer);
                        response.SetQuestionType(questionType);

                        return req.CreateResponse(HttpStatusCode.OK, response); 
                }
                case "AMAZON.CancelIntent":
                case "AMAZON.StopIntent":
                    {
                        var response = MyFirstAlexaSkill.Application.AlexaServiceResponse.CreateSpeechResponse(intentName, "Thank you for playing talking math facts. Goodbye.", true);
                        return req.CreateResponse(HttpStatusCode.OK, response);
                    }
                default:
                    {
                        var response = MyFirstAlexaSkill.Application.AlexaServiceResponse.CreateSpeechResponse(intentName, "Unknown command.  Please try addition, subtraction, multiplication, or division.", true);
                        return req.CreateResponse(HttpStatusCode.OK, response);
                    }
            }

        }

        public static Question GetNextQuestion(QuestionType questionType)
        {
            var random = new System.Random();
            var questionNumber = random.Next(0, 4);

            switch (questionType)
            {
                case QuestionType.Addition:
                    return AdditionQuestions[questionNumber];
                case QuestionType.Subtraction:
                    return SubtractionQuestions[questionNumber];
                case QuestionType.Multiplication:
                    return MultiplicationQuestions[questionNumber];
                case QuestionType.Division:
                    return DivisionQuestions[questionNumber];
                default:
                    return AdditionQuestions[questionNumber];
            }
        }

        private static List<Question> AdditionQuestions = new List<Question> 
        { 
            new Question{Problem="5 plus 5 =",Answer = 10},
            new Question{Problem="5 plus 2 =",Answer = 7},
            new Question{Problem="9 plus 6 =",Answer = 15},
            new Question{Problem="8 plus 3 =",Answer = 11},
            new Question{Problem="2 plus 1 =",Answer = 3},
        };
        private static List<Question> SubtractionQuestions = new List<Question>
        {
            new Question{Problem="8 minus 2 =",Answer = 6},
            new Question{Problem="9 minus 7 =",Answer = 2},
            new Question{Problem="12 minus 9 =",Answer = 3},
            new Question{Problem="5 minus 2 =",Answer = 3},
            new Question{Problem="2 minus 0 =",Answer = 2},
        };
        private static List<Question> MultiplicationQuestions = new List<Question>();
        private static List<Question> DivisionQuestions = new List<Question>();

    }
}
