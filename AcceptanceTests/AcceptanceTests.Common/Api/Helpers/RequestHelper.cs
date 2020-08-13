using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AcceptanceTests.Common.Api.Helpers
{
    public static class RequestHelper
    {
        public static string Serialise(object request)
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            return JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
        }

        public static T Deserialise<T>(string response)
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            return JsonConvert.DeserializeObject<T>(response, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
        }

        public static HttpResponseMessage CreateHttpResponseMessage(object serializeObject, HttpStatusCode httpStatusCode)
        {
            return new HttpResponseMessage(httpStatusCode)
            {
                Content = new StringContent(JsonConvert.SerializeObject(serializeObject), System.Text.Encoding.UTF8, "application/json")
            };
        }

        public static HttpResponseMessage CreateHttpResponseMessage(string content, HttpStatusCode httpStatusCode)
        {
            return new HttpResponseMessage(httpStatusCode)
            {
                Content = new StringContent(content)
            };
        }
    }
}
