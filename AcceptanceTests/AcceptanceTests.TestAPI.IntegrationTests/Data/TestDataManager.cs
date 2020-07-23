﻿using System.Linq;
using System.Threading.Tasks;
using AcceptanceTests.TestAPI.Common.Builders;
using AcceptanceTests.TestAPI.Common.Configuration;
using AcceptanceTests.TestAPI.DAL;
using AcceptanceTests.TestAPI.Domain;
using AcceptanceTests.TestAPI.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AcceptanceTests.TestAPI.IntegrationTests.Data
{
    public class TestDataManager
    {
        private readonly TestContext _context;
        private readonly ServicesConfiguration _services;
        private readonly DbContextOptions<TestApiDbContext> _dbContextOptions;

        public TestDataManager(TestContext context, ServicesConfiguration services,
            DbContextOptions<TestApiDbContext> dbContextOptions)
        {
            _context = context;
            _services = services;
            _dbContextOptions = dbContextOptions;
        }

        public async Task<User> SeedUser(UserType userType = UserType.Judge)
        {
            await using var db = new TestApiDbContext(_dbContextOptions);

            const Application application = Application.TestApi;

            var number = await IterateUserNumber(userType, application);

            var user = new UserBuilder(_context.Config.UsernameStem, number)
                .WithUserType(userType)
                .ForApplication(application)
                .BuildUser();

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        }

        public async Task<int> IterateUserNumber(UserType userType, Application application)
        {
            await using var db = new TestApiDbContext(_dbContextOptions);

            var users = await db.Users
                .Where(x => x.UserType == userType && x.Application == application)
                .AsNoTracking()
                .ToListAsync();

            if (users.Count.Equals(0))
            {
                return 1;
            }

            return users.Select(user => user.Number).ToList().Max() + 1;
        }

        public async Task DeleteUsers()
        {
            await using var db = new TestApiDbContext(_dbContextOptions);

            var users = await db.Users
                .Where(x =>  x.Application == Application.TestApi)
                .AsNoTracking()
                .ToListAsync();

            foreach (var user in users)
            {
                db.Remove(user);
                await db.SaveChangesAsync();
            }
        }
    }
}