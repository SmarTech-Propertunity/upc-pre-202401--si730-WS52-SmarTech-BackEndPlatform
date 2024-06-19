using System.ComponentModel.DataAnnotations;
using _3_Data.Models;

namespace _2_Domain.IAM.Models.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    
    public UserCredentials _UserCredentials { get; set; }
    public UserInformation _UserInformation { get; set; }
}