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
        public  User? User { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required double Offer { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime FinishedDate { get; set; }
        public DateTime? DeletedAt { get; set; }

        public int SelectCount { get; set; }
        public  int? SelectedUserId { get; set; }
        public DateTime? SelectedUserDate { get; set; }
        [ForeignKey("FinishedByUserId")]
        public  User? SelectedUser { get; set; }
        
        public  ICollection<AppliedToTask> AppliedUsers { get; set; }
        public  ICollection<Tag>? Tags { get; set; }
        public Job()
        {
            PostedDate =  DateTime.Now;
            DeletedAt = null;
            SelectedUserDate = null;
            IsCompleted = false;
        }
    }
}
