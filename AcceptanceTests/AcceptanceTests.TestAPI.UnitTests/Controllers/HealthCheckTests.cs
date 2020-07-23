using System;
using System.Net;
using System.Threading.Tasks;
using AcceptanceTests.TestAPI.Common.Builders;
using AcceptanceTests.TestAPI.Contract.Responses;
using AcceptanceTests.TestAPI.Controllers;
using AcceptanceTests.TestAPI.DAL.Queries;
using AcceptanceTests.TestAPI.DAL.Queries.Core;
using AcceptanceTests.TestAPI.Domain;
using AcceptanceTests.TestAPI.Domain.Enums;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace AcceptanceTests.TestAPI.UnitTests.Controllers
{
    public class HealthCheckTests
    {
        private HealthCheckController _controller;
        private Mock<IQueryHandler> _mockQueryHandler;

        [SetUp]
        public void Setup()
        {
            _mockQueryHandler = new Mock<IQueryHandler>();
        }

        [Test]
        public async Task Should_return_ok_result_when_database_is_connected()
        {
            const string emailStem = "made_up_email_stem_for_test";
            const int userNumber = 1;
            var application = Application.TestApi;

            var user = new UserBuilder(emailStem, userNumber)
                .WithUserType(UserType.Judge)
                .ForApplication(application)
                .BuildUser();

            var query = new GetUserByUsernameQuery(user.Username, application);
            
            _controller = new HealthCheckController(_mockQueryHandler.Object);
            _mockQueryHandler.Setup(x => x.Handle<GetUserByUsernameQuery, User>(query))
                .Returns(Task.FromResult(user));

            var result = await _controller.HealthAsync();
            var typedResult = (OkObjectResult)result;
            typedResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        public async Task Should_return_internal_server_error_result_when_database_is_not_connected()
        {
            var exception = new AggregateException("database connection failed");

            _controller = new HealthCheckController(_mockQueryHandler.Object);
            _mockQueryHandler
                .Setup(x => x.Handle<GetUserByUsernameQuery, User>(It.IsAny<GetUserByUsernameQuery>()))
                .ThrowsAsync(exception);

            var result = await _controller.HealthAsync();
            var typedResult = (ObjectResult)result;
            typedResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            var response = (HealthCheckResponse)typedResult.Value;
            response.Successful.Should().BeFalse();
            response.ErrorMessage.Should().NotBeNullOrWhiteSpace();
        }

        [Test]
        public async Task Should_return_the_application_version_from_assembly()
        {
            var result = await _controller.HealthAsync();
            var typedResult = (ObjectResult)result;
            var response = (HealthCheckResponse)typedResult.Value;
            response.Version.Version.Should().NotBeNullOrEmpty();
        }
    }
}