using Lab7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Lab7.Data
{
    public class RailroadContext : DbContext
    {
        public RailroadContext() : base("name=Course1ConnectionString") { }
        public DbSet<Models.Type> Types { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Train> Trains { get; set; }
    }
}
