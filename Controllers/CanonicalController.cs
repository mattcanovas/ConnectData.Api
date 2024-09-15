using System;
using ConnectData.Api.Data.Contexts;
using ConnectData.Api.Data.Extensionsl;
using Microsoft.AspNetCore.Mvc;

namespace ConnectData.Api.Controllers;

[ApiController]
[Route("/v1/canonical")]
public class CanonicalController(ConnectDataContext context) : ControllerBase
{

    private readonly ConnectDataContext _context = context;


    [HttpGet("{produto:string}/{id:int}")]
    public ActionResult GetClienteCanonicalDataFromProduct(string produto, int id)
    {
        try
        {
            var context = DbContextExtensions.GetDbSet(_context, produto);
            var result = context.Find(id);
            return Ok(result);
        } catch(Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }
}
