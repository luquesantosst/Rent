using MarkRent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkRent.Infra.Mapping
{
    public class FutureEventMapping : IEntityTypeConfiguration<FutureEvent>
    {
        public void Configure(EntityTypeBuilder<FutureEvent> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.VehicleId).IsRequired();

            builder.HasOne(x => x.Vehicle)
                .WithOne(x => x.FutureEvent)
                .HasForeignKey<FutureEvent>(x => x.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Model).IsRequired();
        }
    }
}
