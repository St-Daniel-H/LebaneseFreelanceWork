using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LebUpwor.core.Models;
namespace LebUpwor.core.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public  string FirstName { get; set; }
        public  string LastName { get; set; }

        public bool? IsOnline { get; set; }

        public string? ProfilePicture { get; set; }
    }
}
