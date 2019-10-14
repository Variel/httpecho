using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace HttpEcho.RouteConstraints
{
    public class IntroHostNameRouteConstraint : IRouteConstraint
    {
        private readonly string _primaryDomain;

        public IntroHostNameRouteConstraint([NotNull] string primaryDomain)
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

            return host == _primaryDomain;
        }
    }
}
