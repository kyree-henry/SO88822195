using SO88822195.Module.SchedulEmail.Data.Domain;

namespace SO88822195.Module.SchedulEmail.Data.Core.Hubs
{
    public interface IReminderHub
    {
        Task ReceiveReminder(string title);
        Task ReceiveUpdatedReminders(IEnumerable<Reminder> reminders);
    }
}
