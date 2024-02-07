using LebUpwor.core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LebUpwork.Api.Resources.Update;

namespace LebUpwork.Api.Resources
{
    public class JobResources
    {
        public int JobId { get; set; }
        public int UserId { get; set; }
        public  string? Title { get; set; }
        public  string? Description { get; set; }
        public  double? Offer { get; set; }
        public DateTime? PostedDate { get; set; }
        public ICollection<TagResources> Tags { get; set; }

    }
}

