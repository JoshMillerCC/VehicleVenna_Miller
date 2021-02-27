using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace VehicleVenna_Miller
{
    public enum VehicleTypes
    {
        Car,
        SportsCar,
        Truck,
        Motorcycle,
        MotorHome
    }
    public class vehicle
    {
        public VehicleTypes type { get; set; }
        public string buyerFirstName { get; set; }
        public string buyerLastName { get; set; }
        public string buyerEmail { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string year { get; set; }
        public string listPrice { get; set; }
    }
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [Queue("VehicleQueue")] IAsyncCollector<vehicle> vehicleAsync, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var vehicle = JsonConvert.DeserializeObject<vehicle>(requestBody);
            log.LogInformation("This is my log.");

            string responseMessage = $"Buyer {vehicle.buyerFirstName} {vehicle.buyerLastName} purchased a {vehicle.year} {vehicle.make} " +
            $"{vehicle.model} {vehicle.type} with list price of {vehicle.listPrice}. With discount applied, " +
            $"purchase price is {(Convert.ToDecimal(vehicle.listPrice) - ((Convert.ToDecimal(vehicle.listPrice)) * .085m)).ToString("C")}";

            return new OkObjectResult(responseMessage);
        }
    }
}