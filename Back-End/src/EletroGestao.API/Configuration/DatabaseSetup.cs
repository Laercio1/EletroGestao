using EletroGestao.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EletroGestao.API.Configuration
{
    public static class DatabaseSetup
    {
        public static void AddDatabaseSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // services.AddDbContext<EletroGestaoContext>(options => options
            //    .UseSqlServer(configuration.GetConnectionString("EletroGestaoConnectionString")));

            services.AddDbContext<EletroGestaoContext>(options => options
                .UseSqlite("Data Source=EletroGestao.db"));
        }
    }
}
