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
    public class PriceDayRepository : IPriceDayRepository
    {
        private readonly AppDbContext _context;

        public PriceDayRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<double> GetPriceByDay(int day)
        {
            return await _context.PriceDays
                        .AsNoTracking()
                        .Where(p => p.Day == day)
                        .Select(p => p.Price)
                        .FirstOrDefaultAsync();
        }
    }
}
