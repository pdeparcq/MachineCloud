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
            Bind<MachineCloudContext>().ToSelf().InSingletonScope();

            //Units of Works
            Bind<IUnitOfWork>()
                  .ToMethod(context => context.Kernel.Get<MachineCloudContext>());

            //Repositories
            Bind<IUnitOfMeasurementRepository>().To<UnitOfMeasurementRepository>().InSingletonScope();
            Bind<IAssetTypeRepository>().To<AssetTypeRepository>().InSingletonScope();

            //Services
            Bind<IAssetTypeService>().To<AssetTypeService>().InSingletonScope();
            Bind<IUnitOfMeasurementService>().To<UnitOfMeasurementService>().InSingletonScope();
        }
    }
}
