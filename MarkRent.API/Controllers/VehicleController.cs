using MarkRent.API.ViewModel.Request.Vehicle;
using MarkRent.Application.Services;
using MarkRent.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MarkRent.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost]
        [Description("Cadastrar uma novo moto")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(VehiclePostViewModel model)
        {
            try
            {
                await _vehicleService.Create(new()
                {
                    LicensePlate = model.LicensePlate,
                    Model = model.Model,
                    Year = model.Year
                });

                return StatusCode(201);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetAll")]
        [Description("Consultar motos existentes")]
        public async Task<IActionResult> GetAll([FromQuery] string? licensePlate)
        {
            return Ok(await _vehicleService.GetAll(licensePlate));
        }

        [HttpGet("{id}")]
        [Description("Consultar motos existentes por Id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _vehicleService.GetById(id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}/licensePlate")]
        [Description("Modificar a placa de uma moto")]
        public async Task<IActionResult> UpdateLicensePlate([FromBody] string licensePlate, Guid id)
        {
            await _vehicleService.UpdateLicensePlate(licensePlate, id);

            return Ok("Placa modificada com sucesso");
        }

        [HttpDelete("{id}")]
        [Description("Remover uma moto")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _vehicleService.Delete(id);

                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
