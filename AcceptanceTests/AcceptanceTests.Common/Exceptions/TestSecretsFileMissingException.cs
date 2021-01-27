using System;

namespace AcceptanceTests.Common.Exceptions
{
    public class TestSecretsFileMissingException : Exception
    {
        public TestSecretsFileMissingException(string env) : base($"Missing test secrets for running against: {env}") {}
    }
}
