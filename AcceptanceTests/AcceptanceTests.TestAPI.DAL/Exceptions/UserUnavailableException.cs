using System;

namespace AcceptanceTests.TestAPI.DAL.Exceptions
{
    public class UserUnavailableException : Exception
    {
        public UserUnavailableException(Guid allocationId) : base($"User is already allocated. Allocation Id {allocationId}.")
        {
        }

        public UserUnavailableException(string username) : base($"User {username} is already allocated")
        {
        }

        public UserUnavailableException() : base($"User is already allocated")
        {
        }
    }
}
