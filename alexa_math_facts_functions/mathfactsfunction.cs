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
            var outputSpeech = "Hello Liam";
            var response = CreateOutputSpeechResponse(intentName, outputSpeech);

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
    }
}
