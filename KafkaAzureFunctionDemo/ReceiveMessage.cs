using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using Microsoft.Extensions.Logging;

namespace KafkaAzureFunctionDemo
{
    public class ReceiveMessage
    {        
        [FunctionName("ReceiveMessage")]
        public void Run(
            [KafkaTrigger("consumer_cluster",
                          "consumer_topic",
                          Username = "consumer_key",
                          Password = "consumer_connectionString",
                          Protocol = BrokerProtocol.SaslSsl,
                          AuthenticationMode = BrokerAuthenticationMode.Plain,
                          ConsumerGroup = "$Default")] KafkaEventData<string>[] events,
            ILogger log)
        {            

            foreach (KafkaEventData<string> eventData in events)
            {
                log.LogInformation($"C# Kafka trigger function processed a message: {eventData.Value}");
            }
        }
    }
}
