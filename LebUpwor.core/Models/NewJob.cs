using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Models
{
    public class NewJob
    {
        public required int JobId { get; set; }
        public required int UserId { get; set; }
        public required DateTime date { get; set; }
        NewJob()
        {
            date = DateTime.Now;
        }
        public Job? Job { get; set; }
        public User? User { get; set; }
    }
}
