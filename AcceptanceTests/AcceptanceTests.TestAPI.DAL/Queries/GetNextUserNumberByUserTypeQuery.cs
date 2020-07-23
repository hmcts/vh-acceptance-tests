using System.Linq;
using System.Threading.Tasks;
using AcceptanceTests.TestAPI.DAL.Helpers;
using AcceptanceTests.TestAPI.DAL.Queries.Core;
using AcceptanceTests.TestAPI.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AcceptanceTests.TestAPI.DAL.Queries
{
    public class GetNextUserNumberByUserTypeQuery : IQuery
    {
        public UserType UserType { get; set; }
        public Application Application { get; set; }

        public GetNextUserNumberByUserTypeQuery(UserType userType, Application application)
        {
            UserType = userType;
            Application = application;
        }
    }

    public class GetNextUserNumberByUserTypeQueryHandler : IQueryHandler<GetNextUserNumberByUserTypeQuery, Integer>
    {
        private readonly TestApiDbContext _context;

        public GetNextUserNumberByUserTypeQueryHandler(TestApiDbContext context)
        {
            _context = context;
        }

        public async Task<Integer> Handle(GetNextUserNumberByUserTypeQuery query)
        {
            var users = await _context.Users
                .Where(x => x.UserType == query.UserType && x.Application == query.Application)
                .AsNoTracking()
                .ToListAsync();

            if (users.Count.Equals(0))
            {
                return 1;
            }

            return users.Select(user => user.Number).ToList().Max() + 1;
        }
    }
}
