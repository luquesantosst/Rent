using MarkRent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarkRent.Infra.Mapping
{
    public class VehicleMapping : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Model).IsRequired().HasMaxLength(40);
            builder.Property(x => x.Year).IsRequired().HasMaxLength(4);
            builder.Property(x => x.LicensePlate).IsRequired().HasMaxLength(8);
        }
    }
}
