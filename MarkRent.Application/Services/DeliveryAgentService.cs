using MarkRent.Domain.DTOs.DeliveryAgent;
using MarkRent.Domain.DTOs.Vehicle;
using MarkRent.Domain.Entities;
using MarkRent.Domain.Entities.Enums;
using MarkRent.Domain.Exceptions;
using MarkRent.Domain.Interfaces.Repository;
using MarkRent.Domain.Interfaces.Service;

namespace MarkRent.Application.Services
{
    public class DeliveryAgentService : IDeliveryAgentService
    {
        private readonly IDeliveryAgentRepository _deliveryAgentRepository;

        public DeliveryAgentService(IDeliveryAgentRepository deliveryAgentRepository)
        {
            _deliveryAgentRepository = deliveryAgentRepository;
        }

        public async Task Create(CreateDeliveryAgentDTO createDto)
        {
            CreateValidation(createDto);

            var deliveryAgent = await this.GetByDocuments(createDto.CNPJ, createDto.CNH_Number);

            if (deliveryAgent is not null)
                throw new ConflictException("Já existe um entregador com o mesmo CNPJ e/ou CNH cadastrado.");

            deliveryAgent = new DeliveryAgent
            {
                Name = createDto.Name,
                Birthdate = createDto.Birthdate,
                CNH_Number = createDto.CNH_Number,
                CNH_Type = createDto.CNH_Type,
                CNPJ = createDto.CNPJ
            };

            await _deliveryAgentRepository.CreateAsync(deliveryAgent);
        }

        public async Task<GetDeliveryAgentDTO> GetById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Nenhum entregador informado para busca.");

            var instance = await _deliveryAgentRepository.GetById(id);

            if (instance is null)
                throw new KeyNotFoundException("O entregador informado não existe.");

            return new GetDeliveryAgentDTO()
            {
                Id = instance.Id,
                Name = instance.Name,
                Birthdate = instance.Birthdate,
                CNH_Number = instance.CNH_Number,
                CNH_Type = instance.CNH_Type,
                CNPJ = instance.CNPJ

            } ?? throw new ApplicationException("Entregador não econtrada.");
        }

        public async Task<DeliveryAgent> GetByDocuments(string cnpj, string cnhNumber)
        {
            return await _deliveryAgentRepository.GetByDocuments(cnpj, cnhNumber);
        }

        public async Task<bool> UpdateCNHImageAsync(Guid deliveryAgentId, string imagePath)
        {
            return await _deliveryAgentRepository.UpdateCNHImageAsync(deliveryAgentId, imagePath);
        }

        private static void CreateValidation(CreateDeliveryAgentDTO dto)
        {

            if (dto is null)
            {
                throw new ArgumentException("Nenhum Dado do entregador informado.");
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("O nome do entregador é obrigatório.");
            }

            if (dto.Birthdate == DateTime.MinValue)
            {
                throw new ArgumentException("A data de nascimento do entregador é obrigatória.");
            }

            if (string.IsNullOrWhiteSpace(dto.CNH_Number))
            {
                throw new ArgumentException("O numero da CNH é obrigatório.");
            }

            if (dto.CNH_Type == TypeCNH.None)
            {
                throw new ArgumentException("A tipo da CNH do entregador é obrigatória.");
            }

            if (string.IsNullOrWhiteSpace(dto.CNPJ))
            {
                throw new ArgumentException("O CNPJ é obrigatório.");
            }
        }
    }


}
