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
    public class DeliveryAgentRepository : IDeliveryAgentRepository
    {
        private readonly AppDbContext _context;

        public DeliveryAgentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(DeliveryAgent deliveryAgent)
        {
            await _context.DeliveryAgents.AddAsync(deliveryAgent);
            await _context.SaveChangesAsync();
        }


        public async Task<DeliveryAgent> GetById(Guid id)
        {
            return await _context.DeliveryAgents
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<DeliveryAgent> GetByDocuments(string cnpj, string cnhNumber)
        {
            return await _context.DeliveryAgents
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.CNPJ == cnpj || x.CNH_Number == cnhNumber);
        }

        public async Task<bool> UpdateCNHImageAsync(Guid deliveryAgentId, string imagePath)
        {
            var agent = await _context.DeliveryAgents.FindAsync(deliveryAgentId);
            if (agent == null) return false;

            agent.CNH_Image = imagePath;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
