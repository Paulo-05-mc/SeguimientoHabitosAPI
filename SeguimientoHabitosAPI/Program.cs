using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SeguimientoHabitosAPI.Data;
using SeguimientoHabitosAPI.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddControllers();
// Registra primero la configuración
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Luego registra el TokenService
builder.Services.AddScoped<ITokenService, TokenService>();
// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "Seguimiento de Hábitos API", 
        Version = "v1",
        Description = "API para el sistema de seguimiento de hábitos personales",
        Contact = new OpenApiContact {
            Name = "Tu Nombre",
            Email = "tu@email.com"
        }
    });

    // Configuración para usar JWT en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando el esquema Bearer. Ejemplo: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configuración de la base de datos (usando tu connection string)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuración de servicios personalizados
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
// Agrega aquí otros servicios que necesites

// Configuración de autenticación JWT (simplificada para tu configuración actual)
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey) || jwtKey.Length < 32)
{
    throw new Exception("La clave JWT no es válida. Debe tener al menos 32 caracteres.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = false, // Puedes cambiar a true si agregas Issuer en appsettings
            ValidateAudience = false, // Puedes cambiar a true si agregas Audience en appsettings
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

// Configuración de autorización por roles
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("CoachOrAdmin", policy => policy.RequireRole("Coach", "Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

var app = builder.Build();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Seguimiento Hábitos API v1");
        c.RoutePrefix = "swagger"; // Accede en /swagger
    });
}

app.UseHttpsRedirection();

// Middlewares de autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Aplicar migraciones automáticamente (solo para desarrollo)
try
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}
catch (Exception ex)
{
    app.Logger.LogError(ex, "Error al aplicar migraciones de base de datos");
}

app.Run();