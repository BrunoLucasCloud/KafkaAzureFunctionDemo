using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using Microsoft.Extensions.Logging;

namespace KafkaAzureFunctionDemo
{
    public class ReceiveMessage
    {        
        [FunctionName("ReceiveMessage")]
        public void Run(
            [KafkaTrigger("Cluster",
                          "Topic",
                          Username = "key",
                          Password = "ConnectionString",
                          Protocol = BrokerProtocol.SaslSsl,
                          AuthenticationMode = BrokerAuthenticationMode.Plain,
                          ConsumerGroup = "$Default")] KafkaEventData<string>[] events,
            ILogger log)
        {
            log.LogInformation("hi");

            foreach (KafkaEventData<string> eventData in events)
            {
                log.LogInformation($"C# Kafka trigger function processed a message: {eventData.Value}");
            }
        }
    }
}
