using Microsoft.AspNetCore.Mvc;
using ThePodcastProject.Application.Dtos;
using ThePodcastProject.Application.Services;

namespace ThePodcastProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CabinController : ControllerBase
    {
        private readonly CabinService service;

        public CabinController(CabinService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await service.GetAllCabinsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await service.GetCabinByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CabinDto dto)
        {
            await service.AddCabinAsync(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(CabinDto dto)
        {
            await service.UpdateCabinAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteCabinAsync(id);
            return Ok();
        }
    }
}