using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using AcceptanceTests.TestAPI.Contract.Responses;
using AcceptanceTests.TestAPI.DAL.Queries;
using AcceptanceTests.TestAPI.DAL.Queries.Core;
using AcceptanceTests.TestAPI.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AcceptanceTests.TestAPI.Controllers
{
    [Produces("application/json")]
    [Route("HealthCheck")]
    [AllowAnonymous]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly IQueryHandler _queryHandler;

        public HealthCheckController(IQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
        }

        /// <summary>
        /// Check Service Health
        /// </summary>
        /// <returns>Error if fails, otherwise OK status</returns>
        [HttpGet("health")]
        [SwaggerOperation(OperationId = "CheckServiceHealth")]
        [ProducesResponseType(typeof(HealthCheckResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HealthCheckResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> HealthAsync()
        {
            var response = new HealthCheckResponse {Version = GetApplicationVersion()};
            try
            {
                const string username = "health";
                var query = new GetUserByUsernameQuery(username);
                await _queryHandler.Handle<GetUserByUsernameQuery, User>(query);
                response.Successful = true;
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.ErrorMessage = ex.Message;
            }

            return response.Successful ? Ok(response) : StatusCode((int)HttpStatusCode.InternalServerError, response);
        }

        private static HealthCheckResponse.ApplicationVersion GetApplicationVersion()
        {
            var applicationVersion = new HealthCheckResponse.ApplicationVersion
            {
                Version = GetExecutingAssemblyAttribute<AssemblyFileVersionAttribute>(a => a.Version)
            };
            return applicationVersion;
        }

        private static string GetExecutingAssemblyAttribute<T>(Func<T, string> value) where T : Attribute
        {
            var attribute = (T)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(T));
            return value.Invoke(attribute);
        }
    }
}
