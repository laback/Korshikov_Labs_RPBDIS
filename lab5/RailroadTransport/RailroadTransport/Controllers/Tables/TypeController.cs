using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailroadTransport.Data;
using RailroadTransport.Models;

namespace RailroadTransport.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TypeController : Controller
    {
        private RailroadContext railroadContext;
        public TypeController(RailroadContext rc)
        {
            railroadContext = rc;
        }
        public IActionResult Index()
        {
            return View(railroadContext.Types.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("TypeId,NameOfType")] Models.Type type)
        {
            if (!String.IsNullOrEmpty(type.NameOfType))
            {
                railroadContext.Types.Add(type);
                railroadContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(type);
        }

        public IActionResult Edit(int? id)
        {
            var type = railroadContext.Types.Find(id);
            return View(type);
        }
        [HttpPost]
        public IActionResult Edit([Bind("TypeId,NameOfType")] Models.Type type)
        {
            railroadContext.Update(type);
            railroadContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            var type = railroadContext.Types.Find(id);
            return View(type);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleConfirmed(int id)
        {
            var type = railroadContext.Types.Where(t => t.TypeId == id).FirstOrDefault();
            var trains = railroadContext.Trains.Where(t => t.TypeId == id);
            railroadContext.Trains.RemoveRange(trains);
            railroadContext.Types.Remove(type);
            railroadContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
