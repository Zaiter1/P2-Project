using System;
using System.Collections.Generic;
using System.Text;

namespace ThePodcastProject.Application.Dtos
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public ClientDto ? Client { get; set; } //? agregado


        public int CabinId { get; set; }
        public CabinDto ? Cabin { get; set; } //? agregado


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public string ReservationState { get; set; } // activada,cancelada o finalizada

        public ICollection<EquipmentDto> Equipments { get; set; } = new List<EquipmentDto>();
    }
}
