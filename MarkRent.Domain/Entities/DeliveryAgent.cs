using MarkRent.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkRent.Domain.Entities
{
    public class DeliveryAgent
    {
        public DeliveryAgent() 
        { 
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        
        [RegularExpression(@"^\d+$", ErrorMessage = "Apenas números são permitidos.")]
        public string CNPJ { get; set; }
        public DateTime Birthdate { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Apenas números são permitidos.")]
        public string CNH_Number { get; set; }
        public TypeCNH CNH_Type { get; set; }
        public string? CNH_Image { get; set; }
    }
}
