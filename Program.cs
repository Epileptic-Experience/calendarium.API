using calendarium.API.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// tomo los datos de la coneccion desde launchSettings.json, que a su vez los toma de las variables de entorno definidas en launchSettings.json
var host = Environment.GetEnvironmentVariable("DB_HOST");
var port = Environment.GetEnvironmentVariable("DB_PORT");
var db = Environment.GetEnvironmentVariable("DB_NAME");
var user = Environment.GetEnvironmentVariable("DB_USER");
var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();



app.MapUserEndpoints();
app.MapLocationEndpoints();
app.MapAppointmentEndpoints();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        // Esto aplicará las migraciones pendientes automáticamente al iniciar el contenedor
        context.Database.Migrate();
        Console.WriteLine("DEBUG: Migraciones aplicadas con éxito.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"DEBUG: Error al aplicar migraciones: {ex.Message}");
    }
}



app.Run();
