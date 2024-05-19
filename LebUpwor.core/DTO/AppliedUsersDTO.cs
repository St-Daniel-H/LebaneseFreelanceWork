using LebUpwor.core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.DTO
{
    public class AppliedUsersDTO
    {
     //   public int AppliedToTaskId { get; set; }  
        public DateTime AppliedDate { get; set; }
        public required int JobId { get; set; }
        public required int UserId { get; set; }
        public int SelectedUserId { get; set; }
        public DateTime PostedDate { get; set; }
        public UserDTO? User { get; set; }

    }
}
