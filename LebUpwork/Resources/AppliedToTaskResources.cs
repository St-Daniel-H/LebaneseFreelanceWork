using LebUpwor.core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LebUpwork.Api.Resources
{
    public class AppliedToTaskResources
    {
        public int AppliedToTaskId { get; set; }
        public DateTime AppliedDate { get; set; }
        public int? JobId { get; set; }
        public  User? User { get; set; }

        public  Job? Job { get; set; }
    }
}

