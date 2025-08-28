using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class VendasContext : DbContext
{
    public DbSet<Venda> Vendas { get; set; }
    public DbSet<ItemPedido> ItensPedidos { get; set; }


    public string DbPath { get; }

    public VendasContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "blogging.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}


public class Venda
{
    [JsonIgnore]
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public decimal Preco { get; set; }

    public List<ItemPedido> ItensPedidos { get; set; } = new(); //Relacao 1:N

}
public class ItemPedido
{
    [JsonIgnore]
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public decimal PrecoUnitario { get; set; }
    public string NomeProduto { get; set; }
    public int Quantidade { get; set; }
    public decimal Subtotal { get; set; }
    [JsonIgnore]
    public virtual Venda? Venda { get; set; } //Gera a chave estrangeira automaticamente

}