using MarkRent.API.ViewModel.Request.Hire;
using MarkRent.Application.Services;
using MarkRent.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace MarkRent.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HireController : ControllerBase
    {
        private readonly IHireService _hireService;

        public HireController(IHireService hireService)
        {
            _hireService = hireService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(HirePostViewModel model)
        {
            try
            {
                await _hireService.Create(new()
                {
                    DeliverAgentId = model.DeliverAgentId,
                    EndDate = model.EndDate,
                    Plan = model.Plan,
                    EstimatedEndDate = model.EstimatedEndDate,
                    VehicleId = model.VehicleId
                });

                return StatusCode(201);

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        [Description("Consultar contratações por Id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _hireService.GetById(id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{hireId}/devolution")]
        public async Task<IActionResult> CalculateTotalPrice(Guid hireId, [FromBody] DateTime devolutionDate)
        {
            try
            {
                await _hireService.CalculateTotalPrice(hireId, devolutionDate);

                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
