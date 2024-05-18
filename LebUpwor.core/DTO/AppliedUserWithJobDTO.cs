using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.DTO
{
    public class AppliedUserWithJobDTO
    {
        public DateTime AppliedDate { get; set; }
        public required int JobId { get; set; }
        public required int UserId { get; set; }
        public UserDTO? User { get; set; }
        public JobDTO? Job { get; set; }
    }
}
