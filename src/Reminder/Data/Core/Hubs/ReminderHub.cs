using Microsoft.AspNetCore.SignalR;
using SO88822195.Module.SchedulEmail.Data.Domain;

namespace SO88822195.Module.SchedulEmail.Data.Core.Hubs
{
    public class ReminderHub : Hub<IReminderHub>
    {
        public async Task SendReminder(string title)
        {
            await Clients.All.ReceiveReminder(title);
        }
        
        public async Task UpdateReminders(IEnumerable<Reminder> reminders)
        {
            await Clients.All.ReceiveUpdatedReminders(reminders);
        }
    }
}