
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Proyecto2;
using Proyecto2.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var jwt = builder.Configuration.GetSection("Jwt");

var secret = jwt["Key"]
    ?? throw new InvalidOperationException(
        "Falta configurar Jwt:Key");

var issuer = jwt["Issuer"]
    ?? throw new InvalidOperationException(
        "Falta configurar Jwt:Issuer");

var audience = jwt["Audience"]
    ?? throw new InvalidOperationException(
        "Falta configurar Jwt:Audience");

if (System.Text.Encoding.UTF8.GetByteCount(secret) < 32)
    throw new InvalidOperationException(
        "La clave JWT debe tener al menos 32 bytes.");

builder.Services
    .AddAuthentication(
        JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = issuer,
                ValidAudience = audience,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(secret)),

                ClockSkew = TimeSpan.Zero
            };
    });

builder.Services.AddAuthorization();


builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<BdConnection>();
builder.Services.AddScoped<UsuarioData>();
builder.Services.AddScoped<CatalogoData>();
var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();