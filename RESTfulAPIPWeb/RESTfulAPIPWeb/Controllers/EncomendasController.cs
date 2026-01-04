using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfulAPIPWeb.Data;
using RESTfulAPIPWeb.Entities;

namespace RESTfulAPIPWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class EncomendasController : ControllerBase
{
    private readonly AppDbContext _context;

    public EncomendasController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/Encomendas
    [HttpPost]
    public async Task<IActionResult> CriarEncomenda([FromBody] Encomenda encomenda)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) return Unauthorized();

        // Associar a encomenda ao utilizador autenticado
        encomenda.ClienteId = userId;
        encomenda.Data = DateTime.UtcNow;
        encomenda.Estado = EstadoEncomenda.Pendente;
        encomenda.pagamentoEfetuado = false; // Simulação inicial

        // Validar stock e preços (boa prática)
        foreach (var item in encomenda.ProdutosEncomendados)
        {
            var produtoDb = await _context.Produtos.FindAsync(item.ProdutoId);
            if (produtoDb != null)
            {
                // Atualiza o stock
                produtoDb.EmStock -= item.Quantidade;
                // Garante que o preço na encomenda é o preço atual do produto
                item.Preco = produtoDb.Preco ?? 0;
            }
        }

        _context.Encomendas.Add(encomenda);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEncomenda), new { id = encomenda.Id }, encomenda);
    }

    // GET: api/Encomendas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Encomenda>> GetEncomenda(int id)
    {
        var encomenda = await _context.Encomendas
                                      .Include(e => e.ProdutosEncomendados)
                                      .FirstOrDefaultAsync(e => e.Id == id);

        if (encomenda == null) return NotFound();

        return encomenda;
    }

    // GET: api/Encomendas/MinhasEncomendas
    [HttpGet("MinhasEncomendas")]
    public async Task<ActionResult<IEnumerable<Encomenda>>> GetMinhasEncomendas()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return await _context.Encomendas
                             .Where(e => e.ClienteId == userId)
                             .Include(e => e.ProdutosEncomendados)
                             .OrderByDescending(e => e.Data)
                             .ToListAsync();
    }
}