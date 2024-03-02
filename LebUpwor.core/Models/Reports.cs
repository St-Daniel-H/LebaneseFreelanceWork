using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Models
{
    public class Report
    {
        [Key]
        public required int ReportId { get; set; }
        public int? ReportedById { get; set; }
        public  int? ReportedUserId { get; set; }

        public int? ReportedPostId { get; set; }
        public int? ReportedMessageId { get; set; }
        public required string Details { get; set; }
        public DateTime Date { get; set; }
        public Report()
        {
            Date = DateTime.Now;
        }
        public bool IsFinishJobFailure { get; set; }
        public  User? ReportedBy { get; set; }
        public  User? ReportedUser { get; set; }
        public  Job? ReportedPost { get; set; }
        public  Message? ReportedMessage { get; set; }
    }
}
