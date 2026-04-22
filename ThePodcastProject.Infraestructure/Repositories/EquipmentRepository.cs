using Microsoft.EntityFrameworkCore;
using ThePodcastProject.Domain.Entities;
using ThePodcastProject.Persistence.DataContext;

namespace ThePodcastProject.Infraestructure.Repositories
{
    public class EquipmentRepository
    {
        private readonly ThePodcastProjectApplicationContext context;

        public EquipmentRepository(ThePodcastProjectApplicationContext context)
        {
            this.context = context;
        }

        public async Task<List<Equipment>> GetAllEquipmentsAsync()
        {
            return await context.Equipments
                .Include(e => e.Reservations)
                    .ThenInclude(r => r.Cabin)
                .ToListAsync();
        }

        public async Task<Equipment?> GetEquipmentByIdAsync(int id)
        {
            return await context.Equipments
                .Include(e => e.Reservations)
                    .ThenInclude(r => r.Cabin)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddEquipmentAsync(Equipment equipment)
        {
            context.Equipments.Add(equipment);

        }

        public async Task UpdateEquipmentAsync(Equipment equipment)
        {
            context.Equipments.Update(equipment);

        }

        public async Task DeleteEquipmentAsync(int id)
        {
            var equipment = await context.Equipments.FindAsync(id);
            if (equipment != null)
            {
                context.Equipments.Remove(equipment);

            }
        }
    }
}
