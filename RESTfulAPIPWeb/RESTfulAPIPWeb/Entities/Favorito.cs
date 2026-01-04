using RESTfulAPIPWeb.Data;
using System.Text.Json.Serialization;

namespace RESTfulAPIPWeb.Entities;

public class Favorito
{
    public int Id { get; set; }

    // CORREÇÃO AQUI: Tem de ser string, não int
    public string ClienteId { get; set; } = null!;

    public int ProdutoId { get; set; }

    [JsonIgnore]
    public Produto Produto { get; set; } = null!;
}