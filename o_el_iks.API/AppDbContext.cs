using Microsoft.EntityFrameworkCore;
using o_el_iks.API.Entities;

namespace o_el_iks.API;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<AuctionData> Auctions { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
    {
        
    }
    
    
    
    
}