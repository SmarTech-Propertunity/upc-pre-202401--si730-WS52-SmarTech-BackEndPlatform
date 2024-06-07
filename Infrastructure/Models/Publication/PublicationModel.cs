using System.ComponentModel.DataAnnotations;
using _3_Shared.Domain.Models;

namespace _3_Data.Models.Publication;

public class PublicationModel : ModelBase
{
    [Key]
    [Required]
    [Range(
        0, int.MaxValue, 
        ErrorMessage = "'Id' must be a valid number."
    )]
    public int Id { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    [Required]
    [StringLength(
        (int) PublicationConstraints.MaxTitleLength, 
        MinimumLength = (int) PublicationConstraints.MinTitleLength, 
        ErrorMessage = "'Title' must have a valid string length range."
    )]
    public string Title { get; set; }
    
    [Required]
    [StringLength(
        (int) PublicationConstraints.MaxDescriptionLength, 
        MinimumLength = (int) PublicationConstraints.MinDescriptionLength, 
        ErrorMessage = "'Description' must have a valid string length range."
    )]
    public string Description { get; set; }
    
    [Required]
    [Range(
        (int) PublicationConstraints.MinPrice, 
        double.MaxValue, 
        ErrorMessage = "'Price' can't be negative."
    )]
    public float Price { get; set; }
    
    [Required]
    public LocationModel _Location { get; set; }
}