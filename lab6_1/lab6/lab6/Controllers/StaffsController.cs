using lab6.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RailroadTransport.Data;
using Microsoft.EntityFrameworkCore;
using RailroadTransport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        RailroadContext db;
        public StaffsController(RailroadContext context)
        {
            this.db = context;
        }

        [HttpGet]
        public List<StaffViewModel> Get()
        {
            var staffs = db.Staffs.Include(p => p.Post).OrderBy(s => s.StaffId).Select(s =>
            new StaffViewModel
            {
                StaffId = s.StaffId,
                FIO = s.FIO,
                Age = s.Age,
                WorkExp = s.WorkExp,
                TrainId = s.TrainId,
                PostId = s.PostId,
                NameOfPost = s.Post.NameOfPost.Replace(" ", "")
            });
            return staffs.ToList();
        }
        [HttpGet("posts")]
        public IEnumerable<Post> GetPosts()
        {
            return db.Posts.ToList();
        }
        // GET api/staffs/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Staff staff = db.Staffs.FirstOrDefault(x => x.StaffId == id);
            if (staff == null)
                return NotFound();
            return new ObjectResult(staff);
        }
        // POST api/staffs
        [HttpPost]
        public IActionResult Post([FromBody] Staff staff)
        {
            if (staff == null)
            {
                return BadRequest();
            }

            db.Staffs.Add(staff);
            db.SaveChanges();
            return Ok(staff);
        }
        // PUT api/staffs/
        [HttpPut]
        public IActionResult Put([FromBody] Staff staff)
        {
            if (staff == null)
            {
                return BadRequest();
            }
            if (!db.Staffs.Any(x => x.StaffId == staff.StaffId))
            {
                return NotFound();
            }

            db.Update(staff);
            db.SaveChanges();
            return Ok(staff);
        }

        // DELETE api/staffs/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Staff staff = db.Staffs.FirstOrDefault(x => x.StaffId == id);
            if (staff == null)
            {
                return NotFound();
            }
            db.Staffs.RemoveRange(staff);
            db.SaveChanges();
            return Ok(staff);
        }
    }
}
