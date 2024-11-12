using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicShopApp.Models;

namespace MusicShopApp.Data
{
    public class MusicShopAppContext : DbContext
    {
        public MusicShopAppContext (DbContextOptions<MusicShopAppContext> options)
            : base(options)
        {
        }

        public DbSet<MusicShopApp.Models.Client> Client { get; set; } = default!;
        public DbSet<MusicShopApp.Models.Album> Album { get; set; } = default!;
        public DbSet<MusicShopApp.Models.Purchase> Purchase { get; set; } = default!;
    }
}
