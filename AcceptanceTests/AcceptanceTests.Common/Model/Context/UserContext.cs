using System;
using System.Collections.Generic;
using System.Linq;
using AcceptanceTests.Common.Model.User;

namespace AcceptanceTests.Common.Model.Context
{
    public class UserContext
    {
        public UserSecrets TestUserSecrets { get; set; }
        public List<UserAccountBase> TestUsers { get; set; }
        public UserAccountBase CurrentUser { get; set; }

        public string GetUsername(UserAccountBase testUser)
        {
            return testUser.DisplayName + TestUserSecrets.TestUsernameStem;
        }

        public UserAccountBase GetFirstOrDefaultUserByRole(string role)
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

        public IEnumerable<UserAccountBase> GetAllUsersByRole(string role)
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
