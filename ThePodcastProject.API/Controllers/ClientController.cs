using Microsoft.AspNetCore.Mvc;
using ThePodcastProject.Application.Dtos;
using ThePodcastProject.Application.Services;

namespace ThePodcastProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientService service;

        public ClientController(ClientService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await service.GetAllClientsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await service.GetClientByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClientDto dto)
        {
            await service.AddClientAsync(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(ClientDto dto)
        {
            await service.UpdateClientAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteClientAsync(id);
            return Ok();
        }
    }
}