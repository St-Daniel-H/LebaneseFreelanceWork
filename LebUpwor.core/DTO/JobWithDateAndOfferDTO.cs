using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.DTO
{
    public class JobWithDateAndOfferDTO
    {
        public int JobId { get; set; }
        public DateTime PostedDate { get; set; }
        public double Offer { get; set; }
    }
}
