namespace _1_API.Request;

public class UserRegistrationRequest
{
    public string Username { set; get; }
    public string Password { set; get; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}