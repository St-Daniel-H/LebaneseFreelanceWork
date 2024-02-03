using LebUpwor.core.Models;

namespace LebUpwork.Api.Resources.Update
{
    public class UpdateUserProfilePicture
    {
        public required IFormFile ProfilePicture { get; set; }
    }
}
