namespace LebUpwork.Api.Settings
{
    public class JwtSettings
    {
        public required string Issuer { get; set; }

        public required string Audience { get; set; }
        public required string Secret { get; set; }

        public int ExpirationInDays { get; set; }

    }
}
