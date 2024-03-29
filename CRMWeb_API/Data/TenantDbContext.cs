using CRMWeb_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CRMWeb_API.Data
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options)
        : base(options)
        {
        }

        public DbSet<Tenant> Tenants { get; set; }

    }
}
