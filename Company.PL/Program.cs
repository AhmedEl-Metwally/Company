using Company.BLL.Mapping;
using Company.BLL.Services.Implementation;
using Company.BLL.Services.Interface;
using Company.DAL.Data.Contexts;
using Company.DAL.Models.IdentityModels;
using Company.DAL.Repositories.Implementation;
using Company.DAL.Repositories.Interface;
using Company.DAL.Repositories.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews
                 (options =>{ options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
    options.UseLazyLoadingProxies();    
});

builder.Services.AddScoped<IDepartmentRepositories,DepartmentRepositories>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeRepositories, EmployeeRepositories>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddIdentity<ApplicationUsers,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAutoMapper(Mapping => Mapping.AddProfile(new MappingProfile()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}")
    .WithStaticAssets();


app.Run();
