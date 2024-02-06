using LebUpwor.core.Models;

namespace LebUpwork.Api.Resources.Update
{
    public class UpdateUserTags
    {
        public required ICollection<UpdateUserTagsResource> Tags { get; set; }
    }
}
