using EletroGestao.Dominio.ClienteRoot;
using EletroGestao.Dominio.PedidoRoot;
using EletroGestao.Dominio.ProdutoRoot;
using EletroGestao.Infra.Data.TypeConfiguration.Clientes;
using EletroGestao.Infra.Data.TypeConfiguration.Pedidos;
using EletroGestao.Infra.Data.TypeConfiguration.Produtos;
using Microsoft.EntityFrameworkCore;

namespace EletroGestao.Infra.Data.Context
{
    public class EletroGestaoContext : DbContext
    {
        public EletroGestaoContext()
        {

        }

        public EletroGestaoContext(DbContextOptions<EletroGestaoContext> options) : base(options) { }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Pedido> Pedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoTypeConfiguration());

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
