﻿using PowerInformation.Interfaces;
using System;

namespace PowerInformation.Services
{
    public class ShuttingPowerService : IShuttingPowerService
    {
        public void SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent)
        {
            uint status = PowerInformationInterOp.SetSuspendState(hibernate, forceCritical, disableWakeEvent);

            if (status == 0)
            {
                throw new Exception($"SetSuspendState return status: {status}");
            }
        }
    }
}