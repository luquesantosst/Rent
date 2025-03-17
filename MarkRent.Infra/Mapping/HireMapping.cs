using MarkRent.Domain.Entities;
using MarkRent.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarkRent.Infra.Mapping
{
    public class HireMapping : IEntityTypeConfiguration<Hire>
    {
        public void Configure(EntityTypeBuilder<Hire> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.DeliveryAgent)
                .WithMany() 
                .HasForeignKey(x => x.DeliverAgentId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(x => x.Vehicle)
                .WithMany() 
                .HasForeignKey(x => x.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.EstimatedEndDate).IsRequired();
            builder.Property(x => x.DevolutionDate);
            builder.Property(x => x.PricePerDay);
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.Plan).IsRequired();
        }
    }
}
