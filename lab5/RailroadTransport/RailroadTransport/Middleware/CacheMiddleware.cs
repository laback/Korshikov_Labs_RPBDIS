using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RailroadTransport.Data;
using RailroadTransport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailroadTransport.Middleware
{
    public class CacheMiddleware
    {
        private readonly RequestDelegate _next;
        IMemoryCache cache;
        public CacheMiddleware(RequestDelegate next, IMemoryCache cache)
        {
            this._next = next;
            this.cache = cache;
        }

        public async Task Invoke(HttpContext context, RailroadContext dbcontext)
        {
            IEnumerable<Staff> staffs;
            IEnumerable<Post> posts;
            IEnumerable<Train> trains;
            if (!cache.TryGetValue("Post", out posts))
            {
                cache.Set("Post", dbcontext.Posts.ToList(), new MemoryCacheEntryOptions());
            }
            if (!cache.TryGetValue("Train", out trains))
            {
                cache.Set("Train", dbcontext.Trains.ToList(), new MemoryCacheEntryOptions());
            }
            if (!cache.TryGetValue("Staff", out staffs))
            {
                cache.Set("Staff", dbcontext.Staffs.Include(p => p.Post).ToList(), new MemoryCacheEntryOptions());
            }
            await _next.Invoke(context);
        }
    }
    public static class CacheExtensions
    {
        public static IApplicationBuilder UseCacheMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CacheMiddleware>();
        }
    }
}
