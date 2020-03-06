using System;

namespace AcceptanceTests.Common.Api.Uris
{
    public static class VideoWebUriFactory
    {
        public static class VideoWebMediaEventEndpoints
        {
            private const string ApiRoot = "conferences";
            public static string SelfTestFailureEvents(Guid conferenceId) => $"{ApiRoot}/{conferenceId}/selftestfailureevents";
        }

        public static class VideoWebParticipantsEndpoints
        {
            private const string ApiRoot = "conferences";
            public static string SelfTestResult(Guid? conferenceId, Guid? participantId) => $"{ApiRoot}/{conferenceId}/participants/{participantId}/selftestresult";
        }

        public static class VideoWebCallbackEndpoints
        {
            private const string ApiRoot = "callback";
            public static string Event => $"{ApiRoot}";
        }
    }
}
