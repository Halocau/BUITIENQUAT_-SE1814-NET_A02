using Buitienquat_SE1814_NET_A02.Models;

namespace BUITIENQUAT__SE1814_NET_A02.Repositories
{
    public interface ICustomerRepository
    {
        List<Customer> GetAllCustomers();
        List<Customer> SearchCustomersByDiscountRate(int discountRate);
        Customer GetCustomerById(string id);
        void Add(Customer customer);
        void Update(Customer customer);
        void AddCustomers(List<Customer> customers);
        void DeleteCustomer(Customer customer);
    }
}
