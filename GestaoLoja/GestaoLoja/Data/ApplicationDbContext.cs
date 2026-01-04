using GestaoLoja.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestaoLoja.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<ModoEntrega> ModosEntrega { get; set; }
    public DbSet<Favorito> Favoritos { get; set; }
    public DbSet<CarrinhoCompras> CarrinhosCompras { get; set; }
    public DbSet<Encomenda> Encomendas { get; set; }
    public DbSet<EncomendaItem> EncomendaItems { get; set; }
}
