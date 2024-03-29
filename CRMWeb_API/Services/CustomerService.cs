using CRMWeb_API.Data;
using CRMWeb_API.Dto;
using CRMWeb_API.Models;

namespace CRMWeb_API.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly ApplicationDbContext _context; // database context
        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var customers = _context.Customers.ToList();
            return customers;
        }

        // get a single product
        public Customer GetCustomerById(int id)
        {
            var customer = _context.Customers.Where(x => x.CustomerId == id).FirstOrDefault();
            return customer;
        }

        // create a new product
        public Customer CreateCustomer(CreateCustomerRequest request)
        {
            var customer = new Customer();
            customer.CustomerName = request.CustomerName;
            customer.CustomerCity = request.CustomerCity;

            _context.Add(customer);
            _context.SaveChanges();

            return customer;
        }


        public bool DeleteCustomer(int id)
        {
            var product = _context.Customers.Where(x => x.CustomerId == id).FirstOrDefault();

            if (product != null)
            {
                _context.Remove(product);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
