using Microsoft.EntityFrameworkCore;
using RadioXXI.Models;

namespace RadioXXI.Context
{
    public class RadioContext : DbContext
    {
        public RadioContext(DbContextOptions<RadioContext> options) : base(options)
        {

        }

        public DbSet<StreamServer> StreamServers { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Comms> Comms { get; set; }
        public DbSet<Photos> Photos { get; set; }
    }
}
