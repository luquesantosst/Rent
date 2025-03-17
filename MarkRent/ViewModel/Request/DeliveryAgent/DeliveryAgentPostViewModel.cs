using MarkRent.Domain.Entities.Enums;

namespace MarkRent.API.ViewModel.Request.DeliveryAgent
{
    public record DeliveryAgentPostViewModel(string Name, string CNPJ, DateTime Birthdate, string CNH_Number, TypeCNH CNH_Type);
    
    
}
