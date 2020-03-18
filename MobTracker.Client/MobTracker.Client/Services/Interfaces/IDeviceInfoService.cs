using MobTracker.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobTracker.Client.Services.Interfaces
{
    public interface IDeviceInfoService
    {
        DeviceBadge DeviceBadge { get; }
    }
}
