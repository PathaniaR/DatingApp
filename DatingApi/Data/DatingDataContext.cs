using Microsoft.EntityFrameworkCore;
using DatingApi.Entities;

namespace DatingApi.Data
{
    public class DatingDataContext:DbContext
    {
      public DatingDataContext(DbContextOptions options):base(options)
      {
      }
      public DbSet<AppUser> AppUsers { get; set; }
    }
}