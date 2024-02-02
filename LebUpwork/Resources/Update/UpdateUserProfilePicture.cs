namespace LebUpwork.Api.Resources.Update
{
    public class UpdateUserProfilePicture
    {
        public required int UserId;
        public required IFormFile ProfilePicture { get; set; }
    }
}
