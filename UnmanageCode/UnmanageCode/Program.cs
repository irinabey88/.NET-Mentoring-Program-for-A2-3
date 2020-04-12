using PowerInformation.Services;
using System;

namespace UnmanageCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var powerService = new PowerInformationService();

            var lastSleepTime = powerService.GetLastSleepTime();
            var lastWakeTime = powerService.GetLastWakeTime();
            var systemBatteryState = powerService.GetSystemBatteryState();
            var processorInformation = powerService.GetProcessorPowerInformation();

            var hybernationService = new HybernationService();
            hybernationService.ReserveFile();
            //hybernationService.DeleteFile();

            var shuttingPowerService = new ShuttingPowerService();
            //shuttingPowerService.SetSuspendState(false, false, false);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
