using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSignalR();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.SetIsOriginAllowed(_ => true)
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            });
        });
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseCors("AllowAll");
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<WebSocketHub>("/schedulehub");
            endpoints.MapControllers();
        });
    }
}