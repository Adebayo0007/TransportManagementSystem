using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TrainStationManagementApplication.Data;
using TrainStationManagementApplication.EmailServices.Implementations;
using TrainStationManagementApplication.EmailServices.Interfaces;
using TrainStationManagementApplication.Repository.Implementations;
using TrainStationManagementApplication.Repository.Interfaces;
using TrainStationManagementApplication.Service.Implementation;
using TrainStationManagementApplication.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("TrainConnectionString");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAddressService, AddressService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
builder.Services.AddScoped<IPassengerService, PassengerService>();

builder.Services.AddScoped<IRouteRepository, RouteRepository>();
builder.Services.AddScoped<IRouteServicer, RouteService>();

builder.Services.AddScoped<ITrainRepository, TrainRepository>();
builder.Services.AddScoped<ITrainService, TrainService>();

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<ITripService, TripService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IMailService, MailService>();



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(config =>
               {
                   config.LoginPath = "/users/loginUser";
                   config.Cookie.Name = "TrainStationManagementApplication";
                   config.LogoutPath = "/users/loginUser";
               });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
