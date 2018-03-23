using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using alexamathfunctions.application;

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

            var requestData = req.Content.ReadAsAsync<AlexaServiceRequest>().Result;

            var intentName = requestData?.request?.intent?.name;
            var outputSpeech = GetSpeechResponse(intentName);
            var response = AlexaServiceResponse.CreateOutputSpeechResponse(intentName, outputSpeech);

            return req.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
