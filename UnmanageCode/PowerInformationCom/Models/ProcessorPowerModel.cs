using System.Linq;
using System.Runtime.InteropServices;

namespace PowerInformationCom.Model
{
    [ComVisible(true)]
    [Guid("04772CB6-AD16-4D1A-89BC-4E16E8ACB8CC")]
    public class ProcessorPowerModel
    {
        public ProcessorPowerInformation[] Processors;

        public override string ToString()
        {
            if (Processors == null || Processors.Length == 0)
            {
                return "There is no processor power information.";
            }

            return string.Join("\n", Processors.Select(x => x.ToString()));
        }
    }
}
