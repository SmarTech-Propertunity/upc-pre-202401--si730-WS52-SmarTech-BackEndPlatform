namespace _3_Data.Models;

public class RefreshTokenModel
{
    public string ExpiredToken { set; get; }
    public string RefreshToken { set; get; }
    public int UserId { set; get; }
}