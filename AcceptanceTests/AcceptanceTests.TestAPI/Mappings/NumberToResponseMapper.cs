using AcceptanceTests.TestAPI.Contract.Responses;
using AcceptanceTests.TestAPI.DAL.Helpers;

namespace AcceptanceTests.TestAPI.Mappings
{
    public static class NumberToResponseMapper
    {
        public static IteratedUserNumberResponse MapToResponse(Integer integer)
        {
            return new IteratedUserNumberResponse
            {
                Number = integer
            };
        }
    }
}
