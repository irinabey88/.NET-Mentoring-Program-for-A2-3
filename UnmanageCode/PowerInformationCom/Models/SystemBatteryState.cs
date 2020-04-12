using System.Runtime.InteropServices;

namespace PowerInformationCom.Model
{
    [ComVisible(true)]
    [Guid("411D7C89-CA82-4040-ABA7-17D0534691BE")]
    public class SystemBatteryState
    {
        public bool AcOnLine;
        public bool BatteryPresent;
        public bool Charging;
        public bool Discharging;
        public long MaxCapacity;
        public long RemainingCapacity;
        public long Rate;
        public long EstimatedTimeInSeconds;
        public long DefaultAlert1;
        public long DefaultAlert2;

        public override string ToString() => $"SystemBatteryStateModel: AcOnLine: {AcOnLine}, BatteryPresent: {BatteryPresent}, BatteryPresent: {BatteryPresent}, Charging: {Charging}, Discharging: {Discharging}, MaxCapacity: {MaxCapacity}, RemainingCapacity: {RemainingCapacity}, Rate: {Rate}, EstimatedTimeInSeconds: {EstimatedTimeInSeconds}, DefaultAlert1: {DefaultAlert1}, DefaultAlert2: {DefaultAlert2}";
    }
}
