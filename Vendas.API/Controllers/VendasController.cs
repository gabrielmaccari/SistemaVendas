using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class VendasController : ControllerBase
{
    private readonly VendasContext _context;

    public VendasController(VendasContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Venda>>> GetPedidoVendas()
    {
        return await _context.Vendas.ToListAsync();
    }

    // GET: api/produtos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Venda>> GetVenda(int id)
    {
        var venda = await _context.Vendas.FindAsync(id);
        List<ItemPedido> itens = await _context.ItensPedidos.Where(i => i.VendaId == id).ToListAsync();

        if (venda == null)
        {
            return NotFound();
        }
        venda.ItensPedidos = itens;
        return venda;
    }


    [HttpPost]
public async Task<ActionResult<Venda>> PostVenda(Venda venda, [FromServices] EstoqueService estoqueService)
{
    _context.Vendas.Add(venda);
    await _context.SaveChangesAsync();

    foreach (var item in venda.ItensPedidos)
    {
        var sucesso = await estoqueService.AtualizarEstoquePositivo(item.ProdutoId, item.Quantidade);
        if (!sucesso)
        {
            return BadRequest($"Não foi possível atualizar o estoque do produto {item.ProdutoId}");
        }
    }

    return CreatedAtAction(nameof(GetVenda), new { id = venda.Id }, venda);
}


    /*
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVenda(int id, Venda venda)
    {
        if (id != venda.Id)
        {
            return BadRequest();
        }

        _context.Entry(venda).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Vendas.Any(e => e.Id == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }
    

    // DELETE: api/produtos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVenda(int id)
    {
        var venda = await _context.Vendas.FindAsync(id);
        if (venda == null)
        {
            return NotFound();
        }

        _context.Vendas.Remove(venda);
        await _context.SaveChangesAsync();

        return NoContent();
        
    }
    */
}
