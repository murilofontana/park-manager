using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkManager.Domain;

namespace ParkManager.Infrastructure.Configurations
{
  public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
  {
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
      builder.ToTable("Vehicles");

      builder.HasKey(v => v.Id);

      builder.Property(pm => pm.Id)
      .ValueGeneratedOnAdd();

      builder.Property(v => v.Branch)
                .IsRequired()
                .HasMaxLength(100);

      builder.Property(v => v.Model)
          .IsRequired()
          .HasMaxLength(100);

      builder.Property(v => v.Color)
          .IsRequired()
          .HasMaxLength(50);

      builder.Property(v => v.Plate)
          .IsRequired()
          .HasMaxLength(20);

      builder.Property(v => v.Type)
          .IsRequired();
    }
  }
}
