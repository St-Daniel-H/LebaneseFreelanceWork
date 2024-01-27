using LebUpwor.core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LebUpwork.Api.Resources
{
    public class JobResources
    {

        [Key]
        public int JobId { get; set; }
        public User? User { get; set; }
        public  string? Title { get; set; }
        public  string? Description { get; set; }
        public  double? Offer { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? PostedDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public  User? FinishedByUser { get; set; }

        public  ICollection<AppliedToTask>? AppliedUsers { get; set; }
    }
}

