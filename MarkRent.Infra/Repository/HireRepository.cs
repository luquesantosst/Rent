using MarkRent.Domain.Entities;
using MarkRent.Domain.Interfaces.Repository;
using MarkRent.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkRent.Infra.Repository
{
    public class HireRepository : IHireRepository
    {
        private readonly AppDbContext _context;

        public HireRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Hire hire)
        {
            await _context.Hires.AddAsync(hire);
            await _context.SaveChangesAsync();
        }

        public async Task<Hire> GetById(Guid id)
        {
            return await _context.Hires
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> GetDeliverAgentActiveHire(Guid deliverAgentId)
        {
            var hasActiveHire = await _context.Hires
                 .AsNoTracking()
                 .AnyAsync(x => x.DeliverAgentId == deliverAgentId && x.EndDate >= DateTime.UtcNow);

            return hasActiveHire;
        }

        public async Task<bool> GetVehicleActiveHire(Guid vehicleId)
        {
            var hasActiveHire = await _context.Hires
                 .AsNoTracking()
                 .AnyAsync(x => x.DeliverAgentId == vehicleId && x.EndDate >= DateTime.UtcNow);

            return hasActiveHire;

        }

        public async Task UpdateHire(Guid hireId, double price, DateTime devolutionDate)
        {
            var hire = await _context.Hires.FindAsync(hireId);

            hire.PricePerDay = price;
            hire.DevolutionDate = devolutionDate;

            _context.Hires.Update(hire);
            await _context.SaveChangesAsync();
        }
    }
}
