using Microsoft.EntityFrameworkCore;
using SO88822195.Module.SchedulEmail.Data.Domain;

namespace SO88822195.Module.SchedulEmail.Data.Presistance
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<Reminder> Reminders { get; set; }
    }
}