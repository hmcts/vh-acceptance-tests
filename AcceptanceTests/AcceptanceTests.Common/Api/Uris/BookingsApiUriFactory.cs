using System;

namespace AcceptanceTests.Common.Api.Uris
{
    public static class BookingsApiUriFactory
    {
        public static class HearingVenueEndpoints
        {
            private const string ApiRoot = "hearingvenues";
            public static string GetVenues => $"{ApiRoot}";
        }

        public static class BookingsApiHealthCheckEndpoints
        {
            private const string ApiRoot = "healthCheck";
            public static string HealthCheck => $"{ApiRoot}/health";
        }

        public static class CaseTypesEndpoints
        {
            private const string ApiRoot = "casetypes";
            public static string GetCaseRolesForCaseType(string caseTypeName) => $"{ApiRoot}/{caseTypeName}/caseroles";
            public static string GetHearingRolesForCaseRole(string caseTypeName, string caseRoleName) => $"{ApiRoot}/{caseTypeName}/caseroles/{caseRoleName}/hearingroles";
            public static string GetCaseTypes => $"{ApiRoot}/";
        }

        public static class HearingsEndpoints
        {
            private const string ApiRoot = "hearings";
            public static string GetHearingsByUsername(string username) => $"{ApiRoot}/?username={username}";
            public static string GetHearingDetailsById(Guid? hearingId) => $"{ApiRoot}/{hearingId}";
            public static string BookNewHearing => $"{ApiRoot}";
            public static string UpdateHearingDetails(Guid hearingId) => $"{ApiRoot}/{hearingId}";
            public static string UpdateHearingStatus(Guid? hearingId) => $"{ApiRoot}/{hearingId}";
            public static string RemoveHearing(Guid? hearingId) => $"{ApiRoot}/{hearingId}";
            public static string GetHearingsByAnyCaseType(int limit = 100) => $"{ApiRoot}/types?limit={limit}";
            public static string GetHearingsByCaseNumber => $"{ApiRoot}/audiorecording/casenumber";
        }

        public static class HearingsParticipantsEndpoints
        {
            private const string ApiRoot = "hearings";
            public static string GetAllParticipantsInHearing(Guid hearingId) => $"{ApiRoot}/{hearingId}/participants";
            public static string GetParticipantInHearing(Guid hearingId, Guid participantId) => $"{ApiRoot}/{hearingId}/participants/{participantId}";
            public static string AddParticipantsToHearing(Guid hearingId) => $"{ApiRoot}/{hearingId}/participants";
            public static string UpdateParticipantDetails(Guid hearingId, Guid participantId) => $"{ApiRoot}/{hearingId}/participants/{participantId}";
            public static string RemoveParticipantFromHearing(Guid hearingId, Guid participantId) => $"{ApiRoot}/{hearingId}/participants/{participantId}";
            public static string SuitabilityAnswers(Guid hearingId, Guid participantId) => $"{ApiRoot}/{hearingId}/participants/{participantId}/suitability-answers";
        }

        public static class ParticipantsEndpoints
        {
            private const string ApiRoot = "Participants";
            public static string GetParticipantsByUsername(string username) => $"/{ApiRoot}/username/{username}";
        }

        public static class PersonEndpoints
        {
            private const string ApiRoot = "persons";
            public static string GetPersonByUsername(string username) => $"{ApiRoot}/username/{username}";
            public static string GetPersonByContactEmail(string contactEmail) => $"{ApiRoot}/contactEmail/{contactEmail}";
            public static string GetSuitabilityAnswersByEmail(string username) => $"{ApiRoot}/username/{username}/suitability-answers";
        }
    }
}
