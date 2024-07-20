using Microsoft.EntityFrameworkCore;
using SO88822195.Module.Hierarchy.Data.Domain;
using SO88822195.Module.Hierarchy.Data.Presistance;

namespace SO88822195.Module.Hierarchy.Data.Services
{
    public class DepartmentService
    {
        private readonly DataContext _context;
        private readonly ILogger<DepartmentService> _logger;
        public DepartmentService(DataContext context, ILogger<DepartmentService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Department> GetDepartmentById(Guid id)
        {
            return await _context.Departments.FindAsync(id) ?? default!;
        }

        public async Task<List<Department>> GetSubDepartments(Guid? departmentId)
        {
            return await _context.Departments
                .Where(d => d.ParentDepartmentId == departmentId)
                .ToListAsync();
        }

        public async Task<List<Department>> GetParentDepartments(Guid departmentId)
        {
            List<Department> parents = [];
            Department? department = await _context.Departments.FindAsync(departmentId);

            while (department != null && department.ParentDepartmentId.HasValue)
            {
                department = await _context.Departments
                    .Include(d => d.ParentDepartment)
                    .FirstOrDefaultAsync(d => d.Id == department.ParentDepartmentId);

                if (department != null)
                {
                    parents.Add(department);
                }
            }

            return parents;
        }
    }
}
