using System;
using System.Collections.Generic;
using System.Text;

namespace ThePodcastProject.Application.Dtos
{
    public class EquipmentDto
    {
        public int Id { get; set; }
        public  string ? Name { get; set; } 
        public  string ? Type { get; set; } 
        public  string ? Description { get; set; } 

        public ICollection<ReservationDto> Reservations { get; set; } = new List<ReservationDto>();

    }
}
