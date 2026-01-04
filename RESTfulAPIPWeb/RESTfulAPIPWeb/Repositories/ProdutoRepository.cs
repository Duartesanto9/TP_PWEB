using Microsoft.EntityFrameworkCore;
using RESTfulAPIPWeb.Data;
using RESTfulAPIPWeb.Entities;

namespace RESTfulAPIPWeb.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext dbContext;

    public ProdutoRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Produto>> ObterProdutosPorCategoriaAsync(int categoriaId)
    {
        return await dbContext.Produtos
            .Where(p => p.CategoriaId == categoriaId)
            .Where(x => x.Imagem.Length > 0)
            .Include("modoentrega")
            .Include("categoria")
            .OrderBy(o => o.Nome)
            .ToListAsync();
    }

    public async Task<IEnumerable<Produto>> ObterProdutosPromocaoAsync()
    {
        return await dbContext.Produtos
            .Where(p => p.Promocao == true)
            .Where(x => x.Imagem.Length > 0)
            .Include("modoentrega")
            .Include("categoria")
            .OrderBy(p => p.categoria.Ordem)
            .ThenBy(p => p.Nome)
            .ToListAsync();
    }

    public async Task<IEnumerable<Produto>> ObterProdutosMaisVendidosAsync()
    {
        return await dbContext.Produtos
            .Where(p => p.MaisVendido)
            .Where(x => x.Imagem!.Length > 0)
            .Include("modoentrega")
            .Include("categoria")
            .OrderBy(p => p.categoria.Ordem)
            .ThenBy(p => p.Nome)
            .ToListAsync();
    }

    public async Task<IEnumerable<Produto>> ObterTodosProdutosAsync()
    {
        return await dbContext.Produtos
            .Where(x => x.Imagem!.Length > 0)
            .Include("modoentrega")
            .Include("categoria")
            .OrderBy(p => p.categoria.Ordem)
            .ThenBy(p => p.Nome)
            .ToListAsync();
    }

    public async Task<Produto> ObterDetalheProdutoAsync(int id)
    {
        var detalheProduto = await dbContext.Produtos
            .Where(x => x.Imagem!.Length > 0)
            .Include("modoentrega")
            .Include("categoria")
            .FirstOrDefaultAsync(p => p.Id == id);
        if(detalheProduto is null)
            throw new InvalidOperationException();
        return detalheProduto;
    }
}
