using System;
using System.Collections.Generic;
using System.Text;

namespace ThePodcastProject.Application.Dtos
{
    public class CabinDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Capacity { get; set; }
        public required string Description { get; set; }

        public ICollection<ReservationDto> Reservations { get; set; } = new List<ReservationDto>();
    }
}
