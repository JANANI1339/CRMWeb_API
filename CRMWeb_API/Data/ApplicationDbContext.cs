using CRMWeb_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CRMWeb_API.Data
{
    public partial class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>().HasData(new Tenant
            {
                TenantId = 1,
                TenantName = "Tenant1",
                EmailId="",
                IsDeleted = false,
                CreatedTime= DateTime.Now,
            });
        }
    }
}
