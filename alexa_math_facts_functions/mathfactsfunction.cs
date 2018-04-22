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
                        var question = GetNextQuestion(QuestionType.Addition);

                        var outputSpeech = "Welcome to addition math facts.  Let's start with the first question. ";

                        var response = requestData.WithQuestionResponse(question, outputSpeech);
                        return req.CreateResponse(HttpStatusCode.OK, response); 
                    } 
                case "subtraction":
                    {
                        var question = GetNextQuestion(QuestionType.Subtraction);

                        var outputSpeech = "Welcome to subtraction math facts.  Let's start with the first question. ";

                        var response = requestData.WithQuestionResponse(question, outputSpeech);
                        return req.CreateResponse(HttpStatusCode.OK, response); 
                    } 
                case "answer":
                    {
                        var expectedAnswer = requestData.GetExpectedAnswer();
                        var actualAnswer = requestData.GetActualAnswer();
                        var questionType = requestData.GetQuestionType();

                        var nextQuestion = GetNextQuestion(questionType);

                        var isAnswerCorrect = expectedAnswer == actualAnswer;
                        var numberofquestiensaswerd = requestData.GetNumberOfQuestionsAsked();

                        if (isAnswerCorrect)
                        {
                            if(numberofquestiensaswerd>20)
                            {
                                var stopResponse = requestData.WithSpeechResponse("you have done 20 questions");
                                return req.CreateResponse(HttpStatusCode.OK, stopResponse);
                            }
                            var outputSpeech = $"Correct.  The next question is ";
                            var response = requestData.WithQuestionResponse(nextQuestion, outputSpeech);
                            return req.CreateResponse(HttpStatusCode.OK, response);
                        }
                        else
                        {
                            if (numberofquestiensaswerd > 20)
                            {
                                var stopResponse = requestData.WithSpeechResponse("you have done 20 questions");
                                return req.CreateResponse(HttpStatusCode.OK, stopResponse);
                            }
                            var outputSpeech = $"Incorrect.  The correct answer is {expectedAnswer}. The next question is ";
                            var response = requestData.WithQuestionResponse(nextQuestion, outputSpeech);
                            return req.CreateResponse(HttpStatusCode.OK, response);
                        }
                        
                    }
                case "AMAZON.CancelIntent":
                case "AMAZON.StopIntent":
                    {
                        var response = requestData.WithSpeechResponse("Thank you for playing talking math facts. Goodbye.");
                        return req.CreateResponse(HttpStatusCode.OK, response);
                    }
                default:
                    {
                        var response = requestData.WithRepromptResponse("Unknown command.  Please try addition, subtraction, multiplication, or division.");
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

        private static readonly List<Question> AdditionQuestions = new List<Question> 
        { 
            new Question{Problem="9 plus 8 =",Answer = 17,Type=QuestionType.Addition},
            new Question{Problem="5 plus 4 =",Answer = 9,Type=QuestionType.Addition},
            new Question{Problem="4 plus 4 =",Answer = 8,Type=QuestionType.Addition},
            new Question{Problem="9 plus 6 =",Answer = 15,Type=QuestionType.Addition},
            new Question{Problem="5 plus 5 =",Answer = 10,Type=QuestionType.Addition},
            new Question{Problem="8 plus 6 =",Answer = 14,Type=QuestionType.Addition},
            new Question{Problem="3 plus 7 =",Answer = 10,Type=QuestionType.Addition},
            new Question{Problem="7 plus 5 =",Answer = 12,Type=QuestionType.Addition},
            new Question{Problem="4 plus 5 =",Answer = 9,Type=QuestionType.Addition},
            new Question{Problem="4 plus 9 =",Answer = 13,Type=QuestionType.Addition},
            new Question{Problem="3 plus 8 =",Answer = 11,Type=QuestionType.Addition},
            new Question{Problem="5 plus 9 =",Answer = 14,Type=QuestionType.Addition},
        };
        private static readonly List<Question> SubtractionQuestions = new List<Question>
        {
            new Question{Problem="9 minus 5 =",Answer = 4,Type=QuestionType.Subtraction},
            new Question{Problem="16 minus 7 =",Answer = 9,Type=QuestionType.Subtraction},
            new Question{Problem="7 minus 4 =",Answer = 3,Type=QuestionType.Subtraction},
            new Question{Problem="13 minus 4 =",Answer = 9,Type=QuestionType.Subtraction},
            new Question{Problem="18 minus 9 =",Answer = 9,Type=QuestionType.Subtraction},
            new Question{Problem="14 minus 8 =",Answer = 6,Type=QuestionType.Subtraction},
            new Question{Problem="9 minus 3 =",Answer = 6,Type=QuestionType.Subtraction},
            new Question{Problem="15 minus 7 =",Answer = 8,Type=QuestionType.Subtraction},
            new Question{Problem="10 minus 4 =",Answer = 6,Type=QuestionType.Subtraction},
            new Question{Problem="11 minus 8 =",Answer = 3,Type=QuestionType.Subtraction},
            new Question{Problem="11 minus 6 =",Answer = 5,Type=QuestionType.Subtraction},
            new Question{Problem="12 minus 3 =",Answer = 9,Type=QuestionType.Subtraction},
        };
        private static readonly List<Question> MultiplicationQuestions = new List<Question>();
        private static readonly List<Question> DivisionQuestions = new List<Question>();

    }
}
