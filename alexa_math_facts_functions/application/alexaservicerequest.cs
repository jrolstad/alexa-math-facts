using System.Collections.Generic;

namespace alexamathfunctions.application
{
    public class AlexaServiceRequest
    {
        public Session session { get; set; }
        public Request request { get; set; }
        public string version { get; set; }
    }

    public class Application
    {
        public string applicationId { get; set; }
    }

    public class User
    {
        public string userId { get; set; }

        public string accessToken { get; set; }

        public UserPermissions permissions { get; set; }
    }

    public class UserPermissions
    {
        public string consentToken { get; set; }
    }

    public class Session
    {
        public string sessionId { get; set; }
        public Application application { get; set; }
        public Dictionary<string, string> attributes { get; set; }
        public User user { get; set; }
        public bool @new { get; set; }
    }

    public class Request
    {
        public string type { get; set; }
        public string requestId { get; set; }
        public string locale { get; set; }
        public string timestamp { get; set; }
        public string dialogState { get; set; }
        public string reason { get; set; }
        public Intent intent { get; set; }
        public AlexaError error { get; set; }
    }

    public class Intent
    {
        public string name { get; set; }
        public string confirmationStatus { get; set; }
        public Dictionary<string, Slot> slots { get; set; }
    }

    public class Slot
    {
        public string name { get; set; }
        public string value { get; set; }
        public string confirmationStatus { get; set; }
    }

    public class AlexaError
    {
        public string type { get; set; }
        public string message { get; set; }
    }


}

