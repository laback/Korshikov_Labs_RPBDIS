using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RailroadTransport.Data;
using RailroadTransport.Models;
using RailroadTransport.ViewModels;

namespace RailroadTransport.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TrainController : Controller
    {
        private RailroadContext railroadContext;
        public TrainController(RailroadContext rc)
        {
            railroadContext = rc;
        }
        public IActionResult Index(SortState sortState,int page = 1)
        {
            IEnumerable<Train> trains = railroadContext.Trains.Include(t => t.Type);
            trains = SortSearch(trains, sortState);
            int pageSize = 20;
            int count = trains.Count();
            trains = trains.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            TrainViewModel viewModel = new TrainViewModel
            {
                PageViewModel = pageViewModel,
                Trains = trains,
                SortViewModel = new SortViewModel(sortState)
            };
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(railroadContext.Types, "TypeId", "NameOfType");
            return View();
        }
        public IActionResult Create([Bind("TrainId,TypeId,IsFirm")] Train train)
        {
            if (train.TypeId > 0 && train.IsFirm == true || train.IsFirm == false)
            {
                railroadContext.Add(train);
                railroadContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["TypeId"] = new SelectList(railroadContext.Types, "TypeId", "NameOfType",train.TypeId);
            return View(train);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var train = railroadContext.Trains.Find(id);
            ViewData["TypeId"] = new SelectList(railroadContext.Types, "TypeId", "NameOfType", train.TypeId);
            return View(train);
        }
        public IActionResult Edit([Bind("TrainId,TypeId,IsFirm")] Train train)
        {
            railroadContext.Update(train);
            railroadContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            var train = railroadContext.Trains.Include(t => t.Type).Where(t => t.TrainId == id).FirstOrDefault();
            return View(train);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleConfirmed(int id)
        {
            var train = railroadContext.Trains.Find(id);
            var staffs = railroadContext.Staffs.Where(t => t.TrainId == id);
            railroadContext.Staffs.RemoveRange(staffs);
            railroadContext.Trains.Remove(train);
            railroadContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IEnumerable<Train> SortSearch(IEnumerable<Train> trains, SortState sortState)
        {
            switch(sortState)
            {
                case SortState.NameOfTypeAcs:
                    trains = trains.OrderBy(t => t.Type.NameOfType);
                    break;
                case SortState.NameOfTypeDecs:
                    trains = trains.OrderByDescending(t => t.Type.NameOfType);
                    break;
            }
            return trains;
        }
    }
}
