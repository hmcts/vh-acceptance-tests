﻿using System.Net.Http;
using System.Threading.Tasks;
using AcceptanceTests.Common.Api.Helpers;

namespace AcceptanceTests.TestAPI.IntegrationTests.Helpers
{
    public static class Response
    {
        public static async Task<T> GetResponses<T>(HttpContent content)
        {
            var json = await content.ReadAsStringAsync();
            return RequestHelper.DeserialiseSnakeCaseJsonToResponse<T>(json);
        }
    }
}