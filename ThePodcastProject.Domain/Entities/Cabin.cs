using System;
using System.Collections.Generic;
using System.Text;

namespace ThePodcastProject.Domain.Entities
{
    public class Cabin
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Capacity { get; set; }
        public required string Description { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
