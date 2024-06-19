using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using _3_Data.Models;
using _3_Shared.Domain.Models;

namespace _2_Domain.IAM.Models.Entities;

[ComplexType]
public class UserInformation : ModelBase
{
    [Required] 
    [MaxLength(15)]
    public string Name { get; set; }
    
    [MaxLength(30)]
    public string Lastname { get; set; }
    
    [MaxLength(9)]
    public string PhoneNumber { get; set; }
    
    public int UserLevel { get; set; } = (int) UserRole.NormalUser;
}