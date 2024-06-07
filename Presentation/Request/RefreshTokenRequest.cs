namespace _1_API.Request;

public class RefreshTokenRequest
{
    public string ExpiredToken { set; get; }
    public string RefreshToken { set; get; }
}