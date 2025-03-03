
using BUITIENQUAT__SE1814_NET_A02.Models;
using Buitienquat_SE1814_NET_A02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using System.Xml.Serialization;

namespace BUITIENQUAT__SE1814_NET_A02.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly Ass2SignalRRazorPagesContext _context;

        public IndexModel(Ass2SignalRRazorPagesContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            List<Customer> customers = _context.Customers.ToList();
            ViewData["Customers"] = customers;
        }

        [BindProperty]
        public int? DiscountRate { get; set; } // To bind the search input
        public string Message { get; set; }    // To display search results message
        public IActionResult OnPostSearch()
        {
            // Nếu không nhập giá trị
            if (!DiscountRate.HasValue)
            {
                var customers = _context.Customers.ToList();
                Message = "Please enter a discount rate to search";
                ViewData["Customers"] = customers;
                return Page();
            }

            // Kiểm tra giá trị có nằm trong khoảng [0-60] không
            if (DiscountRate.Value >= 0 && DiscountRate.Value <= 60)
            {
                // Lọc khách hàng có DiscountRate <= giá trị nhập vào
                var customers = _context.Customers
                    .Where(c => c.DiscountRate <= DiscountRate.Value)
                    .ToList();

                if (customers.Any())
                {
                    Message = $"Found {customers.Count} customers with discount rate ≤ {DiscountRate}";
                }
                else
                {
                    Message = $"No customers found with discount rate ≤ {DiscountRate}";
                }

                ViewData["Customers"] = customers;
            }
            else
            {
                // Nếu ngoài khoảng [0-60], hiển thị toàn bộ danh sách và thông báo lỗi
                var customers = _context.Customers.ToList();
                Message = "Discount rate must be between 0 and 60";
                ViewData["Customers"] = customers;
            }

            return Page();
        }
        public IActionResult OnPostDelete(string cusId)
        {
            var customerId = _context.Customers.FirstOrDefault(c => c.CustomerId.Equals(cusId));
            if (customerId == null)
            {
                return NotFound();
            }
            //delete
            _context.Customers.Remove(customerId);
            _context.SaveChanges();

            //notyf success
            TempData["SuccessMessage"] = $"Đã xóa thành công nhân viên với ID: {cusId}";

            return RedirectToPage();
        }

        public IActionResult OnPostUploadJson(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["ErrorMessage"] = "Please select a JSON file.";
                return Page();
            }

            try
            {
                using (var stream = new StreamReader(file.OpenReadStream()))
                {
                    string jsonContent = stream.ReadToEnd();
                    var customers = JsonSerializer.Deserialize<List<Customer>>(jsonContent);

                    if (customers != null && customers.Count > 0)
                    {
                        _context.Customers.AddRange(customers);
                        _context.SaveChanges();
                        TempData["SuccessMessage"] = "Customers added successfully from JSON file.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Invalid JSON file format.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error processing file: {ex.Message}";
            }

            return RedirectToPage();
        }

        public IActionResult OnPostUploadXml(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["ErrorMessage"] = "Please select an XML file.";
                return Page();
            }

            try
            {
                using (var stream = new StreamReader(file.OpenReadStream()))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>));
                    var customers = (List<Customer>)serializer.Deserialize(stream);

                    if (customers != null && customers.Count > 0)
                    {
                        _context.Customers.AddRange(customers);
                        _context.SaveChanges();
                        TempData["SuccessMessage"] = "Customers added successfully from XML file.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Invalid XML file format.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error processing file: {ex.Message}";
            }

            return RedirectToPage();
        }
    }

}
