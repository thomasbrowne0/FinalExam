using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class WebSocketHub : Hub
{
    

    public async Task SendScheduleUpdate()
    {
        
        await Clients.All.SendAsync("ReceiveScheduleUpdate", "Schedule updated");
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.SendAsync("Connected", "Connected to hub");
        await base.OnConnectedAsync();
    }
}