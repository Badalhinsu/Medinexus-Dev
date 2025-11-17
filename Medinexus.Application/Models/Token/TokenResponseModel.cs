namespace Medinexus.Application.Models.Token
{
    public class TokenResponseModel
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? AccessTokenExpiry { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
    }
}
