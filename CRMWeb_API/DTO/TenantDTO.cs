using System.ComponentModel.DataAnnotations;

namespace CRMWeb_API.DTO
{
    public class TenantDTO
    {
        public int TenantId { get; set; }

        [Required]
        [MaxLength(50)]
        public string TenantName { get; set; }

        public string EmailId { get; set; }
    }
}
