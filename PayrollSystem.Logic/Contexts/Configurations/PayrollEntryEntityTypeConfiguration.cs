using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayrollSystem.Logic.Domain.PayrollEntries;

namespace PayrollSystem.Logic.Contexts.Configurations
{
    internal sealed class PayrollEntryEntityTypeConfiguration : IEntityTypeConfiguration<PayrollEntry>
    {
        public void Configure(EntityTypeBuilder<PayrollEntry> builder)
        {
            builder.ToTable("PayrollEntries"/*); /*/, b => b.ExcludeFromMigrations());//*/

            builder.Property(e => e.ID)
                .HasColumnName("PayrollEntryID");

            builder.Property(e => e.HoursWorked)
                .HasColumnName("HoursWorked")
                .HasColumnType("float")
                .IsRequired();

            builder.Property(e => e.HoursOvertime)
                .HasColumnName("HoursOvertime")
                .HasColumnType("float")
                .IsRequired();

            builder.OwnsOne(e => e.Date, d =>
            {
                d.Property(p => p.Start)
                .HasColumnName("StartDate")
                .HasColumnType("date")
                .IsRequired();

                d.Property(p => p.End)
                .HasColumnName("EndDate")
                .HasColumnType("date")
                .IsRequired();
            });

            builder.Navigation(e => e.Date)
                .IsRequired();

            builder.Navigation(e => e.SalaryAdjustmentDetails)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne(d => d.CurrentPosition)
                .WithMany(p => p.PayrollEntries)
                .HasForeignKey("CurrentPositionID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PayrollEntries_Positions")
                .IsRequired();

            builder.HasOne(d => d.Employee)
                .WithMany(p => p.PayrollEntries)
                .HasForeignKey("EmployeeID")
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PayrollEntries_Employees")
                .IsRequired();
        }
    }
}
