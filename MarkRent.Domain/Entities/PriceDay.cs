using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkRent.Domain.Entities
{
    public class PriceDay
    {
        public PriceDay() { Id = Guid.NewGuid(); }
        public Guid Id { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Apenas números são permitidos.")]
        public int Day { get; set; }
        public double Price { get; set; }
    }
}
