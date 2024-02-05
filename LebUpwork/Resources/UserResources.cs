namespace LebUpwork.Api.Resources
{
    public class UserResources
    {
        public  string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public double? Token { get; set; }

        public string? Email { get; set; }
        public string? Status { get; set; }

        public DateTime? JoinedDate { get; set; }
        public DateTime? LastSeenDate { get; set; }

        public bool? IsOnline { get; set; }
        public string? ProfilePicture { get; set; }
        public string? CVpdf { get; set; }
        public int? RoleId { get; set; }
    }
}
