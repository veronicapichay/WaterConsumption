using ConsumptionTracker.Data;
using Microsoft.EntityFrameworkCore;
using ConsumptionTracker.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//connection string config to postgre db
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

//register services
builder.Services.AddScoped<IDataContext>(provider => provider.GetService<DataContext>());

//register repository
builder.Services.AddScoped<IWaterConsumptionRepository, WaterConsumptionRepository>();

builder.Services.AddControllersWithViews();

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