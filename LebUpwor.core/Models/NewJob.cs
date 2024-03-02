using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Models
{
    public class NewJob
    {
        public  int JobId { get; set; }
        public  int UserId { get; set; }
        public  DateTime date { get; set; }
        public NewJob()
        {
            date = DateTime.Now;
        }
        public Job? Job { get; set; }
        public User? User { get; set; }
    }
}
