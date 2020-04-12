using PowerInformationCom.Model;
using System;
using System.Runtime.InteropServices;

namespace PowerInformationCom.Interfaces
{
    [ComVisible(true)]
    [Guid("21E06684-9F79-43BA-986E-7AF5B17684B6")]
    public interface IPowerInformationCom
    {

        long GetLastSleepTime();
        long GetLastWakeTime();
        SystemBatteryState GetSystemBatteryState();
        ProcessorPowerModel GetProcessorPowerModel();

        void DeleteFile();
        void ReserveFile();

        void SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

    }
}
