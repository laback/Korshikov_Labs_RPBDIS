using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RailroadTransport.Data;
using RailroadTransport.Models;
using RailroadTransport.ViewModels;

namespace RailroadTransport.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StopController : Controller
    {
        private RailroadContext railroadContext;
        private IMemoryCache cache;
        public StopController(RailroadContext rc, IMemoryCache cache)
        {
            railroadContext = rc;
            this.cache = cache;
        }
        public IActionResult Index(string NameOfStop, SortState sortState, int page = 1)
        {
            StopViewModel viewModel;
            if (!cache.TryGetValue("stopViewModel", out viewModel))
            {
                viewModel = SetViewModel(NameOfStop, sortState, page);
                cache.Set("stopViewModel", viewModel);
            }
            else
            {
                viewModel = SetViewModel(NameOfStop ?? viewModel.NameOfStop, sortState, page);
                cache.Set("stopViewModel", viewModel);
            }
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("StopId,NameOfStop")] Stop stop)
        {
            if (!String.IsNullOrEmpty(stop.NameOfStop))
            {
                railroadContext.Stops.Add(stop);
                railroadContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stop);
        }

        public IActionResult Edit(int? id)
        {
            var stop = railroadContext.Stops.Find(id);
            return View(stop);
        }
        [HttpPost]
        public IActionResult Edit([Bind("StopId,NameOfStop")] Stop stop)
        {
            railroadContext.Update(stop);
            railroadContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            var post = railroadContext.Stops.Find(id);
            return View(post);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleConfirmed(int id)
        {
            var stop = railroadContext.Stops.Find(id);
            var Stop = railroadContext.Schedules.Where(b => b.StopId == id);
            railroadContext.Schedules.RemoveRange(Stop);
            railroadContext.Stops.Remove(stop);
            railroadContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IEnumerable<Stop> SortSearch(IEnumerable<Stop> stops, SortState sortState, string NameOfStop)
        {
            stops = stops.Where(n => n.NameOfStop.Contains(NameOfStop ?? ""));
            switch (sortState)
            {
                case SortState.NameOfStopAcs:
                    stops = stops.OrderBy(n => n.NameOfStop);
                    break;
                case SortState.NameOfStopDecs:
                    stops = stops.OrderByDescending(n => n.NameOfStop);
                    break;
            }
            return stops;
        }
        public IActionResult ClearCache()
        {
            cache.Remove("stopViewModel");
            return RedirectToAction("Index");
        }
        private StopViewModel SetViewModel(string NameOfStop, SortState sortState, int page = 1)
        {
            IEnumerable<Stop> stops = railroadContext.Stops;
            stops = SortSearch(stops, sortState, NameOfStop ?? "");
            int pageSize = 10;
            int count = stops.Count();
            stops = stops.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            StopViewModel viewModel = new StopViewModel
            {
                Stops = stops,
                PageViewModel = pageViewModel,
                NameOfStop = NameOfStop,
                SortViewModel = new SortViewModel(sortState)
            };
            return viewModel;
        }
    }
}
