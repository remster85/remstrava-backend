using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GetStravaMileage
{
    public static class Function1
    {
        [FunctionName("GetStravaMileage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string timex = req.Query["timex"];


            var request = WebRequest.Create("https://remstravaactivities.azurewebsites.net/api/ActivitiesFromStorage");
            var response = request.GetResponse();

            // Get the stream containing content returned by the server. 
            // The using block ensures the stream is automatically closed. 
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.  
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.  
                string responseFromServer = reader.ReadToEnd();
                // Display the content.  
                Console.WriteLine(responseFromServer);

                JArray jarray = JArray.Parse(responseFromServer);
                //2019-06

                foreach (var jToken in jarray)
                {
                    var start_date_local = ((DateTime)((JValue)((JObject)jToken).GetValue("start_date_local")).Value).ToString("yyyy-MM-dd"); ;
                    var distance = ((JValue)((JObject)jToken).GetValue("distance")).Value.ToString();
                    var distanceLong = double.Parse(distance);
                }

                var totalDistance = jarray.Where(x => ((DateTime)((JValue)((JObject)x).GetValue("start_date_local")).Value).ToString("yyyy-MM-dd").StartsWith(timex)).Sum(x => double.Parse(((JValue)((JObject)x).GetValue("distance")).Value.ToString()));

                return timex != null
                    ? (ActionResult)new OkObjectResult($"{totalDistance}")
                    : new BadRequestObjectResult("Please pass a timex on the query string ?timex=");
            }


        }
    }
}
