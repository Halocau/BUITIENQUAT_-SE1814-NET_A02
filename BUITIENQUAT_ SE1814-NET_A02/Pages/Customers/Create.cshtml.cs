
using BUITIENQUAT__SE1814_NET_A02.Hubs;
using BUITIENQUAT__SE1814_NET_A02.Models;
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
        private readonly Ass2SignalRRazorPagesContext _context;
        private readonly IHubContext<SignalRServer> _hubContext;

        public CreateModel(Ass2SignalRRazorPagesContext context, IHubContext<SignalRServer> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }



        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var newCustomer = Customer;
            _context.Customers.Add(Customer);
            await _context.SaveChangesAsync();
            // Gửi thông báo qua SignalR
            await _hubContext.Clients.All.SendAsync("LoadCustomer");
            //return RedirectToAction(nameof(Index));
            return RedirectToPage("./index");
        }

    }
}
