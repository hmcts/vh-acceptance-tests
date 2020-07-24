using System.Threading.Tasks;
using AcceptanceTests.TestAPI.DAL.Queries.Core;
using AcceptanceTests.TestAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace AcceptanceTests.TestAPI.DAL.Queries
{
    public class GetUserByUsernameQuery : IQuery
    {
        public string Username { get; set; }

        public GetUserByUsernameQuery(string username)
        {
            Username = username;
        }
    }

    public class GetUserByUsernameQueryHandler : IQueryHandler<GetUserByUsernameQuery, User>
    {
        private readonly TestApiDbContext _context;

        public GetUserByUsernameQueryHandler(TestApiDbContext context)
        {
            _context = context;
        }

        public async Task<User> Handle(GetUserByUsernameQuery query)
        {
            return await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Username.ToLower() == query.Username.ToLower());
        }
    }
}
