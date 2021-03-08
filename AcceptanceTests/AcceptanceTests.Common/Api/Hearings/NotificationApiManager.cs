using AcceptanceTests.Common.Api.Helpers;
using AcceptanceTests.Common.Api.Uris;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using System.Net;

namespace AcceptanceTests.Common.Api.Hearings
{
    public class NotificationApiManager
    {
        private readonly string _apiUrl;
        private readonly string _bearerToken;

        public NotificationApiManager(string notificationApiUrl, string notificationApiBearerToken)
        {
            _apiUrl = notificationApiUrl;
            _bearerToken = notificationApiBearerToken;
        }

        public int PollForPasswordResetNotificationCount(string email, int timeout = 60)
        {
            var endpoint = NotificationApiUriFactory.NotificationEndpoints.GetNotificationByEmail(email);
            var response = new Polling()
                            .WithEndpoint(endpoint)
                            .Url(_apiUrl)
                            .Token(_bearerToken)
                            .UntilStatusIs(HttpStatusCode.OK)
                            .Poll(timeout);

            var notificationResponses = new JsonDeserializer().Deserialize<List<NotificationResponse>>(response);                 
            return notificationResponses != null ? notificationResponses.Count:0;
        }

        public bool PollForNotificationByHearingAndParticipant(string notificationType, string hearingId, string participantId, int timeout = 60)
        {
            var endpoint = NotificationApiUriFactory.NotificationEndpoints.GetNotificationByHearingAndParticipant(notificationType,hearingId,participantId);
            var response = new Polling()
                            .WithEndpoint(endpoint)
                            .Url(_apiUrl)
                            .Token(_bearerToken)
                            .UntilStatusIs(HttpStatusCode.OK)
                            .Poll(timeout);

            var notificationResponse = new JsonDeserializer().Deserialize<NotificationResponse>(response);
            return !string.IsNullOrEmpty(notificationResponse?.Id);
        }

        public class NotificationResponse
        {
            public string Id { get; set; }
        }

    }
}
