using PowerInformation.Enums;
using PowerInformation.Interfaces;
using System;
using System.Runtime.InteropServices;

namespace PowerInformation.Services
{
    public class HybernationService : IHybernationService
    {
        public void DeleteFile()
        {
            Hibernate(HibernateAction.Delete);
        }

        public void ReserveFile()
        {
            Hibernate(HibernateAction.Reserve);
        }

        private void Hibernate(HibernateAction hibernateAction)
        {
            int intSize = Marshal.SizeOf<bool>();
            IntPtr intPtr = Marshal.AllocCoTaskMem(intSize);
            Marshal.WriteByte(intPtr, (byte)hibernateAction);

            var status = PowerInformationInterOp.CallNtPowerInformation(
                (int)PowerInformationLevel.SystemReserveHiberFile,
                intPtr,
                intSize,
                IntPtr.Zero,
                0);
            Marshal.FreeHGlobal(intPtr);

            if (status != PowerInformationInterOp.STATUS_SUCCESS)
            {
                throw new Exception($"CallNtPowerInformation return status: {status}");
            }
        }
    }
}
