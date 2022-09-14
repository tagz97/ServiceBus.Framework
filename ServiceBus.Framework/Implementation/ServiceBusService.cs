using Microsoft.Extensions.Logging;
using ServiceBus.Framework.Interfaces;
using ServiceBus.Framework.Models;

namespace ServiceBus.Framework.Implementation
{
    /// <summary>
    /// Service for sending messages to service bus using a client
    /// </summary>
    public class ServiceBusService : IServiceBusService
    {
        private readonly IServiceBusClient _serviceBusClient;
        private readonly ILogger<ServiceBusService> _logger;

        public ServiceBusService(IServiceBusClient serviceBusClient, ILogger<ServiceBusService> logger)
        {
            _serviceBusClient = serviceBusClient;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<bool> QueueBatchMessageAsync<T>(IEnumerable<T> entities)
        {
            List<Event<T>> events = new();
            foreach (var entity in entities)
            {
                events.Add(new Event<T>(entity));
            }

            var resp = await _serviceBusClient.SendBatchMessageAsync(events);
            if (!resp.Success)
            {
                _logger.LogError($"Unable to queue batch messages. Error:\n{resp.Error}");
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> QueueMessageAsync<T>(T entity)
        {
            Event<T> @event = new(entity);
            var resp = await _serviceBusClient.SendMessageAsync(@event);
            if (!resp.Success)
            {
                _logger.LogError($"Unable to queue message. Error:{resp.Error}");
                return false;
            }

            return true;
        }
    }
}
