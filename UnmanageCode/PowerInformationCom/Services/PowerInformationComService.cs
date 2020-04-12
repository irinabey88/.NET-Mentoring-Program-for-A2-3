using PowerInformation.Services;
using PowerInformationCom.Interfaces;
using PowerInformationCom.Model;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace PowerInformationCom.Services
{
    [ComVisible(true)]
    [Guid("AE9AACB0-0CFF-46F4-BDBE-E3153D8D08A9")]
    public class PowerInformationComService : IPowerInformationCom
    {
        private PowerInformationService _powerInformationService;
        private HybernationService _hybernationService;
        private ShuttingPowerService _shuttingPowerService;

        public PowerInformationComService()
        {
            _powerInformationService = new PowerInformationService();
            _hybernationService = new HybernationService();
            _shuttingPowerService = new ShuttingPowerService(); 
        }

        public long GetLastSleepTime()
        {
            return _powerInformationService.GetLastSleepTime();
        }

        public long GetLastWakeTime()
        {
            return _powerInformationService.GetLastWakeTime();
        }

        public SystemBatteryState GetSystemBatteryState()
        {
            var batteryState = _powerInformationService.GetSystemBatteryState();
            return new SystemBatteryState()
            {
                AcOnLine = batteryState.AcOnLine,
                BatteryPresent = batteryState.BatteryPresent,
                Charging = batteryState.Charging,
                Discharging = batteryState.Discharging,
                MaxCapacity = batteryState.MaxCapacity,
                RemainingCapacity = batteryState.RemainingCapacity,
                Rate = batteryState.Rate,
                EstimatedTimeInSeconds = batteryState.EstimatedTime,
                DefaultAlert1 = batteryState.DefaultAlert1,
                DefaultAlert2 = batteryState.DefaultAlert2,
            };
        }

        public ProcessorPowerModel GetProcessorPowerModel()
        {
            var processorsInformation = _powerInformationService.GetProcessorPowerInformation();

            return new ProcessorPowerModel
            {
                Processors = processorsInformation.Select(x => new ProcessorPowerInformation
                {
                    Number = x.Number,
                    MaxMhz = x.MaxMhz,
                    CurrentMhz = x.CurrentMhz,
                    MhzLimit = x.MhzLimit,
                    MaxIdleState = x.MaxIdleState,
                    CurrentIdleState = x.CurrentIdleState,
                }).ToArray()
            };
        }

        public void DeleteFile()
        {
            _hybernationService.DeleteFile();
        }

        public void ReserveFile()
        {
            _hybernationService.ReserveFile();
        }

        public void SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent)
        {
            _shuttingPowerService.SetSuspendState(hibernate, forceCritical, disableWakeEvent);
        }
    }
}
