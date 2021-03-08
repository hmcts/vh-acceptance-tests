using System;
using System.Collections.Generic;
using System.Text;

namespace AcceptanceTests.Common.Api.Uris
{
    public static class NotificationApiUriFactory
    {
        public static class NotificationEndpoints
        {
            private const string ApiRoot = "notification";
            public static string GetNotificationByEmail(string email) => $"{ApiRoot}/{email}";

            public static string GetNotificationByHearingAndParticipant(string notificationType, string hearingId, string participantId) => $"{ApiRoot}/{notificationType}/{hearingId}/{participantId}";
        }
    }
}
