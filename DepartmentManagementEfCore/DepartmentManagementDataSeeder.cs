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
                            WasChangedDate = DateTime.Now,
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
                            Department = itDep,
                        },
                        new Employee
                        {
                            FullName = "Силин Рубен Васильевич",
                            Position = "Senior developer",
                            WasAddedDate = DateTime.Now,
                            WasChangedDate = DateTime.Now,
                            WasEmployedDate = DateTime.Now - TimeSpan.FromDays(300),
                            Department = itDep
                        }
                    };

                Department accountingDep = new Department
                {
                    Name = "Бухгалтерия",
                    WasAddedDate = DateTime.Now,
                    WasChangedDate = DateTime.Now,
                };

                accountingDep.Employees = new List<Employee>
                {
                    new Employee
                    {
                        FullName = "Панова Дарьяна Серапионовна",
                        Position = "Главный бухгалтер",
                        WasAddedDate = DateTime.Now,
                        WasChangedDate = DateTime.Now,
                        WasEmployedDate = DateTime.Now - TimeSpan.FromDays(55),
                        Department = accountingDep
                    },
                    new Employee
                    {
                        FullName = "Беляева Надежда Якововна",
                        Position = "Бухгалтер",
                        WasAddedDate = DateTime.Now,
                        WasChangedDate = DateTime.Now,
                        WasEmployedDate = DateTime.Now - TimeSpan.FromDays(1),
                        Department = accountingDep
                    },
                    new Employee
                    {
                        FullName = "Григорьева Ася Валерьяновна",
                        Position = "Бухгалтер",
                        WasAddedDate = DateTime.Now,
                        WasChangedDate = DateTime.Now,
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
                    new Employee
                    {
                        FullName = "Брагина Марианна Александровна",
                        Position = "Финансист",
                        WasAddedDate = DateTime.Now,
                        WasEmployedDate = DateTime.Now - TimeSpan.FromDays(7),
                        Department = financialDep
                    },
                };

                Department legalDep = new Department
                {
                    Name = "Юридический отдел",
                    WasAddedDate = DateTime.Now
                };

                legalDep.Employees = new List<Employee>
                {
                    new Employee
                    {
                        FullName = "Назаров Оскар Вадимович",
                        Position = "Главный юрист",
                        WasAddedDate = DateTime.Now,
                        WasEmployedDate = DateTime.Now - TimeSpan.FromDays(225),
                        Department = legalDep
                    },
                    new Employee
                    {
                        FullName = "Петров Прохор Серапионович",
                        Position = "Юрист",
                        WasAddedDate = DateTime.Now,
                        WasEmployedDate = DateTime.Now - TimeSpan.FromDays(11),
                        Department = legalDep
                    },
                    new Employee
                    {
                        FullName = "Пахомова Алина Аркадьевна",
                        Position = "Юрист-экономист",
                        WasAddedDate = DateTime.Now,
                        WasEmployedDate = DateTime.Now - TimeSpan.FromDays(7),
                        Department = legalDep
                    }
                };

                Department salesDep = new Department
                {
                    Name = "Торговый отдел",
                    WasAddedDate = DateTime.Now
                };

                salesDep.Employees = new List<Employee>
                {
                    new Employee
                    {
                        FullName = "Бобылёва Изабелла Федоровна",
                        Position = "Глава отдела продаж",
                        WasAddedDate = DateTime.Now,
                        WasEmployedDate = DateTime.Now - TimeSpan.FromDays(225),
                        Department = salesDep
                    },
                    new Employee
                    {
                        FullName = "Костин Эльдар Авксентьевич",
                        Position = "Продавец-консультант",
                        WasAddedDate = DateTime.Now,
                        WasEmployedDate = DateTime.Now - TimeSpan.FromDays(11),
                        Department = salesDep
                    },
                    new Employee
                    {
                        FullName = "Щукина Полина Николаевна",
                        Position = "Продавец-консультант",
                        WasAddedDate = DateTime.Now,
                        WasEmployedDate = DateTime.Now - TimeSpan.FromDays(7),
                        Department = salesDep
                    }
                };

                context.Departments.AddRange(itDep, accountingDep, financialDep, legalDep, salesDep);
                context.SaveChanges();
            }
        }
    }
}
