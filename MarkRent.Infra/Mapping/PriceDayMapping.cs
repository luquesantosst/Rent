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
    public class PriceDayMapping : IEntityTypeConfiguration<PriceDay>
    {
        public void Configure(EntityTypeBuilder<PriceDay> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(new PriceDay { Day = 7, Price = 30 });
            builder.HasData(new PriceDay { Day = 15, Price = 28 });
            builder.HasData(new PriceDay { Day = 30, Price = 22 });
            builder.HasData(new PriceDay { Day = 45, Price = 20 });
            builder.HasData(new PriceDay { Day = 50, Price = 18 });
        }
    }
}
