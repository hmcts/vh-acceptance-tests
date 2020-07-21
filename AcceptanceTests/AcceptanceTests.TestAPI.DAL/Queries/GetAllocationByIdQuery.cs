using System;
using System.Threading.Tasks;
using AcceptanceTests.TestAPI.DAL.Queries.Core;
using AcceptanceTests.TestAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace AcceptanceTests.TestAPI.DAL.Queries
{
    public class GetAllocationByIdQuery : IQuery
    {
        public Guid AllocationId { get; set; }

    public GetAllocationByIdQuery(Guid allocationId)
    {
        AllocationId = allocationId;
    }
    }

    public class GetAllocationByIdQueryHandler : IQueryHandler<GetAllocationByIdQuery, Allocation>
    {
        private readonly TestApiDbContext _context;

        public GetAllocationByIdQueryHandler(TestApiDbContext context)
        {
            _context = context;
        }

        public async Task<Allocation> Handle(GetAllocationByIdQuery query)
        {
            return await _context.Allocations
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == query.AllocationId);
        }
    }
}
