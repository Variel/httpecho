using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HttpEcho.Models
{
    public class HttpEchoUser : IdentityUser
    {
        public DateTimeOffset? LastLoginAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        public ICollection<Endpoint> Endpoints { get; set; } = new HashSet<Endpoint>();
    }
}
