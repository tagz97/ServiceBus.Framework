﻿using Newtonsoft.Json;

namespace ServiceBus.Framework.Models
{
    /// <summary>
    /// Event class for creating new events for processing
    /// </summary>
    /// <typeparam name="T">Type of data to be sent</typeparam>
    public class Event<T>
    {
        private DateTimeOffset _eventCreated;
        private string _message;

        /// <summary>
        /// CTOR to initialise a new event from <see cref="T"/>
        /// </summary>
        /// <param name="data">Data to serialize for the message</param>
        public Event(T data)
        {
            _eventCreated = DateTimeOffset.UtcNow;
            Message = JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Get the DateTimeOffset that the event was created at
        /// </summary>
        [JsonProperty("eventCreated")]
        public DateTimeOffset EventCreatedDateTimeOffset
        {
            get { return _eventCreated; }
        }

        /// <summary>
        /// The message to be added to the queue
        /// </summary>
        [JsonProperty("message")]
        public string Message { 
            get { return JsonConvert.DeserializeObject(_message).ToString(); }
            set { _message = value; }
        }
    }
}
