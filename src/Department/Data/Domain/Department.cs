using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SO88822195.Module.Hierarchy.Data.Domain
{
    public class Department
    {
        public Department()
        {
            SubDepartments = [];
        }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Logo { get; set; } = default!;

        public Guid? ParentDepartmentId { get; set; }
        public Department? ParentDepartment { get; set; }

        public ICollection<Department>? SubDepartments { get; set; }
    }
}