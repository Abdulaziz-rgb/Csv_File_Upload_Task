using Microsoft.EntityFrameworkCore;
using Task_Project.DataAccess;
using Task_Project.Handlers;
using Task_Project.Interfaces;
using Task_Project.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddSingleton<IFileHandler, FileHandler>();
builder.Services.AddDbContextPool<AppDbContext>(options =>
{
    // change Connection String from appsettings.json file
    // I used connection string for my local sql server here!
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDb"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
}
);

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
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
