using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

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
            var intentName = requestData?.Request?.Intent?.Name;

            switch (intentName)
            {
                case "addition":{
                    var outputSpeech = "5 plus 5 equals";
                    var answer = 10;
                    var response = MyFirstAlexaSkill.Application.AlexaServiceResponse.CreateQuestionResponse(intentName, outputSpeech, false);
                    response.sessionAttributes.Add("answer", answer.ToString());
                    return req.CreateResponse(HttpStatusCode.OK, response); 
                } 
                case "answer":{
                        var expectedAnswer = requestData.Session.Attributes["answer"] as string;
                        var actualAnswer = requestData.Request.Intent.Slots["answerValue"].Value;

                        var outputSpeech = "I don't know";
                        if(expectedAnswer == actualAnswer)
                        {
                            outputSpeech = "Correct";
                        }
                        else
                        {
                            outputSpeech = $"Incorrect.  The correct answer is {expectedAnswer} and you said {actualAnswer}.";
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
