using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EletroGestao.Dominio.PedidoRoot;

namespace EletroGestao.Infra.Data.TypeConfiguration.Pedidos
{
    public class PedidoTypeConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(pk => pk.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id")
                .HasColumnType("char(36)")
                .IsRequired();

            builder.Property(p => p.IdCliente)
                .HasColumnName("IdCliente")
                .HasColumnType("char(36)")
                .IsRequired();

            builder.Property(p => p.NomeCliente)
                .HasColumnName("nomeCliente")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(p => p.IdProduto)
                .HasColumnName("IdProduto")
                .HasColumnType("char(36)")
                .IsRequired();

            builder.Property(p => p.NomeProduto)
                .HasColumnName("nomeProduto")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(p => p.CEP)
                .HasColumnName("CEP")
                .HasColumnType("varchar(10)");

            builder.Property(p => p.Regiao)
                .HasColumnName("regiao")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(p => p.NumeroPedido)
                .HasColumnName("numeroPedido")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(p => p.Data)
               .HasColumnName("data")
               .HasColumnType("datetime")
               .IsRequired();

            builder.Property(p => p.ValorProduto)
                .HasColumnName("valorProduto")
                .HasColumnType("decimal(15,5)")
                .IsRequired();

            builder.Property(p => p.ValorFrete)
                .HasColumnName("valorFrete")
                .HasColumnType("decimal(15,5)")
                .IsRequired();

            builder.Property(p => p.ValorFinal)
                .HasColumnName("valorFinal")
                .HasColumnType("decimal(15,5)")
                .IsRequired();

            builder.Property(p => p.DataEntrega)
               .HasColumnName("dataEntrega")
               .HasColumnType("datetime")
               .IsRequired();

            builder.Property(p => p.DataCadastro)
               .HasColumnName("data_cadastro")
               .HasColumnType("datetime")
               .IsRequired();

            builder.ToTable("Pedidos");
        }
    }
}
