using System.ComponentModel.Design;
using NEGOSUDAPI.Services.ProductsServices;
using Microsoft.EntityFrameworkCore;
using NEGOSUDAPI.Data;
using NEGOSUDAPI.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using NEGOSUDAPI.Services.CustomersServices;
using NEGOSUDAPI.Services.CategoriesServices;
using NEGOSUDAPI.Services.SuppliersServices;
using NEGOSUDAPI.Services.OrdersServices;
using NEGOSUDAPI.Services.OrderDetailsServices;



var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


// services
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICustomersService, CustomersService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<ISuppliersService, SuppliersService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  
    
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NEGOSUD");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
