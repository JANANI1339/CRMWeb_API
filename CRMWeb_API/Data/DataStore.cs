using CRMWeb_API.DTO;

namespace CRMWeb_API.Data
{
    public static class DataStore
    {
        public static List<TenantDTO> TenantList = new List<TenantDTO>
            {
                new TenantDTO{TenantId = 1, TenantName="Tenant1" },
                new TenantDTO{TenantId = 2, TenantName="Tenant2" }
            };
    }
}
