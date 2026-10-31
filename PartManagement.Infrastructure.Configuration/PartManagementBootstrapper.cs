using _0_Framework.Infrastructure;
using _01_MROQuery.Contracts.Organization;
using _01_MROQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PartManagement.Application;
using PartManagement.Application.Basic_data;
using PartManagement.Application.Contracts.Basic_data.EquipmentType;
using PartManagement.Application.Contracts.Inventory.InventoryItem;
using PartManagement.Application.Contracts.Inventory.StockMovement;
using PartManagement.Application.Contracts.Part;
using PartManagement.Application.Contracts.Stock.StockType;
using PartManagement.Application.Contracts.Stock.Store;
using PartManagement.Application.Inventory;
using PartManagement.Application.Stock;
using PartManagement.Application.StockApplications;
using PartManagement.Configuration.Permissions;
using PartManagement.Domain.Basic_data.EquipmentTypeAgg;
using PartManagement.Domain.InventoryAgg.InventoryItemAgg;
using PartManagement.Domain.InventoryAgg.StockMovementAgg;
using PartManagement.Domain.PartAgg;
using PartManagement.Domain.Stock.StockTypeAgg;
using PartManagement.Domain.Stock.StoreAgg;
using PartManagement.Infrastructure.EFCore;
using PartManagement.Infrastructure.EFCore.Repository;
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

            services.AddTransient<IStoreApplication, StoreApplication>();
            services.AddTransient<IStoreRepository, StoreRepository>();

            services.AddTransient<IPartApplication, PartApplication>();
            services.AddTransient<IPartRepository, PartRepository>();

            services.AddTransient<IInventoryItemApplication, InventoryItemApplication>();
            services.AddTransient<IInventoryItemRepository, InventoryItemRepository>();

            services.AddTransient<IStockMovementApplication, StockMovementApplication>();
            services.AddTransient<IStockMovementRepository, StockMovementRepository>();

            services.AddTransient<IOrganizationQuery, OrganizationQuery>();

            services.AddTransient<IPermissionExposer, PartPermissionExposer>();




            services.AddDbContext<PartContext>(x => x.UseSqlServer(connectionString));
        }
    }
}

