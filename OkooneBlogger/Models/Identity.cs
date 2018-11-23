using System;

namespace OkooneBlogger.Models
{
    public class Identity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}