using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApi.Helpers
{
    public class MessageParams:UserParams
    {
        public string UserName { get; set; }
        public string Container { get; set; } = "Unread";
    }
}
