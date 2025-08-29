using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class VendasPedidoController : ControllerBase
{
    private readonly VendasContext _context;

    public VendasPedidoController(VendasContext context)
    {
        _context = context;
    }
    /*
    // GET: api/produtos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemPedido>>> GetItemPedido()
    {
        return await _context.ItensPedidos.ToListAsync();
    }

    // GET: api/produtos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ItemPedido>> GetItemPedido(int id)
    {
        var produto = await _context.ItensPedidos.FindAsync(id);

        if (produto == null)
        {
            return NotFound();
        }

        return produto;
    }

    // POST: api/produtos
    [HttpPost]
    public async Task<ActionResult<ItemPedido>> PostItemPedido(ItemPedido item)
    {
        _context.ItensPedidos.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetItemPedido), new { id = item.Id }, item);
    }
    */
    // PUT: api/produtos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutItemPedido(int id, ItemPedido item)
    {
        if (id != item.Id)
        {
            return BadRequest();
        }

        _context.Entry(item).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.ItensPedidos.Any(e => e.Id == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: api/produtos/5
    [HttpDelete("{VendaId}")]
    public async Task<ActionResult<Venda>> DeleteItemPedido(int VendaId, [FromServices] EstoqueService estoqueService)
    {
        var venda = await _context.ItensPedidos.FindAsync(VendaId);
        if (venda == null)
        {
            return NotFound();
        }
        await estoqueService.AtualizarEstoqueNegativo(venda.ProdutoId, venda.Quantidade);
        _context.ItensPedidos.Remove(venda);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
