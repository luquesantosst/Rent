using MarkRent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkRent.Domain.Interfaces.Repository
{
    public interface IHireRepository
    {
        Task CreateAsync(Hire hire);
        Task<Hire> GetById(Guid id);
        Task<bool> GetDeliverAgentActiveHire(Guid deliverAgentId);
        Task<bool> GetVehicleActiveHire(Guid vehicleId);
        Task UpdateHire(Guid hireId, double price, DateTime devolutionDate);
    }
}
