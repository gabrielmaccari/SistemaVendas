using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class EstoqueContext : DbContext
{
    public EstoqueContext(DbContextOptions<EstoqueContext> options) : base(options)
    {
    }
    public DbSet<Produto> Produtos { get; set; }
}


public class Produto
{
    [JsonIgnore]
    public int ProdutoId { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int QuantidadeEstoque { get; set; }
}