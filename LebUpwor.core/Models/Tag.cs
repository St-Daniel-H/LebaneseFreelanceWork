using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Models
{
    public class Tag
    {
        [Key]
        public required int TagId { get; set; }
        public required string TagName { get; set; }

        public int? AddedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Tag()
        {
            CreatedDate = DateTime.Now;
            Users = new List<User>(); 
            Jobs = new List<Job>();   
        }
        [ForeignKey("AddedByUserId")]
        public  User? AddedByUser { get; set; }
        public  ICollection<User> Users { get; set; }
        public  ICollection<Job> Jobs { get; set; }
    }
}
