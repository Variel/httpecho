using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpEcho.Models
{
    public class Comment
    {
        public string Id { get; set; } = Guid.NewGuid().ToString().ToLower().Replace("-", "");

        public string RequestId { get; set; }
        public BinRequest Request { get; set; }

        public string WriterId { get; set; }
        public HttpEchoUser Writer { get; set; }

        public string Content { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    }
}
