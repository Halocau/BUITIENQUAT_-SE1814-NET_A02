using Microsoft.AspNetCore.SignalR;

namespace BUITIENQUAT__SE1814_NET_A02.Hubs
{
    public class SignalRServer : Hub
    {
        public async Task SendCustomerUpdate()
        {
            await Clients.All.SendAsync("LoadCustomer");
        }
    }
}
