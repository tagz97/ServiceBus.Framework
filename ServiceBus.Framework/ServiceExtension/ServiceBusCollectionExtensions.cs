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
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ArgumentException("Please specify a valid connection string in your application configuration");
                }
                if (string.IsNullOrEmpty(queueOrTopicName))
                {
                    throw new ArgumentException("Please specify a valid queue or topic name in your application configuration");
                }
            }

            AddServiceBusClient(services, connectionString, queueOrTopicName, enabled);
            services.AddSingleton<IServiceBusService, ServiceBusService>();

            return services;
        }

        /// <summary>
        /// Injects <see cref="IServiceBusClient"/> service into <paramref name="services"/>
        /// </summary>
        /// <param name="connectionString">Connection string to the Azure Service Bus</param>
        /// <param name="queueOrTopicName">Azure service bus queue or topic name</param>
        /// <param name="enabled">Creates sender if <see langword="true"/></param>
        /// <returns>Provided <paramref name="services"/> collection</returns>
        private static IServiceCollection AddServiceBusClient
            (this IServiceCollection services, string connectionString, string queueOrTopicName, bool enabled)
        {
            services.AddSingleton((s) =>
            {
                ServiceBusSender sender = null;
                if (enabled)
                {
                    Azure.Messaging.ServiceBus.ServiceBusClient client = new(connectionString);
                    sender = client.CreateSender(queueOrTopicName);
                }

                IServiceBusClient serviceBusClient = new Implementation.ServiceBusClient(sender);

                return serviceBusClient;
            });

            return services;
        }
    }
}
