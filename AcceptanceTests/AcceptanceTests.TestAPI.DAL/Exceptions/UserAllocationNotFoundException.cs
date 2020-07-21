using System;

namespace AcceptanceTests.TestAPI.DAL.Exceptions
{
    public class UserAllocationNotFoundException : Exception
    {
        public UserAllocationNotFoundException(Guid userId) : base($"User with Id {userId} allocation does not exist")
        {
        }

        public UserAllocationNotFoundException(string username) : base($"Username {username} allocation does not exist")
        {
        }
    }
}
