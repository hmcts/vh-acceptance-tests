using AcceptanceTests.TestAPI.DAL.Helpers;
using AcceptanceTests.TestAPI.Mappings;
using FluentAssertions;
using NUnit.Framework;

namespace AcceptanceTests.TestAPI.UnitTests.Mappings
{
    public class NumberToResponseMapperTests
    {
        [Test]
        public void Should_map_all_properties()
        {
            var number = new Integer(1);
            var response = NumberToResponseMapper.MapToResponse(number);
            response.Number.Should().Be(number);
        }
    }
}
