using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.MultiTenant.MultiTenant
{
    public class QuodyMutiTenantDbContext : IdentityDbContext<IdentityUser>
    {
        public QuodyMutiTenantDbContext(DbContextOptions<QuodyMutiTenantDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TenantInfo>().Ignore(p => p.MultiTenantContext);

            modelBuilder.Entity<TenantInfo>()
                .Property(p => p.Items)
                .HasConversion(
                    c => JsonConvert.SerializeObject(c),
                    c => c == null ? new Dictionary<string, object>() : JsonConvert.DeserializeObject<IDictionary<string, object>>(c)
                );            
        }

        public DbSet<QuodyTenantInfo> QuodyTenantInfo { get; set; }
    }

    public class QuodyTenantInfo : TenantInfo
    {
        [Required(AllowEmptyStrings = false)]
        [ForeignKey("CreatedBy")]
        public string UserId { get; set; }
        public IdentityUser CreatedBy { get; set; }
    }
}
