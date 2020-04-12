using System;
using System.Runtime.InteropServices;

namespace PowerInformation.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SystemBatteryState
    {
        public bool AcOnLine;
        public bool BatteryPresent;
        public bool Charging;
        public bool Discharging;

        public byte spare1;
        public byte spare2;
        public byte spare3;
        public byte spare4;

        public uint MaxCapacity;
        public uint RemainingCapacity;
        public uint Rate;
        public uint EstimatedTime;
        public uint DefaultAlert1;
        public uint DefaultAlert2;
    }

}
