using Microsoft.AspNetCore.Identity;

namespace GestaoLoja.Data;

public class ApplicationUser : IdentityUser
{
    public string? Nome { get; set; }
    public string? Apelido { get; set; }
    public long? NIF { get; set; }
    public string? Rua { get; set; }
    public string? Localidade { get; set; }
    public string? CodigoPostal { get; set; }
    public string? Cidade { get; set; }
    public string? Pais { get; set; }
    public string? Telemovel { get; set; }
    public DateTime DataRegisto { get; set; }
    public byte[]? Fotografia { get; set; }
    public string? Estado { get; set; }
}
