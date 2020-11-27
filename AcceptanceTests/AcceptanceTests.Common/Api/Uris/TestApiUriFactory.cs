using System;

namespace AcceptanceTests.Common.Api.Uris
{
    public static class TestApiUriFactory
    {
        public static class AllocationEndpoints
        {
            public const string ApiRoot = "allocations";
            public static string AllocateSingleUser => $"{ApiRoot}/allocateUser";
            public static string AllocateUsers => $"{ApiRoot}/allocateUsers";
            public static string UnallocateUsers => $"{ApiRoot}/unallocateUsers";
        }

        public static class ConferenceEndpoints
        {
            public const string ApiRoot = "/conferences";
            public static string GetConferenceById(Guid conferenceId) => $"{ApiRoot}/{conferenceId:D}";

            public static string GetConferenceByHearingRefId(Guid hearingRefId) =>
                $"{ApiRoot}/hearings/{hearingRefId:D}";

            public static string CreateConference => ApiRoot;

            public static string DeleteConference(Guid hearingRefId, Guid conferenceId) =>
                $"{ApiRoot}/{hearingRefId:D}/{conferenceId:D}";

            public static string GetConferencesForJudge(string username) =>
                $"{ApiRoot}/today/judge?username={username}";

            public static string GetConferencesForVho = $"{ApiRoot}/today/vho";

            public static string GetAudioRecordingLinkByHearingId(Guid hearingId) =>
                $"{ApiRoot}/audio/{hearingId:D}";

            public static string GetTasksByConferenceId(Guid conferenceId) => $"{ApiRoot}/Tasks/{conferenceId:D}";
            public static string CreateVideoEvent => $"{ApiRoot}/events";
            public static string GetSelfTestScore(Guid conferenceId, Guid participantId) => $"{ApiRoot}/{conferenceId}/participants/{participantId}/score";
            public static string DeleteParticipant(Guid conferenceId, Guid participantId) => $"{ApiRoot}/{conferenceId}/participants/{participantId}";
        }

        public static class HealthCheckEndpoints
        {
            private const string ApiRoot = "/health";
            public static string CheckServiceHealth => $"{ApiRoot}/health";
        }

        public static class HearingEndpoints
        {
            private const string ApiRoot = "/hearings";
            public static string CreateHearing => ApiRoot;

            public static string GetHearing(Guid hearingId)
            {
                return $"{ApiRoot}/{hearingId:D}";
            }

            public static string GetHearingsByUsername(string username)
            {
                return $"{ApiRoot}/username/{username}";
            }

            public static string ConfirmHearing(Guid hearingId)
            {
                return $"{ApiRoot}/{hearingId:D}";
            }

            public static string DeleteHearing(Guid hearingId)
            {
                return $"{ApiRoot}/{hearingId:D}";
            }

            public static string UpdateSuitabilityAnswers(Guid hearingId, Guid participantId)
            {
                return $"{ApiRoot}/{hearingId}/participants/{participantId}/update-suitability-answers";
            }

            public static string GetSuitabilityAnswers(string username)
            {
                return $"{ApiRoot}/get-suitability-answers/{username}";
            }
        }

        public static class UserEndpoints
        {
            public const string ApiRoot = "users";

            public static string GetUserByUsername(string username)
            {
                return $"{ApiRoot}/username/{username}";
            }

            public static string GetUserByUserPrincipalName(string username)
            {
                return $"{ApiRoot}/userPrincipalName/{username}";
            }

            public static string GetUserExistsInAd(string username)
            {
                return $"{ApiRoot}/aad/{username}";
            }

            public static string DeleteUserInAd(string contactEmail)
            {
                return $"{ApiRoot}/aad/{contactEmail}";
            }

            public static string GetPersonByUsername(string username)
            {
                return $"{ApiRoot}/person/{username}";
            }

            public static string RefreshJudgesCache()
            {
                return $"{ApiRoot}/judges/cache";
            }
        }

        public static class UtilityEndpoints
        {
            public const string ApiRoot = "utilities";
            public static string DeleteHearings => $"{ApiRoot}/removeTestData";
        }
    }
}
