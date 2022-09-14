namespace ServiceBus.Framework.Interfaces
{
    /// <summary>
    /// ServiceBusService for public consumption of sending messages
    /// </summary>
    public interface IServiceBusService
    {
        /// <summary>
        /// Queue a message to send to the service bus
        /// </summary>
        /// <param name="entity">The object to send as a message</param>
        /// <returns><see langword="true"/> if messages queued successfully. <see langword="false"/> if messages failed to be queued</returns>
        Task<bool> QueueMessageAsync<T>(T entity);
        /// <summary>
        /// Queue a batch message to send to the service bus
        /// </summary>
        /// <param name="entities"><see cref="IEnumerable{T}"/> of objects to send as messages</param>
        /// <returns><see langword="true"/> if messages queued successfully. <see langword="false"/> if messages failed to be queued</returns>
        Task<bool> QueueBatchMessageAsync<T>(IEnumerable<T> entities);
    }
}
