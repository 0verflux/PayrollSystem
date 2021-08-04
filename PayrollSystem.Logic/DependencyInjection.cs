using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PayrollSystem.Logic.Application;
using PayrollSystem.Logic.Contexts;
using PayrollSystem.Logic.Contracts;
using PayrollSystem.Logic.Domain.Employees;
using PayrollSystem.Logic.Domain.PayrollEntries;
using PayrollSystem.Logic.Domain.SalaryAdjustments;
using PayrollSystem.Logic.Mappers;
using System;

namespace PayrollSystem.Logic
{
    public static class DependencyInjection
    {
        //
        // Unity Container Dependency Injection (for test in PayrollSystem.ConsoleUI)
        //
        //public static IUnityContainer AddDependency(this IUnityContainer container)
        //{
        //    var config = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));
        //    var options = new DbContextOptionsBuilder<PayrollDBContext>()
        //        .UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = PayrollDB; Integrated Security = True;")
        //        .EnableSensitiveDataLogging(true)
        //        .LogTo(Console.Write)
        //        .Options;

        //    container.RegisterInstance(config.CreateMapper());
        //    container.RegisterType<PayrollDBContext>(new TransientLifetimeManager(), new InjectionConstructor(options));

        //    return container;
        //}

        public static IServiceCollection AddDependency(this IServiceCollection services, string connectionString)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));

            services.AddSingleton(config.CreateMapper());
            services.AddDbContextFactory<PayrollDBContext>(o => o.UseSqlServer(connectionString), ServiceLifetime.Scoped);
            services.AddDbContext<PayrollDBContext>(o => o.UseSqlServer(connectionString), ServiceLifetime.Scoped);

            services.AddScoped<IEmployeeManager, EmployeeManager>();
            services.AddScoped<IPositionManager, PositionManager>();
            services.AddScoped<ISalaryAdjustmentManager, SalaryAdjustmentManager>();
            services.AddScoped<IPayrollManager, PayrollManager>();
            return services;
        }
    }
}
