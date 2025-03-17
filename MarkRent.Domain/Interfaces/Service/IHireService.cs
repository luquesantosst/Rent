using MarkRent.Domain.DTOs.Hire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkRent.Domain.Interfaces.Service
{
    public interface IHireService
    {
        Task CalculateTotalPrice(Guid hireId, DateTime devolutionDate);
        Task Create(CreateHireDTO hireDto);
        Task<GetHireDTO> GetById(Guid id);
        Task HasVeicleInUse(Guid vehicleId);
    }
}
