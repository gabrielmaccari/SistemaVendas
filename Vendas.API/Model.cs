using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

public class VendasContext : DbContext
{
    public VendasContext(DbContextOptions<VendasContext> options) : base(options)
    {
    }
    public DbSet<Venda> Vendas { get; set; }
    public DbSet<ItemPedido> ItensPedidos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configura relacionamento 1:N
        modelBuilder.Entity<ItemPedido>()
            .HasOne(ip => ip.Venda)           // Cada ItemPedido tem uma Venda
            .WithMany(v => v.ItensPedidos)    // Cada Venda tem muitos ItensPedidos
            .HasForeignKey(ip => ip.VendaId); // FK em ItemPedido
    }

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
    public int VendaId { get; set; }
    [JsonIgnore]
    public Venda? Venda { get; set; }

}