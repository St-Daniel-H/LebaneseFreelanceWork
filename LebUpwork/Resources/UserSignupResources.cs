namespace LebUpwork.Api.Resources
{
    public class UserSignupResources
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public IFormFile? CVpdf { get; set; }
        UserSignupResources()
        {
            PhoneNumber = "0";
        }
    }
}
