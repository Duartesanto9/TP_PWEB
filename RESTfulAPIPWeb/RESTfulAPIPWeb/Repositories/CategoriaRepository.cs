using Microsoft.EntityFrameworkCore;
using RESTfulAPIPWeb.Data;
using RESTfulAPIPWeb.Entities;

namespace RESTfulAPIPWeb.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext dbContext;

    public CategoriaRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Categoria>> GetCategorias()
    {
        return await dbContext.Categorias
            .Where(x => x.Imagem.Length > 0)
            .OrderBy(O => O.Ordem)
            .ThenBy(p => p.Nome)
            .ToListAsync();
    }
}
