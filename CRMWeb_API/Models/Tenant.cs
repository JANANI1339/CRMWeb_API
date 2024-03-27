namespace CRMWeb_API.Models
{
    public class Tenant
    {
        public int TenantId { get; set; }

        public string TenantName { get; set;}

        public string EmailId { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool isDeleted { get; set; }

    }
}
