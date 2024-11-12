using System.ComponentModel.DataAnnotations;

namespace MusicShopApp.Models;

public class Client
{
    public UInt16 Id { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public UInt32 YearOfBirth { get; set; }
    
    [StringLength(13)]
    public string? IDNP { get; set; }
    
    public string FullName => $"{FirstName} {LastName}";
}