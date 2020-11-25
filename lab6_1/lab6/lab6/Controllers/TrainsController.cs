using lab6.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class TrainsController : Controller
    {
        RailroadContext db;
        public TrainsController(RailroadContext context)
        {
            this.db = context;
        }

        [HttpGet]
        public List<TrainViewModel> Get()
        {
            var trains = db.Trains.Include(t => t.Type).Select(t =>
                new TrainViewModel
                {
                    TrainId = t.TrainId,
                    IsFirm = t.IsFirm,
                    TypeId = t.TypeId,
                    NameOfType = t.Type.NameOfType
                });
            return trains.ToList();
        }
        [HttpGet("types")]
        public IEnumerable<RailroadTransport.Models.Type> GetTypes()
        {
            return db.Types.ToList();
        }

        // GET api/trains/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Train train = db.Trains.FirstOrDefault(x => x.TrainId == id);
            if (train == null)
                return NotFound();
            return new ObjectResult(train);
        }
        // POST api/trains
        [HttpPost]
        public IActionResult Post([FromBody] Train train)
        {
            if (train == null)
            {
                return BadRequest();
            }

            db.Trains.Add(train);
            db.SaveChanges();
            return Ok(train);
        }
        // PUT api/trains/
        [HttpPut]
        public IActionResult Put([FromBody] Train train)
        {
            if (train == null)
            {
                return BadRequest();
            }
            if (!db.Trains.Any(x => x.TrainId == train.TrainId))
            {
                return NotFound();
            }

            db.Update(train);
            db.SaveChanges();
            return Ok(train);
        }

        // DELETE api/trains/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Train train = db.Trains.FirstOrDefault(x => x.TrainId == id);
            if (train == null)
            {
                return NotFound();
            }
            db.Staffs.RemoveRange(db.Staffs.Where(t => t.TrainId == train.TrainId));
            db.Schedules.RemoveRange(db.Schedules.Where(t => t.TrainId == train.TrainId));
            db.Trains.Remove(train);
            db.SaveChanges();
            return Ok(train);
        }
    }
}
