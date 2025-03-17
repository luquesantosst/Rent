using MarkRent.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkRent.Domain.DTOs.DeliveryAgent
{
    public class CreateDeliveryAgentDTO
    {
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateTime Birthdate { get; set; }
        public string CNH_Number { get; set; }
        public TypeCNH CNH_Type { get; set; }
    }
}
