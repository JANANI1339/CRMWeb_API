using CRMWeb_API.Data;
using CRMWeb_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CRMWeb_API.Controllers
{
    [Authorize]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Tenant>> GetTenants()
        {
            _logger.LogInformation("Get all Tenants");
            var tenants = _db.Tenants.ToList();
            return Ok(tenants);
        }

        [HttpGet("{id:string}", Name = "GetTenant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Tenant> GetTenant(string id)
        {
            if(string.IsNullOrEmpty(id))
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
            return _db.Tenants.FirstOrDefault(x => x.TenantId == id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Tenant> CreateTenant([FromBody]Tenant tenant)
        {
            if (tenant == null) {
                _logger.LogError($"Input for Tenant Creation is null");
                return BadRequest(tenant);
            }
            if (string.IsNullOrEmpty(tenant.TenantId))
            {
                _logger.LogError($"Invalid TenantId for Creating new Tenant");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            tenant.CreatedTime = DateTime.Now;
            _db.Tenants.Add(tenant);
            _db.SaveChanges();
            _logger.LogInformation("New Tenant Created");
            return CreatedAtRoute("GetTenant",new { id = tenant.TenantId }, tenant);
        }

        [HttpDelete("{id:string}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteTenant(string id)
        {
            if (string.IsNullOrEmpty(id))
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
            tenant.IsDeleted = true;
            _db.Tenants.Update(tenant);
            _db.SaveChanges();
            _logger.LogInformation($"Deleted Tenant {tenant.TenantName}");
            return NoContent();
        }

        [HttpPut("{id:string}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateTenant(string id, [FromBody]Tenant tenant)
        {
            if (tenant == null || tenant.TenantId != id)
            {
                _logger.LogError($"Invalid TenantId for Updating Tenant");
                return BadRequest();
            }
            tenant.UpdatedTime = DateTime.Now;
            try
            {
                _db.Tenants.Update(tenant);
                _db.SaveChanges();

                _logger.LogInformation($"Updated Tenant {tenant.TenantName}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TenantExists(tenant.TenantId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        private bool TenantExists(string id)
        {
            return _db.Tenants.Any(e => e.TenantId == id);
        }
    }
}
