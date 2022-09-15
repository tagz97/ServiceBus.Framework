# ServiceBus.Framework
A service bus framework NuGet package for sending messages to Azure Service Bus.

The service bus service can be added by using the following in your `Startup.cs` or anywhere you perform your DI:
```CSharp
string connectionString = "REPLACE_ME"; // this is the connection string to your Azure Service Bus
string queueOrTopicName = "REPLACE_ME"; // This is the queue or topic name you want to publish messages to
bool enabled = true; // Set this to false if you do not want the service bus initialized in an environment. This will not break the implementation, it will simply not send the message to a queue/topic as the queue/topic is not initialized

builder.Services.AddServiceBusSenderService(connectionString, queueOrTopicName, enabled);
```
To consume, you can use the following in your program:
```CSharp
// Injected service to be consumed
private readonly IServiceBusService _serviceBusService;

// Constructor to get the injected service
public ClassName(IServiceBusService serviceBusService)
{
    _serviceBusService = serviceBusService;
}

// To send a single message
public async Task<bool> SendMessage(Class item)
{
    return await _serviceBusService.QueueMessageAsync<Class>(item);
}

// To send a message batch
public async Task<bool> SendMessages(List<Class> items)
{
     return await _serviceBusService.QueueBatchMessageAsync<Class>(items);
}
```
# Under investigation
- Add a listener for a queue or topic with subscription
