using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OkooneBlogger.Models
{
    public class Article : Identity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? AuthorId { get; set; }
        public User Author { get; set; }
    }
}
