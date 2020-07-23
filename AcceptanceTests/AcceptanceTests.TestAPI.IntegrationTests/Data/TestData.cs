using System.Collections.Generic;
using AcceptanceTests.TestAPI.Contract.Responses;
using AcceptanceTests.TestAPI.Domain;

namespace AcceptanceTests.TestAPI.IntegrationTests.Data
{
    public class TestData
    {
        public Allocation Allocation { get; set; }
        public User User { get; set; }
        public List<User> Users { get; set; }
        public List<UserDetailsResponse> UserResponses { get; set; }
    }
}
