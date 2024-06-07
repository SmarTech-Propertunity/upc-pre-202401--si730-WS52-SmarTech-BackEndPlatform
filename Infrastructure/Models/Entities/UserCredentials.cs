using System.ComponentModel.DataAnnotations.Schema;

namespace _3_Data.Models;

[ComplexType]
public class UserCredentials
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}