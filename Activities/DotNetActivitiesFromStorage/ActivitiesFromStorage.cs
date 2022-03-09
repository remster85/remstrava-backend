

using System;
using System.Threading.Tasks;
using System.Text;
using QuickType;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace DotNetActivitiesFromStorage
{
    public static class ActivitiesFromStorage
    {
        [FunctionName("ActivitiesFromStorage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log, Microsoft.Azure.WebJobs.ExecutionContext context)
        {

            var config = new ConfigurationBuilder()
             .SetBasePath(context.FunctionAppDirectory)
             .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables()
             .Build();

            var stravaStoragePassword = config["StravaStoragePassword"];

            log.LogInformation(stravaStoragePassword);

            var activitiesBuilder = new StringBuilder();

            CloudStorageAccount storageAccount = new CloudStorageAccount(
               new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials("remsstravaactivities", stravaStoragePassword), true);

            // Create a blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get a reference to a container named "mycontainer."
            CloudBlobContainer container = blobClient.GetContainerReference("activities");

            BlobContinuationToken token = null;
            do
            {
                BlobResultSegment resultSegment = await container.ListBlobsSegmentedAsync(token);
                token = resultSegment.ContinuationToken;

                foreach (IListBlobItem item in resultSegment.Results)
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blob = (CloudBlockBlob)item;
                        Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);
                        var blobContent = await blob.DownloadTextAsync();
                        activitiesBuilder.Append(blobContent.Substring(2, blobContent.Length - 3) + ",");

                    }

                    else if (item.GetType() == typeof(CloudPageBlob))
                    {
                        CloudPageBlob pageBlob = (CloudPageBlob)item;

                        Console.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);
                    }

                    else if (item.GetType() == typeof(CloudBlobDirectory))
                    {
                        CloudBlobDirectory directory = (CloudBlobDirectory)item;

                        Console.WriteLine("Directory: {0}", directory.Uri);
                    }
                }
            } while (token != null);


            var jsonString = "[" + activitiesBuilder.ToString().Substring(0, activitiesBuilder.ToString().Length - 2) + "}]";

            var activities = JsonConvert.DeserializeObject<List<RunModel>>(jsonString);

            var jsonOutput = JsonConvert.SerializeObject(activities.Select(x => new
            {
                id = x.Id,
                start_date_local = x.StartDateLocal,
                distance = x.Distance,
                type = x.Type,
                workoutType = x.WorkoutType,
                name = x.Name,
                movingTime = x.MovingTime
            }));

            return new OkObjectResult(jsonOutput);

        }
    }
}