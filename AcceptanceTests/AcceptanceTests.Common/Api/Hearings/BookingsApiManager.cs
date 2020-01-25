﻿using System;
using System.Data;
using System.Net;
using System.Threading;
using AcceptanceTests.Common.Api.Clients;
using AcceptanceTests.Common.Api.Helpers;
using AcceptanceTests.Common.Api.Requests;
using AcceptanceTests.Common.Api.Uris;
using RestSharp;

namespace AcceptanceTests.Common.Api.Hearings
{
    public class BookingsApiManager
    {
        private readonly string _bookingsApiUrl;
        private readonly string _bookingsApiBearerToken;

        public BookingsApiManager(string bookingsApiUrl, string bookingsApiBearerToken)
        {
            _bookingsApiUrl = bookingsApiUrl;
            _bookingsApiBearerToken = bookingsApiBearerToken;
        }

        public IRestResponse CreateHearing(object hearingRequest)
        {
            var endpoint = new BookingsApiUriFactory().HearingsEndpoints.BookNewHearing();
            var request = new RequestBuilder().Post(endpoint, hearingRequest);
            var client = new ApiClient(_bookingsApiUrl, _bookingsApiBearerToken).GetClient();
            var response = new RequestExecutor(request).SendToApi(client);
            return response;
        }

        public IRestResponse ConfirmHearingToCreateConference(Guid hearingId, object updateRequest)
        {
            var endpoint = new BookingsApiUriFactory().HearingsEndpoints.UpdateHearingStatus(hearingId);
            var request = new RequestBuilder().Patch(endpoint, updateRequest);
            var client = new ApiClient(_bookingsApiUrl, _bookingsApiBearerToken).GetClient();
            var response = new RequestExecutor(request).SendToApi(client);
            return response;
        }

        public IRestResponse GetHearing(Guid hearingId)
        {
            var endpoint = new BookingsApiUriFactory().HearingsEndpoints.GetHearingDetailsById(hearingId);
            var request = new RequestBuilder().Get(endpoint);
            var client = new ApiClient(_bookingsApiUrl, _bookingsApiBearerToken).GetClient();
            var response = new RequestExecutor(request).SendToApi(client);
            return response;
        }

        public IRestResponse GetHearingsForUsername(string username)
        {
            var endpoint = new BookingsApiUriFactory().HearingsEndpoints.GetHearingsByUsername(username);
            var request = new RequestBuilder().Get(endpoint);
            var client = new ApiClient(_bookingsApiUrl, _bookingsApiBearerToken).GetClient();
            var response = new RequestExecutor(request).SendToApi(client);
            return response;
        }

        public IRestResponse PollForHearingByUsername(string username, string caseName, int timeout = 60)
        {
            for (var i = 0; i < timeout; i++)
            {
                var rawResponse = GetHearingsForUsername(username);
                if (!rawResponse.IsSuccessful) continue;
                if (rawResponse.Content.ToLower().Contains(caseName.ToLower()))
                {
                    return rawResponse;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            throw new DataException($"Hearing with case name '{caseName}' not found after {timeout} seconds.");
        }

        public IRestResponse SetSuitabilityAnswers(Guid hearingId, Guid participantId, object suitabilityRequest)
        {
            var endpoint = new BookingsApiUriFactory().HearingsParticipantsEndpoints.SuitabilityAnswers(hearingId, participantId);
            var request = new RequestBuilder().Put(endpoint, suitabilityRequest);
            var client = new ApiClient(_bookingsApiUrl, _bookingsApiBearerToken).GetClient();
            var response = new RequestExecutor(request).SendToApi(client);
            return response;
        }

        public IRestResponse GetSuitabilityAnswers(string username)
        {
            var endpoint = new BookingsApiUriFactory().PersonEndpoints.GetSuitabilityAnswersByEmail(username);
            var request = new RequestBuilder().Get(endpoint);
            var client = new ApiClient(_bookingsApiUrl, _bookingsApiBearerToken).GetClient();
            var response = new RequestExecutor(request).SendToApi(client);
            return response;
        }

        public IRestResponse UpdateHearing(Guid hearingId, object updateRequest)
        {
            var endpoint = new BookingsApiUriFactory().HearingsEndpoints.UpdateHearingDetails(hearingId);
            var request = new RequestBuilder().Put(endpoint, updateRequest);
            var client = new ApiClient(_bookingsApiUrl, _bookingsApiBearerToken).GetClient();
            var response = new RequestExecutor(request).SendToApi(client);
            return response;
        }

        public IRestResponse UpdateHearingDetails(Guid hearingId, object updateRequest)
        {
            var endpoint = new BookingsApiUriFactory().HearingsEndpoints.UpdateHearingStatus(hearingId);
            var request = new RequestBuilder().Patch(endpoint, updateRequest);
            var client = new ApiClient(_bookingsApiUrl, _bookingsApiBearerToken).GetClient();
            var response = new RequestExecutor(request).SendToApi(client);
            return response;
        }

        public IRestResponse DeleteHearing(Guid hearingId)
        {
            var endpoint = new BookingsApiUriFactory().HearingsEndpoints.RemoveHearing(hearingId);
            var request = new RequestBuilder().Delete(endpoint);
            var client = new ApiClient(_bookingsApiUrl, _bookingsApiBearerToken).GetClient();
            var response = new RequestExecutor(request).SendToApi(client);
            return response;
        }

        public IRestResponse AddParticipantsToHearing(Guid hearingId, object participantsRequest)
        {
            var endpoint = new BookingsApiUriFactory().HearingsParticipantsEndpoints.AddParticipantsToHearing(hearingId);
            var request = new RequestBuilder().Post(endpoint, participantsRequest);
            var client = new ApiClient(_bookingsApiUrl, _bookingsApiBearerToken).GetClient();
            var response = new RequestExecutor(request).SendToApi(client);
            return response;
        }

        public IRestResponse UpdateParticipantDetails(Guid hearingId, Guid participantId, object updateRequest)
        {
            var endpoint = new BookingsApiUriFactory().HearingsParticipantsEndpoints.UpdateParticipantDetails(hearingId, participantId);
            var request = new RequestBuilder().Put(endpoint, updateRequest);
            var client = new ApiClient(_bookingsApiUrl, _bookingsApiBearerToken).GetClient();
            var response = new RequestExecutor(request).SendToApi(client);
            return response;
        }

        public IRestResponse PollForParticipantNameUpdated(string username, string updatedDisplayName, int timeout = 60)
        {
            for (var i = 0; i < timeout; i++)
            {
                var rawResponse = GetHearingsForUsername(username);
                if (!rawResponse.IsSuccessful) continue;
                if (rawResponse.Content.ToLower().Contains(updatedDisplayName.ToLower()))
                {
                    return rawResponse;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            throw new DataException($"Participant with updated name '{updatedDisplayName}' not found after {timeout} seconds.");
        }

        public IRestResponse RemoveParticipant(Guid hearingId, Guid participantId)
        {
            var endpoint = new BookingsApiUriFactory().HearingsParticipantsEndpoints.RemoveParticipantFromHearing(hearingId, participantId);
            var request = new RequestBuilder().Delete(endpoint);
            var client = new ApiClient(_bookingsApiUrl, _bookingsApiBearerToken).GetClient();
            var response = new RequestExecutor(request).SendToApi(client);
            return response;
        }
    }
}
