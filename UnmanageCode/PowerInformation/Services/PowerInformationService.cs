using PowerInformation.Enums;
using PowerInformation.Interfaces;
using PowerInformation.Structures;
using System;
using System.Runtime.InteropServices;

namespace PowerInformation.Services
{
    public class PowerInformationService : IPowerInformationService
    {
        public long GetLastSleepTime()
        {
            long lastSleepTime = GetPowerInformationData<long>(PowerInformationLevel.LastSleepTime);

            return lastSleepTime;
        }

        public long GetLastWakeTime()
        {
            long lastWakeTime = GetPowerInformationData<long>(PowerInformationLevel.LastWakeTime);

            return lastWakeTime;
        }


        public SystemBatteryState GetSystemBatteryState()
        {
            var batteryState = GetPowerInformationData<SystemBatteryState>(PowerInformationLevel.SystemBatteryState);

            return batteryState;
        }

        public ProcessorPowerInformation[] GetProcessorPowerInformation()
        {
            var procCount = Environment.ProcessorCount;
            var procInfo = new ProcessorPowerInformation[procCount];
            var status = PowerInformationInterOp.CallNtPowerInformation(
                (int)PowerInformationLevel.ProcessorInformation,
                IntPtr.Zero,
                0,
                procInfo,
                procInfo.Length * Marshal.SizeOf(typeof(ProcessorPowerInformation))
                );

            if (status != PowerInformationInterOp.STATUS_SUCCESS)
            {
                throw new Exception($"CallNtPowerInformation return status: {status}");
            }

            return procInfo;
        }

        private T GetPowerInformationData<T>(PowerInformationLevel informationLevel)
        {
            var informaitonLevel = (int)informationLevel;
            IntPtr lpInBuffer = IntPtr.Zero;
            int inputBufSize = 0;
            int outputPtrSize = Marshal.SizeOf<T>();
            IntPtr outputPtr = Marshal.AllocCoTaskMem(outputPtrSize);

            var status = PowerInformationInterOp.CallNtPowerInformation(
                informaitonLevel,
                lpInBuffer,
                inputBufSize,
                outputPtr,
                outputPtrSize);

            Marshal.FreeHGlobal(lpInBuffer);
            if (status == PowerInformationInterOp.STATUS_SUCCESS)
            {
                var result = Marshal.PtrToStructure<T>(outputPtr);
                Marshal.FreeHGlobal(outputPtr);
                return result;
            }
            else
            {
                Marshal.FreeHGlobal(outputPtr);
                throw new Exception($"CallNtPowerInformation return status: {status}");
            }
        }
    }
}
