using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkRent.Domain.DTOs.Vehicle
{
    public class VehicleDTO : CreateVehicleDTO
    {
        public Guid Id { get; set; }
    }
}
