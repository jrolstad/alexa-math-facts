using System;
namespace alexa_math_facts_functions.application
{
    public static class AlexaRequestExtensions
    {
        public static string GetIntentName(this AlexaAPI.Request.SkillRequest request)
        {
            return request?.Request?.Intent?.Name;
        }

        public static string GetExpectedAnswer(this AlexaAPI.Request.SkillRequest request)
        {
            object answer = null;
            request.Session.Attributes.TryGetValue("answer", out answer);
            return answer as string;
        }

        public static string GetActualAnswer(this AlexaAPI.Request.SkillRequest request)
        {
            AlexaAPI.Request.Slot slot = null;
            request.Request.Intent.Slots.TryGetValue("answerValue", out slot);

            return slot?.Value;
        }

        public static void SetExpectedAnswer(this MyFirstAlexaSkill.Application.AlexaServiceResponse response, int answer)
        {
            response.sessionAttributes.Add("answer", answer.ToString());
        }

        public static QuestionType GetQuestionType(this AlexaAPI.Request.SkillRequest request)
        {
            object sessionValue = null;
            request.Session.Attributes.TryGetValue("questionType", out sessionValue);

            var questionType = sessionValue as string;
            QuestionType value;

            Enum.TryParse<QuestionType>(questionType, out value);

            return value;
        }

        public static void SetQuestionType(this MyFirstAlexaSkill.Application.AlexaServiceResponse response, QuestionType type)
        {
            response.sessionAttributes.Add("questionType", type.ToString());
        }

        public static MyFirstAlexaSkill.Application.AlexaServiceResponse WithQuestionResponse(this AlexaAPI.Request.SkillRequest request, 
            Question question,
            string speech)
        {
            var intentName = request.GetIntentName();
            var outputSpeech = speech + question.Problem;

            var response = MyFirstAlexaSkill.Application.AlexaServiceResponse.CreateQuestionResponse(intentName, outputSpeech, false);
            response.SetExpectedAnswer(question.Answer);
            response.SetQuestionType(question.Type);

            return response;
        }

        public static MyFirstAlexaSkill.Application.AlexaServiceResponse WithRepromptResponse(this AlexaAPI.Request.SkillRequest request,
            string speech)
        {
            var intentName = request.GetIntentName();
            var response = MyFirstAlexaSkill.Application.AlexaServiceResponse.CreateQuestionResponse(intentName, speech, false);

            return response;
        }

        public static MyFirstAlexaSkill.Application.AlexaServiceResponse WithSpeechResponse(this AlexaAPI.Request.SkillRequest request, string speech)
        {
            var intentName = request.GetIntentName();
            var response = MyFirstAlexaSkill.Application.AlexaServiceResponse.CreateSpeechResponse(intentName, "speech", true);
            return response;
        }

        public static string WithQuestion(this string speech, Question question)
        {
            var speechWithQuestion = speech + question.Problem;

            return speechWithQuestion;
        }
    }
}
