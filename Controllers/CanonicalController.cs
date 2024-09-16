using System;
using ConnectData.Api.Data.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace ConnectData.Api.Controllers;

[ApiController]
[Route("/v1/[controller]")]
public class CanonicalController(ConnectDataContext context, ILogger<CanonicalController> _logger) : ControllerBase
{

    private readonly ConnectDataContext _context = context;

    private readonly ILogger<CanonicalController> _logger = _logger;

    [HttpGet("{produto}/{id:int}")]
    public ActionResult GetClienteCanonicalDataFromProduct(string produto, int id)
    {
        try
        {
            _logger.LogInformation("GET | /v1/Canonical | Iniciando | Timestamp: {} | Produto: {} | IdCliente: {}", DateTime.UtcNow, produto, id);
            switch (produto.ToLower())
            {
                case "fibra":
                    var result = _context.Fibras.FirstOrDefault(c => c.ClienteId == id);
                    result.Cliente = _context.Clientes.FirstOrDefault(c => c.ClienteId == id);
                    _logger.LogInformation("GET | /v1/Canonical | Finalizado | Timestamp: {} | Produto: {} | IdCliente: {}", DateTime.UtcNow, produto, id);
                    return Ok(new { success = true, response = result });
                default:
                    throw new KeyNotFoundException("Entidade não encontrada no context atual.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation("GET | /v1/Canonical | Finalizado | Timestamp: {} | Error: BadRequest", DateTime.UtcNow);
            return BadRequest(new { success = false, message = ex.Message });
        }
    }
}
