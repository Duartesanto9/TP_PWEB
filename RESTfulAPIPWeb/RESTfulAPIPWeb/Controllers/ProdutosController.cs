using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTfulAPIPWeb.Entities;
using RESTfulAPIPWeb.Repositories;

namespace RESTfulAPIPWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository produtoRepository;

    public ProdutosController(IProdutoRepository produtoRepository)
    {
        this.produtoRepository = produtoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetProdutos(string tipoProduto, int? categoriaId = null)
    {
        IEnumerable<Produto> produtos;

        if(tipoProduto == "categoria" && categoriaId != null)
        {
            produtos = await produtoRepository.ObterProdutosPorCategoriaAsync(categoriaId.Value);
        }
        else if(tipoProduto == "promocao")
        {
            var promocoes = await produtoRepository.ObterProdutosPromocaoAsync();
            return Ok(promocoes);
        }
        else if(tipoProduto == "maisvendido")
        {
            produtos = await produtoRepository.ObterProdutosMaisVendidosAsync();
        }
        else if(tipoProduto == "todos")
        {
            produtos = await produtoRepository.ObterTodosProdutosAsync();
        }
        else
        {
            return BadRequest("Tipo de produto inválido");
        }
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetalheProduto(int id)
    {
        Produto produto = await produtoRepository.ObterDetalheProdutoAsync(id);
        if (produto is null)
        {
            return NotFound($"Produto com id={id} não encontrado");
        }
        return Ok(produto);
    }
}
