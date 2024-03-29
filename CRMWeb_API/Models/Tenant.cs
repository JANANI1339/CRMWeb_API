using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMWeb_API.Models
{
    public class Tenant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TenantId { get; set; }

        [Required]
        public string TenantName { get; set;}

        [Required]
        public string EmailId { get; set; }

        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        public bool IsDeleted { get; set; }

    }
}
