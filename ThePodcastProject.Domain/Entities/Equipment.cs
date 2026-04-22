using System;
using System.Collections.Generic;
using System.Text;

namespace ThePodcastProject.Domain.Entities
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    }
}
