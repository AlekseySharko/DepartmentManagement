using System;
using System.Collections.Generic;
using System.Linq;
using DepartmentManagementModels;
using Microsoft.EntityFrameworkCore;

namespace DepartmentManagementEfCore
{
    public static class DepartmentManagementDataSeeder
    {
        public static void Seed(DepartmentManagementContext context)
        {
            context.Database.Migrate();

            context.SaveChanges();

            if (!context.Employees.Any() && !context.Departments.Any())
            {
                Department itDep = new Department
                {
                    Name = "Отдел ИТ",
                    WasAddedDate = DateTime.Now
                };
                itDep.Employees = new List<Employee>
                    {
                        new Employee
                        {
                            FullName = "Кудряшов Власий Онисимович",
                            Position = "Главный инженер",
                            WasAddedDate = DateTime.Now,
                            WasEmployedDate = DateTime.Now - TimeSpan.FromDays(234),
                            Department = itDep
                        },
                        new Employee
                        {
                            FullName = "Самсонов Евгений Авксентьевич",
                            Position = "Младший технический специалист",
                            WasAddedDate = DateTime.Now,
                            WasEmployedDate = DateTime.Now - TimeSpan.FromDays(2),
                            Department = itDep
                        },
                        new Employee
                        {
                            FullName = "Сазонов Соломон Ильяович",
                            Position = "Младший технический специалист",
                            WasAddedDate = DateTime.Now,
                            WasEmployedDate = DateTime.Now - TimeSpan.FromDays(3),
                            Department = itDep
                        },
                        new Employee
                        {
                            FullName = "Попов Гордей Альвианович",
                            Position = "Junior developer",
                            WasAddedDate = DateTime.Now,
                            WasEmployedDate = DateTime.Now - TimeSpan.FromDays(4),
                            Department = itDep
                        },
                        new Employee
                        {
                            FullName = "Силин Рубен Васильевич",
                            Position = "Senior developer",
                            WasAddedDate = DateTime.Now,
                            WasEmployedDate = DateTime.Now - TimeSpan.FromDays(300),
                            Department = itDep
                        }
                    };

                Department accountingDep = new Department
                {
                    Name = "Бухгалтерия",
                    WasAddedDate = DateTime.Now
                };

                accountingDep.Employees = new List<Employee>
                {
                    new Employee
                    {
                        FullName = "Панова Дарьяна Серапионовна",
                        Position = "Главный бухгалтер",
                        WasAddedDate = DateTime.Now,
                        WasEmployedDate = DateTime.Now - TimeSpan.FromDays(55),
                        Department = accountingDep
                    },
                    new Employee
                    {
                        FullName = "Беляева Надежда Якововна",
                        Position = "Бухгалтер",
                        WasAddedDate = DateTime.Now,
                        WasEmployedDate = DateTime.Now - TimeSpan.FromDays(1),
                        Department = accountingDep
                    },
                    new Employee
                    {
                        FullName = "Григорьева Ася Валерьяновна",
                        Position = "Бухгалтер",
                        WasAddedDate = DateTime.Now,
                        WasEmployedDate = DateTime.Now - TimeSpan.FromDays(5),
                        Department = accountingDep
                    },
                    new Employee
                    {
                        FullName = "Рогов Авраам Георгьевич",
                        Position = "Бухгалтер",
                        WasAddedDate = DateTime.Now,
                        WasEmployedDate = DateTime.Now - TimeSpan.FromDays(12),
                        Department = accountingDep
                    }
                };

                Department financialDep = new Department
                {
                    Name = "Отдел финансов",
                    WasAddedDate = DateTime.Now
                };

                financialDep.Employees = new List<Employee>
                {
                    new Employee
                    {
                        FullName = "Петрова Августа Мэлоровна",
                        Position = "Финансовый директор",
                        WasAddedDate = DateTime.Now,
                        WasEmployedDate = DateTime.Now - TimeSpan.FromDays(225),
                        Department = financialDep
                    },
                    new Employee
                    {
                        FullName = "Брагин Ростислав Владленович",
                        Position = "Финансист",
                        WasAddedDate = DateTime.Now,
                        WasEmployedDate = DateTime.Now - TimeSpan.FromDays(11),
                        Department = financialDep
                    },
                    new Employee
                    {
                        FullName = "Самойлова Марианна Евсеевна",
                        Position = "Финансист",
                        WasAddedDate = DateTime.Now,
                        WasEmployedDate = DateTime.Now - TimeSpan.FromDays(7),
                        Department = financialDep
                    },
                };

                context.Departments.AddRange(itDep, accountingDep, financialDep);
                context.SaveChanges();
            }
        }
    }
}
