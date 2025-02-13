<<<<<<< HEAD


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
=======
using Microsoft.EntityFrameworkCore;
using ProyectAntivirusBackend.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar PostgreSQL (corrige "UserBgsql" a "UseNpgsql")
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
>>>>>>> 81f3d0b4ef2085c7c882d6b10a80097d6c8fd2c2

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilitar Swagger en desarrollo (corrige "UserSwagger" a "UseSwagger")
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

<<<<<<< HEAD
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Proyecto Antivirus"));


app.Run();
=======
app.UseHttpsRedirection(); // Corrige "UserHitsRedirection"
app.MapControllers();
app.Run();
>>>>>>> 81f3d0b4ef2085c7c882d6b10a80097d6c8fd2c2
