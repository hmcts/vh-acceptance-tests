using System;
using System.Net;
using AcceptanceTests.Common.Api.Clients;
using AcceptanceTests.Common.Api.Requests;
using AcceptanceTests.Common.Api.Uris;
using FluentAssertions;
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

        public IRestResponse GetHearing(Guid? hearingId)
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

        public IRestResponse SetSuitabilityAnswers(Guid? hearingId, Guid? participantId, object suitabilityRequest)
        {
            if (hearingId == null || participantId == null)
            {
                throw new DataMisalignedException("Values cannot be null");
            }

            var endpoint =
                new BookingsApiUriFactory().HearingsParticipantsEndpoints.SuitabilityAnswers((Guid) hearingId,
                    (Guid) participantId);
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
    }
}
