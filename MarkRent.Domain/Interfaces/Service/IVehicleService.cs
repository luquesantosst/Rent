using MarkRent.Domain.DTOs.Vehicle;

namespace MarkRent.Domain.Interfaces.Service
{
    public interface IVehicleService
    {
        Task Create(CreateVehicleDTO dto);
        Task Delete(Guid id);
        Task<IEnumerable<GetVehicleDTO>> GetAll(string? licensePlate);
        Task<GetVehicleDTO> GetById(Guid id);
        Task UpdateLicensePlate(string licensePlate, Guid id);
    }
}
