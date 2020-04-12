using PowerInformation.Structures;

namespace PowerInformation.Interfaces
{
    public interface IPowerInformationService
    {
        long GetLastSleepTime();

        long GetLastWakeTime();

        SystemBatteryState GetSystemBatteryState();

        ProcessorPowerInformation[] GetProcessorPowerInformation();
    }
}
