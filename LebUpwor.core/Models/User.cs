using LebUpwor.core.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public required string Password { get; set; }
        public required string Salt { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }

        public bool? IsOnline { get; set; }
        public double Token { get; set; }
        public DateTime JoinedDate { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime LastSeenDate { get; set; }
        public string? ProfilePicture { get; set; }
        public string? CVpdf { get; set; }
        public int? RoleId { get; set; }
        public User()
        {
            Token = 0;
            JoinedDate = DateTime.Now;
            LastSeenDate = DateTime.Now;
        }


        // Navigation property for the associated role
        public  Role? Role { get; set; }
        public  ICollection<Job>? JobsPosted { get; set; }
        public  ICollection<Job>? JobsFinished { get; set; }
        public  ICollection<AppliedToTask>? AppliedToTasks { get; set; }
        public  ICollection<Message> SentMessages { get; set; }
        public  ICollection<Message> ReceivedMessages { get; set; }
        public  ICollection<CashOutHistory> CashOutHistory { get; set; }
        public  ICollection<TokenHistory> SentTokenHistories { get; set; }
        public  ICollection<TokenHistory> ReceivedTokenHistories { get; set; }

        public  ICollection<Tag>? Tags { get; set; }

    }
}