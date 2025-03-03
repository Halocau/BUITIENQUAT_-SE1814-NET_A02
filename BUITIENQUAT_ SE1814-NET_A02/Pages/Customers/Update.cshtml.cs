using BUITIENQUAT__SE1814_NET_A02.Models;
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
        private readonly Ass2SignalRRazorPagesContext _context;

        public UpdateModel(Ass2SignalRRazorPagesContext context)
        {
            _context = context;
        }

        public void OnGet(string cusId)
        {
            Customer cus = _context.Customers.FirstOrDefault(c => c.CustomerId.Equals(cusId));
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
            var oldCustomer = _context.Customers.FirstOrDefault(c => c.CustomerId.Equals(Customer.CustomerId));
            if (oldCustomer == null)
            {
                return NotFound(); // Trả về lỗi nếu không tìm thấy
            }

            // Cập nhật các thuộc tính của đối tượng cũ
            oldCustomer.CustomerName = Customer.CustomerName;
            oldCustomer.Address = Customer.Address;
            oldCustomer.Phone = Customer.Phone;
            oldCustomer.DiscountRate = Customer.DiscountRate;

            // Cập nhật đối tượng được theo dõi bởi EF
            _context.Customers.Update(oldCustomer);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}