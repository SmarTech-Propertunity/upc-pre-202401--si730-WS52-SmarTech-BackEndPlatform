using System.ComponentModel.DataAnnotations;

namespace _3_Data.Models.Publication;

public class GetPublicationModel
{
    public int Id { set; get; }
    public bool IsActive { set; get; }
}