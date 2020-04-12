using PowerInformation.Structures;
using System;
using System.Runtime.InteropServices;

namespace PowerInformation
{
    internal class PowerInformationInterOp
    {
        internal const uint STATUS_SUCCESS = 0;

        [DllImport("PowrProf.dll")]
        public static extern uint CallNtPowerInformation(
            int informaitonLevel,
            IntPtr inputBuffer,
            int inputBufLength,
            IntPtr outputBuffer,
            int outputBufferLength);

        [DllImport("PowrProf.dll")]
        public static extern uint CallNtPowerInformation(
           int informationLevel,
           IntPtr lpInputBuffer,
           int inputBufSize,
           [Out] ProcessorPowerInformation[] lpOutputBuffer,
           int nOutputBufferSize);

        [DllImport("PowrProf.dll")]
        public static extern uint SetSuspendState(
        bool Hibernate,
        bool ForceCritical,
        bool DisableWakeEvent);
    }
}
