using CRMWeb_API.Dto;
using CRMWeb_API.Models;

namespace CRMWeb_API.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        Customer CreateCustomer(CreateCustomerRequest request);
        bool DeleteCustomer(int id);
    }
}
