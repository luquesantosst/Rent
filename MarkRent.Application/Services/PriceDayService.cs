using MarkRent.Domain.Interfaces.Repository;
using MarkRent.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkRent.Application.Services
{
    public class PriceDayService : IPriceDayService
    {
        private readonly IPriceDayRepository _priceDayRepository;

        public PriceDayService(IPriceDayRepository priceDayRepository)
        {
            _priceDayRepository = priceDayRepository;
        }

        public async Task<double> GetPriceByDay(int day)
        {
            var price = await _priceDayRepository.GetPriceByDay(day);

            return price;
        }
    }
}
