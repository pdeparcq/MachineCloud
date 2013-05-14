using System.Collections.Generic;
using MachineCloud.Domain;
using Ninject;

namespace MachineCloud.Application
{
    public class UnitOfMeasurementService : IUnitOfMeasurementService
    {
        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }

        [Inject]
        public IUnitOfMeasurementRepository UnitOfMeasurementRepository { get; set; }

        public IEnumerable<UnitOfMeasurement> GetAllUnitsOfMeasurement()
        {
            return UnitOfMeasurementRepository.GetAll();
        }

        public UnitOfMeasurement FindUnitOfMeasurementByName(string name)
        {
            return UnitOfMeasurementRepository.Find(name);
        }

        public UnitOfMeasurement CreateNewUnitOfMeasurement(string name, string shortName, SystemTypes systemValueType)
        {
            UnitOfMeasurementRepository.Add(new UnitOfMeasurement{Name = name, ShortName = shortName, ValueType = systemValueType});
            UnitOfWork.Commit();
            return UnitOfMeasurementRepository.Find(name);
        }

        public void RemoveUnitOfMeasurement(string name)
        {
            UnitOfMeasurementRepository.Remove(UnitOfMeasurementRepository.Find(name));
            UnitOfWork.Commit();
        }
    }
}
