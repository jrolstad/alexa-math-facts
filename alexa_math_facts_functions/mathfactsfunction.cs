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
                case "addition":{
                    var outputSpeech = "5 plus 5 equals";
                    var response = MyFirstAlexaSkill.Application.AlexaServiceResponse.CreateQuestionResponse(intentName, outputSpeech, false);
                    response.SetAnswer(10);

                    return req.CreateResponse(HttpStatusCode.OK, response); 
                } 
                case "answer":{
                        var expectedAnswer = requestData.GetExpectedAnswer();
                        var actualAnswer = requestData.GetActualAnswer();

                        var outputSpeech = "I don't know";
                        if(expectedAnswer == actualAnswer)
                        {
                            outputSpeech = "Correct";
                        }
                        else
                        {
                            outputSpeech = $"Incorrect.  The correct answer is {expectedAnswer}.";
                        }

                        var response = MyFirstAlexaSkill.Application.AlexaServiceResponse.CreateSpeechResponse(intentName, outputSpeech, true);
                        return req.CreateResponse(HttpStatusCode.OK, response); 
                }
                default:{
                    var response = MyFirstAlexaSkill.Application.AlexaServiceResponse.CreateSpeechResponse(intentName, "Thank you for playing talking math facts", true);
                    return req.CreateResponse(HttpStatusCode.OK, response);
                }
            }

        }


    }
}
