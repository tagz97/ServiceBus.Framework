﻿namespace ServiceBus.Framework.Test
{
    public class ServiceBusCollectionExtensionsTests
    {
        private readonly string _queueOrTopicName = "queueName";
        private readonly string _connectionString = "connectionString";
        private ServiceCollection _services;
        private bool _enabled;

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AddServiceBusService_Enabled_NoConnectionString_ThrowsArgumentException(string connectionString)
        {
            // Arrange
            _enabled = true;
            _services = new();

            // Act
            IServiceCollection action() => _services.AddServiceBusSenderService(connectionString, _queueOrTopicName, _enabled);

            // Assert
            Assert.Throws<ArgumentException>((Func<IServiceCollection>)action);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AddServiceBusService_Enabled_NoQueueOrTopicName_ThrowsArgumentException(string queueOrTopicName)
        {
            // Arrange
            _enabled = true;
            _services = new();

            // Act
            IServiceCollection action() => _services.AddServiceBusSenderService(_connectionString, queueOrTopicName, _enabled);

            // Assert
            Assert.Throws<ArgumentException>((Func<IServiceCollection>)action);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AddServiceBusService_Disabled_NoConnectionString_NoExceptionsIsThrown(string connectionString)
        {
            // Arrange
            _enabled = false;
            _services = new();

            // Act
            var exception = Record.Exception(() => _services.AddServiceBusSenderService(connectionString, _queueOrTopicName, _enabled));

            // Assert
            Assert.Null(exception);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AddServiceBusService_Diasabled_NoQueueOrTopicName_NoExceptionIsThrown(string queueOrTopicName)
        {
            // Arrange
            _enabled = false;
            _services = new();

            // Act
            var exception = Record.Exception(() => _services.AddServiceBusSenderService(_connectionString, queueOrTopicName, _enabled));

            var first = _services.First();

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void AddServiceBusService_Disabled_ValidParameters_ServicesAreInjected()
        {
            // Arrange
            _enabled = false;
            _services = new();

            // Act
            _services.AddServiceBusSenderService(_connectionString, _queueOrTopicName, _enabled);

            var serviceBusClientInjected = _services.Any(x => x.ServiceType == typeof(IServiceBusSenderClient));
            var serviceBusServiceInjected = _services.Any(x => x.ServiceType == typeof(IServiceBusService));

            // Assert
            Assert.True(serviceBusClientInjected);
            Assert.True(serviceBusServiceInjected);
        }

        [Fact]
        public void AddServiceBusService_Enabled_ValidParameters_ServicesAreInjected()
        {
            // Arrange
            _enabled = true;
            _services = new();

            // Act
            _services.AddServiceBusSenderService(_connectionString, _queueOrTopicName, _enabled);

            var serviceBusClientInjected = _services.Any(x => x.ServiceType == typeof(IServiceBusSenderClient));
            var serviceBusServiceInjected = _services.Any(x => x.ServiceType == typeof(IServiceBusService));

            // Assert
            Assert.True(serviceBusClientInjected);
            Assert.True(serviceBusServiceInjected);
        }
    }
}
