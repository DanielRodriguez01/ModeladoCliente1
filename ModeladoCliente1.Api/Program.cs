using Microsoft.EntityFrameworkCore;
using ModeladoCliente1.Api.Data;
using ModeladoCliente1.Api.Interfaces;
using ModeladoCliente1.Api.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Configuracion DbContext con SQL Server...

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connection));



// Inyección de dependencias: repositorios
builder.Services.AddScoped<IRepositorioCliente, RepositorioCliente>();
builder.Services.AddScoped<IRepositorioRegistroCliente, RepositorioRegistroCliente>();



// CORS: permite las llamadas desde mi proyecto Blazor.Client
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient",
        policy => policy
            .WithOrigins("https://localhost:7187", "http://localhost:5168") // <- puertos del Client en tu launchSettings
            .AllowAnyHeader()
            .AllowAnyMethod());
});



// Aqui habilito los CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



// Puerta a las puebas con swagger...
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseCors("AllowBlazorClient");

app.UseAuthorization();

app.MapControllers();

app.Run();

