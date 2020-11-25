using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RailroadTransport.Data;
using RailroadTransport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : Controller
    {
        RailroadContext db;
        public PostsController(RailroadContext context)
        {
            this.db = context;
        }

        [HttpGet]
        public IEnumerable<Post> Get()
        {
            return db.Posts.ToList();
        }
        // GET api/posts/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Post post = db.Posts.FirstOrDefault(x => x.PostId == id);
            if (post == null)
                return NotFound();
            return new ObjectResult(post);
        }
        // POST api/posts
        [HttpPost]
        public IActionResult Post([FromBody] Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }

            db.Posts.Add(post);
            db.SaveChanges();
            return Ok(post);
        }
        // PUT api/posts/
        [HttpPut]
        public IActionResult Put([FromBody] Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }
            if (!db.Posts.Any(x => x.PostId == post.PostId))
            {
                return NotFound();
            }

            db.Update(post);
            db.SaveChanges();
            return Ok(post);
        }

        // DELETE api/posts/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Post post = db.Posts.FirstOrDefault(x => x.PostId == id);
            if (post == null)
            {
                return NotFound();
            }
            var staff = db.Staffs.Where(p => p.PostId == post.PostId);
            db.Staffs.RemoveRange(staff);
            db.Posts.Remove(post);
            db.SaveChanges();
            return Ok(post);
        }
    }
}
