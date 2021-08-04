using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayrollSystem.Logic.Domain.SalaryAdjustments;

namespace PayrollSystem.Logic.Contexts.Configurations
{
    internal sealed class SalaryAdjustmentEntityTypeConfiguration : IEntityTypeConfiguration<SalaryAdjustment>
    {
        public void Configure(EntityTypeBuilder<SalaryAdjustment> builder)
        {
            builder.ToTable("SalaryAdjustments"/*); /*/, b => b.ExcludeFromMigrations());//*/

            builder.HasIndex(x => x.Code, "IX_SalaryAdjustments")
                .IsUnique();

            builder.Property(e => e.ID)
                .HasColumnName("SalaryAdjustmentID");

            builder.Property(e => e.Code)
                .HasColumnName("Code")
                .IsRequired()
                .HasMaxLength(16)
                .UseCollation("SQL_Latin1_General_CP1_CS_AS");

            builder.Property(e => e.Description)
                .HasColumnName("Description")
                .IsRequired(false);
        }
    }
}
