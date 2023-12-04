using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using ServiceBus.Framework.Implementation;
using ServiceBus.Framework.Interfaces;

namespace ServiceBus.Framework.ServiceExtension
{
    public static class ServiceBusCollectionExtensions
    {
        /// <summary>
        /// Injects <see cref="IServiceBusService"/> service into <paramref name="services"/>
        /// </summary>
        /// <param name="connectionString">Connection string to the Azure Service Bus</param>
        /// <param name="queueOrTopicName">Azure service bus queue or topic name</param>
        /// <param name="enabled">Creates sender if <see langword="true"/></param>
        /// <returns>Provided <paramref name="services"/> collection</returns>
        /// <exception cref="ArgumentException"></exception>
        public static IServiceCollection AddServiceBusSenderService(this IServiceCollection services, string connectionString, string queueOrTopicName, bool enabled)
        {
            if (enabled)
            {
                ValidateInput(connectionString, queueOrTopicName);
            }

            services.AddServiceBusClient(connectionString, queueOrTopicName, enabled);
            services.AddSingleton<IServiceBusService, ServiceBusService>();

            return services;
        }

        /// <summary>
        /// Validate user input to ensure values are provided for <paramref name="connectionString"/> and <paramref name="queueOrTopicName"/>
        /// </summary>
        /// <param name="connectionString">The connection <see cref="string"/> for the Service Bus</param>
        /// <param name="queueOrTopicName">The queue or topic name <see cref="string"/> for the Service Bus</param>
        /// <exception cref="ArgumentException"><see cref="ArgumentException"/> when <paramref name="connectionString"/> or <paramref name="queueOrTopicName"/> is null or empty</exception>
        private static void ValidateInput(string connectionString, string queueOrTopicName)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Please specify a valid connection string in your application configuration");
            }

            if (string.IsNullOrEmpty(queueOrTopicName))
            {
                throw new ArgumentException("Please specify a valid queue or topic name in your application configuration");
            }
        }

        /// <summary>
        /// Injects <see cref="IServiceBusSenderClient"/> service into <paramref name="services"/>
        /// </summary>
        /// <param name="connectionString">Connection string to the Azure Service Bus</param>
        /// <param name="queueOrTopicName">Azure service bus queue or topic name</param>
        /// <param name="enabled">Creates sender if <see langword="true"/></param>
        /// <returns>Provided <paramref name="services"/> collection</returns>
        private static IServiceCollection AddServiceBusClient(this IServiceCollection services, string connectionString, string queueOrTopicName, bool enabled)
        {
            services.AddSingleton((s) =>
            {
                ServiceBusSender sender = null;
                if (enabled)
                {
                    ServiceBusClient client = new(connectionString);
                    sender = client.CreateSender(queueOrTopicName);
                }

                IServiceBusSenderClient serviceBusClient = new Implementation.ServiceBusSenderClient(sender);

                return serviceBusClient;
            });

            return services;
        }
    }
}