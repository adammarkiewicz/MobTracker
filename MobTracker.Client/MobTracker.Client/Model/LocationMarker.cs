using System;
using System.Collections.Generic;
using System.Text;

namespace MobTracker.Client.Model
{
    public class LocationMarker
    {
        public string DeviceId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Colour { get; set; }
    }
}
