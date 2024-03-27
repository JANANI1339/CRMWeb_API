using CRMWeb_API.Data;
using CRMWeb_API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CRMWeb_API.Controllers
{
    [Route("api/Tenant")]
    [ApiController]
    public class TenantController: ControllerBase
    {
        private readonly ILogger<TenantController> _logger;
        public TenantController(ILogger<TenantController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TenantDTO>> GetTenants()
        {
            _logger.LogInformation("Get all Tenants");
            return Ok(DataStore.TenantList);
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
            var tenant = DataStore.TenantList.FirstOrDefault(x => x.TenantId == id);
            if(tenant == null)
            {
                _logger.LogError($"No Tenant available for the provided TenantId {id}");
                return NotFound();
            }
            return Ok(DataStore.TenantList.FirstOrDefault(x => x.TenantId == id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TenantDTO> CreateTenant([FromBody]TenantDTO tenant)
        {
            if (tenant == null) {
                _logger.LogError($"Input for Tenant Creation is null");
                return BadRequest(tenant);
            }
            if (tenant.TenantId > 0)
            {
                _logger.LogError($"Invalid TenantId for Creating new Tenant");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            tenant.TenantId = DataStore.TenantList.OrderByDescending(u=>u.TenantId).First().TenantId+1;
            DataStore.TenantList.Add(tenant);
            _logger.LogInformation("New Tenant Created");
            return CreatedAtRoute("GetTenant",new { id = tenant.TenantId }, tenant);
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
            var tenant = DataStore.TenantList.FirstOrDefault(u => u.TenantId == id);
            if (tenant == null)
            {
                _logger.LogError($"No Tenant available for the provided TenantId {id}");
                return NotFound();
            }
            
            DataStore.TenantList.Remove(tenant);
            _logger.LogInformation($"Deleted Tenant {tenant.TenantName}");
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateTenant(int id, [FromBody]TenantDTO tenant)
        {
            if (tenant == null || tenant.TenantId != id)
            {
                _logger.LogError($"Invalid TenantId for Updating Tenant");
                return BadRequest();
            }
            var tenantdetail = DataStore.TenantList.FirstOrDefault(x => x.TenantId == id);
            if (tenantdetail == null)
            {
                _logger.LogError($"No Tenant available for the provided TenantId {id}");
                return BadRequest();
            }
            tenantdetail.TenantName = tenant.TenantName;
            tenantdetail.EmailId = tenant.EmailId;
            _logger.LogInformation($"Updated Tenant {tenant.TenantName}");
            return NoContent();
        }
    }
}
