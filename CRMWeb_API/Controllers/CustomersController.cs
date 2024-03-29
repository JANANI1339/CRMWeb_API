using CRMWeb_API.Dto;
using CRMWeb_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRMWeb_API.Controllers
{
    [Route("api/Customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService; 
        }

        
        [HttpGet]
        public IActionResult Get()
        {
            var list = _customerService.GetAllCustomers();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Post(CreateCustomerRequest request)
        {
            var result = _customerService.CreateCustomer(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _customerService.DeleteCustomer(id);
            return Ok(result);
        }

    }
}
