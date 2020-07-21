using System;

namespace AcceptanceTests.TestAPI.DAL.Exceptions
{
    public class EnvironmentNotFoundException : Exception
    {
        public EnvironmentNotFoundException(string environmentName) : base($"Environment with name {environmentName} does not exist")
        {
        }
    }
}
