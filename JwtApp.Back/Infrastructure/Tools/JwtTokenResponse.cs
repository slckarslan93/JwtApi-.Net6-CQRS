namespace JwtApp.Back.Infrastructure.Tools
{
    public class JwtTokenResponse
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }

        public JwtTokenResponse(string token, DateTime expireDate)
        {
            ExpireDate = expireDate;
            Token = token;
        }
    }
}