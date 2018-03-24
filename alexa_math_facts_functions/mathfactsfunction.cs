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

            var outputSpeech = "";
            var isEnd = false;
            if (requestData.Session.New == true)
            {
                outputSpeech = $"ready set go get smatrt, {intentName}";
                isEnd = false;
            }
            else
            {
                outputSpeech = "That is all";
                isEnd = true;
            }
            
            var response = CreateOutputSpeechResponse2(intentName, outputSpeech,isEnd);

            return req.CreateResponse(HttpStatusCode.OK, response);
        }

        public static AlexaAPI.Response.SkillResponse CreateOutputSpeechResponse(string intent, string outputSpeech)
        {
            var response = new AlexaAPI.Response.SkillResponse
            {
                Version = "1.0",
                SessionAttributes = new Dictionary<string, object>(),
                Response = new AlexaAPI.Response.ResponseBody
                {
                    OutputSpeech = new AlexaAPI.Response.PlainTextOutputSpeech
                    {
                        Text = outputSpeech
                    },
                    Card = new AlexaAPI.Response.SimpleCard()
                    {
                        Title = intent,
                        Content = outputSpeech
                    },
                    ShouldEndSession = true
                }
            };

            return response;
        }

        public static MyFirstAlexaSkill.Application.AlexaServiceResponse CreateOutputSpeechResponse2(string intent, string outputSpeech, bool isEnd)
        {
            var response = MyFirstAlexaSkill.Application.AlexaServiceResponse.CreateOutputSpeechResponse(intent, outputSpeech, isEnd);

            return response;
        }
    }
}
