using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OkooneBlogger.Models
{
    public class User : People
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IList<Article> Articles { get; set; }
    }
}
