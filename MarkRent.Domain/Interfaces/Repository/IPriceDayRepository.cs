using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkRent.Domain.Interfaces.Repository
{
    public interface IPriceDayRepository
    {
        Task<double> GetPriceByDay(int day);
    }
}
