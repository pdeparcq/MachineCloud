using System.Data.Entity;
using MachineCloud.Domain;
using MachineCloud.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MachineCloud.Test
{
    [TestClass]
    public class AssetTypeTest : DropCreateDatabaseAlways<MachineCloudContext>
    {
        [TestInitialize]
        public void Initialize()
        {
            Database.SetInitializer(this);
        }

        private AssetType CreateGeoLocationAssetType()
        {
            var degrees = new UnitOfMeasurement { Name = "Degrees", ShortName = "°", ValueType = SystemTypes.Double };

            var geoLocation = new AssetType { Name = "GeoLocation" };
            geoLocation.AddProperty(new AssetProperty { Name = "Longitude", UnitOfMeasurement = degrees });
            geoLocation.AddProperty(new AssetProperty { Name = "Latitude", UnitOfMeasurement = degrees });

            return geoLocation;
        }

        private AssetType CreateVehicleAssetType(AssetType geoLocationAssetType)
        {
            
            var milesPerHour = new UnitOfMeasurement { Name = "MilesPerHour", ShortName = "mph", ValueType = SystemTypes.Integer };

            var vehicle = new AssetType { Name = "Vehicle" };
            vehicle.AddProperty(new AssetProperty { Name = "SerialNumber", IsUniqueIdentifier = true, ValueType = SystemTypes.String });
            vehicle.AddProperty(new AssetProperty { Name = "Position", Type = geoLocationAssetType });
            vehicle.AddProperty(new AssetProperty { Name = "Speed", UnitOfMeasurement = milesPerHour });
            return vehicle;
        }

        [TestMethod]
        public void CanCreateAssetType()
        {
            CreateVehicleAssetType(CreateGeoLocationAssetType());
        }

        [TestMethod]
        public void CanSaveUnitOfMeasurement()
        {
            using (var context = new MachineCloudContext())
            {
                context.Database.Initialize(true);
                var repository = new UnitOfMeasurementRepository { Context = context };
                repository.Add(new UnitOfMeasurement { Name = "Degrees", ShortName = "°", ValueType = SystemTypes.Double });
                context.Commit();
            }           
        }

        [TestMethod]
        public void CanSaveAssetType()
        {
            using (var context = new MachineCloudContext())
            {
                context.Database.Initialize(true);
                var repository = new AssetTypeRepository {Context = context};
                repository.Add(CreateVehicleAssetType(CreateGeoLocationAssetType()));
                context.Commit();
            }
        }
    }
}
