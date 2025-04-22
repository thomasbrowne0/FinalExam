using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddScoped<IEmployeeService, EmployeeService>();
        // Removed AddWebSockets as it's not needed in .NET 6
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
                    // Handle WebSocket connection here
                }
                else
                {
                    context.Response.StatusCode = 400;
                }
            });
        });
    }
}