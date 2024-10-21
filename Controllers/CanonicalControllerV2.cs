using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace ConnectData.Api.Controllers
{
    [ApiController]
    [Route("/v2/[controller]")]
    public class CanonicalControllerV2 : ControllerBase
    {

        IConfigurationRoot _configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
        
        [HttpGet("{produto:string}/{idCliente:int}")]
        public ActionResult GetCanonicalData(string produto, int idCliente)
        {
            using var _conn = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection!"));
            var sql = @"SELECT * FROM @produto WHERE @nomeProduto.ClienteId = @id INNER JOIN Cliente ON @produto.ClienteId = Cliente.ClienteId;";
            var result = _conn.QueryFirstOrDefaultAsync<object>(sql, new { nomeProduto = produto.ToLower(), id = idCliente });
            return Ok(new { success = true, response = new JsonResult(result) });
        }

    }
}
