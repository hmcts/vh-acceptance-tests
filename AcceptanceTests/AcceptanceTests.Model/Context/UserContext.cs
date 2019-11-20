using System;
using System.Collections.Generic;
using System.Linq;
using AcceptanceTests.Model.Role;
using AcceptanceTests.Model.User;

namespace AcceptanceTests.Model.Context
{
    public class UserContext
    {
        public UserSecrets TestUserSecrets { get; set; }
        public List<TestUser> TestUsers { get; set; }
        public TestUser CurrentUser { get; set; }

        public string GetUsername(TestUser testUser)
        {
            return testUser.Displayname + TestUserSecrets.TestUsernameStem;
        }

        public TestUser GetFirstOrDefaultUserByRole(string role)
        {
            var user = TestUsers.FirstOrDefault(x => x.Role.Equals(EnumParser.ParseText<UserRole>(role)));

            if (user != null)
            {
                user.Username = GetUsername(user);
            } else
            {
                throw new Exception($"Couldn't find user with role {role} in UserContext.TestUsers");
            }
            return user;
        }

        public IEnumerable<TestUser> GetAllUsersByRole(string role)
        {
            var allUsers = TestUsers.Where(x => x.Role.Equals(EnumParser.ParseText<UserRole>(role))).ToList();

            if (allUsers != null)
            {
                return allUsers;
            }
            else
            {
                throw new Exception($"Couldn't find users with role {role} in UserContext.TestUsers");
            } 
        }
    }
}
