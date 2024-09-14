using System;
using Microsoft.AspNetCore.Mvc;

namespace ConnectData.Api.Controllers;

[ApiController]
[Route("/v1/canonical")]
public class CanonicalController : ControllerBase
{
    [HttpGet("{produto:string}/{id:int}")]
    public ActionResult GetClienteCanonicalDataFromProduct(string produto, int id)
    {
        return Ok();
    }
}
