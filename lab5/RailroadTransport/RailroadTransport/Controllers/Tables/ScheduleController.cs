using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RailroadTransport.Data;
using RailroadTransport.Models;
using RailroadTransport.ViewModels;

namespace RailroadTransport.Controllers
{
    [Authorize]
    public class ScheduleController : Controller
    {
        private RailroadContext railroadContext;
        private IMemoryCache cache;
        public ScheduleController(RailroadContext rc, IMemoryCache cache)
        {
            railroadContext = rc;
            this.cache = cache;
        }
        public IActionResult Index(string Stop, TimeSpan StartTime, TimeSpan EndTime, bool IsSorted, int page = 1)
        {
            ScheduleViewModel viewModel;
            if(IsSorted)
            {
                IEnumerable<Schedule> schedules = railroadContext.Schedules.OrderBy(t => t.TrainId).ThenBy(d => d.Distance).ThenByDescending(t => t.TimeOfArrive).Include(p => p.Stop); 
                int pageSize = 20;
                int count = schedules.Count();
                schedules = schedules.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                viewModel = new ScheduleViewModel()
                {
                    Schedules = schedules,
                    PageViewModel = pageViewModel
                };
            }
            else
            {
                if (!cache.TryGetValue("scheduleViewModel", out viewModel))
                {
                    viewModel = SetViewModel(Stop, StartTime, EndTime, page);
                    cache.Set("scheduleViewModel", viewModel);
                }
                else
                {
                    if (StartTime.TotalMilliseconds == 0 && viewModel.StartTime.TotalMilliseconds != 0)
                        StartTime = viewModel.StartTime;
                    if (EndTime.TotalMilliseconds == 0 && viewModel.EndTime.TotalMilliseconds != 0)
                        EndTime = viewModel.EndTime;
                    viewModel = SetViewModel(Stop ?? viewModel.Stop, StartTime, EndTime, page);
                    cache.Set("scheduleViewModel", viewModel);
                }
            }
            return View(viewModel);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["TrainId"] = new SelectList(railroadContext.Trains, "TrainId", "TrainId");
            ViewData["StopId"] = new SelectList(railroadContext.Stops, "StopId", "NameOfStop");

            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create([Bind("ScheduleId,TrainId,Day,StopId,Distance, TimeOfArrive, TimeOfDeparture")] Schedule schedule)
        {
            if (schedule.Distance > 0)
            {
                railroadContext.Schedules.Add(schedule);
                railroadContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["TrainId"] = new SelectList(railroadContext.Trains, "TrainId", "TrainId");
            ViewData["StopId"] = new SelectList(railroadContext.Stops, "StopId", "NameOfStop");
            return View(schedule);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            var staff = railroadContext.Schedules.Find(id);
            ViewData["StopId"] = new SelectList(railroadContext.Stops, "StopId", "NameOfStop");
            ViewData["TrainId"] = new SelectList(railroadContext.Trains, "TrainId", "TrainId");
            return View(staff);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit([Bind("ScheduleId,TrainId,Day,StopId,Distance, TimeOfArrive, TimeOfDeparture")] Schedule schedule)
        {
            railroadContext.Update(schedule);
            railroadContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            var result = railroadContext.Schedules.Include(s => s.Stop).Where(s => s.ScheduleId == id).FirstOrDefault();
            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleConfirmed(int id)
        {
            var schedule = railroadContext.Schedules.Find(id);
            railroadContext.Schedules.Remove(schedule);
            railroadContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult ClearCache()
        {
            cache.Remove("scheduleViewModel");
            return RedirectToAction("Index");
        }   
        public IEnumerable<Schedule> SortSearch(IEnumerable<Schedule> schedules, string Stop)
        {
            schedules = schedules.Where(n => n.Stop.NameOfStop.Contains(Stop ?? ""));
            if (!string.IsNullOrEmpty(Stop))
                schedules.OrderByDescending(t => t.TimeOfArrive);
            return schedules;
        }
        private ScheduleViewModel SetViewModel(string Stop, TimeSpan StartTime, TimeSpan EndTime, int page = 1)
        {
            IEnumerable<Schedule> schedules;
            ScheduleViewModel model;
            if (page > 1)
            {
                cache.TryGetValue("scheduleViewModel", out model);
                if (model != null)
                    schedules = railroadContext.Schedules.Include(b => b.Stop).Where(i => i.ScheduleId > model.Schedules.Last().ScheduleId);
                else
                    schedules = railroadContext.Schedules.Include(b => b.Stop);
            }
            else
                schedules = railroadContext.Schedules.Include(b => b.Stop);
            schedules = SortSearch(schedules, Stop);
            int pageSize = 20;
            int count = schedules.Count();
            int countOfTrain = railroadContext.Schedules.Include(b => b.Stop).Where(t => t.TimeOfArrive > StartTime && t.TimeOfArrive < EndTime).Count();
            schedules = schedules.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            int countOfStops = railroadContext.Stops.Count();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            ScheduleViewModel viewModel = new ScheduleViewModel
            {
                Schedules = schedules,
                PageViewModel = pageViewModel,
                StartTime = StartTime,
                EndTime = EndTime,
                CountOfTrains = countOfTrain,
                Stop = Stop,
                CountOfStops = countOfStops
            };
            return viewModel;
        }
    }
}
