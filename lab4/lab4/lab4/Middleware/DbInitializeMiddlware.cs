using lab2;
using lab3.Infrastracture;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace lab3.Middleware
{
    public class DbInitializeMiddlware
    {
        private readonly RequestDelegate _next;
        public DbInitializeMiddlware(RequestDelegate next, IConfiguration configuration)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context, ApplicationContext ac)
        {
            var Staff = ac.Staffs;
            if (Staff.Count() == 0)
            {
                ac.Database.ExecuteSqlCommand("FillPosts");
                ac.Database.ExecuteSqlCommand("FillTrains");
                ac.Database.ExecuteSqlCommand("FillStaff");
            }
            
            await _next.Invoke(context);
        }
    }
    public static class DbInitializerExtensions
    {
        public static IApplicationBuilder UseDbInitializerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DbInitializeMiddlware>();
        }
    }
}