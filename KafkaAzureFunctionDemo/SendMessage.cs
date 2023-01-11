using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using Microsoft.Extensions.Logging;
using System.IO;

namespace KafkaAzureFunctionDemo
{
    public class SendMessage
    {      
        [FunctionName("SendMessage")]
        public IActionResult Output(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            [Kafka("producer_cluster",
                   "producer_topic",
                   Username = "producer_key",
                   Password = "producer_connectionString",
                   Protocol = BrokerProtocol.SaslSsl,
                   AuthenticationMode = BrokerAuthenticationMode.Plain
            )] out string eventData,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string message = new StreamReader(req.Body).ReadToEnd();
            
            string responseMessage = string.IsNullOrEmpty(message)
                ? "This HTTP triggered function executed successfully. Pass a message in the query string"
                : $"Message {message} sent to the broker. This HTTP triggered function executed successfully.";
            eventData = $"Received message: {message}";

            return new OkObjectResult(responseMessage);
        }
    }
}
