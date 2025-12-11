using ClassHub.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;


var builder = WebApplication.CreateBuilder(args);


// JWT AUTHENTICATION SETUP
var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],

        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["Key"])
        ),

        ValidateLifetime = true
    };
});

builder.Services.AddScoped<ClassHub.Services.JwtService>();

// Add services to the container.
builder.Services.AddControllers();

// MySQL DATABASE CONNECTION
builder.Services.AddDbContext<ExternalDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("ExternalDb"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ExternalDb"))
    ));

builder.WebHost.UseUrls("http://0.0.0.0:8080");

var app = builder.Build();



app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();


app.Run();
