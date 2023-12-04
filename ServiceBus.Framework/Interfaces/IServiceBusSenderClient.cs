using ServiceBus.Framework.Models;
using ServiceBus.Framework.Response;

namespace ServiceBus.Framework.Interfaces
{
    /// <summary>
    /// Abstraction of the ServiceBusClient for sending messages
    /// </summary>
    public interface IServiceBusSenderClient
    {
        /// <summary>
        /// Send a message to a queue or topic
        /// </summary>
        /// <typeparam name="T">Type of object for the message</typeparam>
        /// <param name="event">Event for processing <see cref="Event{T}"/></param>
        /// <returns><see cref="ServiceBusResponse"/></returns>
        Task<ServiceBusResponse> SendMessageAsync<T>(Event<T> @event);

        /// <summary>
        /// Send a batch of messages to the queue or topic
        /// </summary>
        /// <typeparam name="T">Type of object for the message</typeparam>
        /// <param name="event">IEnumerable events for processing</param>
        /// <returns><see cref="ServiceBusResponse"/></returns>
        Task<ServiceBusResponse> SendBatchMessageAsync<T>(IEnumerable<Event<T>> @event);
    }
}
