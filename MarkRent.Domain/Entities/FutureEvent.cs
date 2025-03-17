using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkRent.Domain.Entities
{
    public class FutureEvent
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public string Model { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
