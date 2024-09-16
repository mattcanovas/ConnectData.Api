using ConnectData.Api.Data.Contexts;
using ConnectData.Api.Models;
using ConnectData.Api.Resources;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace ConnectData.Api.Services
{
    public class FibraService
    {
        private readonly ConnectDataContext _context;
        private readonly ILogger<FibraService> _logger;

        public FibraService(ConnectDataContext context, ILogger<FibraService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Fibra> GetAllFibras()
        {
            _logger.LogInformation("GET | /Fibra | Iniciando | Timestamp: {Timestamp}", DateTime.UtcNow);
            var result = _context.Fibras.ToList();
            if (result != null)
            {
                foreach (var item in result)
                {
                    item.Cliente = _context.Clientes.FirstOrDefault(p => item.ClienteId == item.ClienteId);
                }
            }
            _logger.LogInformation("GET | /Fibra | Finalizado | Timestamp: {Timestamp} | Success: true | Response: {Response}", DateTime.UtcNow, result);
            return result;
        }

        public Fibra GetFibraById(int id)
        {
            _logger.LogInformation("GET | /Fibra/{Id} | Iniciando | Timestamp: {Timestamp}", id, DateTime.UtcNow);
            var result = _context.Fibras.FirstOrDefault(p => p.Id == id);
            if (result != null)
            {
                result.Cliente = _context.Clientes.FirstOrDefault(p => p.ClienteId == result.ClienteId);
            }
            _logger.LogInformation("GET | /Fibra/{Id} | Finalizado | Timestamp: {Timestamp} | Success: true | Response: {Response}", DateTime.UtcNow, result);
            return result;
        }

        public void CreateFibra(FibraResource resource)
        {
            _logger.LogInformation("POST | /Fibra | Iniciando | Timestamp: {Timestamp} | Request: {Request}", DateTime.UtcNow, resource);
            var model = new Fibra
            {
                Tipo = resource.Tipo,
                Velocidade = resource.Velocidade,
                Plano = resource.Plano,
                ClienteId = resource.ClienteId,
            };
            _context.Fibras.Add(model);
            _context.SaveChanges();
            var responseBody = model;
            responseBody.Cliente = _context.Clientes.FirstOrDefault(c => c.ClienteId == resource.ClienteId);
            _logger.LogInformation("POST | /Fibra | Finalizado | Timestamp: {Timestamp} | Success: true | Response: {Response}", DateTime.UtcNow, responseBody);
        }

        public void DeleteFibra(int id)
        {
            _logger.LogInformation("DELETE | /Fibra/{Id} | Iniciando | Timestamp: {Timestamp}", id, DateTime.UtcNow);
            var model = _context.Fibras.Find(id);
            if (model == null)
            {
                throw new KeyNotFoundException("Entidade requisitada não encontrada, por favor tente novamente.");
            }
            _context.Fibras.Remove(model);
            _context.SaveChanges();
            _logger.LogInformation("DELETE | /Fibra/{Id} | Finalizado | Timestamp: {Timestamp} | Response: 204 NoContent", id, DateTime.UtcNow);
        }
    }
}