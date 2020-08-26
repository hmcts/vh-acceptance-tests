namespace AcceptanceTests.Common.Api.Hearings
{
    public class TestApiManager
    {
        private readonly string _testApiUrl;
        private readonly string _testApiBearerToken;

        public TestApiManager(string testApiUrl, string testApiBearerToken)
        {
            _testApiUrl = testApiUrl;
            _testApiBearerToken = testApiBearerToken;
        }


    }
}
