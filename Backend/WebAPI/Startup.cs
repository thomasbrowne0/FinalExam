using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using FinalExam.Backend.Application.Interfaces;
using FinalExam.Backend.Application.Repositories; 


public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddScoped<IEmployeeService, EmployeeService>();
        // Removed AddWebSockets as it's not needed in .NET 6
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddScoped<ICompanyRepository, CompanyRepository>();

    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseWebSockets();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.Map("/ws", async context =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                }
                else
                {
                    context.Response.StatusCode = 400;
                }
            });
        });
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinalExam API V1");
        });
    }
}