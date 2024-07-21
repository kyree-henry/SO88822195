using System.ComponentModel.DataAnnotations;

namespace SO88822195.Module.SchedulEmail.Data.Domain
{
    public class Reminder
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; } = default!;

        [Required]
        public DateTime? DateTime { get; set; }
    }
}