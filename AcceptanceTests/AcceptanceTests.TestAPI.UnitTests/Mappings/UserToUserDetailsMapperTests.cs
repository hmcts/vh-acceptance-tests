using AcceptanceTests.TestAPI.Common.Builders;
using AcceptanceTests.TestAPI.Domain.Enums;
using AcceptanceTests.TestAPI.Mappings;
using FluentAssertions;
using NUnit.Framework;

namespace AcceptanceTests.UnitTests.Mappings
{
    public class UserToUserDetailsMapperTests
    {
        [Test]
        public void Should_map_all_properties()
        {
            var user = new UserBuilder("made_up_email_stem_for_test", 1)
                .WithUserType(UserType.Individual)
                .ForApplication(Application.AdminWeb)
                .Build();

            var response = UserToDetailsResponseMapper.MapUserToResponse(user);
            response.Should().BeEquivalentTo(user);
        }
    }
}
