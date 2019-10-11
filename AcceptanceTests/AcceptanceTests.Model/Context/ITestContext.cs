using System;
using System.Collections.Generic;
using AcceptanceTests.Model.User;

namespace AcceptanceTests.Model.Context
{
    public interface ITestContext
    {
        string BaseUrl { get; set; }
        string VideoAppUrl { get; set; }
        string BookingsApiBearerToken { get; set; }
        string BookingsApiBaseUrl { get; set; }
        UserSecrets TestUserSecrets { get; set; }
        List<TestUser> TestUsers { get; set; }
        TestUser CurrentUser { get; set; }
        Guid HearingId { get; set; }

        TestUser GetFirstOrDefaultClerkUser();
        TestUser GetFirstOrDefaultIndividualUser();
        TestUser GetFirstOrDefaultRepresentativeUser();
        TestUser GetFirstOrDefaultCaseAdminUser();
        TestUser GetFirstOrDefaultNonAdminUser();
        IEnumerable<TestUser> GetAllCaseAdminUsers();
        IEnumerable<TestUser> GetAllIndividualUsers();
        IEnumerable<TestUser> GetAllRepresentativeUsers();
    }
}