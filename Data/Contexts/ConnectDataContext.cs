using ConnectData.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ConnectData.Api.Data.Contexts;

public class ConnectDataContext(DbContextOptions<ConnectDataContext> options) : DbContext(options)
{

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Fibra> Fibras { get; set; }
}
