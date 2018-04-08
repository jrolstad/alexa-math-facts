﻿using System;
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

            var resolvedValue = Enum.TryParse<QuestionType>(questionType, out value);

            return value;
        }

        public static void SetQuestionType(this MyFirstAlexaSkill.Application.AlexaServiceResponse response, QuestionType type)
        {
            response.sessionAttributes.Add("questionType", type.ToString());
        }
    }
}
