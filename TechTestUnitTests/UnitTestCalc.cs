using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TechTestCalc;
using Xunit;

namespace TechTestUnitTests;

public class UnitTestCalc
{
    [Fact]
    public async Task TestCalc()
    {
        var expression = "5 * 2 + 10 - 3";
        var correctResult = 17;

        var memStream = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new { Expression = expression })));

        var mockRequest = new Moq.Mock<HttpRequest>();
        mockRequest.SetupGet(p => p.Body).Returns(memStream);

        var functionResponse = await Function1.Run(mockRequest.Object);

        JObject calcResponse;
        if (functionResponse is OkObjectResult objectResult)
        {
            calcResponse = JObject.FromObject(objectResult.Value);
        }
        else
        {
            var jsonResult = functionResponse as JsonResult;
            Assert.NotNull(jsonResult);
            calcResponse = JObject.FromObject(jsonResult.Value);
        }

        Assert.NotNull(calcResponse);
        var r = calcResponse["Result"];
        r ??= calcResponse["result"];
        Assert.NotNull(r);
        Assert.True(r!.Value<int>() == correctResult);
    }
}