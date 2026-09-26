using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PartManagement.Application.Contracts.Stock.StockType;
using PartManagement.Application.StockApplications;
using PartManagement.Configuration.Permissions;
using PartManagement.Domain.Stock.StockTypeAgg;
using PartManagement.Infrastructure.EFCore;
using PartManagement.Infrastructure.EFCore.Repository.Stock;

namespace PartManagement.Configuration
{
    public class PartManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IStockTypeApplication, StockTypeApplication>();
            services.AddTransient<IStockTypeRepository, StockTypeRepository>();


            services.AddTransient<IPermissionExposer, PartPermissionExposer>();

            services.AddDbContext<PartContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
