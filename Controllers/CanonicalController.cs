using System;
using ConnectData.Api.Data.Contexts;
using ConnectData.Api.Data.Extensionsl;
using Microsoft.AspNetCore.Mvc;

namespace ConnectData.Api.Controllers;

[ApiController]
[Route("/v1/[controller]")]
public class CanonicalController(ConnectDataContext context, ILogger<CanonicalController> _logger) : ControllerBase
{

    private readonly ConnectDataContext _context = context;

    private readonly ILogger<CanonicalController> _logger;

    [HttpGet("{produto:string}/{id:int}")]
    public ActionResult GetClienteCanonicalDataFromProduct(string produto, int id)
    {
        try
        {
            _logger.LogInformation("GET | /v1/Canonical | Iniciando | Timestamp: {} | Produto: {} | IdCliente: {}", DateTime.UtcNow, produto, id);
            var context = DbContextExtensions.GetDbSet(_context, produto);
            var result = context.Find(id);
            _logger.LogInformation("GET | /v1/Canonical | Finalizado | Timestamp: {} | Produto: {} | IdCliente: {}", DateTime.UtcNow, produto, id);
            return Ok(new { success = false, response = result });
        } catch(Exception ex)
        {
            _logger.LogInformation("GET | /v1/Canonical | Finalizado | Timestamp: {} | Error: BadRequest", DateTime.UtcNow);
            return BadRequest(new { success = false, message = ex.Message });
        }
    }
}
