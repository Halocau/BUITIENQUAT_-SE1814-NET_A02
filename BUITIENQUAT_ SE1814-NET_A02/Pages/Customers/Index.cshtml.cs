﻿
using BUITIENQUAT__SE1814_NET_A02.Hubs;
using BUITIENQUAT__SE1814_NET_A02.Models;
using BUITIENQUAT__SE1814_NET_A02.Repositories;
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
        [BindProperty]
        public int? DiscountRate { get; set; }
        public string Message { get; set; }
        //private readonly Ass2SignalRRazorPagesContext _context;
        private readonly ICustomerRepository _customerRepository;
        private readonly IHubContext<SignalRServer> _hubContext;

        public IndexModel(ICustomerRepository customerRepository, IHubContext<SignalRServer> hubContext)
        {
            _customerRepository = customerRepository;
            _hubContext = hubContext;
        }

        public void OnGet()
        {
            ViewData["Customers"] = _customerRepository.GetAllCustomers();
        }


        public IActionResult OnPostSearch()
        {
            // Nếu không nhập giá trị
            if (!DiscountRate.HasValue)
            {
                Message = "Please enter a discount rate to search";
                ViewData["Customers"] = _customerRepository.GetAllCustomers();
                return Page();
            }

            // Kiểm tra giá trị có nằm trong khoảng [0-60] không
            if (DiscountRate.Value >= 0 && DiscountRate.Value <= 60)
            {
                // Lọc khách hàng có DiscountRate <= giá trị nhập vào
                var customers = _customerRepository.SearchCustomersByDiscountRate(DiscountRate.Value);

                Message = customers.Any() ?
                      $"Found {customers.Count} customers with discount rate ≤ {DiscountRate}" :
                      $"No customers found with discount rate ≤ {DiscountRate}";

                ViewData["Customers"] = customers;
            }
            else
            {
                // Nếu ngoài khoảng [0-60], hiển thị toàn bộ danh sách và thông báo lỗi
                Message = "Discount rate must be between 0 and 60";
                ViewData["Customers"] = _customerRepository.GetAllCustomers();
            }

            return Page();
        }
        public IActionResult OnPostDelete(string cusId)
        {
            var customerId = _customerRepository.GetCustomerById(cusId);
            if (customerId == null)
            {
                return NotFound();
            }
            //delete
            _customerRepository.DeleteCustomer(customerId);

            //notyf success
            TempData["SuccessMessage"] = $"Successfully deleted customer with ID: {cusId}";
            _hubContext.Clients.All.SendAsync("LoadCustomer"); // Gửi thông báo
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
                    string jsonContent = stream.ReadToEnd();// read json 
                    var customers = JsonSerializer.Deserialize<List<Customer>>(jsonContent);// change

                    if (customers != null && customers.Count > 0)
                    {
                        _customerRepository.AddCustomers(customers);
                        TempData["SuccessMessage"] = "Customers added successfully from JSON file.";
                        _hubContext.Clients.All.SendAsync("LoadCustomer"); // Gửi thông báo
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
                        _customerRepository.AddCustomers(customers);
                        TempData["SuccessMessage"] = "Customers added successfully from XML file.";
                        _hubContext.Clients.All.SendAsync("LoadCustomer"); // Gửi thông báo
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
