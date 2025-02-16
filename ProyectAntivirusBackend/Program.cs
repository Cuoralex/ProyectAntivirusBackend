using Microsoft.EntityFrameworkCore;
using ProyectAntivirusBackend.Data;
<<<<<<< HEAD
using ProyectAntivirusBackend.Repositories;
using ProyectAntivirusBackend.Services;

var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
=======
// Configurar PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Registrar Repositorios y Servicios
builder.Services.AddScoped<IOpportunityTypeRepository, OpportunityTypeRepository>();
builder.Services.AddScoped<OpportunityTypeService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();


// Configurar PostgreSQL (corrige "UserBgsql" a "UseNpgsql")
=======
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
>>>>>>> origin/DevDavalejo
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
<<<<<<< HEAD

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
>>>>>>> origin/DevCuoralex
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
=======
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Habilitar Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Proyecto Antivirus"));
>>>>>>> origin/DevDavalejo
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

<<<<<<< HEAD
app.MapStaticAssets();
app.MapControllers();
<<<<<<< HEAD

app.Run();
=======
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Proyecto Antivirus V1.0"));


app.UseHttpsRedirection(); // Corrige "UserHitsRedirection"
app.MapControllers();
app.Run();
>>>>>>> origin/DevCuoralex
=======
app.MapControllers();

app.Run();

internal class SupabaseConfiguration
{
    public required string Url { get; set; }
    public required string Key { get; set; }
}
>>>>>>> origin/DevDavalejo
