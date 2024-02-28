using Microsoft.EntityFrameworkCore;
using RegisterAPI.Entity.Entities;

namespace RegisterAPI.Repository.Context
{
    public class RegisterAPIContext : DbContext
    {
        public RegisterAPIContext(DbContextOptions<RegisterAPIContext> options) : base(options) { }

        public DbSet<Client> Client { get; set; }
        public DbSet<Integration> Integration { get; set; }
    }
}
