using System.ComponentModel.Design;
using NEGOSUDAPI.Services.ProductsServices;
using Microsoft.EntityFrameworkCore;
using NEGOSUDAPI.Data;
using NEGOSUDAPI.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using NEGOSUDAPI.Services.CustomersServices;



var builder = WebApplication.CreateBuilder(args);

// Configuration du service DbContext pour utiliser SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


/*TODO: Configurer les endpoints de l'API*/

// Enregistrement des services
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICustomersService, CustomersService>();

// Ajout de Swagger pour la documentation de l'API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  // Cette ligne est nécessaire pour configurer Swagger
    
// Ajout des services pour les contrôleurs avec des vues Razor
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuration du pipeline de gestion des requêtes HTTP
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

/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products/{action=Create}/{id?}");
*/

app.Run();
