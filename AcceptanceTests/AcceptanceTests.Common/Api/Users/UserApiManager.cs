using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using AcceptanceTests.Common.Api.Clients;
using AcceptanceTests.Common.Api.Requests;
using AcceptanceTests.Common.Api.Uris;
using AcceptanceTests.Common.Configuration.Users;
using RestSharp;

namespace AcceptanceTests.Common.Api.Users
{
    public class UserApiManager
    {
        private readonly string _userApiUrl;
        private readonly string _userApiBearerToken;

        public UserApiManager(string userApiUrl, string userApiBearerToken)
        {
            _userApiUrl = userApiUrl;
            _userApiBearerToken = userApiBearerToken;
        }

        public bool ParticipantsExistInAad(List<UserAccount> userAccounts, int timeout)
        {
            return UserManager.GetNonClerkParticipantUsers(userAccounts).Select(user => CheckIfParticipantExistsInAad(user.AlternativeEmail, timeout)).FirstOrDefault();
        }

        public bool CheckIfParticipantExistsInAad(string alternativeEmail, int timeout)
        {
            for (var i = 0; i < timeout; i++)
            {
                var response = GetUser(alternativeEmail);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            return false;
        }

        public IRestResponse GetUser(string email)
        {
            var endpoint = new UserApiUriFactory().UserEndpoints.GetUserByEmail(email);
            var request = new RequestBuilder().Get(endpoint);
            var client = new ApiClient(_userApiUrl, _userApiBearerToken).GetClient();
            var response = new RequestExecutor(request).SendToApi(client);
            return response;
        }
    }
}
