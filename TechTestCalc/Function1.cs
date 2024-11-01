using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace TechTestCalc;

public static class Function1
{
    /// POST the following JSON to /api/calc
    /// {"expression": "5 * 2 + 10 - 3"}
    /// We expect a JSON response with the result: (optional with steps)
    /// {
    ///  "result": "17"
    /// }

    [FunctionName("Function1")]
    public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "calc")] HttpRequest req)
    {
        throw new NotImplementedException();        
    }
}
