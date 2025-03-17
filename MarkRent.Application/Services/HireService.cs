using MarkRent.Domain.DTOs.DeliveryAgent;
using MarkRent.Domain.DTOs.Hire;
using MarkRent.Domain.DTOs.Vehicle;
using MarkRent.Domain.Entities;
using MarkRent.Domain.Entities.Enums;
using MarkRent.Domain.Exceptions;
using MarkRent.Domain.Interfaces.Repository;
using MarkRent.Domain.Interfaces.Service;
using System.Data.Common;
using System.Numerics;

namespace MarkRent.Application.Services
{
    public class HireService : IHireService
    {
        private readonly IHireRepository _hireRepository;
        private readonly IVehicleService _vehicleService;
        private readonly IDeliveryAgentService _deliveryAgentService;
        private readonly IPriceDayService _priceDayService;
        public HireService(IHireRepository hireRepository, IDeliveryAgentService deliveryAgentService, IVehicleService vehicleService, IPriceDayService priceDayService)
        {
            _hireRepository = hireRepository;
            _deliveryAgentService = deliveryAgentService;
            _vehicleService = vehicleService;
            _priceDayService = priceDayService;
        }

        public async Task Create(CreateHireDTO createDto)
        {
            CreateValidation(createDto);

            var deliveryAgent = await _deliveryAgentService.GetById(createDto.DeliverAgentId);

            if (deliveryAgent == null)
                throw new KeyNotFoundException("Entregador não encontrado.");

            if (deliveryAgent.CNH_Type != TypeCNH.A)
                throw new ConflictException("Somente entregadores habilitados na categoria A podem efetuar uma locação.");


            var vehicle = await _vehicleService.GetById(createDto.VehicleId);

            if (vehicle == null)
                throw new KeyNotFoundException("Veículo não encontrado.");

            await this.HasActiveHire(createDto.DeliverAgentId);

            await this.HasVeicleInUse(createDto.VehicleId);

            var hire = new Hire
            {
                DeliverAgentId = createDto.DeliverAgentId,
                VehicleId = createDto.VehicleId,
                EndDate = createDto.EndDate,
                EstimatedEndDate = createDto.EstimatedEndDate,
                Plan = createDto.Plan,
                StartDate = DateTime.UtcNow.AddDays(1)
            };

            await _hireRepository.CreateAsync(hire);
        }

        public async Task<GetHireDTO> GetById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ApplicationException("Locação não informada.");

            var instance = await _hireRepository.GetById(id);

            if (instance is null)
                throw new KeyNotFoundException("A Locação informada não existe.");


            return new GetHireDTO()
            {
                Id = instance.Id,
                DeliverAgentId = instance.DeliverAgentId,
                VehicleId = instance.VehicleId,
                EndDate = instance.EndDate,
                StartDate = instance.StartDate,
                EstimatedEndDate = instance.EstimatedEndDate,
                Plan = instance.Plan,
                PricePerDay = instance.PricePerDay,
                DevolutionDate = instance.DevolutionDate,
                

            } ?? throw new ApplicationException("Locação não econtrada.");
        }

        public async Task CalculateTotalPrice(Guid hireId, DateTime devolutionDate)
        {
            var hire = await this.GetById(hireId);

            if (hire == null)
            {
                throw new ArgumentException("Locação não encontrada.");
            }

            double? pricePerDay = await _priceDayService.GetPriceByDay(hire.Plan.Value);

            if (pricePerDay is null || pricePerDay <= 0)
            {
                throw new ArgumentException("O valor por diária deve ser válido.");
            }

            double price = pricePerDay.Value;
            double totalPrice = 0;

            double penaltyRate = hire.Plan == 7 ? 0.20 : (hire.Plan == 15 ? 0.40 : 0);

            if (devolutionDate.Date < hire.EstimatedEndDate.Date) 
            {
                int missedDays = (hire.EstimatedEndDate.Date - devolutionDate.Date).Days; 

                if (missedDays > 0 && penaltyRate > 0)
                {
                    int usedDays = hire.Plan.Value - missedDays;
                    totalPrice = (usedDays * price) + (missedDays * price * penaltyRate);
                }
            }
            else if (devolutionDate.Date > hire.EstimatedEndDate.Date) 
            {
                int additionalDays = (devolutionDate.Date - hire.EstimatedEndDate.Date).Days;
                totalPrice = (hire.Plan.Value * price) + (additionalDays * 50);
            }
            else // Se devolver na data exata, paga o valor normal
            {
                totalPrice = hire.Plan.Value * price;
            }
            
            await _hireRepository.UpdateHire(hireId, totalPrice, devolutionDate);
        }



        private async Task HasActiveHire(Guid deliverId)
        {
            var hasActiveHire = await _hireRepository.GetDeliverAgentActiveHire(deliverId);

            if (hasActiveHire)
                throw new ApplicationException("Entregador já possui uma locação ativa.");
        }

        public async Task HasVeicleInUse(Guid vehicleId)
        {
            var hasActiveHire = await _hireRepository.GetVehicleActiveHire(vehicleId);

            if (hasActiveHire)
                throw new ApplicationException("O uso do veículo está em vigor.");
        }

        private static void CreateValidation(CreateHireDTO dto)
        {
            if (dto is null)
            {
                throw new ArgumentException("Nenhum dado da contratação informado.");
            }

            if (dto.EndDate == DateTime.MinValue)
            {
                throw new ArgumentException("Data de término da locação é obrigatória.");
            }

            if (dto.EstimatedEndDate == DateTime.MinValue)
            {
                throw new ArgumentException("Data de previsão do término da locação é obrigatória.");

            }

            if (!Enum.IsDefined(typeof(HirePlanDays), dto.Plan))
            {
                throw new ArgumentException($"O plano {dto.Plan} não é válido. Os planos disponíveis são 7, 15, 30, 45 e 50.");
            }

            if (dto.VehicleId == Guid.Empty)
            {
                throw new ArgumentException("É obrigatório informar o veículo.");
            }

            if (dto.DeliverAgentId == Guid.Empty)
            {
                throw new ArgumentException("É obrigatório informa o entregador");
            }
        }
    }
}
