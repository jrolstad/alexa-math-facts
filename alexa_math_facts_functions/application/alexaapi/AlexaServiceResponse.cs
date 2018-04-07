using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstAlexaSkill.Application
{
    public class AlexaServiceResponse
    {
        public static AlexaServiceResponse CreateRepromptResponse(string intent, string outputSpeech, bool isEnd)
        {
            var response = new AlexaServiceResponse
          {
              version = "1.1",
              sessionAttributes = new Dictionary<string, string>(),
              response = new Response
              {
                reprompt = new Reprompt{
                        outputSpeech = new OutputSpeech
                        {
                            type = "PlainText",
                            text = outputSpeech
                        }

                },
                shouldEndSession = isEnd
              }
          };

            return response;  
        }
        public static AlexaServiceResponse CreateOutputSpeechResponse(string intent, string outputSpeech, bool isEnd)
        {
            var response = new AlexaServiceResponse
            {
                version = "1.1",
                sessionAttributes = new Dictionary<string, string>(),
                response = new Response
                {
                    outputSpeech = new OutputSpeech
                    {
                        type = "PlainText",
                        text = outputSpeech
                    },
                    card = new Card
                    {
                        type = "Simple",
                        title = intent,
                        content = outputSpeech
                    },
                    shouldEndSession = isEnd
                }
            };

            return response;
        }
        public string version { get; set; }
        public Response response { get; set; }
        public Dictionary<string, string> sessionAttributes { get; set; }
    }

    public class Response
    {
        public OutputSpeech outputSpeech { get; set; }
        public Card card { get; set; }
        public Reprompt reprompt { get; set; }
        public List<Directive> directives { get; set; }
        public bool shouldEndSession { get; set; }
    }

    public class OutputSpeech
    {
        public string type { get; set; }
        public string text { get; set; }
        public string ssml { get; set; }
    }

    public class Card
    {
        public string content { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string text { get; set; }
        public AlexaImage image { get; set; }
    }

    public class AlexaImage
    {
        public string smallImageUrl { get; set; }
        public string largeImageUrl { get; set; }
    }

    public class Reprompt
    {
        public OutputSpeech outputSpeech { get; set; }
    }

    public class Directive
    {
        public string type { get; set; }
        public string playBehavior { get; set; }
        public AudioItem audioItem { get; set; }
    }

    public class AudioItem
    {
        public AlexaStream stream { get; set; }
    }

    public class AlexaStream
    {
        public string token { get; set; }
        public string url { get; set; }
        public int offsetInMilliseconds { get; set; }
    }




}