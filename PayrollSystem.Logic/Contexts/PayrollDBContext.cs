using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PayrollSystem.Logic.Domain.Employees;
using PayrollSystem.Logic.Domain.PayrollEntries;
using PayrollSystem.Logic.Domain.Positions;
using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails;
using PayrollSystem.Logic.Domain.SalaryAdjustments;
using System.Diagnostics;
using System.Reflection;

namespace PayrollSystem.Logic.Contexts
{
    public partial class PayrollDBContext : DbContext
    {
#if DEBUG
        public static readonly ILoggerFactory DebugLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
#endif

        internal DbSet<Employee> Employees { get; set; }
        internal DbSet<Position> Positions { get; set; }
        internal DbSet<PayrollEntry> PayrollEntries { get; set; }
        internal DbSet<SalaryAdjustmentDetail> SalaryAdjustmentDetails { get; set; }
        internal DbSet<SalaryAdjustment> SalaryAdjustments { get; set; }

        public PayrollDBContext(DbContextOptions<PayrollDBContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=FRANCIS-PC\\FDEMIN;Database=PayrollDB;Trusted_Connection=True;");
            }
#if DEBUG
            optionsBuilder.UseLoggerFactory(DebugLoggerFactory);
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
