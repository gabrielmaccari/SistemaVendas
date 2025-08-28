using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

// DTO para atualizar apenas o estoque
public class ProdutoEstoqueDto
{
    public int ProdutoId { get; set; }
    public int QuantidadeEstoque { get; set; }
}

public class EstoqueService
{
    private readonly HttpClient _httpClient;

    public EstoqueService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> AtualizarEstoque(int produtoId, int quantidade)
    {

        var content = new StringContent(
        JsonSerializer.Serialize(quantidade),
        Encoding.UTF8,
        "application/json"
);
        var response = await _httpClient.PutAsync($"api/produtos/{produtoId}/estoque", content);
        return response.IsSuccessStatusCode;
    }
}
