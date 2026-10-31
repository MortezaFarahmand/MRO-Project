using FleetManagement.Application;
using FleetManagement.Application.Basic_data;
using FleetManagement.Application.Contracts.Aircraft;
using FleetManagement.Application.Contracts.AircraftModel;
using FleetManagement.Application.Contracts.AircraftType;
using FleetManagement.Domain.AircraftAgg;
using FleetManagement.Domain.Basic_dataAgg.AircraftModelAgg;
using FleetManagement.Domain.Basic_dataAgg.AircraftTypeAgg;
using FleetManagement.Infrastructure.EFCore;
using FleetManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PartManagement.Configuration
{
    public class FleetManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {

            services.AddTransient<IAircraftApplication, AircraftApplication>();
            services.AddTransient<IAircraftRepository, AircraftRepository>();

            services.AddTransient<IAircraftTypeApplication, AircraftTypeApplication>();
            services.AddTransient<IAircraftTypeRepository, AircraftTypeRepository>();

            services.AddTransient<IAircraftModelApplication, AircraftModelApplication>();
            services.AddTransient<IAircraftModelRepository, AircraftModelRepository>();
            //services.AddTransient<IPermissionExposer, PartPermissionExposer>();

            services.AddDbContext<FleetContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
