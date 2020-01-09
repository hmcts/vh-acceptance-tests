﻿namespace AcceptanceTests.Common.Api.Uris
{
    public class UserApiUriFactory
    {
        public UserApiUriFactory()
        {
            UserEndpoints = new UserEndpoints();
            AccountEndpoints = new AccountEndpoints();
            HealthCheckEndpoints = new HealthCheckEndpoints();
        }

        public UserEndpoints UserEndpoints { get; set; }
        public AccountEndpoints AccountEndpoints { get; set; }
        public HealthCheckEndpoints HealthCheckEndpoints { get; set; }
    }

    public class AccountEndpoints
    {
        private static string ApiRoot => "accounts";
        public string AddUserToGroup => $"{ApiRoot}/user/group";

        public string GetGroupByName(string groupName)
        {
            return $"{ApiRoot}/group/?name={groupName}";
        }

        public string GetGroupById(string groupId)
        {
            return $"{ApiRoot}/group/{groupId}";
        }

        public string GetGroupsForUser(string userId)
        {
            return $"{ApiRoot}/user/{userId}/groups";
        }
    }

    public class UserEndpoints
    {
        private static string ApiRoot => "users";
        public string CreateUser => $"{ApiRoot}";

        public string GetUserByAdUserId(string userId)
        {
            return $"{ApiRoot}/{userId}";
        }

        public string GetUserByAdUserName(string userName)
        {
            return $"{ApiRoot}/username/{userName}";
        }

        public string DeleteAnAadAccount(string userName)
        {
            return $"{ApiRoot}/username/{userName}";
        }

        public string GetUserByEmail(string email)
        {
            return $"{ApiRoot}/email/{email}";
        }

        public string GetJudges()
        {
            return $"{ApiRoot}/judges";
        }
    }

    public class HealthCheckEndpoints
    {
        private static string ApiRoot => "healthcheck";

        public string CheckServiceHealth()
        {
            return $"{ApiRoot}/health";
        }
    }
}
