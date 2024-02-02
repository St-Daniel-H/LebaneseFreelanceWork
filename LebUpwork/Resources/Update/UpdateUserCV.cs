using LebUpwor.core.Models;

namespace LebUpwork.Api.Resources.Update
{
    public class UpdateUserCV
    {
        public required int UserId { get; set; }
        public required IFormFile CVpdf { get; set; }
    }
}
