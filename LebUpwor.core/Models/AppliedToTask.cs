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
        public required int JobId { get; set; }
        public required int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        //public virtual ICollection<User>? AppliedUsers { get; set; }
        [ForeignKey("JobId")]
        public virtual Job? Job { get; set; }
    }
}
