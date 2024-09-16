using Microsoft.AspNetCore.Mvc;

namespace ConnectData.Api.Controllers;

[ApiController]
[Route("/v1/[controller]")]
public class MetricsController : ControllerBase
{

    [HttpGet]
    public IActionResult getMetrics()
    {
        return Redirect("/metrics");
    }

}
