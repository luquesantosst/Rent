using MarkRent.API.ViewModel.Request.DeliveryAgent;
using MarkRent.Domain.Entities;
using MarkRent.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace MarkRent.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliveryAgentController : ControllerBase
    {
        private readonly IDeliveryAgentService _deliveryAgentService;
        private readonly IStorageService _storageService;

        public DeliveryAgentController(IDeliveryAgentService deliveryAgentService, IStorageService storageService)
        {
            _deliveryAgentService = deliveryAgentService;
            _storageService = storageService;
        }

        [HttpPost]
        [Description("Cadastrar um novo entregador")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(DeliveryAgentPostViewModel model)
        {
            try
            {
                await _deliveryAgentService.Create(new()
                {
                    Name = model.Name,
                    CNPJ = model.CNPJ,
                    Birthdate = model.Birthdate,
                    CNH_Number = model.CNH_Number,
                    CNH_Type = model.CNH_Type
                });

                return StatusCode(201);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("upload-cnh/{id}")]
        public async Task<IActionResult> UploadCNH(Guid id, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("Nenhum arquivo enviado.");

                var allowedExtensions = new[] { ".png", ".bmp" };
                var extension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                    return BadRequest("Formato de arquivo inválido. Apenas PNG e BMP são permitidos.");

                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();

                string filePath = await _storageService.UploadAsync(fileBytes, file.FileName, file.ContentType);

                var updateResult = await _deliveryAgentService.UpdateCNHImageAsync(id, filePath);

                return StatusCode(201, new { Message = "CNH enviada com sucesso!", FilePath = filePath });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
