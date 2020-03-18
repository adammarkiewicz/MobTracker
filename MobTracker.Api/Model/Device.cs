using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobTracker.Api.Model
{
    public class Device
    {
        public string Id { get; set; }
        public string ConnectionId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
    }
}
