using System;
using System.Collections.Generic;
using System.Text;

namespace ThePodcastProject.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }


        public int CabinId { get; set; }
        public Cabin Cabin { get; set; }


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public string ReservationState { get; set; } // activada,cancelada o finalizada

        public ICollection<Equipment> Equipments { get; set; } = new List<Equipment>();
    }
}
