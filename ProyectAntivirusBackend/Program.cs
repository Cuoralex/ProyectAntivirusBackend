using Microsoft.EntityFrameworkCore;
using ProyectAntivirusBackend.Data;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

// Configuración de Supabase
var supabaseConfiguration = builder.Configuration
    .GetSection("Supabase")
    .Get<SupabaseConfiguration>();

builder.Services.AddScoped<Client>(provider =>
{
    if (supabaseConfiguration == null)
    {
        throw new InvalidOperationException("Supabase configuration is missing.");
    }
    return new Client(supabaseConfiguration.Url, supabaseConfiguration.Key);
});


// Configuración de PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Habilitar Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Proyecto Antivirus"));
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

internal class SupabaseConfiguration
{
    public required string Url { get; set; }
    public required string Key { get; set; }
}
