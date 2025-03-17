using MarkRent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkRent.Domain.Interfaces.Repository
{
    public interface IDeliveryAgentRepository
    {
        Task CreateAsync(DeliveryAgent deliveryAgent);
        Task<DeliveryAgent> GetByDocuments(string cnpj, string cnhNumber);
        Task<DeliveryAgent> GetById(Guid id);
        Task<bool> UpdateCNHImageAsync(Guid deliveryAgentId, string imagePath);
    }
}
