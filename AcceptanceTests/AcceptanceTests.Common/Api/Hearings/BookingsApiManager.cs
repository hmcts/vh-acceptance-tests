using System;
using System.Data;
using System.Threading;
using AcceptanceTests.Common.Api.Clients;
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
            var endpoint = BookingsApiUriFactory.HearingsEndpoints.BookNewHearing;
            var request = RequestBuilder.Post(endpoint, hearingRequest);
            var client = ApiClient.CreateClient(_bookingsApiUrl, _bookingsApiBearerToken);
            return RequestExecutor.SendToApi(request, client);
        }

        public IRestResponse ConfirmHearingToCreateConference(Guid hearingId, object updateRequest)
        {
            var endpoint = BookingsApiUriFactory.HearingsEndpoints.UpdateHearingStatus(hearingId);
            var request = RequestBuilder.Patch(endpoint, updateRequest);
            var client = ApiClient.CreateClient(_bookingsApiUrl, _bookingsApiBearerToken);
            return RequestExecutor.SendToApi(request, client);
        }

        public IRestResponse GetHearing(Guid hearingId)
        {
            var endpoint = BookingsApiUriFactory.HearingsEndpoints.GetHearingDetailsById(hearingId);
            var request = RequestBuilder.Get(endpoint);
            var client = ApiClient.CreateClient(_bookingsApiUrl, _bookingsApiBearerToken);
            return RequestExecutor.SendToApi(request, client);
        }

        public IRestResponse GetHearingsForUsername(string username)
        {
            var endpoint = BookingsApiUriFactory.HearingsEndpoints.GetHearingsByUsername(username);
            var request = RequestBuilder.Get(endpoint);
            var client = ApiClient.CreateClient(_bookingsApiUrl, _bookingsApiBearerToken);
            return RequestExecutor.SendToApi(request, client);
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
            var endpoint = BookingsApiUriFactory.HearingsParticipantsEndpoints.SuitabilityAnswers(hearingId, participantId);
            var request = RequestBuilder.Put(endpoint, suitabilityRequest);
            var client = ApiClient.CreateClient(_bookingsApiUrl, _bookingsApiBearerToken);
            return RequestExecutor.SendToApi(request, client);
        }

        public IRestResponse GetSuitabilityAnswers(string username)
        {
            var endpoint = BookingsApiUriFactory.PersonEndpoints.GetSuitabilityAnswersByEmail(username);
            var request = RequestBuilder.Get(endpoint);
            var client = ApiClient.CreateClient(_bookingsApiUrl, _bookingsApiBearerToken);
            return RequestExecutor.SendToApi(request, client);
        }

        public IRestResponse UpdateHearing(Guid hearingId, object updateRequest)
        {
            var endpoint = BookingsApiUriFactory.HearingsEndpoints.UpdateHearingDetails(hearingId);
            var request = RequestBuilder.Put(endpoint, updateRequest);
            var client = ApiClient.CreateClient(_bookingsApiUrl, _bookingsApiBearerToken);
            return RequestExecutor.SendToApi(request, client);
        }

        public IRestResponse UpdateHearingDetails(Guid hearingId, object updateRequest)
        {
            var endpoint = BookingsApiUriFactory.HearingsEndpoints.UpdateHearingStatus(hearingId);
            var request = RequestBuilder.Patch(endpoint, updateRequest);
            var client = ApiClient.CreateClient(_bookingsApiUrl, _bookingsApiBearerToken);
            return RequestExecutor.SendToApi(request, client);
        }

        public IRestResponse DeleteHearing(Guid hearingId)
        {
            var endpoint = BookingsApiUriFactory.HearingsEndpoints.RemoveHearing(hearingId);
            var request = RequestBuilder.Delete(endpoint);
            var client = ApiClient.CreateClient(_bookingsApiUrl, _bookingsApiBearerToken);
            return RequestExecutor.SendToApi(request, client);
        }

        public IRestResponse AddParticipantsToHearing(Guid hearingId, object participantsRequest)
        {
            var endpoint = BookingsApiUriFactory.HearingsParticipantsEndpoints.AddParticipantsToHearing(hearingId);
            var request = RequestBuilder.Post(endpoint, participantsRequest);
            var client = ApiClient.CreateClient(_bookingsApiUrl, _bookingsApiBearerToken);
            return RequestExecutor.SendToApi(request, client);
        }

        public IRestResponse UpdateParticipantDetails(Guid hearingId, Guid participantId, object updateRequest)
        {
            var endpoint = BookingsApiUriFactory.HearingsParticipantsEndpoints.UpdateParticipantDetails(hearingId, participantId);
            var request = RequestBuilder.Put(endpoint, updateRequest);
            var client = ApiClient.CreateClient(_bookingsApiUrl, _bookingsApiBearerToken);
            return RequestExecutor.SendToApi(request, client);
        }

        public IRestResponse GetHearingsByAnyCaseType(int limit)
        {
            var endpoint = BookingsApiUriFactory.HearingsEndpoints.GetHearingsByAnyCaseType(limit);
            var request = RequestBuilder.Get(endpoint);
            var client = ApiClient.CreateClient(_bookingsApiUrl, _bookingsApiBearerToken);
            return RequestExecutor.SendToApi(request, client);
        }

        public bool PollForParticipantNameUpdated(string username, string updatedDisplayName, int timeout = 60)
        {
            for (var i = 0; i < timeout; i++)
            {
                var rawResponse = GetHearingsForUsername(username);
                if (!rawResponse.IsSuccessful) continue;
                if (rawResponse.Content.ToLower().Contains(updatedDisplayName.ToLower()))
                {
                    return true;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            return false;
        }

        public IRestResponse RemoveParticipant(Guid hearingId, Guid participantId)
        {
            var endpoint = BookingsApiUriFactory.HearingsParticipantsEndpoints.RemoveParticipantFromHearing(hearingId, participantId);
            var request = RequestBuilder.Delete(endpoint);
            var client = ApiClient.CreateClient(_bookingsApiUrl, _bookingsApiBearerToken);
            return RequestExecutor.SendToApi(request, client);
        }
    }
}
