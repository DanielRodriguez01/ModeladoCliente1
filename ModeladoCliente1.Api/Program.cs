using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ModeladoCliente1.Api.Data;
using ModeladoCliente1.Api.Entidades;
using ModeladoCliente1.Api.Interfaces;
using ModeladoCliente1.Api.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connection));


// Aqui agrego Identity...

builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


// yo aqui agrego  Roles...
builder.Services.AddScoped<RoleManager<IdentityRole>>();


// aqui la configuracion, token JWT...
var claveSecreta = "MI_CLAVE_SUPER_ULTRA_SEGURA_PARA_JWT_2026_123456";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(claveSecreta))
    };
});



builder.Services.AddScoped<IRepositorioCliente, RepositorioCliente>();

builder.Services.AddScoped<IRepositorioRegistroCliente, RepositorioRegistroCliente>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient",
        policy => policy
            .WithOrigins("https://localhost:7187")
            .AllowAnyHeader()
            .AllowAnyMethod());
});



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddOutputCache();


var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = { "Admin", "Usuario" };

    foreach (var rol in roles)
    {
        if (!await roleManager.RoleExistsAsync(rol))
        {
            await roleManager.CreateAsync(new IdentityRole(rol));
        }
    }
}



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}



app.UseHttpsRedirection();



app.UseCors("AllowBlazorClient");

app.UseOutputCache();



app.UseAuthentication();

app.UseAuthorization();



app.MapControllers();

app.Run();