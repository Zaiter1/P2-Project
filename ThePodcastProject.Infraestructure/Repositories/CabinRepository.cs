using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ThePodcastProject.Domain.Entities;
using ThePodcastProject.Persistence.DataContext;

namespace ThePodcastProject.Infraestructure.Repositories
{
    public class CabinRepository
    {
        private readonly ThePodcastProjectApplicationContext context;

        public CabinRepository(ThePodcastProjectApplicationContext context)
        {
            this.context = context;
        }

        public async Task<List<Cabin>> GetAllCabinsAsync()
        {
            return await context.Cabins
                .Include(c => c.Reservations)
                .ToListAsync();
        }

        public async Task<Cabin?> GetCabinByIdAsync(int id)
        {
            return await context.Cabins
                .Include(c => c.Reservations)
                .ThenInclude(r => r.Client)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCabinAsync(Cabin cabin)
        {
            context.Cabins.Add(cabin);

        }

        public async Task UpdateCabinAsync(Cabin cabin)
        {
            context.Cabins.Update(cabin);

        }

        public async Task DeleteCabinAsync(int id)
        {
            var cabin = await context.Cabins.FindAsync(id);
            if (cabin != null)
            {
                context.Cabins.Remove(cabin);

            }
        }
    }
}
