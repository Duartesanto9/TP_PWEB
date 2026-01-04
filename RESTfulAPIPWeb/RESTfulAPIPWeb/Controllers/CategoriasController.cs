using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTfulAPIPWeb.Entities;
using RESTfulAPIPWeb.Repositories;

namespace RESTfulAPIPWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaRepository categoriaRepository;

    public CategoriasController(ICategoriaRepository categoriaRepository)
    {
        this.categoriaRepository = categoriaRepository;
    }

    [HttpGet]
    // [Authorize]
    public async Task<IActionResult> Get()
    {
        IEnumerable<Categoria> categorias;
        categorias = await categoriaRepository.GetCategorias();
        return Ok(categorias);
    }
}
