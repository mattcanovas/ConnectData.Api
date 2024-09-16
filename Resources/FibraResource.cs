using System.Security.Permissions;

namespace ConnectData.Api.Resources;

public class FibraResource
{
    public string Tipo { get; set; }
    
    public string Velocidade { get; set; }
    
    public string Plano { get; set; }
    
    public int ClienteId { get; set; }
}
