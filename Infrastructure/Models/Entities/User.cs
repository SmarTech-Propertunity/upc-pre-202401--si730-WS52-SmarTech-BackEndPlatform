using System.ComponentModel.DataAnnotations;

namespace _3_Data.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    
    public UserCredentials _UserCredentials { get; set; }
    public UserInformation _UserInformation { get; set; }
}