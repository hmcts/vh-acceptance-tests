using System;
using System.Threading.Tasks;
using AcceptanceTests.TestAPI.DAL.Queries.Core;
using AcceptanceTests.TestAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace AcceptanceTests.TestAPI.DAL.Queries
{
    public class GetUserByIdQuery : IQuery
    {
        public Guid Id { get; set; }

        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, User>
    {
        private readonly TestApiDbContext _context;

        public GetUserByIdQueryHandler(TestApiDbContext context)
        {
            _context = context;
        }

        public async Task<User> Handle(GetUserByIdQuery query)
        {
            return await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == query.Id);
        }
    }
}
