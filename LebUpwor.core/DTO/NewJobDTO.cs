using LebUpwor.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.DTO
{
    public class NewJobDTO
    {
        public  int JobId { get; set; }
        public  int UserId { get; set; }
        public DateTime date { get; set; }
        public virtual JobWithDateAndOfferDTO? Job { get; set; }
        public virtual UserWithTokensDTO? User { get; set; }
    }
}
