using Microsoft.AspNetCore.Mvc.RazorPages;
using SO88822195.Module.Hierarchy.Data.Domain;
using SO88822195.Module.Hierarchy.Data.Services;

namespace SO88822195.Module.Hierarchy.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DepartmentService _departmentService;
        public IndexModel(ILogger<IndexModel> logger, DepartmentService departmentService)
        {
            _logger = logger;
            _departmentService = departmentService;
        }

        public List<Department> SubDepartments { get; set; } = [];
        public List<Department> ParentDepartments { get; set; } = [];
        public List<Department> Departments { get; set; } = [];
        public Department? SelectedDepartment { get; set; }
        public string? Id { get; set; }
        public bool InvalidId { get; set; }
        public async Task OnGetAsync(string? id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Guid selectedDepartmentId;
                bool isValidId = Guid.TryParse(id, out selectedDepartmentId);
                if (isValidId)
                {
                    Department? selectedDepartment = await _departmentService.GetDepartmentById(selectedDepartmentId);
                    if (selectedDepartment != default)
                    {
                        SelectedDepartment = selectedDepartment;
                        SubDepartments = await _departmentService.GetSubDepartments(selectedDepartmentId);
                        ParentDepartments = await _departmentService.GetParentDepartments(selectedDepartmentId);
                    }
                    else
                    {
                        InvalidId = true;
                    }
                    
                }
                else
                {
                    InvalidId = true;
                }
            }

            Departments = await _departmentService.GetSubDepartments(null);
        }
    }
}