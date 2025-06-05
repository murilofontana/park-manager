using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkManager.Domain;

namespace ParkManager.Infrastructure.Configuration;

public class ParkingMovementConfiguration : IEntityTypeConfiguration<ParkingMovement>
{
  public void Configure(EntityTypeBuilder<ParkingMovement> builder)
  {
    builder.ToTable("ParkingMovements");
    builder.HasKey(pm => pm.Id);

    builder.Property(pm => pm.EntryDate)
        .IsRequired();

    builder.Property(pm => pm.ExitDate)
        .IsRequired(false);

    builder.Property(pm => pm.VehicleId)
        .IsRequired();

    builder.Property(pm => pm.EstablishmentId)
        .IsRequired();

    builder.Property(pm => pm.Type)
        .IsRequired();
  }
}