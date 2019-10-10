using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HttpEcho
{
    public class HostNameRouteConstraint : IRouteConstraint
    {
        private readonly HostNameRouteConstraintOptions _options;

        public HostNameRouteConstraint(HostNameRouteConstraintOptions options)
        {
            _options = options;
            _options.PrimaryDomain = _options.PrimaryDomain.ToLower();
        }

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if (httpContext == null) return false;

            var host = httpContext.Request.Query["x-override-host"].ToString();
            if (string.IsNullOrWhiteSpace(host))
                host = httpContext.Request.Host.Host;

            host = host.ToLower();

            if (!_options.AllowPrimaryDomain && host == _options.PrimaryDomain)
                return false;

            if (!_options.AllowSubdomain && host.EndsWith(_options.PrimaryDomain) && host != _options.PrimaryDomain)
                return false;

            if (_options.AllowPrimaryDomain && host == _options.PrimaryDomain)
                return true;

            if (_options.AllowSubdomain && host.EndsWith(_options.PrimaryDomain) && host != _options.PrimaryDomain)
                return true;

            return false;
        }
    }
}
