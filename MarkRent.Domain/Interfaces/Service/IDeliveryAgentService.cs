using MarkRent.Domain.DTOs.DeliveryAgent;
using MarkRent.Domain.DTOs.Vehicle;
using MarkRent.Domain.Entities;

namespace MarkRent.Domain.Interfaces.Service
{
    public interface IDeliveryAgentService
    {
        Task Create(CreateDeliveryAgentDTO dto);
        Task<DeliveryAgent> GetByDocuments(string cnpj, string cnhNumber);
        Task<GetDeliveryAgentDTO> GetById(Guid id);
        Task<bool> UpdateCNHImageAsync(Guid deliveryAgentId, string imagePath);
    }
}
