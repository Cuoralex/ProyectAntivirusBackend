using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProyectAntivirusBackend.Data;
using ProyectAntivirusBackend.Repositories;
using ProyectAntivirusBackend.Services;
using AutoMapper;
using ProyectAntivirusBackend.Service;

var builder = WebApplication.CreateBuilder(args);

// Configurar PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Registrar Repositorios y Servicios
builder.Services.AddScoped<IOpportunityTypeRepository, OpportunityTypeRepository>();
builder.Services.AddScoped<OpportunityTypeService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<IOpportunityTypeRepository, OpportunityTypeRepository>();
builder.Services.AddScoped<OpportunityTypeService>();



// Configurar PostgreSQL (corrige "UserBgsql" a "UseNpgsql")
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProyectAntivirusBackend API", Version = "v1" });
    c.EnableAnnotations();
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Proyecto Antivirus V1.0"));


app.UseHttpsRedirection(); // Corrige "UserHitsRedirection"
app.MapControllers();
app.Run();
