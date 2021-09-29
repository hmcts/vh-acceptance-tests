using System.Collections.Generic;
using AcceptanceTests.Common.Api.Helpers;
using RestSharp;

namespace AcceptanceTests.Common.Api.Requests
{
    public static class RequestBuilder
    {
        public static RestRequest Get(string path) => new RestRequest(path, Method.GET);

        public static RestRequest Get(string path, Dictionary<string, string> queryParameters)
        {
            NUnit.Framework.TestContext.WriteLine($"RequestBuilder: path {path}");
            var request = new RestRequest(path, Method.GET);
            foreach (var (key, value) in queryParameters)
            {
            NUnit.Framework.TestContext.WriteLine($"RequestBuilder: add Key {key} value {value}");
                request.AddQueryParameter(key, value);
            }

            return request;
        }

        public static RestRequest Post(string path, object requestBody)
        {
            var request = new RestRequest(path, Method.POST);
            request.AddParameter("Application/json", RequestHelper.Serialise(requestBody),
                ParameterType.RequestBody);
            return request;
        }

        public static RestRequest Delete(string path) => new RestRequest(path, Method.DELETE);

        public static RestRequest Put(string path, object requestBody)
        {
            var request = new RestRequest(path, Method.PUT);
            request.AddParameter("Application/json", RequestHelper.Serialise(requestBody),
                ParameterType.RequestBody);
            return request;
        }

        public static RestRequest Patch(string path, object requestBody = null)
        {
            var request = new RestRequest(path, Method.PATCH);
            request.AddParameter("Application/json", RequestHelper.Serialise(requestBody),
                ParameterType.RequestBody);
            return request;
        }
    }
}
