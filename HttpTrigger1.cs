using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

public static class ReadBlobFunction
{
    [FunctionName("ReadBlob")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
        [Blob("ct1/Busan.json", FileAccess.Read, Connection = "AzureWebJobsStorage")] Stream blobStream,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        if (blobStream == null)
        {
            return new NotFoundResult();
        }

        using (StreamReader reader = new StreamReader(blobStream))
        {
            string blobContent = await reader.ReadToEndAsync();
            return new OkObjectResult(blobContent);
        }
    }
}