using System.Threading.Tasks;
using AcceptanceTests.TestAPI.DAL.Queries.Core;
using AcceptanceTests.TestAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace AcceptanceTests.TestAPI.DAL.Queries
{
    public class GetAllocationByUsernameQuery : IQuery
    {
        public string Username { get; set; }

        public GetAllocationByUsernameQuery(string username)
        {
            Username = username;
        }
    }

    public class GetAllocationByUsernameQueryHandler : IQueryHandler<GetAllocationByUsernameQuery, Allocation>
    {
        private readonly TestApiDbContext _context;

        public GetAllocationByUsernameQueryHandler(TestApiDbContext context)
        {
            _context = context;
        }

        public async Task<Allocation> Handle(GetAllocationByUsernameQuery query)
        {
            return await _context.Allocations
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Username == query.Username);
        }
    }
}
