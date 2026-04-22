using System;
using System.Collections.Generic;
using System.Text;

namespace ThePodcastProject.Application.Dtos
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<ReservationDto> Reservations { get; set; } = new List<ReservationDto>();

    }
}
