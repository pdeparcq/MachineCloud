using System.ComponentModel.DataAnnotations;

namespace MachineCloud.Domain
{
    public enum SystemTypes
    {
        Boolean,
        String,
        Double,
        Integer
    }

    public class UnitOfMeasurement
    {
        [Key]
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public SystemTypes ValueType { get; set; }
    }
}
