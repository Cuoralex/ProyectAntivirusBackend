using Microsoft.EntityFrameworkCore;
using ProyectAntivirusBackend.Data;
using ProyectAntivirusBackend.Repositories;
using ProyectAntivirusBackend.Service;

var builder = WebApplication.CreateBuilder(args);

// Configurar PostgreSQL (corrige "UserBgsql" a "UseNpgsql")
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IInstitutionRepository, InstitutionRepository>();
builder.Services.AddScoped<IInstitutionService, InstitutionService>();

var app = builder.Build();

// Habilitar Swagger en desarrollo (corrige "UserSwagger" a "UseSwagger")
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Corrige "UserHitsRedirection"
app.MapControllers();
app.Run();
