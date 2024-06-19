namespace _3_Data.Models;

public class ModelBase
{
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? UpdatedData { get; set; }
    
    public bool IsActive { get; set; } = true;
}