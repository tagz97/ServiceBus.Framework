namespace ServiceBus.Framework.Response
{
    /// <summary>
    /// Service bus response for sending a message or batch message to queue or topic
    /// </summary>
    public class ServiceBusResponse
    {
        /// <summary>
        /// Create response from parameters
        /// </summary>
        /// <param name="success"><see langword="true"/> if successful or <see langword="false"/> if failed</param>
        /// <param name="error">Error message if <see cref="Success"/> is <see langword="false"/></param>
        public ServiceBusResponse(bool success, string error = null)
        {
            Success = success;
            Error = error;
        }

        /// <summary>
        /// <see langword="true"/> if successful or <see langword="false"/> if failed
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Error message if <see cref="Success"/> is <see langword="false"/>
        /// </summary>
        public string Error { get; private set; }
    }
}
