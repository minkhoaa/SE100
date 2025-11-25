using ClinicManagement_API.Features.booking_service.service;
using ClinicManagement_API.Infrastructure.Persisstence;
using ClinicManagement_API.Features.booking_service.endpoint;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



var connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRING__CLINIC")
                    ?? builder.Configuration.GetConnectionString("Clinic_DB")
                    ?? throw new Exception("connectionString is missing");

builder.Services.AddDbContext<ClinicDbContext>(option => option.UseNpgsql(connectionString));


// DI
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IClinicService, ClinicService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IServiceService, ServiceService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
app.MapBookingClinicEndpoint();
app.MapBookingServiceEndpoint();
app.MapBookingDoctorEndpoint();
app.MapBookingSlotEndpoint();
app.MapBookingEndpoint();
app.MapClinicEndpoint();
app.MapDoctorEndpoint();
app.MapServiceEndpoint();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ClinicDbContext>();
    await db.Database.MigrateAsync();
}

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
