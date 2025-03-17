using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkRent.Domain.DTOs.Hire
{
    public class GetHireDTO
    {
        public Guid Id { get; set; }
        public Guid DeliverAgentId { get; set; }
        public Guid VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public DateTime? DevolutionDate { get; set; }
        public double? PricePerDay { get; set; }
        public int? Plan { get; set; }
    }
}
