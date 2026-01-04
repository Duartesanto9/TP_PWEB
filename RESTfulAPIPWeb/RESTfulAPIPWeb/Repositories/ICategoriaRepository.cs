using RESTfulAPIPWeb.Entities;

namespace RESTfulAPIPWeb.Repositories;

public interface ICategoriaRepository
{
    public Task<IEnumerable<Categoria>> GetCategorias();
}