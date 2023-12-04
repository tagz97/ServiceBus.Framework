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
        private readonly IServiceBusSenderClient _serviceBusClient;
        private readonly ILogger<ServiceBusService> _logger;

        /// <summary>
        /// Default constructor to initialise the Service
        /// </summary>
        /// <param name="serviceBusClient"><see cref="IServiceBusSenderClient"/> from DI container</param>
        /// <param name="logger"><see cref="Ilogger"/> from DI container</param>
        public ServiceBusService(IServiceBusSenderClient serviceBusClient, ILogger<ServiceBusService> logger)
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
                _logger.LogError("{Class} {Method}: Unable to queue batch messages. Error Message:\n{Message}", nameof(ServiceBusService), nameof(QueueBatchMessageAsync), resp.Message);
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
                _logger.LogError("{Class} {Method}: Unable to queue message. Error Message:\n{Message}", nameof(ServiceBusService), nameof(QueueBatchMessageAsync), resp.Message);

                return false;
            }

            return true;
        }
    }
}