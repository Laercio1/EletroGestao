using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EletroGestao.Dominio.ClienteRoot;

namespace EletroGestao.Infra.Data.TypeConfiguration.Clientes
{
    public class ClienteTypeConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(pk => pk.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id")
                .HasColumnType("char(36)")
                .IsRequired();

            builder.Property(p => p.NomeRazaoSocial)
                .HasColumnName("nomeRazaoSocial")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(p => p.Documento)
                .HasColumnName("documento")
                .HasColumnType("varchar(14)")
                .IsRequired();

            builder.Property(p => p.DataCadastro)
               .HasColumnName("data_cadastro")
               .HasColumnType("datetime")
               .IsRequired();

            builder.ToTable("Clientes");
        }
    }
}
