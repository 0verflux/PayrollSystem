using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayrollSystem.Logic.Domain.Employees;

namespace PayrollSystem.Logic.Contexts.Configurations
{
    internal sealed class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees"/*); /*/, b => b.ExcludeFromMigrations());//*/

            builder.Property(e => e.ID)
                .HasColumnName("EmployeeID");

            builder.OwnsOne(e => e.PersonalInformation, pi =>
            {
                pi.Property(p => p.FullName)
                .HasColumnName("FullName")
                .IsRequired()
                .HasMaxLength(256);

                pi.Property(p => p.Address)
                .HasColumnName("Address")
                .IsRequired()
                .HasMaxLength(512);

                pi.Property(p => p.Gender)
                .HasColumnName("Gender")
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength(true);

                pi.Property(p => p.BirthDate)
                .HasColumnName("BirthDate")
                .HasColumnType("date")
                .IsRequired();
            });

            builder.Navigation(e => e.PersonalInformation)
                .IsRequired();

            builder.OwnsOne(e => e.EmploymentDate, ed =>
            {
                ed.Property(p => p.Start)
                .HasColumnName("EmploymentStartDate")
                .HasColumnType("date")
                .IsRequired();

                ed.Property(p => p.End)
                .HasColumnName("EmploymentEndDate")
                .HasColumnType("date")
                .IsRequired(false);

                ed.Ignore(p => p.Days);
                ed.Ignore(p => p.DaysSinceEndDate);
                ed.Ignore(p => p.HasEndDate);
                ed.Ignore(p => p.TotalDays);
            });

            builder.Navigation(e => e.EmploymentDate)
                .IsRequired();

            builder.Property<int?>("PositionID")
                .HasColumnName("PositionID");

            builder.HasOne(d => d.Position)
                .WithMany(p => p.Employees)
                .HasForeignKey("PositionID")
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Employees_Positions");
        }
    }
}
