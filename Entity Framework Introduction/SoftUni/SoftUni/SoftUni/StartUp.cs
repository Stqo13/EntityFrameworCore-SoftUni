using SoftUni.Data;
using SoftUni.Models;
using System.Globalization;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using SoftUniContext context = new SoftUniContext();
            Console.WriteLine(RemoveTown(context));
        }

        //03
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employees = context.Employees
                .OrderBy(e => e.EmployeeId)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary
                })
                .ToList();

            var sb = new StringBuilder();

            foreach (var item in employees)
            {
                sb.AppendLine($"{item.FirstName} {item.LastName} {item.MiddleName} {item.JobTitle} {item.Salary:f2}");
            }

            return sb.ToString();
        }

        //04
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employees = context.Employees
                .OrderBy(e => e.FirstName)
                .Where(e => e.Salary > 50000)
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary

                }).ToList();

            var sb = new StringBuilder();

            foreach (var item in employees)
            {
                sb.AppendLine($"{item.FirstName} - {item.Salary:f2}");
            }

            return sb.ToString();
        }

        //05
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var employees = context.Employees
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .Where(d => d.Department.Name == "Research and Development")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    DepartmentName = e.Department.Name,
                    e.Salary

                }).ToList();

            var sb = new StringBuilder();

            foreach (var item in employees)
            {
                sb.AppendLine($"{item.FirstName} {item.LastName} from {item.DepartmentName} - ${item.Salary:f2}");
            }

            return sb.ToString();
        }

        //06
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            Address address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            var emp = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");


            if (emp != null)
            {
                emp.Address = address;

                context.SaveChanges();
            }

            var employees = context.Employees
                .OrderByDescending(e => e.AddressId)
                .Select(e => new
                {
                    AddressText = e.Address.AddressText
                })
                .Take(10).ToList();

            var sb = new StringBuilder();

            foreach (var item in employees)
            {
                sb.AppendLine(item.AddressText);
            }

            return sb.ToString();
        }

        //07
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.EmployeesProjects,
                    ManagerFirstName = e.Manager.FirstName,
                    ManagerLastName = e.Manager.LastName

                }).Take(10).ToList();

            var sb = new StringBuilder();

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.FirstName} {emp.LastName} - Manager: {emp.ManagerFirstName} {emp.ManagerLastName}");

                foreach (var proj in emp.EmployeesProjects)
                {
                    Project project = context.Projects.FirstOrDefault(p => p.ProjectId == proj.ProjectId);
                    if (project != null)
                    {
                        char[] delimiters = { '/', ' ' };
                        string[] date = project.StartDate.ToString().Split(delimiters);
                        int year = int.Parse(date[2]);
                        if (year >= 2001 && year <= 2003)
                        {
                            if (project.EndDate == null)
                            {
                                sb.AppendLine($"--{project.Name} - {project.StartDate} - not finished");
                            }
                            else
                            {
                                sb.AppendLine($"--{project.Name} - {project.StartDate} - {project.EndDate}");
                            }
                        }
                    }
                }
            }

            return sb.ToString();
        }

        //08
        public static string GetAddressesByTown(SoftUniContext context)
        {
            var addresses = context.Addresses
                .Select(a => new
                {
                    a.AddressText,
                    TownName = a.Town.Name,
                    EmployeeCount = a.Employees.Count()
                })
                .OrderByDescending(a => a.EmployeeCount)
                .ThenBy(a => a.TownName)
                .ThenBy(a => a.AddressText)
                .Take(10).ToList();

            var sb = new StringBuilder();

            foreach (var item in addresses)
            {
                sb.AppendLine($"{item.AddressText}, {item.TownName} - {item.EmployeeCount} employees");
            }

            return sb.ToString();
        }

        //09
        public static string GetEmployee147(SoftUniContext context)
        {
            var employee = context.Employees
                .Where(e => e.EmployeeId == 147)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    Projects = e.EmployeesProjects.Select(p => new
                    {
                        ProjectName = p.Project.Name
                    })

                }).ToList();

            var sb = new StringBuilder();

            foreach (var item in employee)
            {
                sb.AppendLine($"{item.FirstName} {item.LastName} - {item.JobTitle}");

                foreach (var proj in item.Projects.OrderBy(p => p.ProjectName))
                {
                    sb.AppendLine(proj.ProjectName);
                }
            }

            return sb.ToString();
        }

        //10
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departments = context.Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .ThenBy(d => d.Name)
                .Select(d => new
                {
                    DepartmentName = d.Name,
                    ManagerFirstName = d.Manager.FirstName,
                    ManagerLastName = d.Manager.LastName,
                    Employees = d.Employees.Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.JobTitle
                    })
                }).ToList();

            var sb = new StringBuilder();

            foreach (var item in departments)
            {
                sb.AppendLine($"{item.DepartmentName} - {item.ManagerFirstName} {item.ManagerLastName}");

                foreach (var employee in item.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                {
                    sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                }
            }

            return sb.ToString();
        }

        //11
        public static string GetLatestProjects(SoftUniContext context)
        {
            var projects = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .OrderBy(p => p.Name)
                .ToList();

            var sb = new StringBuilder();

            foreach (var item in projects)
            {
                sb.AppendLine($"{item.Name}")
                  .AppendLine($"{item.Description}")
                  .AppendLine($"{item.StartDate}");
            }

            return sb.ToString();
        }

        //12
        public static string IncreaseSalaries(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e =>
                    e.Department.Name == "Engineering"
                    || e.Department.Name == "Tool Design"
                    || e.Department.Name == "Marketing"
                    || e.Department.Name == "Information Services"
                )
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    NewSalary = e.Salary * 1.12m
                })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            context.SaveChanges();

            var sb = new StringBuilder();

            foreach (var item in employees)
            {
                sb.AppendLine($"{item.FirstName} {item.LastName} (${item.NewSalary:f2})");
            }

            return sb.ToString();
        }

        //13
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                })
                .Where(e => e.FirstName.ToLower().StartsWith("sa"))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            var sb = new StringBuilder();

            foreach (var item in employees)
            {
                sb.AppendLine($"{item.FirstName} {item.LastName} - {item.JobTitle} - (${item.Salary:f2})");
            }

            return sb.ToString();
        }

        //14
        public static string DeleteProjectById(SoftUniContext context)
        {
            var employeesProjects = context.EmployeesProjects
                .Where(ep => ep.ProjectId == 2)
                .ToList();

            context.RemoveRange(employeesProjects);

            var projToRemove = context.Projects.Find(2);
            context.Remove(projToRemove);

            context.SaveChanges();

            var projects = context.Projects
                .Take(10)
                .Select(p => p.Name)
                .ToList();

            var sb = new StringBuilder();

            foreach (var item in projects)
            {
                sb.AppendLine(item);
            }

            return sb.ToString();
        }

        //15
        public static string RemoveTown(SoftUniContext context)
        {
            var employeesToSet = context.Employees
                .Where(e => e.Address.Town.Name == "Seattle")
                .ToList();

            foreach (var item in employeesToSet)
            {
                item.AddressId = null;
            }

            var addressesToDelete = context.Addresses
                .Where(a => a.Town.Name == "Seattle")
                .ToList();

            int count = addressesToDelete.Count();

            context.RemoveRange(addressesToDelete);

            var town = context.Towns.FirstOrDefault(t => t.Name == "Seattle");
            context.Remove(town);

            context.SaveChanges();

            var sb = new StringBuilder();

            sb.Append($"{count} addresses in Seattle were deleted");

            return sb.ToString();
        }
    }
}