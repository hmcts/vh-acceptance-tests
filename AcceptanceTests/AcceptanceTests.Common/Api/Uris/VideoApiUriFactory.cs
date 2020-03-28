﻿using System;

namespace AcceptanceTests.Common.Api.Uris
{
    public static class VideoApiUriFactory
    {

        public static class VideoEventsEndpoints
        {
            private const string ApiRoot = "events";
            public static string Event => $"{ApiRoot}";
        }

        public static class VideoApiParticipantsEndpoints
        {
            private const string ApiRoot = "conferences";
            public static string AddParticipantsToConference(Guid conferenceId) => $"{ApiRoot}/{conferenceId}/participants";
            public static string RemoveParticipantFromConference(Guid conferenceId, Guid participantId) => $"{ApiRoot}/{conferenceId}/participants/{participantId}";
            public static string GetSelfTestScore(Guid conferenceId, Guid? participantId) => $"{ApiRoot}/{conferenceId}/participants/{participantId}/selftestresult";
        }

        public static class ConferenceEndpoints
        {
            private const string ApiRoot = "conferences";
            public static string BookNewConference => $"{ApiRoot}";
            public static string UpdateConferenceStatus(Guid conferenceId) => $"{ApiRoot}/{conferenceId}";
            public static string GetConferenceDetailsByUsername(string username) => $"{ApiRoot}/?username={username}";
            public static string GetConferenceDetailsById(Guid conferenceId) => $"{ApiRoot}/{conferenceId}";
            public static string GetConferenceByHearingRefId(Guid hearingRefId) => $"{ApiRoot}/hearings/{hearingRefId}";
            public static string RemoveConference(Guid? conferenceId) => $"{ApiRoot}/{conferenceId}";
            public static string GetTodaysConferences => $"{ApiRoot}/today/vho";
        }

        public class VideoApiHealthCheckEndpoints
        {
            private const string ApiRoot = "/healthcheck";
            public static string CheckServiceHealth => $"{ApiRoot}/health";
        }
    }
}
