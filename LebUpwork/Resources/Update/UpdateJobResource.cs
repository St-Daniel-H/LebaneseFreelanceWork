namespace LebUpwork.Api.Resources.Update
{
    public class UpdateJobResource
    {
        public int JobId { get; set; }
       // public int UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double? Offer { get; set; }
        public ICollection<UpdateUserTagsResource> Tags { get; set; }
    }
}
