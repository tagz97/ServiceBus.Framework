using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using ServiceBus.Framework.Interfaces;
using ServiceBus.Framework.Models;
using ServiceBus.Framework.Response;

namespace ServiceBus.Framework.Implementation
{
    /// <summary>
    /// Service bus client that sends messages to a queue or topic
    /// </summary>
    public class ServiceBusClient : IServiceBusClient
    {
        private readonly ServiceBusSender _serviceBusSender;

        public ServiceBusClient(ServiceBusSender serviceBusSender)
        {
            _serviceBusSender = serviceBusSender;
        }

        /// <inheritdoc/>
        public async Task<ServiceBusResponse> SendBatchMessageAsync<T>(IEnumerable<Event<T>> @event)
        {
            if (_serviceBusSender != null)
            {
                try
                {
                    using ServiceBusMessageBatch messageBatch = await _serviceBusSender.CreateMessageBatchAsync();
                    foreach (var eventItem in @event)
                    {
                        if(!messageBatch.TryAddMessage(new ServiceBusMessage(JsonConvert.SerializeObject(eventItem))))
                        {
                            throw new Exception($"The message {eventItem.Message} could not be added to the message batch");
                        }
                    }
                    await _serviceBusSender.SendMessagesAsync(messageBatch);
                }
                catch (Exception ex)
                {
                    return new ServiceBusResponse(false, ex.Message);
                }
            }

            return new ServiceBusResponse(true);
        }

        /// <inheritdoc/>
        public async Task<ServiceBusResponse> SendMessageAsync<T>(Event<T> @event)
        {
            if (_serviceBusSender != null)
            {
                try
                {
                    ServiceBusMessage message = new(JsonConvert.SerializeObject(@event));
                    await _serviceBusSender.SendMessageAsync(message);
                }
                catch (Exception ex)
                {
                    return new ServiceBusResponse(false, ex.Message);
                }
            }

            return new ServiceBusResponse(true);
        }
    }
}
