using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfulAPIPWeb.Data;
using RESTfulAPIPWeb.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace RESTfulAPIPWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class FavoritosController : ControllerBase
{
    private readonly AppDbContext _context;

    public FavoritosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Favorito>>> GetMeusFavoritos()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return await _context.Favoritos
                             .Where(f => f.ClienteId == userId)
                             .Include(f => f.Produto)
                             .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Favorito>> AdicionarFavorito([FromBody] int produtoId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var existe = await _context.Favoritos
            .AnyAsync(f => f.ClienteId == userId && f.ProdutoId == produtoId);

        if (existe) return Conflict("Produto já está nos favoritos.");

        var favorito = new Favorito { ClienteId = userId, ProdutoId = produtoId };

        _context.Favoritos.Add(favorito);
        await _context.SaveChangesAsync();

        return Ok(favorito);
    }

    [HttpDelete("{produtoId}")]
    public async Task<IActionResult> RemoverFavorito(int produtoId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var favorito = await _context.Favoritos
            .FirstOrDefaultAsync(f => f.ClienteId == userId && f.ProdutoId == produtoId);

        if (favorito == null) return NotFound();

        _context.Favoritos.Remove(favorito);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}