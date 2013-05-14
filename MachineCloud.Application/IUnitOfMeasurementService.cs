using System.Collections.Generic;
using MachineCloud.Domain;

namespace MachineCloud.Application
{
    public interface IUnitOfMeasurementService
    {
        IEnumerable<UnitOfMeasurement> GetAllUnitsOfMeasurement();
        UnitOfMeasurement FindUnitOfMeasurementByName(string name);
        UnitOfMeasurement CreateNewUnitOfMeasurement(string name, string shortName, SystemTypes systemValueType);
        void RemoveUnitOfMeasurement(string name);
    }
}
