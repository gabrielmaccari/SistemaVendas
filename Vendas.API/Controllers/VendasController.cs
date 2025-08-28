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
        var produto = await _context.Vendas.FindAsync(id);

        if (produto == null)
        {
            return NotFound();
        }

        return produto;
    }

    // POST: api/produtos
    [HttpPost]
    public async Task<ActionResult<Venda>> PostVenda(Venda venda)
    {
        _context.Vendas.Add(venda);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVenda), new { id = venda.Id }, venda);
    }

    // PUT: api/produtos/5
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
}
