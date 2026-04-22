//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using ThePodcastProject.Persistence.DataContext;

//namespace ThePodcastProject.Infraestructure.Repositories
//{
//    public class ClientRepository
//    {
//        private readonly ThePodcastProjectApplicationContext context;

//        public ClientRepository(ThePodcastProjectApplicationContext context)
//        {
//            this.context = context;
//        }

//        public async Task<List<Domain.Entities.Client>> GetAllClientsAsync()
//        {
//            return await context.Clients
//            .Include(c => c.Reservations)
//                .ThenInclude(r => r.Cabin)
//            .ToListAsync();
//        }

//        public async Task<Domain.Entities.Client> GetClientByIdAsync(int id)
//        {
//            return await context.Clients
//            .Include(c => c.Reservations)
//                .ThenInclude(r => r.Cabin)
//            .FirstOrDefaultAsync(c => c.Id == id);
//        }
//        public async Task AddClientAsync(Domain.Entities.Client client)
//        {
//            context.Clients.Add(client);
//
//        }
//        public async Task UpdateClientAsync(Domain.Entities.Client client)
//        {
//            context.Clients.Update(client);
//
//        }
//        public async Task DeleteClientAsync(int id)
//        {
//            var client = await context.Clients.FindAsync(id);
//            if (client != null)
//            {
//                context.Clients.Remove(client);
//
//            }


//        }
//    }
//}

using Microsoft.EntityFrameworkCore;
using ThePodcastProject.Domain.Entities;
using ThePodcastProject.Persistence.DataContext;

namespace ThePodcastProject.Infraestructure.Repositories
{
    public class ReservationRepository
    {
        private readonly ThePodcastProjectApplicationContext context;

        public ReservationRepository(ThePodcastProjectApplicationContext context)
        {
            this.context = context;
        }

        // 🔹 Obtener todas las reservas
        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            return await context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Cabin)
                .Include(r => r.Equipments)
                .ToListAsync();
        }

        // 🔹 Obtener por ID
        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            return await context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Cabin)
                .Include(r => r.Equipments)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        // 🔥 🔴 VALIDACIÓN DE HORARIO (CLAVE DEL PROYECTO)
        public async Task<bool> HasConflictAsync(Reservation reservation)
        {
            return await context.Reservations.AnyAsync(r =>
                r.CabinId == reservation.CabinId &&
                r.Id != reservation.Id &&
                r.StartDate < reservation.EndDate &&
                reservation.StartDate < r.EndDate
            );
        }

        // 🔹 Crear reserva
        public async Task<string> AddReservationAsync(Reservation reservation)
        {
            // Validar conflicto
            var conflict = await HasConflictAsync(reservation);

            if (conflict)
                return "La cabina ya está reservada en ese horario";

            context.Reservations.Add(reservation);


            return "Reserva creada correctamente";
        }

        // 🔹 Actualizar reserva
        public async Task<string> UpdateReservationAsync(Reservation reservation)
        {
            var conflict = await HasConflictAsync(reservation);

            if (conflict)
                return "Conflicto de horario";

            context.Reservations.Update(reservation);


            return "Reserva actualizada";
        }

        // 🔹 Eliminar
        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                context.Reservations.Remove(reservation);

            }
        }
    }
}