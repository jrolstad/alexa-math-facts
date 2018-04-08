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
                        var outputSpeech = "Welcome to addition math facts.  Let's start with the first question. ";

                        var question = GetNextQuestion(QuestionType.Addition);
                        outputSpeech += question.Problem;

                        var response = MyFirstAlexaSkill.Application.AlexaServiceResponse.CreateQuestionResponse(intentName, outputSpeech, false);
                        response.SetExpectedAnswer(question.Answer);
                        response.SetQuestionType(QuestionType.Addition);

                        return req.CreateResponse(HttpStatusCode.OK, response); 
                    } 
                case "subtraction":
                    {
                        var outputSpeech = "Welcome to subtraction math facts.  Let's start with the first question. ";

                        var question = GetNextQuestion(QuestionType.Subtraction);
                        outputSpeech += question.Problem;

                        var response = MyFirstAlexaSkill.Application.AlexaServiceResponse.CreateQuestionResponse(intentName, outputSpeech, false);
                        response.SetExpectedAnswer(question.Answer);
                        response.SetQuestionType(QuestionType.Subtraction);

                        return req.CreateResponse(HttpStatusCode.OK, response); 
                    } 
                case "answer":
                    {
                        var expectedAnswer = requestData.GetExpectedAnswer();
                        var actualAnswer = requestData.GetActualAnswer();

                        string outputSpeech = null;
                        if(expectedAnswer == actualAnswer)
                        {
                            outputSpeech = "Correct. ";
                        }
                        else
                        {
                            outputSpeech = $"Incorrect.  The correct answer is {expectedAnswer}. ";
                        }

                        var questionType = requestData.GetQuestionType();
                        var nextQuestion = GetNextQuestion(questionType);
                        outputSpeech += ("The next question is " + nextQuestion.Problem);

                        var response = MyFirstAlexaSkill.Application.AlexaServiceResponse.CreateQuestionResponse(intentName, outputSpeech, false);
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
            var questionNumber = random.Next(0, 11);

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
            new Question{Problem="9 plus 8 =",Answer = 17},
            new Question{Problem="5 plus 4 =",Answer = 9},
            new Question{Problem="4 plus 4 =",Answer = 8},
            new Question{Problem="9 plus 6 =",Answer = 15},
            new Question{Problem="5 plus 5 =",Answer = 10},
            new Question{Problem="8 plus 6 =",Answer = 14},
            new Question{Problem="3 plus 7 =",Answer = 10},
            new Question{Problem="7 plus 5 =",Answer = 12},
            new Question{Problem="4 plus 5 =",Answer = 9},
            new Question{Problem="4 plus 9 =",Answer = 13},
            new Question{Problem="3 plus 8 =",Answer = 11},
            new Question{Problem="5 plus 9 =",Answer = 14},
        };
        private static List<Question> SubtractionQuestions = new List<Question>
        {
            new Question{Problem="9 minus 5 =",Answer = 4},
            new Question{Problem="16 minus 7 =",Answer = 9},
            new Question{Problem="7 minus 4 =",Answer = 3},
            new Question{Problem="13 minus 4 =",Answer = 9},
            new Question{Problem="18 minus 9 =",Answer = 9},
            new Question{Problem="14 minus 8 =",Answer = 6},
            new Question{Problem="9 minus 3 =",Answer = 6},
            new Question{Problem="15 minus 7 =",Answer = 8},
            new Question{Problem="10 minus 4 =",Answer = 6},
            new Question{Problem="11 minus 8 =",Answer = 3},
            new Question{Problem="11 minus 6 =",Answer = 5},
            new Question{Problem="12 minus 3 =",Answer = 9},
        };
        private static List<Question> MultiplicationQuestions = new List<Question>();
        private static List<Question> DivisionQuestions = new List<Question>();

    }
}
