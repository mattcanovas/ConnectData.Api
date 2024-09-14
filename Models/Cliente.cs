using System.ComponentModel.DataAnnotations;

namespace ConnectData.Api.Models;

public class Cliente
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Nome { get;set; }

    [Required]
    public string Cpf { get; set; }

    [Required]
    public string Telefone { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Endereco { get; set; }

    [Required]
    public DateOnly DataCadastro { get; set; }

}
