using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpEcho.Models
{
    public class BinRequest
    {
        public string Id { get; set; } = Guid.NewGuid().ToString().ToLower().Replace("-", "");

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        public string UserId { get; set; }
        public HttpEchoUser User { get; set; }

        public string EndpointId { get; set; }
        public Endpoint Endpoint { get; set; }

        public string Url { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public byte[] Body { get; set; }

        public string ErrorMessage { get; set; }

        public bool Starred { get; set; }
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
