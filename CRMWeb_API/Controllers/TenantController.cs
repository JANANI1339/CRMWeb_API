using CRMWeb_API.Data;
using CRMWeb_API.DTO;
using CRMWeb_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRMWeb_API.Controllers
{
    [Route("api/Tenant")]
    [ApiController]
    public class TenantController: ControllerBase
    {
        private readonly ILogger<TenantController> _logger;
        private readonly ApplicationDbContext _db;
        public TenantController(ILogger<TenantController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TenantDTO>> GetTenants()
        {
            _logger.LogInformation("Get all Tenants");
            return Ok(_db.Tenants);
        }

        [HttpGet("{id:int}", Name = "GetTenant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TenantDTO> GetTenant(int id)
        {
            if(id == 0)
            {
                _logger.LogError("Id cannot be 0 for GetTenant request");
                return BadRequest();
            }
            var tenant = _db.Tenants.FirstOrDefault(x => x.TenantId == id);
            if(tenant == null)
            {
                _logger.LogError($"No Tenant available for the provided TenantId {id}");
                return NotFound();
            }
            return Ok(_db.Tenants.FirstOrDefault(x => x.TenantId == id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TenantDTO> CreateTenant([FromBody]TenantDTO tenantDTO)
        {
            if (tenantDTO == null) {
                _logger.LogError($"Input for Tenant Creation is null");
                return BadRequest(tenantDTO);
            }
            if (tenantDTO.TenantId > 0)
            {
                _logger.LogError($"Invalid TenantId for Creating new Tenant");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Tenant tenant = new Tenant
            {
                TenantName = tenantDTO.TenantName,
                CreatedTime = DateTime.UtcNow,
                IsDeleted = false,
                EmailId = tenantDTO.EmailId,
            };
            
            _db.Tenants.Add(tenant);
            _db.SaveChanges();
            _logger.LogInformation("New Tenant Created");
            return CreatedAtRoute("GetTenant",new { id = tenantDTO.TenantId }, tenantDTO);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteTenant(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Id cannot be 0 for Delete Tenant request");
                return BadRequest();
            }
            var tenant = _db.Tenants.FirstOrDefault(u => u.TenantId == id);
            if (tenant == null)
            {
                _logger.LogError($"No Tenant available for the provided TenantId {id}");
                return NotFound();
            }
            
            _db.Tenants.Remove(tenant);
            _db.SaveChanges();
            _logger.LogInformation($"Deleted Tenant {tenant.TenantName}");
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateTenant(int id, [FromBody]TenantDTO tenantDto)
        {
            if (tenantDto == null || tenantDto.TenantId != id)
            {
                _logger.LogError($"Invalid TenantId for Updating Tenant");
                return BadRequest();
            }
            Tenant tenant = new Tenant
            {
                TenantName = tenantDto.TenantName,
                TenantId = id,
                UpdatedTime = DateTime.UtcNow,
                EmailId = tenantDto.EmailId,
                IsDeleted = false
            };
            _db.Tenants.Update(tenant);
            _db.SaveChanges();
            _logger.LogInformation($"Updated Tenant {tenantDto.TenantName}");
            return NoContent();
        }
    }
}
