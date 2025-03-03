using BUITIENQUAT__SE1814_NET_A02.Models;
using BUITIENQUAT__SE1814_NET_A02.Repositories;
using Buitienquat_SE1814_NET_A02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace BUITIENQUAT__SE1814_NET_A02.Pages.Customers
{
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public Customer Customer { get; set; }
        private readonly ICustomerRepository _customerRepository;

        public UpdateModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void OnGet(string cusId)
        {
            Customer cus = _customerRepository.GetCustomerById(cusId);
            if (cus != null)
            {
                Customer = cus; // Gán trực tiếp vào Customer để binding với form
            }
            ViewData["cus"] = cus; // Giữ lại nếu cần dùng ở nơi khác
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Lấy đối tượng cũ từ database
            var existingCustomer = _customerRepository.GetCustomerById(Customer.CustomerId);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            // Cập nhật các thuộc tính của đối tượng cũ
            existingCustomer.CustomerName = Customer.CustomerName;
            existingCustomer.Address = Customer.Address;
            existingCustomer.Phone = Customer.Phone;
            existingCustomer.DiscountRate = Customer.DiscountRate;

            // Cập nhật đối tượng được theo dõi bởi EF
            _customerRepository.Update(existingCustomer);

            return RedirectToPage("./Index");
        }
    }
}