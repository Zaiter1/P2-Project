using System;
using System.Collections.Generic;
using System.Text;
using ThePodcastProject.Persistence.DataContext;

namespace ThePodcastProject.Infraestructure.Repositories
{
    public class UnitOfWork
    {
        private readonly ThePodcastProjectApplicationContext context;

        public ThePodcastProjectApplicationContext Context => context;

        public ClientRepository Clients { get; }
        public CabinRepository Cabins { get; }
        public EquipmentRepository Equipments { get; }
        public ReservationRepository Reservations { get; }


        public UnitOfWork(ThePodcastProjectApplicationContext context, CabinRepository cabinRepository,
            ClientRepository clientRepository, EquipmentRepository equipmentRepository, ReservationRepository
            reservationRepository)
        {
            this.context= context;
            Cabins= cabinRepository;
            Clients= clientRepository;
            Equipments= equipmentRepository;
            Reservations= reservationRepository;
        }

        public async Task CompleteAsync() 
        {
            await context.SaveChangesAsync();
        }
    }
}
