using System;

namespace ConnectData.Api.Models;

public class Fibra
{
    public string Tipo { get; set; }

    public string Velocidade { get; set; }

    public string Plano { get; set; }

    public int ClienteId { get; set; }

    public virtual Cliente Cliente { get; set; }
}
