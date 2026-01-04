using GestaoLoja.Data;
using System.Text.Json.Serialization;

namespace GestaoLoja.Entities;

public class Favorito
{
    public int Id { get; set; }
    public string ClienteId { get; set; } = null!;
    // [JsonIgnore]
    // public ApplicationUser Cliente { get; set; } = null!;

    public int ProdutoId { get; set; }
    [JsonIgnore]
    public Produto Produto { get; set; } = null!;
}
