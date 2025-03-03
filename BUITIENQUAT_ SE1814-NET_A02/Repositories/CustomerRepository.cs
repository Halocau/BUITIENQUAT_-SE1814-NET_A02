using BUITIENQUAT__SE1814_NET_A02.Models;
using Buitienquat_SE1814_NET_A02.Models;

namespace BUITIENQUAT__SE1814_NET_A02.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly Ass2SignalRRazorPagesContext _context;

        public CustomerRepository(Ass2SignalRRazorPagesContext context)
        {
            _context = context;
        }

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void AddCustomers(List<Customer> customers)
        {
            _context.Customers.AddRange(customers);
            _context.SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer GetCustomerById(string id)
        {
            return _context.Customers.FirstOrDefault(c => c.CustomerId.Equals(id));
        }

        public List<Customer> SearchCustomersByDiscountRate(int discountRate)
        {
            return _context.Customers.Where(c => c.DiscountRate <= discountRate).ToList();
        }

        public void Update(Customer customer)
        {
            var cusId = GetCustomerById(customer.CustomerId);
            if (cusId != null)
            {
                _context.Customers.Update(customer);
                _context.SaveChanges();
            }
        }
    }
}
