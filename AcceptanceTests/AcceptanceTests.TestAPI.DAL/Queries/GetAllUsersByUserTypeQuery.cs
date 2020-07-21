using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcceptanceTests.TestAPI.DAL.Queries.Core;
using AcceptanceTests.TestAPI.Domain;
using AcceptanceTests.TestAPI.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AcceptanceTests.TestAPI.DAL.Queries
{
    public class GetAllUsersByUserTypeQuery : IQuery
    {
        public UserType UserType { get; set; }
        public Application Application { get; set; }

    public GetAllUsersByUserTypeQuery(UserType userType, Application application)
        {
            UserType = userType;
            Application = application;
        }
    }

    public class GetAllUsersByUserTypeQueryHandler : IQueryHandler<GetAllUsersByUserTypeQuery, List<User>>
    {
        private readonly TestApiDbContext _context;

        public GetAllUsersByUserTypeQueryHandler(TestApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> Handle(GetAllUsersByUserTypeQuery query)
        {
            return await _context.Users
                .Where(x => x.UserType == query.UserType && x.Application == query.Application)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
