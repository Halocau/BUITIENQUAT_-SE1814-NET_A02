using BUITIENQUAT__SE1814_NET_A02.Models;
using BUITIENQUAT__SE1814_NET_A02.Repositories;
using Buitienquat_SE1814_NET_A02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BUITIENQUAT__SE1814_NET_A02.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public DetailsModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer Customer { get; set; } // Thuộc tính Customer phải có ở đây

        public IActionResult OnGet(string cusId)
        {
            Customer = _customerRepository.GetCustomerById(cusId);
            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}