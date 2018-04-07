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
            var answer = 10;

            var isEnd = intentName == "AMAZON.StopIntent";

            var response = MyFirstAlexaSkill.Application.AlexaServiceResponse.CreateRepromptResponse(intentName,"5 plus 5 =",false);
            response.sessionAttributes.Add("answer", answer.ToString());

           /* if (requestData.Session.New == true)
            {
                if(intentName == "addition")
                {
                    outputSpeech = $"5 plus 5 =";
                    answer = 10;
                }
                if(intentName == "subtraction")
                {
                    outputSpeech = $"3 minus 2 =";
                    answer = 1;
                }


                isEnd = false;
            }
            else
            {
                
                var slots = string.Join(",", requestData.Request.Intent?.Slots?.Keys);
                outputSpeech = $"The intent is {intentName} and slot names are {slots}"; 


                isEnd = true;
            }
            
            var response = MyFirstAlexaSkill.Application.AlexaServiceResponse.CreateOutputSpeechResponse(intentName, outputSpeech, isEnd);
            response.sessionAttributes.Add("answer", answer.ToString());

            if (isEnd == false)
            {
                response.response.reprompt = new MyFirstAlexaSkill.Application.Reprompt 
                { 
                    outputSpeech = new MyFirstAlexaSkill.Application.OutputSpeech
                    {
                        type = "PlainText",
                        text = ""
                    },
                };
            }
            */
                    
            return req.CreateResponse(HttpStatusCode.OK, response);
        }


    }
}
