namespace ServiceBus.Framework.Response
{
    /// <summary>
    /// Service bus response for sending a message or batch message to queue or topic
    /// </summary>
    public class ServiceBusResponse
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ServiceBusResponse()
        {
            Success = false;
        }

        /// <summary>
        /// Create response from parameters
        /// </summary>
        /// <param name="success"><see langword="true"/> if successful or <see langword="false"/> if failed</param>
        public ServiceBusResponse(bool success)
        {
            Success = success;
        }

        /// <summary>
        /// Create response from parameters
        /// </summary>
        /// <param name="success"><see langword="true"/> if successful or <see langword="false"/> if failed</param>
        /// <param name="message">Error message if <see cref="Success"/> is <see langword="false"/></param>
        public ServiceBusResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        /// <summary>
        /// <see langword="true"/> if successful or <see langword="false"/> if failed
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Error message if <see cref="Success"/> is <see langword="false"/>
        /// </summary>
        public string Message { get; set; }
    }
}
