using System;
using AcceptanceTests.TestAPI.Domain.Enums;

namespace AcceptanceTests.TestAPI.IntegrationTests.Configuration
{
    public static class ApiUriFactory
    {
        public static class AllocationEndpoints
        {
            public const string ApiRoot = "allocations";
            public static string GetAllocationByUserId(Guid userId) => $"{ApiRoot}/{userId}";
            public static string AllocateByUserId(Guid userId) => $"{ApiRoot}/{userId}";
            public static string AllocateByUserTypeAndApplication => ApiRoot;
            public static string CreateAllocation => ApiRoot;
            public static string DeleteAllocation => ApiRoot;
        }

        public static class HealthCheckEndpoints
        {
            private const string ApiRoot = "/healthCheck";
            public static string CheckServiceHealth => $"{ApiRoot}/health";
        }

        public static class UserEndpoints
        {
            public const string ApiRoot = "users";
            public static string GetUserById(Guid userId) => $"{ApiRoot}/{userId:D}";
            public static string GetAllUsersByUserTypeAndApplication(UserType userType, Application application) => $"{ApiRoot}/?userType={userType}&application={application}";
            public static string GetIteratedUserNumber(UserType userType, Application application) => $"{ApiRoot}/iterate/?userType={userType}&application={application}";
            public static string CreateUser => ApiRoot;
            public static string DeleteUser(Guid userId) => $"{ApiRoot}/?userId={userId:D}";
        }
    }
}
