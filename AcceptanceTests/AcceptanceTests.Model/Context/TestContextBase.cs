using System;
using System.Collections.Generic;
using System.Linq;
using AcceptanceTests.Model.User;
using AcceptanceTests.Model.Role;

namespace AcceptanceTests.Model.Context
{
    public class TestContextBase : ITestContext
    {
        public string BaseUrl { get; set; }
        public string VideoAppUrl { get; set; }
        public string BookingsApiBearerToken { get; set; }
        public string BookingsApiBaseUrl { get; set; }
        public UserSecrets TestUserSecrets { get; set; }
        public List<TestUser> TestUsers { get; set; }
        public TestUser CurrentUser { get; set; }
        public Guid HearingId { get; set; }

        public TestUser GetFirstOrDefaultClerkUser()
        {
            return TestUsers.FirstOrDefault(x => x.Role.Equals(UserRole.CLERK));
        }

        public TestUser GetFirstOrDefaultCaseAdminUser()
        {
            return TestUsers.FirstOrDefault(x => x.Role.Equals(UserRole.CASE_ADMIN));
        }

        public TestUser GetFirstOrDefaultNonAdminUser()
        {
            return GetFirstOrDefaultIndividualUser();
        }

        public IEnumerable<TestUser> GetAllCaseAdminUsers()
        {
            return TestUsers.Where(x => x.Role.Equals(UserRole.CASE_ADMIN)).ToList();
        }

        public TestUser GetFirstOrDefaultIndividualUser()
        {
            return TestUsers.FirstOrDefault(x => x.Role.Equals(UserRole.INDIVIDUAL));
        }

        public TestUser GetFirstOrDefaultRepresentativeUser()
        {
            return TestUsers.FirstOrDefault(x => x.Role.Equals(UserRole.REPRESENTATIVE));
        }

        public IEnumerable<TestUser> GetAllIndividualUsers()
        {
            return TestUsers.Where(x => x.Role.Equals(UserRole.INDIVIDUAL)).ToList();
        }

        public IEnumerable<TestUser> GetAllRepresentativeUsers()
        {
            return TestUsers.Where(x => x.Role.Equals(UserRole.REPRESENTATIVE)).ToList();
        }
    }
}
