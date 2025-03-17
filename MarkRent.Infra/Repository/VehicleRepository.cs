using MarkRent.Domain.Entities;
using MarkRent.Domain.Interfaces.Repository;
using MarkRent.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace MarkRent.Infra.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext _context;

        public VehicleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task CreateFutureEvent(FutureEvent @event) // lembrar de verificar o melhor jeito de fazer a parte do rabbit do ano
        {
            await _context.FutureEvents.AddAsync(@event);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync(string? licensePlate)
        {
            var query = _context.Vehicles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(licensePlate))
            {
                query = query.Where(x => x.LicensePlate.Contains(licensePlate));
            }

            return await query.ToListAsync();
        }

        public async Task<Vehicle> GetById(Guid id)
        {
            return await _context.Vehicles
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateLicensePlate(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Vehicle vehicle)
        {
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
        }
    }
}
