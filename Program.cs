
using DSW_3P_PR02.Repositories;
using DSW_3P_PR02.Services;
using Microsoft.EntityFrameworkCore;
using DSW_3P_PR02.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar OpenAPI.
builder.Services.AddOpenApi();

// Habilitar los controladores.
builder.Services.AddControllers();

// Configurar la inyección de dependencias.
builder.Services.AddScoped<IEventoRepository, EventoSQLRepository>();

builder.Services.AddScoped<IEventoService, EventoService>();

// Configurar Entity Framework Core.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServerConnection")
    )
);

var app = builder.Build();

// Configurar el entorno de desarrollo.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/openapi/v1.json",
            "Gestor de Eventos API v1"
        );
    });
}

// Configurar las rutas de la aplicación.
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
