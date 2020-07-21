using System;

namespace AcceptanceTests.TestAPI.DAL.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string username, string environment) : base($"User {username} with environment {environment} already exists")
        {
        }
    }
}
