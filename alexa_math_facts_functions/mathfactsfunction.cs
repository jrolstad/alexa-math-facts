﻿using System.Collections.Generic;
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
                outputSpeech = $"ready set go get smart , {intentName}";
                isEnd = false;
            }
            else
            {
                outputSpeech = "That is all";
                isEnd = true;
            }
            
            var response = MyFirstAlexaSkill.Application.AlexaServiceResponse.CreateOutputSpeechResponse(intentName, outputSpeech, isEnd);

            return req.CreateResponse(HttpStatusCode.OK, response);
        }


    }
}
