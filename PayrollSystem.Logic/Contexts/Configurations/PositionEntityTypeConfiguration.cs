using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayrollSystem.Logic.Domain.Positions;

namespace PayrollSystem.Logic.Contexts.Configurations
{
    internal sealed class PositionEntityTypeConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.ToTable("Positions"/*); /*/, b => b.ExcludeFromMigrations());//*/

            builder.HasIndex(e => e.Name, "IX_Positions")
                .IsUnique();

            builder.Property(e => e.ID)
                .HasColumnName("PositionID");

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(e => e.RatePerHour)
                .HasColumnName("RatePerHour")
                .HasColumnType("decimal(14, 4)")
                .IsRequired();
        }
    }
}
