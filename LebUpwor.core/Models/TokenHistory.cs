using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Models
{
    public class TokenHistory
    {
        [Key]
        public int TokenHistoryId { get; set; }
        public required double AmountSent { get; set; }
        public required DateTime Date {get; set; }

        public required int SenderId { get; set; }
        public required int ReceiverId { get; set; }
        public required int TaskId { get; set; }
        public  Job Job { get; set; }
        [ForeignKey("SenderId")]
        public virtual  User Sender { get; set; }
        [ForeignKey("ReceiverId")]
        public   User Receiver { get; set; }
        public TokenHistory()
        {
            Date = DateTime.Now;
        }
    }
}
