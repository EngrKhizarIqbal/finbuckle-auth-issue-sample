using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;

namespace HelpDesk.MultiTenant.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly TenantInfo _tenantInfo;

        public ApplicationDbContext(TenantInfo tenantInfo, DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            _tenantInfo = tenantInfo ?? throw new NullReferenceException(nameof(tenantInfo));
            ChangeTracker.LazyLoadingEnabled = false;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrWhiteSpace(_tenantInfo.ConnectionString)) throw new NullReferenceException(nameof(_tenantInfo.ConnectionString));

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_tenantInfo.ConnectionString).EnableSensitiveDataLogging();
            }

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }

    public class ToDoItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Completed { get; set; }
    }

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private static readonly string _designTimeConnectionString = $"Data Source=(localdb)\\MSSQLLocalDb; Initial Catalog=HelpDesk.MultiTenant.idsa.Core; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False";

        public ApplicationDbContext CreateDbContext(string[] args)
        {            
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(_designTimeConnectionString);

            return new ApplicationDbContext(new TenantInfo(
                id: Guid.Empty.ToString(),
                identifier: Guid.Empty.ToString(),
                name: Guid.Empty.ToString(),
                _designTimeConnectionString,
                items: null
            ),
            optionsBuilder.Options);
        }
    }
}