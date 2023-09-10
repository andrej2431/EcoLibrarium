using Microsoft.EntityFrameworkCore;
using EcoLibrariumServer.Models;

namespace EcoLibrariumServer.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
    }
}
