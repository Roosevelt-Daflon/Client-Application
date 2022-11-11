
using Client_Application.Data;
using Client_Application.Repository;
using Client_Application.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();
builder.Services.AddScoped<IRepository, EntityFrameWorkRepository>();
builder.Services.AddSingleton<ICepService, ViaCepService>();
builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Clientes}/{action=Index}/{id?}");

app.Run();
