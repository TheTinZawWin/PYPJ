namespace PYPJ.Models.CustomModels
{
    public class AuthenticateModel
    {
    }
    public class TokenResponse
    {
        public string JWTToken { get; set; }
        public string RefreshToken { get; set; }
    }
    public class JWTSetting
    {
        public string securitykey { get; set; }
    }
}
