using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayrollSystem.Logic.Domain.SalaryAdjustmentDetails;
namespace PayrollSystem.Logic.Contexts.Configurations
{
    internal sealed class SalaryAdjustmentDetailEntityTypeConfiguration : IEntityTypeConfiguration<SalaryAdjustmentDetail>
    {
        public void Configure(EntityTypeBuilder<SalaryAdjustmentDetail> builder)
        {
            builder.ToTable("SalaryAdjustmentDetails"/*); /*/, b => b.ExcludeFromMigrations());//*/

            builder.Property(e => e.ID)
                .HasColumnName("SalaryAdjustmentDetailID");

            builder.HasIndex(new[] { "PayrollEntryID", "SalaryAdjustmentID" }, "IX_SalaryAdjustmentDetails")
                .IsUnique();

            builder.OwnsOne(e => e.Percentage, pcg =>
            {
                pcg.Property(p => p.Ratio)
                    .HasColumnName("Ratio")
                    .HasColumnType("float")
                    .IsRequired();

                pcg.Property(p => p.Value)
                .HasColumnName("Value")
                .HasColumnType("decimal(14, 4)")
                .IsRequired();
            });

            builder.Navigation(e => e.Percentage)
                .IsRequired();

            builder.HasOne(d => d.PayrollEntry)
                .WithMany(p => p.SalaryAdjustmentDetails)
                .HasForeignKey("PayrollEntryID")
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SalaryAdjustmentDetails_PayrollEntries")
                .IsRequired();

            builder.HasOne(d => d.SalaryAdjustment)
                .WithMany(p => p.SalaryAdjustmentDetails)
                .HasForeignKey("SalaryAdjustmentID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalaryAdjustmentDetails_SalaryAdjustments")
                .IsRequired();
        }
    }
}
