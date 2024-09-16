using ConnectData.Api.Data.Contexts;
using ConnectData.Api.Models;
using ConnectData.Api.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Data.Entity;
using System.Text.Json;

namespace ConnectData.Api.Controllers;

[ApiController]
[Route("/v1/[controller]")]
public class FibraController(ConnectDataContext context, ILogger<ClienteController> logger) : ControllerBase
{

    private readonly ConnectDataContext _context = context;

    private readonly ILogger<ClienteController> _logger = logger;

    [HttpGet]
    public IActionResult GetAllClientes()
    {
        _logger.LogInformation("GET | /Cliente | Iniciando | Timestamp: {}", DateTime.UtcNow);
        var result = _context.Fibras.ToList();
        if (result != null)
        {
            foreach(var item in result)
            {
                item.Cliente = _context.Clientes.FirstOrDefault(p => item.ClienteId == item.ClienteId);
            }
        }
        _logger.LogInformation("GET | /Cliente | Finalizado | Timestamp: {} | Success: true | Reponse: {}", DateTime.UtcNow, JsonSerializer.Serialize(result));
        return Ok(new { success = true, response = result });
    }

    [HttpGet("{id:int}")]
    public IActionResult GetFibraById(int id)
    {
        _logger.LogInformation("GET | /Cliente/{} | Iniciando | Timestamp: {}", id, DateTime.UtcNow);
        var result = _context.Fibras.FirstOrDefault(p => p.Id == id);
        if (result != null)
        {
            result.Cliente = _context.Clientes.FirstOrDefault(p => p.ClienteId == result.ClienteId);
        }
        _logger.LogInformation("GET | /Cliente | Finalizado | Timestamp: {} | Success: true | Reponse: {}", DateTime.UtcNow, JsonSerializer.Serialize(result));
        return Ok(new { success = true, response = result });
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteFibra(int id)
    {
        _logger.LogInformation("DELETE | /Cliente/{} | Iniciando | Timestamp: {}", id, DateTime.UtcNow);
        var model = _context.Clientes.Find(id);
        if (model == null)
        {
            return NotFound(new { success = false, message = "Entidade requisitada não encontrada, por favor tente novamente." });
        }
        _context.Clientes.Remove(model);
        _context.SaveChanges();
        _logger.LogInformation("DELETE | /Cliente/{} | Finalizado | Timestamp: {} | Response: 204 NoContent", id, DateTime.UtcNow);
        return Ok(new { sucess = true });
    }
}
