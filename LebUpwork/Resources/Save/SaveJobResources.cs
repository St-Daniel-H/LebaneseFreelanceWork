using LebUpwor.core.Models;

namespace LebUpwork.Api.Resources.Save
{
    public class SaveJobResources
    {
        public User? User { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double? Offer { get; set; }
    }
}
