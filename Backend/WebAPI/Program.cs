using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Remove any code that overrides the environment
// Add code to print current environment
Console.WriteLine($"Current environment: {builder.Environment.EnvironmentName}");

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add infrastructure services
var isDevelopment = builder.Environment.IsDevelopment();
Console.WriteLine($"Environment: {(isDevelopment ? "Development" : "Production")}");

// Configure services with the current environment
builder.Services.AddInfrastructureServices(
    builder.Configuration, 
    isDevelopment);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Apply migrations based on current environment
try
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<Infrastructure.Persistence.ApplicationDbContext>();
        Console.WriteLine($"Using database: {context.Database.GetConnectionString()}");
        
        context.Database.MigrateAsync().Wait();
        Console.WriteLine("Migration completed successfully");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error during migration: {ex.Message}");
    Console.WriteLine($"Stack trace: {ex.StackTrace}");
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
