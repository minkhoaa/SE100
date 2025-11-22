using ClinicManagement_API.Infrastructure.Persisstence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



var connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRING__CLINIC")
                    ?? builder.Configuration.GetConnectionString("Clinic_DB")
                    ?? throw new Exception("connectionString is missing");

builder.Services.AddDbContext<ClinicDbContext>(option => option.UseNpgsql(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ClinicDbContext>();
    await db.Database.MigrateAsync();
}

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

