using ClassHub.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

app.Run();
