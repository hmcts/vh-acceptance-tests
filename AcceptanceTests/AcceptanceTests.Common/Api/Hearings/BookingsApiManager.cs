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

        public IRestResponse RemoveParticipant(Guid hearingId, Guid participantId)
        {
            var endpoint = BookingsApiUriFactory.HearingsParticipantsEndpoints.RemoveParticipantFromHearing(hearingId, participantId);
            var request = RequestBuilder.Delete(endpoint);
            var client = ApiClient.CreateClient(_bookingsApiUrl, _bookingsApiBearerToken);
            return RequestExecutor.SendToApi(request, client);
        }
    }
}
