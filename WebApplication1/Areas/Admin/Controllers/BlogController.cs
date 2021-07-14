using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
namespace WebApplication1.Areas.Admin.Controllers
{
    public class BlogController : Controller
    {
        DBLAPTOPEntities db = new DBLAPTOPEntities();
        // GET: Admin/Blog

        public ActionResult BlogList()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(db.Blogs.ToList());
        }

        public ActionResult createBlog()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.IDTopic = db.Topics.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult createBlog(Blog blog)
        {
            blog.DateCreate = DateTime.Now;
            blog.UserID = Int32.Parse(Session["userID"].ToString());
            db.Blogs.Add(blog);
            db.SaveChanges();
            return RedirectToAction("BlogList");
        }

        public ActionResult editBlog(int? id)
        {
            if (Session["userID"] == null && id == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.IDTopic = db.Topics.ToList();
            return View(db.Blogs.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult editBlog(Blog blog)
        {
            var newBlog = db.Blogs.Find(blog.IDBlog);
            
            newBlog.UserID = Int32.Parse(Session["userID"].ToString());
            newBlog.Title = blog.Title;
            newBlog.Content = blog.Content;
            newBlog.DateCreate = DateTime.Now;
            newBlog.IDTopic = blog.IDTopic;

            db.SaveChanges();
            return RedirectToAction("BlogList");
        }

        public ActionResult deleteBlog(int? id)
        {
            if (Session["userID"] == null && id == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("BlogList");
        }

        public ActionResult blogDetail(int? id)
        {
            if (Session["userID"] == null && id == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(db.Blogs.Find(id));
        }
    }
}