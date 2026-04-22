using Microsoft.EntityFrameworkCore;
using ThePodcastProject.Domain.Entities;
using ThePodcastProject.Persistence.DataContext;

namespace ThePodcastProject.Infraestructure.Repositories
{
    public class ClientRepository
    {
        private readonly ThePodcastProjectApplicationContext context;

        public ClientRepository(ThePodcastProjectApplicationContext context)
        {
            this.context = context;
        }

        public async Task<List<Client>> GetAllClientsAsync()
        {
            return await context.Clients
            .Include(c => c.Reservations)
                .ThenInclude(r => r.Cabin)
            .ToListAsync();
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            return await context.Clients
            .Include(c => c.Reservations)
                .ThenInclude(r => r.Cabin)
            .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task AddClientAsync(Client client)
        {
            context.Clients.Add(client);

        }
        public async Task UpdateClientAsync(Client client)
        {
            context.Clients.Update(client);

        }
        public async Task DeleteClientAsync(int id)
        {
            var client = await context.Clients.FindAsync(id);
            if (client != null)
            {
                context.Clients.Remove(client);

            }


        }
    }
}

