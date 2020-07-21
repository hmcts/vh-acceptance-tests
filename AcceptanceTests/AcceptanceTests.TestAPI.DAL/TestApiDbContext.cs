using System;
using System.Linq;
using System.Reflection;
using AcceptanceTests.TestAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace AcceptanceTests.TestAPI.DAL
{
    public class TestApiDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Allocation> Allocations { get; set; }

        public TestApiDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var applyGenericMethods = typeof(ModelBuilder).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            var applyGenericApplyConfigurationMethods = applyGenericMethods.Where(m =>
                m.IsGenericMethod && m.Name.Equals("ApplyConfiguration", StringComparison.OrdinalIgnoreCase));
            var applyGenericMethod = applyGenericApplyConfigurationMethods.FirstOrDefault(m =>
                m.GetParameters().FirstOrDefault()?.ParameterType.Name == "IEntityTypeConfiguration`1");

            foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
                .Where(c => c.IsClass && !c.IsAbstract && !c.ContainsGenericParameters))
            {
                foreach (var iFace in type.GetInterfaces())
                {
                    if (!iFace.IsConstructedGenericType ||
                        iFace.GetGenericTypeDefinition() != typeof(IEntityTypeConfiguration<>)) continue;
                    if (applyGenericMethod != null)
                    {
                        var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(iFace.GenericTypeArguments[0]);
                        applyConcreteMethod.Invoke(modelBuilder, new[] { Activator.CreateInstance(type) });
                    }

                    break;
                }
            }
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified))
            {
                var updatedDateProperty =
                    entry.Properties.AsQueryable().FirstOrDefault(x => x.Metadata.Name == "UpdatedDate");
                if (updatedDateProperty != null)
                {
                    updatedDateProperty.CurrentValue = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }
    }
}
