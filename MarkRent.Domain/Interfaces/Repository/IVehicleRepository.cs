using MarkRent.Domain.Entities;

namespace MarkRent.Domain.Interfaces.Repository
{
    public interface IVehicleRepository
    {
        Task CreateAsync(Vehicle vehicle);
        Task CreateFutureEvent(FutureEvent @event);
        Task Delete(Vehicle vehicle);
        Task<IEnumerable<Vehicle>> GetAllAsync(string? licensePlate);
        Task<Vehicle> GetById(Guid id);
        Task UpdateLicensePlate(Vehicle vehicle);
    }
}
