using LebUpwor.core.Models;
using LebUpwork.Api.Resources.Update;

namespace LebUpwork.Api.Resources.Save
{
    public class SaveJobResources
    { 
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double? Offer { get; set; }
        public ICollection<UpdateUserTagsResource> Tags { get; set; }
    }
}
