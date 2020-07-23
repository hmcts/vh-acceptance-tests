using System;
using AcceptanceTests.TestAPI.Domain.Enums;

namespace AcceptanceTests.TestAPI.DAL.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string username, Application application) : base($"User {username} in {application} already exists")
        {
        }
    }
}
