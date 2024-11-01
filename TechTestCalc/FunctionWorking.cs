using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace TechTestCalc;

public static class FunctionWorking
{
    [FunctionName("FunctionWorking")]
    public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "calcworking")] HttpRequest req, ILogger log)
    {

        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var data = JsonConvert.DeserializeObject<CalcRequestWorking>(requestBody);

        return new OkObjectResult(new CalcResponseWorking { Result = (int)new NCalc.Expression(data.Expression).Evaluate() });
    }
}

public class CalcRequestWorking
{
    public string Expression { get; set; }
}

public class CalcResponseWorking
{
    public int Result { get; set; }
}
