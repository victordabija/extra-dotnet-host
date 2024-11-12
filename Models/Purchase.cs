using Microsoft.Build.Framework;

namespace MusicShopApp.Models;

public class Purchase
{
    public UInt16 Id { get; set; }
    
    [Required]
    public UInt16 ClientId { get; set; }
    
    public virtual Client Client { get; set; }
    
    [Required]
    public UInt16 AlbumID { get; set; }
    
    public virtual Album Album { get; set; }
    
    public DateTime Date { get; set; }
}