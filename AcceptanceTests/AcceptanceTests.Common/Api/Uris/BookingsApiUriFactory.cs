﻿using System;

namespace AcceptanceTests.Common.Api.Uris
{
    public class BookingsApiUriFactory
    {
        public BookingsApiUriFactory()
        {
            HearingVenueEndpoints = new HearingVenueEndpoints();
            CaseTypesEndpoints = new CaseTypesEndpoints();
            HearingsEndpoints = new HearingsEndpoints();
            ParticipantsEndpoints = new ParticipantsEndpoints();
            PersonEndpoints = new PersonEndpoints();
            HealthCheckEndpoints = new BookingsApiHealthCheckEndpoints();
            HearingsParticipantsEndpoints = new HearingsParticipantsEndpoints();
        }

        public HearingVenueEndpoints HearingVenueEndpoints { get; set; }
        public CaseTypesEndpoints CaseTypesEndpoints { get; set; }
        public HearingsEndpoints HearingsEndpoints { get; set; }
        public ParticipantsEndpoints ParticipantsEndpoints { get; set; }
        public PersonEndpoints PersonEndpoints { get; set; }
        public BookingsApiHealthCheckEndpoints HealthCheckEndpoints { get; set; }
        public HearingsParticipantsEndpoints HearingsParticipantsEndpoints { get; set; }
    }

    public class HearingVenueEndpoints
    {
        private static string ApiRoot => "hearingvenues";
        public string GetVenues => $"{ApiRoot}";
    }

    public class BookingsApiHealthCheckEndpoints
    {
        private static string ApiRoot => "healthCheck";
        public string HealthCheck => $"{ApiRoot}/health";
    }

    public class CaseTypesEndpoints
    {
        private static string ApiRoot => "casetypes";
        public string GetCaseRolesForCaseType(string caseTypeName) => $"{ApiRoot}/{caseTypeName}/caseroles";
        public string GetHearingRolesForCaseRole(string caseTypeName, string caseRoleName) => $"{ApiRoot}/{caseTypeName}/caseroles/{caseRoleName}/hearingroles";

        public string GetCaseTypes() => $"{ApiRoot}/";
    }

    public class HearingsEndpoints
    {
        private static string ApiRoot => "hearings";
        public string GetHearingsByUsername(string username) => $"{ApiRoot}/?username={username}";
        public string GetHearingDetailsById(Guid? hearingId) => $"{ApiRoot}/{hearingId}";
        public string BookNewHearing() => $"{ApiRoot}";
        public string UpdateHearingDetails(Guid hearingId) => $"{ApiRoot}/{hearingId}";
        public string UpdateHearingStatus(Guid? hearingId) => $"{ApiRoot}/{hearingId}";
        public string RemoveHearing(Guid? hearingId) => $"{ApiRoot}/{hearingId}";
    }

    public class HearingsParticipantsEndpoints
    {
        private static string ApiRoot => "hearings";
        public string GetAllParticipantsInHearing(Guid hearingId) => $"{ApiRoot}/{hearingId}/participants";

        public string GetParticipantInHearing(Guid hearingId, Guid participantId) =>
            $"{ApiRoot}/{hearingId}/participants/{participantId}";

        public string AddParticipantsToHearing(Guid hearingId) => $"{ApiRoot}/{hearingId}/participants";

        public string UpdateParticipantDetails(Guid hearingId, Guid participantId) =>
            $"{ApiRoot}/{hearingId}/participants/{participantId}";

        public string RemoveParticipantFromHearing(Guid hearingId, Guid participantId) =>
            $"{ApiRoot}/{hearingId}/participants/{participantId}";

        public string SuitabilityAnswers(Guid hearingId, Guid participantId) =>
            $"{ApiRoot}/{hearingId}/participants/{participantId}/suitability-answers";
    }

    public class ParticipantsEndpoints
    {
        private static string ApiRoot => "Participants";
        public string GetParticipantsByUsername(string username) => $"/{ApiRoot}/username/{username}";
    }

    public class PersonEndpoints
    {
        private static string ApiRoot => "persons";
        public string GetPersonByUsername(string username) => $"{ApiRoot}/username/{username}";
        public string GetPersonByContactEmail(string contactEmail) => $"{ApiRoot}/contactEmail/{contactEmail}";

    }
}