using Microsoft.EntityFrameworkCore;
using RailroadTransport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RailroadTransport.Data
{
    public class RailroadContext : DbContext
    {
        public RailroadContext(DbContextOptions<RailroadContext> options) : base(options) { }
        public DbSet<Models.Type> Types { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
    }
}
