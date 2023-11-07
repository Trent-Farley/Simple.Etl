#nullable disable
using Microsoft.EntityFrameworkCore;

namespace Simple.Etl.Models.Entities
{
    public class Log
    {
        
        public int LogId { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public DateTime LogDate { get; set; }
    }
}
