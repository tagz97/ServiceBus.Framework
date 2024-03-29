﻿using ServiceBus.Framework.Response;

namespace ServiceBus.Framework.Interfaces
{
    /// <summary>
    /// Interface for sending messages to Service Bus
    /// </summary>
    public interface IServiceBusService
    {
        /// <summary>
        /// Queue a message to send to the service bus
        /// </summary>
        /// <param name="entity">The object to send as a message</param>
        /// <returns><see cref="ServiceBusResponse"/> which contains status <see cref="ServiceBusResponse.Success"/> <see langword="true"/> if message queued successfully. <see langword="false"/> if message failed to be queued</returns>
        Task<ServiceBusResponse> QueueMessageAsync<T>(T entity);

        /// <summary>
        /// Queue a batch message to send to the service bus
        /// </summary>
        /// <param name="entities"><see cref="IEnumerable{T}"/> of objects to send as messages</param>
        /// <returns><see cref="ServiceBusResponse"/> which contains status <see cref="ServiceBusResponse.Success"/> <see langword="true"/> if messages queued successfully. <see langword="false"/> if messages failed to be queued</returns>
        Task<ServiceBusResponse> QueueBatchMessageAsync<T>(IEnumerable<T> entities);
    }
}
