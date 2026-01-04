using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RESTfulAPIPWeb.Data;
using RESTfulAPIPWeb.Entities;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<ModoEntrega> ModosEntrega { get; set; }
    // Adicionar estes:
    public DbSet<Favorito> Favoritos { get; set; }
    public DbSet<CarrinhoCompras> CarrinhosCompras { get; set; }
    public DbSet<Encomenda> Encomendas { get; set; }
    public DbSet<EncomendaItem> EncomendaItems { get; set; }
}