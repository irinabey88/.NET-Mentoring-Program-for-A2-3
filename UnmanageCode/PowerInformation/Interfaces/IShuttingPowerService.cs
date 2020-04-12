using System;
namespace PowerInformation.Interfaces
{
    public interface IShuttingPowerService
    {
        void SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);
    }
}
