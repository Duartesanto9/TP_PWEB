using RESTfulAPIPWeb.Data; // Ajustado para o namespace da API
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RESTfulAPIPWeb.Entities; // Ajustado para o namespace da API

public class CarrinhoCompras
{
    public int Id { get; set; }

    [Range(0, Double.MaxValue, ErrorMessage = "A Quantidade não pode ser negativa")]
    public double Quantidade { get; set; }

    // Relacionamento com Produto
    public int ProdutoId { get; set; }
    [JsonIgnore]
    public Produto Produto { get; set; } = null!;

    // Relacionamento com Cliente
    public string ClienteId { get; set; } = null!;
}