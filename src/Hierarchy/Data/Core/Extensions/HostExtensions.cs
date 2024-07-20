using Microsoft.EntityFrameworkCore;
using SO88822195.Module.Hierarchy.Data.Domain;
using SO88822195.Module.Hierarchy.Data.Presistance;

namespace SO88822195.Module.Hierarchy.Data.Core.Extensions
{
    public static class HostExtensions
    {
        public static IHost SeedAsync(this IHost host)
        {
            SeedDatabaseAsync(host);
            return host;
        }

        private static async void SeedDatabaseAsync(IHost host)
        {
            using IServiceScope scope = host.Services.CreateScope();
            DataContext dataContext = scope.ServiceProvider.GetService<DataContext>()!;
            if (dataContext != null)
            {
                dataContext.Database.Migrate();
                await SeedData(dataContext);
            }
        }

        private async static Task SeedData(DataContext dataContext)
        {
            if (!dataContext.Departments.Any())
            {
                List<Department> departments =
                [
                    new Department { Id = Guid.NewGuid(), Name = "Corporate Office", Logo = "https://via.placeholder.com/30" },
                    new Department { Id = Guid.NewGuid(), Name = "Finance", Logo = "https://via.placeholder.com/30" },
                    new Department { Id = Guid.NewGuid(), Name = "Human Resources", Logo = "https://via.placeholder.com/30" },
                    new Department { Id = Guid.NewGuid(), Name = "IT Department", Logo = "https://via.placeholder.com/30" },
                    new Department { Id = Guid.NewGuid(), Name = "Marketing", Logo = "https://via.placeholder.com/30" },
                    new Department { Id = Guid.NewGuid(), Name = "Sales", Logo = "https://via.placeholder.com/30" },
                    new Department { Id = Guid.NewGuid(), Name = "Operations", Logo = "https://via.placeholder.com/30" },
                    new Department { Id = Guid.NewGuid(), Name = "Customer Support", Logo = "https://via.placeholder.com/30" },
                    new Department { Id = Guid.NewGuid(), Name = "Research and Development", Logo = "https://via.placeholder.com/30" },
                    new Department { Id = Guid.NewGuid(), Name = "Legal", Logo = "https://via.placeholder.com/30" },
                    new Department { Id = Guid.NewGuid(), Name = "Procurement", Logo = "https://via.placeholder.com/30" },
                    new Department { Id = Guid.NewGuid(), Name = "Logistics", Logo = "https://via.placeholder.com/30" }
                ];

                dataContext.Departments.AddRange(departments);
                foreach (Department department in departments)
                {
                    AddSubDepartments(dataContext, department, 3, 5); // 3 levels deep with 5 sub-departments at each level
                }


                await dataContext.SaveChangesAsync();
            }
        }

        private static void AddSubDepartments(DataContext dataContext, Department parent, int levels, int subDepartmentsPerLevel)
        {
            if (levels <= 0)
                return;

            Random rnd = new ();

            for (int i = 1; i <= subDepartmentsPerLevel; i++)
            {
                Department subDepartment = new()
                {
                    Id = Guid.NewGuid(),
                    Name = $"{parent.Name} Sub-{i}",
                    ParentDepartment = parent,
                    Logo = "https://via.placeholder.com/30"
                };

                dataContext.Departments.Add(subDepartment);

                // Recursively add sub-departments
                AddSubDepartments(dataContext, subDepartment, levels - 1, subDepartmentsPerLevel);
            }
        }

    }
}