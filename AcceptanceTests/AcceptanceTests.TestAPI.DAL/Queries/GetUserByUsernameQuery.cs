using System.Threading.Tasks;
using AcceptanceTests.TestAPI.DAL.Queries.Core;
using AcceptanceTests.TestAPI.Domain;
using AcceptanceTests.TestAPI.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AcceptanceTests.TestAPI.DAL.Queries
{
    public class GetUserByUsernameQuery : IQuery
    {
        public string Username { get; set; }
        public Application Application { get; set; }

        public GetUserByUsernameQuery(string username, Application application)
        {
            Username = username;
            Application = application;
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
                .SingleOrDefaultAsync(
                    x => x.Username.ToLower() == query.Username.ToLower() && x.Application == query.Application);
        }
    }
}
