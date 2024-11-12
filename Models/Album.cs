using System.ComponentModel.DataAnnotations;

namespace MusicShopApp.Models;

public class Album
{
    public UInt16 Id { get; set; }

    public string? Title { get; set; }

    public string? Author { get; set; }

    [Range(2000, 2022)] 
    public UInt16 YearOfProduction { get; set; }

    public UInt16 Duration { get; set; }

    public float Price { get; set; }
}