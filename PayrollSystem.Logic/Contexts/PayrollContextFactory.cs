using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PayrollSystem.Logic.Contexts
{
    internal class DesignTimeContextFactory : IDesignTimeDbContextFactory<PayrollDBContext>
    {
        public PayrollDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PayrollDBContext>();
            optionsBuilder.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = PayrollDB; Integrated Security = True;");

            return new PayrollDBContext(optionsBuilder.Options);
        }
    }
}
