using BUITIENQUAT__SE1814_NET_A02;
using BUITIENQUAT__SE1814_NET_A02.Hubs;
using BUITIENQUAT__SE1814_NET_A02.Models;
using BUITIENQUAT__SE1814_NET_A02.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
// đăng kí database ảo cho ứng dụng
builder.Services.AddDbContext<Ass2SignalRRazorPagesContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapHub<SignalRServer>("/signalRServer");
app.Run();
