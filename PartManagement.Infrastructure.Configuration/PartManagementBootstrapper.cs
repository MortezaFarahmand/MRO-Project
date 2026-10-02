using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PartManagement.Application.Basic_data;
using PartManagement.Application.Contracts.Basic_data.EquipmentType;
using PartManagement.Application.Contracts.Stock.StockType;
using PartManagement.Application.StockApplications;
using PartManagement.Configuration.Permissions;
using PartManagement.Domain.Basic_data.EquipmentTypeAgg;
using PartManagement.Domain.Stock.StockTypeAgg;
using PartManagement.Infrastructure.EFCore;
using PartManagement.Infrastructure.EFCore.Repository.Basic_data;
using PartManagement.Infrastructure.EFCore.Repository.Stock;

namespace PartManagement.Configuration
{
    public class PartManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IStockTypeApplication, StockTypeApplication>();
            services.AddTransient<IStockTypeRepository, StockTypeRepository>();

            services.AddTransient<IEquipmentTypeApplication, EquipmentTypeApplication>();
            services.AddTransient<IEquipmentTypeRepository, EquipmentTypeRepository>();

            services.AddTransient<IPermissionExposer, PartPermissionExposer>();

            services.AddDbContext<PartContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
