using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkRent.Domain.Entities
{
    public class Hire
    {
        public Hire()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Guid DeliverAgentId { get; set; }
        public DeliveryAgent DeliveryAgent { get; set; }
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public DateTime? DevolutionDate { get; set; }
        public double? PricePerDay { get; set; }
        public int Plan { get; set; }

    }
}
