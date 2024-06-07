using System.Diagnostics.CodeAnalysis;
using _3_Data.Models;

namespace _1_API.Response;

public class AuthenticationResponse
{
    public string token { get; set; }
    public string refreshToken { get; set; }
    public bool result { get; set; }
    public string message { get; set; }
}