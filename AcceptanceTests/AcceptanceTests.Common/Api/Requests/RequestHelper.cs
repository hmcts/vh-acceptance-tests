using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AcceptanceTests.Common.Api.Requests
{
    public static class RequestHelper
    {
        public static string SerialiseRequestToSnakeCaseJson(object request)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            return JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
        }

        public static T DeserialiseSnakeCaseJsonToResponse<T>(string response)
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
    }
}
