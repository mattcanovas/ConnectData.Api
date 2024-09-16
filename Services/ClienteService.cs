using ConnectData.Api.Data.Contexts;
using ConnectData.Api.Models;
using ConnectData.Api.Resources;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class ClienteService
{
    private readonly ConnectDataContext _context;
    private readonly ILogger<ClienteService> _logger;

    public ClienteService(ConnectDataContext context, ILogger<ClienteService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IEnumerable<Cliente> GetAllClientes()
    {
        _logger.LogInformation("Iniciando GetAllClientes | Timestamp: {}", DateTime.UtcNow);
        var clientes = _context.Clientes.ToList();
        _logger.LogInformation("Finalizado GetAllClientes | Timestamp: {} | Success: true", DateTime.UtcNow);
        return clientes;
    }

    public Cliente GetClienteById(int id)
    {
        _logger.LogInformation("Iniciando GetClienteById para ID: {} | Timestamp: {}", id, DateTime.UtcNow);
        var cliente = _context.Clientes.Find(id);
        _logger.LogInformation("Finalizado GetClienteById para ID: {} | Timestamp: {} | Success: true", id, DateTime.UtcNow);
        return cliente;
    }

    public void CreateClient(ClienteResource resource)
    {
        _logger.LogInformation("Iniciando CreateClient | Timestamp: {} | Request: {}", DateTime.UtcNow, JsonSerializer.Serialize(resource));
        var model = new Cliente
        {
            Cpf = resource.Cpf,
            Email = resource.Email,
            Endereco = resource.Endereco,
            Nome = resource.Nome,
            Telefone = resource.Telefone,
            DataCadastro = DateOnly.FromDateTime(DateTime.Now)
        };
        _context.Clientes.Add(model);
        _context.SaveChanges();
        _logger.LogInformation("Finalizado CreateClient | Timestamp: {} | Success: true | Response: {}", DateTime.UtcNow, JsonSerializer.Serialize(model));
    }

    public void DeleteCliente(int id)
    {
        _logger.LogInformation("Iniciando DeleteCliente para ID: {} | Timestamp: {}", id, DateTime.UtcNow);
        var model = _context.Clientes.Find(id);
        if (model == null)
        {
            throw new KeyNotFoundException("Entidade requisitada não encontrada.");
        }
        _context.Clientes.Remove(model);
        _context.SaveChanges();
        _logger.LogInformation("Finalizado DeleteCliente para ID: {} | Timestamp: {} | Success: true", id, DateTime.UtcNow);
    }
}
