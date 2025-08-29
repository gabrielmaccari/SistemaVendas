using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly EstoqueContext _context;

    public ProdutosController(EstoqueContext context)
    {
        _context = context;
    }

    // GET: api/produtos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
    {
        return await _context.Produtos.ToListAsync();
    }

    // GET: api/produtos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Produto>> GetProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);

        if (produto == null)
        {
            return NotFound();
        }

        return produto;
    }

    // POST: api/produtos
    [HttpPost]
    public async Task<ActionResult<Produto>> PostProduto(Produto produto)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduto), new { id = produto.ProdutoId }, produto);
    }

    // PUT: api/produtos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduto(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
        {
            return BadRequest();
        }

        _context.Entry(produto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Produtos.Any(e => e.ProdutoId == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: api/produtos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
        {
            return NotFound();
        }

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    [HttpPut("{id}/estoquepositivo")]
    public async Task<IActionResult> AtualizarEstoquePositivo(int id, [FromBody] int quantidade)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null) return NotFound();

        produto.QuantidadeEstoque -= quantidade;
        await _context.SaveChangesAsync();

        return NoContent();
    }
    [HttpPut("{id}/estoquenegativo")]
    public async Task<IActionResult> AtualizarEstoqueNegativo(int id, [FromBody] int quantidade)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null) return NotFound();

        produto.QuantidadeEstoque += quantidade;
        await _context.SaveChangesAsync();

        return NoContent();
    }

}
