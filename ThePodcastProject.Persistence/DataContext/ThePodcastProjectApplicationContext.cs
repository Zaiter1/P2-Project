using ThePodcastProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace ThePodcastProject.Persistence.DataContext
{
    public class ThePodcastProjectApplicationContext : DbContext
    {
        public ThePodcastProjectApplicationContext(DbContextOptions options): base(options) { }


        public DbSet<Client> Clients { get; set; }
        public DbSet<Cabin> Cabins { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Reservation> Reservations { get; set; }


    }

 
}
