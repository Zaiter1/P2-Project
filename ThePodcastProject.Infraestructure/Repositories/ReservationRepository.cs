
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

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            return await context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Cabin)
                .Include(r => r.Equipments)
                .ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            return await context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Cabin)
                .Include(r => r.Equipments)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> HasConflictAsync(Reservation reservation)
        {
            return await context.Reservations.AnyAsync(r =>
                r.CabinId == reservation.CabinId &&
                r.Id != reservation.Id &&
                r.StartDate < reservation.EndDate &&
                reservation.StartDate < r.EndDate
            );
        }

        public async Task<string> AddReservationAsync(Reservation reservation)
        {
            var conflict = await HasConflictAsync(reservation);

            if (conflict)
                return "La cabina ya está reservada en ese horario";

            context.Reservations.Add(reservation);


            return "Reserva creada correctamente";
        }

        public async Task<string> UpdateReservationAsync(Reservation reservation)
        {
            var conflict = await HasConflictAsync(reservation);

            if (conflict)
                return "Conflicto de horario";

            context.Reservations.Update(reservation);


            return "Reserva actualizada";
        }

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
