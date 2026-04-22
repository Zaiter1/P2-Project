using Microsoft.AspNetCore.Mvc;
using ThePodcastProject.Application.Dtos;
using ThePodcastProject.Application.Services;

namespace ThePodcastProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService service;

        public ReservationController(ReservationService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await service.GetAllReservationsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await service.GetReservationByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationDto dto)
        {
            //await service.AddReservationAsync(dto);
            //return Ok();
            try
            {
                await service.AddReservationAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update(ReservationDto dto)
        {
            await service.UpdateReservationAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteReservationAsync(id);
            return Ok();
        }
    }
}