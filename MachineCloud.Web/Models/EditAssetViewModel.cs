using System.Text;
using MachineCloud.Domain;

namespace MachineCloud.Web.Models
{
    public class EditAssetViewModel
    {
        public Asset Asset { get; set; }
        public string ParentPropertyName { get; set; }

        public string GetFullPropertyName(string propertyName)
        {
            var sb = new StringBuilder();
            if (ParentPropertyName != null)
                sb.Append(ParentPropertyName).Append(".");
            return sb.Append(propertyName).ToString();
        }
    }
}