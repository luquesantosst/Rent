using MarkRent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkRent.Infra.Mapping
{
    public class DeliveryAgentMapping : IEntityTypeConfiguration<DeliveryAgent>
    {
        public void Configure(EntityTypeBuilder<DeliveryAgent> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CNH_Type).HasConversion<string>().IsRequired();
            builder.Property(x => x.CNH_Number).IsRequired().HasMaxLength(9);
            builder.Property(x => x.CNPJ).IsRequired().HasMaxLength(14);
            builder.Property(x => x.Birthdate).IsRequired();
            builder.Property(x => x.CNH_Image);
        }
    }
}
