using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OkooneBlogger.Models
{
    public class Identity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
