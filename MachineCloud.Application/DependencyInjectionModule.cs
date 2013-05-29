using MachineCloud.Domain;
using MachineCloud.Infrastructure.Repositories;
using Ninject;
using Ninject.Modules;

namespace MachineCloud.Application
{
    public class DependencyInjectionModule : NinjectModule
    {
        public override void Load()
        {
            //DbContexts
            Bind<MachineCloudContext>().ToSelf().InThreadScope();

            //Units of Works
            Bind<IUnitOfWork>()
                  .ToMethod(context => context.Kernel.Get<MachineCloudContext>());

            //Repositories
            Bind<IUnitOfMeasurementRepository>().To<UnitOfMeasurementRepository>().InThreadScope();
            Bind<IAssetTypeRepository>().To<AssetTypeRepository>().InThreadScope();
            Bind<IAssetRepository>().To<AssetRepository>().InThreadScope();

            //Services
            Bind<IAssetTypeService>().To<AssetTypeService>().InThreadScope();
            Bind<IUnitOfMeasurementService>().To<UnitOfMeasurementService>().InThreadScope();
            Bind<IAssetService>().To<AssetService>().InThreadScope();
        }
    }
}
