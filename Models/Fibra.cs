using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnectData.Api.Models;

public class Fibra
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Tipo { get; set; }

    [Required]
    public string Velocidade { get; set; }

    [Required]
    public string Plano { get; set; }

    [Required]
    public int ClienteId { get; set; }

    public Cliente Cliente { get; set; }
}
