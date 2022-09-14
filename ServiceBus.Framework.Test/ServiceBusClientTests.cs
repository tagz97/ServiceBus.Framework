namespace ServiceBus.Framework.Test
{
    public class ServiceBusClientTests
    {
        private readonly Mock<ServiceBusSender> _serviceBusSenderMock = new();
        private readonly ContentClass _content = new()
        {
            Id = "1"
        };

        [Fact]
        public async void SendMessageAsync_ServiceBusSenderIsNull_ReturnsSuccessResponseAndSenderIsNotCalled()
        {
            // Arrange
            var client = new Implementation.ServiceBusClient(null);

            // Act
            var result = await client.SendMessageAsync(new Event<ContentClass>(_content));

            // Assert
            Assert.True(result.Success);
            Assert.Equal(0, _serviceBusSenderMock.Invocations.Count);
        }

        [Fact]
        public async void SendMessageAsync_ServiceBusSenderIsNotNull_ReturnsSuccessResponseAndSenderIsCalled()
        {
            // Arrange
            var client = new Implementation.ServiceBusClient(_serviceBusSenderMock.Object);

            // Act
            var response = await client.SendMessageAsync(new Event<ContentClass>(_content));

            // Assert
            Assert.True(response.Success);
            Assert.Contains("SendMessageAsync", _serviceBusSenderMock.Invocations.First().ToString());
        }

        [Fact]
        public async void SendBatchMessageAsync_ServiceBusSenderIsNull_ReturnsSuccessResponseAndSenderIsNotCalled()
        {
            // Arrange
            var client = new Implementation.ServiceBusClient(null);

            // Act
            var result = await client.SendBatchMessageAsync(new List<Event<ContentClass>>());

            // Assert
            Assert.True(result.Success);
            Assert.Equal(0, _serviceBusSenderMock.Invocations.Count);
        }

        [Fact]
        public async void SendBatchMessageAsync_ServiceBusSenderIsNotNull_ReturnsSuccessResponseAndSenderIsCalled()
        {
            // Arrange
            var client = new Implementation.ServiceBusClient(_serviceBusSenderMock.Object);

            // Act
            var response = await client.SendMessageAsync(new Event<ContentClass>(_content));

            // Assert
            Assert.True(response.Success);
            Assert.Contains("SendMessageAsync", _serviceBusSenderMock.Invocations.First().ToString());
        }
    }
}