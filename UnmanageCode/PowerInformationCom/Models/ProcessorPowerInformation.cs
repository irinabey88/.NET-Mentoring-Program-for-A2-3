using System.Runtime.InteropServices;

namespace PowerInformationCom.Model
{
    [ComVisible(true)]
    [Guid("E61B298D-6208-474C-BA8A-00FDD5D8E0F8")]
    public class ProcessorPowerInformation
    {
        public uint Number;
        public uint MaxMhz;
        public uint CurrentMhz;
        public uint MhzLimit;
        public uint MaxIdleState;
        public uint CurrentIdleState;

        public override string ToString() => $"ProcessorPowerInformationModel:  Number: {Number}, MaxMhz: {MaxMhz}, CurrentMhz: {CurrentMhz}, MhzLimit: {MhzLimit}, MaxIdleState: {MaxIdleState}, CurrentIdleState: {CurrentIdleState}";
    }
}
