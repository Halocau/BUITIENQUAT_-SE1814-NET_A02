
using BUITIENQUAT__SE1814_NET_A02.Hubs;
using BUITIENQUAT__SE1814_NET_A02.Models;
using BUITIENQUAT__SE1814_NET_A02.Repositories;
using Buitienquat_SE1814_NET_A02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

//@microsoft / signalr@latest
namespace BUITIENQUAT__SE1814_NET_A02.Pages.Customers
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Customer Customer { get; set; }
        private readonly ICustomerRepository _customerRepository;
        private readonly IHubContext<SignalRServer> _hubContext;

        public CreateModel(ICustomerRepository customerRepository, IHubContext<SignalRServer> hubContext)
        {
            _customerRepository = customerRepository;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _customerRepository.Add(Customer);

            // Gửi thông báo qua SignalR
            await _hubContext.Clients.All.SendAsync("LoadCustomer");
            //return RedirectToAction(nameof(Index));
            return RedirectToPage("./index");
        }

    }
}
