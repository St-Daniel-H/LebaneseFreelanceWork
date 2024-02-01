using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public int GoogleAccountId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public  string Password { get; set; }
        public  string Salt { get; set; }
        public string? PhoneNumber { get; set; }

        public double Token { get; set; }
        public DateTime JoinedDate { get; set; }
        public string? ProfilePicture { get; set; }
        public string? CVpdf { get; set; }
        public int? RoleId { get; set; }
        public User()
        {
            Token = 0;
            JoinedDate = DateTime.Now;
        }


        // Navigation property for the associated role
        public virtual Role? Role { get; set; }
        public virtual ICollection<Job>? JobsPosted { get; set; }
        public virtual ICollection<Job>? JobsFinished { get; set; }
        public virtual ICollection<AppliedToTask>? AppliedToTasks { get; set; }
        public virtual ICollection<Message> SentMessages { get; set; }
        public virtual ICollection<Message> ReceivedMessages { get; set; }
        public virtual ICollection<CashOutHistory> CashOutHistory { get; set; }
        public virtual ICollection<TokenHistory> SentTokenHistories { get; set; }
        public virtual ICollection<TokenHistory> ReceivedTokenHistories { get; set; }
        


    }
}
