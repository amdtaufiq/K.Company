using K.Company.Core.Interfaces.Repositories;
using K.Company.Core.Interfaces.Services;
using K.Company.Core.Interfaces.Unit;
using K.Company.Core.Services.MainServices;
using K.Company.Infrastructure.Data;
using K.Company.Infrastructure.Repositories;
using K.Company.Infrastructure.Seeders;
using K.Company.Infrastructure.Unit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace K.Company.Infrastructure.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KCompDBContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("connection")),
                ServiceLifetime.Singleton);
        }

        public static void SeedDatabase(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<KCompDBContext>();
                try
                {
                    var seeder = new DataSeeder(context);
                    seeder.Seed();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IOrderService, OrderService>();

            return services;
        }
    }
}
