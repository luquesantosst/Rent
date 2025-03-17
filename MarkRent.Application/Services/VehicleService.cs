using MarkRent.Domain.DTOs.Vehicle;
using MarkRent.Domain.Entities;
using MarkRent.Domain.Exceptions;
using MarkRent.Domain.Interfaces.Messaging;
using MarkRent.Domain.Interfaces.Repository;
using MarkRent.Domain.Interfaces.Service;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MarkRent.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehiclePublisher _vehiclePublisher;
        private readonly IHireRepository _hireRepository;

        public VehicleService(IVehicleRepository vehicleRepository, IVehiclePublisher vehiclePublisher, IHireRepository hireRepository)
        {
            _vehicleRepository = vehicleRepository;
            _vehiclePublisher = vehiclePublisher;
            _hireRepository = hireRepository;
        }

        public async Task Create(CreateVehicleDTO createDto)
        {
            CreateValidation(createDto.Model, createDto.Year, createDto.LicensePlate);

            var licensePlate = await _vehicleRepository.GetAllAsync(createDto.LicensePlate);

            if (licensePlate.Any())
                throw new ApplicationException("Já existe uma moto de mesma placa cadastrada.");

            var vehicle = new Vehicle
            {
                Model = createDto.Model,
                LicensePlate = createDto.LicensePlate,
                Year = createDto.Year,
            };

            await _vehicleRepository.CreateAsync(vehicle);
            await _vehiclePublisher.Publish(vehicle);
        }

        public async Task<IEnumerable<GetVehicleDTO>> GetAll(string? licensePlate)
        {
            var instance = await _vehicleRepository.GetAllAsync(licensePlate);

            return instance.Select(x => new GetVehicleDTO()
            {
                Id = x.Id,
                LicensePlate = x.LicensePlate,
                Year = x.Year,
                Model = x.Model

            }).ToList();
        }

        public async Task<GetVehicleDTO> GetById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Nenhum veículo informado.");

            var instance = await _vehicleRepository.GetById(id);

            if (instance is null)
                throw new KeyNotFoundException("O veículo informado não existe.");

            return new GetVehicleDTO()
            {
                Id = instance.Id,
                LicensePlate = instance.LicensePlate,
                Year = instance.Year,
                Model = instance.Model
            } ?? throw new ApplicationException("Moto não econtrada.");
        }

        public async Task UpdateLicensePlate(string licensePlate, Guid id)
        {
            UpdateValidation(licensePlate, id);

            var instance = await this.GetById(id);

            var vehicle = new Vehicle
            {
                Id = id,
                Model = instance.Model,
                LicensePlate = licensePlate,
                Year = instance.Year,
            };

            await _vehicleRepository.UpdateLicensePlate(vehicle);
        }

        public async Task Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ApplicationException("Nenhum veículo informado para deletar.");

            var instance = await this.GetById(id);

            if (await _hireRepository.GetVehicleActiveHire(instance.Id) is true)
                throw new ConflictException("O veículo possui uma locação em vigor");

            var vehicle = new Vehicle
            {
                Id = instance.Id,
                Model = instance.Model,
                LicensePlate = instance.LicensePlate,
                Year = instance.Year,
            };

            await _vehicleRepository.Delete(vehicle);
        }

        private static void CreateValidation(string model, int year, string licensaPlate)
        {
            if (year < 1 || year > DateTime.Now.Year)
            {
                throw new ArgumentException("Informe um ano valido.");
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                throw new ArgumentException("O Modelo da moto é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(licensaPlate))
            {
                throw new ArgumentException("A placa da moto é obrigatória.");
            }

        }

        private static void UpdateValidation(string licensePlate, Guid id)
        {
            if (string.IsNullOrWhiteSpace(licensePlate))
            {
                throw new ArgumentException("A placa da moto é obrigatoria.");
            }

            if (id == Guid.Empty)
            {
                throw new ArgumentException("Nenhum veículo informado para busca.");
            }
        }
    }
}
