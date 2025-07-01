using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingApp.Models;
using TicketBookingApp.Interfaces;

namespace TicketBookingApp.Services
{
    public class TicketService : ITicketService
    {
        private readonly Container _container;

        public TicketService(CosmosClient dbClient, IConfiguration config)
        {
            var databaseName = config["CosmosDb:DatabaseName"];
            var containerName = config["CosmosDb:ContainerName"];
            _container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddAsync(TicketEntry ticket)
        {
            await _container.CreateItemAsync(ticket, new PartitionKey(ticket.Id));
        }

        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<TicketEntry>(id, new PartitionKey(id));
        }

        public async Task<IEnumerable<TicketEntry>> GetAllAsync()
        {
            var query = _container.GetItemQueryIterator<TicketEntry>("SELECT * FROM c");
            List<TicketEntry> results = new();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<TicketEntry> GetByIdAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<TicketEntry>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch
            {
                return null;
            }
        }

        public async Task UpdateAsync(string id, TicketEntry ticket)
        {
            await _container.UpsertItemAsync(ticket, new PartitionKey(id));
        }
    }
}
