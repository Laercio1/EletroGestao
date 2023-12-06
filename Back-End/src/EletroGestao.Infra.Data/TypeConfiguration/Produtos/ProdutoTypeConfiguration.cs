using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EletroGestao.Dominio.ProdutoRoot;

namespace EletroGestao.Infra.Data.TypeConfiguration.Produtos
{
    public class ProdutoTypeConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(pk => pk.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id")
                .HasColumnType("char(36)")
                .IsRequired();

            builder.Property(p => p.NomeProduto)
                .HasColumnName("nomeProduto")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(p => p.Valor)
                .HasColumnName("valor")
                .HasColumnType("decimal(15,5)")
                .IsRequired();

            builder.Property(p => p.DataCadastro)
               .HasColumnName("data_cadastro")
               .HasColumnType("datetime")
               .IsRequired();

            builder.ToTable("Produtos");
        }
    }
}
