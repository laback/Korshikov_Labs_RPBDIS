using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab2
{
    class Tasks
    {
        public void Task1(DbContextOptions<ApplicationContext> options)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var posts = db.Posts.ToList();
                foreach (Post p in posts)
                {
                    Console.WriteLine($"{p.postId} - {p.nameOfPost}");
                }
            }
        }
        public void Task2(int i, DbContextOptions<ApplicationContext> options)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var posts = (from post in db.Posts
                             where post.postId > i
                             select post).ToList();
                foreach (Post p in posts)
                {
                    Console.WriteLine($"{p.postId} - {p.nameOfPost}");
                }
            }
        }
        public void Task3(DbContextOptions<ApplicationContext> options)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var posts = (from staff in db.Staffs
                             group staff by staff.postId into pos
                             select new {
                                 НомерДолжности = pos.Key,
                                 КоличествоСотрудников = pos.Count()
                             }).ToList();
                foreach (var p in posts)
                {
                    Console.WriteLine($"{p.НомерДолжности} - {p.КоличествоСотрудников}");
                }
            }
        }
        public void Task4(DbContextOptions<ApplicationContext> options)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var result = (from post in db.Posts
                              from staff in db.Staffs
                              where staff.postId == post.postId
                              select new { post, staff }).ToList();
                foreach (var p in result)
                {
                    Console.WriteLine($"{p.staff.staffId}  {p.post.postId} {p.post.nameOfPost}");
                }
            }
        }
        public void Task5(string name, DbContextOptions<ApplicationContext> options)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var result = (from post in db.Posts
                              from staff in db.Staffs
                              where staff.postId == post.postId && post.nameOfPost == name
                              select new { post, staff }).ToList();
                foreach (var p in result)
                {
                    Console.WriteLine($"{p.staff.staffId}  {p.post.postId} {p.post.nameOfPost}");
                }
            }
        }
        public void Task6(string name, DbContextOptions<ApplicationContext> options)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                Post p = new Post(name);
                db.Posts.Add(p);
                db.SaveChanges();
            }
        }
        public void Task7(int postId, int trainId, DbContextOptions<ApplicationContext> options)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                Staff s = new Staff(trainId, postId);
                db.Staffs.Add(s);
                db.SaveChanges();
            }
        }
        public void Task8(int postId, DbContextOptions<ApplicationContext> options)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var posts = db.Posts.Where(p => p.postId == postId);
                db.Posts.RemoveRange(posts);
                db.SaveChanges();
            }
        }
        public void Task9(int staffId, DbContextOptions<ApplicationContext> options)
        {
            using ApplicationContext db = new ApplicationContext(options);
            var staffs = db.Staffs.Where(s => s.staffId == staffId);
            db.Staffs.RemoveRange(staffs);
            db.SaveChanges();
        }
        public void Task10(int postId, string nameOfPost, DbContextOptions<ApplicationContext> options)
        {
            using ApplicationContext db = new ApplicationContext(options);
            Post posts = db.Posts.Where(s => s.postId == postId)
                .FirstOrDefault();
            posts.nameOfPost = nameOfPost;
            db.SaveChanges();
        }
    }
}
