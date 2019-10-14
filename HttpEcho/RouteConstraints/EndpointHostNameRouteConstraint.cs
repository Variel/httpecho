﻿using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace HttpEcho.RouteConstraints
{
    public class EndpointHostNameRouteConstraint : IRouteConstraint
    {
        private readonly string _primaryDomain;

        public EndpointHostNameRouteConstraint([NotNull] string primaryDomain)
        {
            _primaryDomain = primaryDomain.ToLower();
        }

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if (httpContext == null) return false;

            var host = httpContext.Request.Query["x-override-host"].ToString();
            if (string.IsNullOrWhiteSpace(host))
                host = httpContext.Request.Host.Host;

            host = host.ToLower();

            if (!host.EndsWith(_primaryDomain))
                return false;

            host = host.Replace(_primaryDomain, "");

            var match = Regex.Match(host, @"^(?<endpoint>[a-zA-Z0-9\-]+)\.(?<userId>[a-zA-Z0-9]+)\.$");

            if (!match.Success)
                return false;

            values.Add("endpoint", match.Groups["endpoint"]);
            values.Add("userId", match.Groups["userId"]);

            return true;
        }
    }
}
