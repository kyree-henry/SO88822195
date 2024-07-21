using FormHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SO88822195.Module.SchedulEmail.Data.Domain;
using SO88822195.Module.SchedulEmail.Data.Services;

namespace SO88822195.Module.SchedulEmail.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ReminderService _reminderService;
    public IndexModel(ILogger<IndexModel> logger, ReminderService reminderService)
    {
        _logger = logger;
        _reminderService = reminderService;
    }

    [BindProperty]
    public Reminder Input { get; set; }

    public List<Reminder> Data { get; set; } = [];

    public async Task OnGetAsync()
    {
        Data = await _reminderService.GetRemindersAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _reminderService.SetReminderAsync(Input);
        return FormResult.CreateSuccessResult("Reminder Added Successfully! <script>$('#addReminderModal').modal('hide');</script>");
    }
}