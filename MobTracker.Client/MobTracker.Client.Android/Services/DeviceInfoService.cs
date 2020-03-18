using System;
using MobTracker.Client.Droid.Services;
using MobTracker.Client.Model;
using MobTracker.Client.Services.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;
using Android.Provider;

[assembly: Dependency(typeof(DeviceInfoService))]
namespace MobTracker.Client.Droid.Services
{
    public class DeviceInfoService : IDeviceInfoService
    {
        public DeviceBadge DeviceBadge { get; private set; } = new DeviceBadge();

        public DeviceInfoService()
        {
            DeviceBadge.Id = Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Settings.Secure.AndroidId);
            DeviceBadge.Manufacturer = DeviceInfo.Manufacturer;
            DeviceBadge.Model = DeviceInfo.Model;   
        }        
    }
}