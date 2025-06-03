using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ParkManager.Domain;

namespace ParkManager.Infrastructure.Configuration;

public class EstablishmentConfiguration : IEntityTypeConfiguration<Establishment>
{
  public void Configure(EntityTypeBuilder<Establishment> builder)
  {
    builder.ToTable("Establishments");

    builder.HasKey(e => e.Id);

    builder.Property(e => e.Name)
        .IsRequired()
        .HasMaxLength(200);

    builder.OwnsOne(e => e.Cnpj, cnpj =>
    {
      cnpj.Property(c => c.Cnpj)
                  .HasColumnName("Cnpj")
                  .IsRequired()
                  .HasMaxLength(14);
    });

    builder.OwnsOne(e => e.Address, address =>
    {
      address.Property(a => a.City)
                  .HasColumnName("City")
                  .IsRequired()
                  .HasMaxLength(100);

      address.Property(a => a.State)
                  .HasColumnName("State")
                  .IsRequired()
                  .HasMaxLength(2);

      address.Property(a => a.Street)
                  .HasColumnName("Street")
                  .IsRequired()
                  .HasMaxLength(200);

      address.Property(a => a.Number)
                  .HasColumnName("Number")
                  .IsRequired()
                  .HasMaxLength(20);

      address.Property(a => a.Complement)
                  .HasColumnName("Complement")
                  .HasMaxLength(100);

      address.Property(a => a.ZipCode)
                  .HasColumnName("ZipCode")
                  .IsRequired()
                  .HasMaxLength(10);
    });

    builder.Property(e => e.Phone)
        .IsRequired()
        .HasMaxLength(20);

    builder.Property(e => e.MotorcyclesParkingSpaces)
        .IsRequired();

    builder.Property(e => e.CarsParkingSpaces)
        .IsRequired();

    //builder.Ignore(e => e.GetParkingMovements());

    builder.OwnsMany<ParkingMovement>("_parkingMovementList", pm =>
    {
      pm.ToTable("ParkingMovements");
      pm.WithOwner().HasForeignKey("EstablishmentId");

      pm.HasKey(p => p.Guid);

      pm.Property(p => p.Guid)
                  .HasColumnName("Id")
                  .IsRequired();

      pm.Property(p => p.ParkingId)
                  .IsRequired();

      pm.Property(p => p.VehicleId)
                  .IsRequired();

      pm.Property(p => p.Type)
                  .IsRequired();

      pm.Property(p => p.EntryDate)
                  .IsRequired();

      pm.Property(p => p.ExitDate);
    });
  }
}
