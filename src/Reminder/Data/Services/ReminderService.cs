using Hangfire;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SO88822195.Module.SchedulEmail.Data.Core.Hubs;
using SO88822195.Module.SchedulEmail.Data.Domain;
using SO88822195.Module.SchedulEmail.Data.Presistance;

namespace SO88822195.Module.SchedulEmail.Data.Services
{
    public class ReminderService
    {
        private readonly DataContext _context;
        private readonly IHubContext<ReminderHub, IReminderHub> _hubContext;
        public ReminderService(DataContext context, IHubContext<ReminderHub, IReminderHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<List<Reminder>> GetRemindersAsync()
        {
            return await _context.Reminders.OrderByDescending(a => a.DateTime).ToListAsync();
        }

        public async Task SetReminderAsync(Reminder reminder)
        {
            reminder.Id = Guid.NewGuid();
            _context.Reminders.Add(reminder);
            await _context.SaveChangesAsync();

            TimeSpan? delay = reminder.DateTime - DateTime.Now;
            if (delay <= TimeSpan.Zero)
            {
                await SendReminderNotification(reminder.Title);
            }
            else
            {
                BackgroundJob.Schedule(() => SendReminderNotification(reminder.Title), delay!.Value);
                await NotifyClientsOfUpdatedReminders();
            }

        }

        public async Task SendReminderNotification(string title)
        {
            await _hubContext.Clients.All.ReceiveReminder(title);
            await NotifyClientsOfUpdatedReminders();
        }

        private async Task NotifyClientsOfUpdatedReminders()
        {
            IEnumerable<Reminder> reminders = await GetRemindersAsync();
            await _hubContext.Clients.All.ReceiveUpdatedReminders(reminders);
        }
    }
}