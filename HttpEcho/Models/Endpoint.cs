using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HttpEcho.Models
{
    public class Endpoint
    {
        [RegularExpression(@"^[a-z0-9\-]+$")]
        public string Id { get; set; }

        public string UserId { get; set; }
        public HttpEchoUser User { get; set; }

        public ICollection<BinRequest> Requests { get; set; } = new HashSet<BinRequest>();
    }
}
