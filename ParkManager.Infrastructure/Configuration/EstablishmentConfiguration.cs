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

    builder.Property(pm => pm.Id)
      .ValueGeneratedOnAdd();

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

    builder.HasMany(e => e.ParkingMovementList)
    .WithOne()
    .HasForeignKey("EstablishmentId")
    .IsRequired()
    .OnDelete(DeleteBehavior.Cascade);

    //builder.Navigation("_parkingMovementList").UsePropertyAccessMode(PropertyAccessMode.Field);
  }
}
