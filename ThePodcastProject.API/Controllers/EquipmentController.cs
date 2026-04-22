using Microsoft.AspNetCore.Mvc;
using ThePodcastProject.Application.Dtos;
using ThePodcastProject.Application.Services;

namespace ThePodcastProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentController : ControllerBase
    {
        private readonly EquipmentService service;

        public EquipmentController(EquipmentService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await service.GetAllEquipmentsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await service.GetEquipmentByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EquipmentDto dto)
        {
            await service.AddEquipmentAsync(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(EquipmentDto dto)
        {
            await service.UpdateEquipmentAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteEquipmentAsync(id);
            return Ok();
        }
    }
}