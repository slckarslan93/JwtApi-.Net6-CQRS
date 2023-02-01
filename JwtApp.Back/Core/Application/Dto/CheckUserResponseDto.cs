namespace JwtApp.Back.Core.Application.Dto
{
    public class CheckUserResponseDto
    {
        public string Username { get; set; } = String.Empty;
        public string Role { get; set; } = String.Empty;
        public int Id { get; set; }
        public bool IsExist { get; set; }
    }
}