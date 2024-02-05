using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Models
{
    public class CashOutHistory
    {
        [Key]
        public required int CashOutHistoryId { get; set; }
        public double Amount { get; set;}
        public DateTime Date { get; set; }
        public required int UserId { get; set; }
        public  User? User { get; set; }

        CashOutHistory()
        {
            Date = DateTime.Now;
        }
    }
}
