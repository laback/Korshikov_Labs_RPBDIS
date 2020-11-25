using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RailroadTransport.Data;
using RailroadTransport.Models;
using RailroadTransport.ViewModels;

namespace RailroadTransport.Controllers
{
    [Authorize(Roles ="Admin")]
    public class PostController : Controller
    {
        private RailroadContext railroadContext;
        private IMemoryCache cache;
        public PostController(RailroadContext rc, IMemoryCache cache)
        {
            railroadContext = rc;
            this.cache = cache;
        }
        public IActionResult Index(string NameOfPost, SortState sortState, int page = 1)
        {
            PostViewModel viewModel;
            if (!cache.TryGetValue("postViewModel", out viewModel))
            {
                viewModel = SetViewModel(NameOfPost, sortState, page);
                cache.Set("postViewModel", viewModel);
            }
            else
            {
                viewModel = SetViewModel(NameOfPost ?? viewModel.NameOfPost, sortState, page);
                cache.Set("postViewModel", viewModel);
            }
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("PostId,NameOfPost")] Post post)
        {
            if (!String.IsNullOrEmpty(post.NameOfPost))
            {
                railroadContext.Posts.Add(post);
                railroadContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }
        public IActionResult Edit(int? id)
        {
            var post = railroadContext.Posts.Find(id);
            return View(post);
        }
        [HttpPost]
        public IActionResult Edit([Bind("PostId,NameOfPost")] Post post)
        {
            railroadContext.Update(post);
            railroadContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            var post = railroadContext.Posts.Find(id);
            return View(post);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleConfirmed(int id)
        {
            var post = railroadContext.Posts.Find(id);
            var staff = railroadContext.Staffs.Where(p => p.PostId == id);
            railroadContext.Staffs.RemoveRange(staff);
            railroadContext.Posts.Remove(post);
            railroadContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IEnumerable<Post> SortSearch(IEnumerable<Post> posts, SortState sortState, string NameOfPost)
        {
            switch (sortState)
            {
                case SortState.NameOfPostAcs:
                    posts = posts.OrderBy(n => n.NameOfPost);
                    break;
                case SortState.NameOfPostDesc:
                    posts = posts.OrderByDescending(n => n.NameOfPost);
                    break;
            }
            posts = posts.Where(n => n.NameOfPost.Contains(NameOfPost ?? ""));
            return posts;
        }
        public IActionResult ClearCache()
        {
            cache.Remove("postViewModel");
            return RedirectToAction("Index");
        }
        private PostViewModel SetViewModel(string NameOfPost, SortState sortState, int page = 1)
        {
            IEnumerable<Post> posts = railroadContext.Posts;
            posts = SortSearch(posts, sortState, NameOfPost ?? "");
            int pageSize = 10;
            int count = posts.Count();
            posts = posts.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PostViewModel viewModel = new PostViewModel
            {
                Posts = posts,
                PageViewModel = pageViewModel,
                NameOfPost = NameOfPost,
                SortViewModel = new SortViewModel(sortState)
            };
            return viewModel;
        }
    }
}
