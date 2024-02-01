namespace LebUpwork.Api.Resources
{
    public class UserSignupResources
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public IFormFile? CVpdf { get; set; }
        
        
    }
}
