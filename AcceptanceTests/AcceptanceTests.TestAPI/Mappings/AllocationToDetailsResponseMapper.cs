using AcceptanceTests.TestAPI.Contract.Responses;
using AcceptanceTests.TestAPI.Domain;

namespace AcceptanceTests.TestAPI.Mappings
{
    public static class AllocationToDetailsResponseMapper
    {
        public static AllocationDetailsResponse MapToResponse(Allocation allocation)
        {
            return new AllocationDetailsResponse
            {
                Username = allocation.Username,
                Allocated = allocation.Allocated,
                ExpiresAt = allocation.ExpiresAt
            };
        }
    }
}
