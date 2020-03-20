using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace MobTracker.Api.Model
{
    public class Device
    {
        public string Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Colour { get; set; }

        public string ConnectionId { get; set; }
    }
}
