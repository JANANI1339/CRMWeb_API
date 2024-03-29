using CRMWeb_API.Models;
using CRMWeb_API.Services;
using Microsoft.EntityFrameworkCore;

namespace CRMWeb_API.Data
{
    public partial class ApplicationDbContext: DbContext
    {
        private readonly ICurrentTenantService _tenantService;
        public string CurrentTenantId { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentTenantService currentTenantService) : base(options)
        {
            _tenantService = currentTenantService;
            CurrentTenantId = _tenantService.TenantId;
        }

        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>().HasQueryFilter(a => a.TenantId == CurrentTenantId);
        }


        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<TenantChecker>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = CurrentTenantId;
                        break;
                }
            }
            var result = base.SaveChanges();
            return result;
        }

    }
}
