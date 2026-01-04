using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RESTfulAPIPWeb.Entities;

public class ModoEntrega
{
    public int Id { get; set; } // Com "Id" a base de dados sabe que isto é a chave principal

    [StringLength(100)] // Tamanho máximo da variável "Nome"
    [Required] // "Nome" é obrigatório
    public string? Nome { get; set; }

    [StringLength(200)] // Tamanho máximo da variável "Detalhe"
    public string? Detalhe { get; set; }

    [JsonIgnore] // Variavel não é serializada/deserializada
    public ICollection<Produto>? produtos { get; set; }
}
