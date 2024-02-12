using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.DTO
{
    public class JobWithAppliedUsersDTO
    {
        public int JobId { get; set; }
        public UserDTO? User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Offer { get; set; }
        public DateTime PostedDate { get; set; }

        public ICollection<AppliedUsersDTO>? AppliedUsers { get; set; }
    }
}
