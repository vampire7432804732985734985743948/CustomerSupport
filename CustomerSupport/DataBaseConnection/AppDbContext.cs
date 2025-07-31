using CustomerSupport.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupport.DataBaseConnection
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<ContactSupportRequestModel> ContactSupportRequests { get; set; } 
    }
}
