using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpEcho
{
    public class HostNameRouteConstraintOptions
    {
        public bool AllowPrimaryDomain { get; set; }
        public bool AllowSubdomain { get; set; }
        public string PrimaryDomain { get; set; }
        public string RouteName { get; set; }
    }
}
