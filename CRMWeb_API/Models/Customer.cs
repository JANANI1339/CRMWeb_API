namespace CRMWeb_API.Models
{
    public class Customer:TenantChecker
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCity { get; set; }
        public string TenantId { get; set; }
    }
}
