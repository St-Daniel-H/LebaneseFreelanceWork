using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Models
{
    public class AppliedToTask
    {
        [Key]
        public int AppliedToTaskId { get; set; }
        public DateTime AppliedDate { get; set; }
        public required int JobId { get; set; }
        public required int UserId { get; set; }
        public bool IsMarkedAsDone { get; set; }
        public DateTime IsMarkedAsDoneDate { get; set; }
        [ForeignKey("UserId")]
        public  User? User { get; set; }
        public AppliedToTask() { 
        AppliedDate = DateTime.Now;
        }  
        //public virtual ICollection<User>? AppliedUsers { get; set; }
        [ForeignKey("JobId")]
        public  Job? Job { get; set; }
    }
}
