using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Context;

namespace WebApplication5.DBContext;

public class ShopContext: DbContext
{
    public ShopContext(DbContextOptions<ShopContext> options) : base(options)
    {
        
    } 
    
    public DbSet<Data> datas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Data>().HasKey(s => new { s. id});

    }
}