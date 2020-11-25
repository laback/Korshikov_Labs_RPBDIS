using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RailroadTransport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailroadTransport.Data
{
    public class UserIdentityContext : IdentityDbContext<User>
    {
        public UserIdentityContext(DbContextOptions<UserIdentityContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
