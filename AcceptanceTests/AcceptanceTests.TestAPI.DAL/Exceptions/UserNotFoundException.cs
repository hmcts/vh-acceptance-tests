using System;

namespace AcceptanceTests.TestAPI.DAL.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(Guid userId) : base($"User with id {userId} does not exist")
        {
        }

        public UserNotFoundException(string username) : base($"User {username} does not exist")
        {
        }
    }
}
