using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }

        public required int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required double Offer { get; set; }
        public bool IsCompleted { get; set; }
        public  int FinishedByUserId { get; set; }
        [ForeignKey("FinishedByUserId")]
        public virtual User? FinishedByUser { get; set; }

        public virtual ICollection<AppliedToTask> AppliedUsers { get; set; }

        public Job()
        {
            IsCompleted = false;
            FinishedByUserId = 0;
        }
    }
}
