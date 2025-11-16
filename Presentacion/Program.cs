using Aplicacion.Contratos.Persistencia;
using FluentValidation;
using Infraestructura.Persistencia;
using Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ======= CONFIGURACIÓN DE ARQUITECTURA LIMPIA =======

// 1. Configuración de Entity Framework Core con SQL Server
builder.Services.AddDbContext<FoodTruckDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Infraestructura")
    ));

// 2. Registro de Repositorios (Capa de Infraestructura)
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// 3. Registro de MediatR (Capa de Aplicación - CQRS)
// MediatR buscará todos los handlers en el ensamblado de Aplicacion
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(
    typeof(Aplicacion.DTOs.ProductoDto).Assembly));

// 4. Registro de FluentValidation (Capa de Aplicación - Validaciones)
builder.Services.AddValidatorsFromAssembly(
    typeof(Aplicacion.DTOs.ProductoDto).Assembly);

// ======= FIN DE CONFIGURACIÓN DE ARQUITECTURA LIMPIA =======

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
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
