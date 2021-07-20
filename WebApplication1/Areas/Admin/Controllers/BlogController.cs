using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult createBlog(Blog blog, HttpPostedFileBase ImageUpload)
        {
            blog.DateCreate = DateTime.Now;
            blog.UserID = Int32.Parse(Session["userID"].ToString());
            if (ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                string extension = Path.GetExtension(ImageUpload.FileName);
                fileName += extension;
                blog.image = "~/images/blog/" + fileName;
                ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/images/blog"), fileName));
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("BlogList");
            }
            else
            {
                blog.image = "~/images/blog/none.png";
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("BlogList");
            }
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
            
            newBlog.UserID = blog.UserID;
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


        public ActionResult uploadImage(FormCollection frm, HttpPostedFileBase ImageUpload)
        {
            var blog = db.Blogs.Find(Int32.Parse(frm["IDBlog"]));
            if (ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                string extension = Path.GetExtension(ImageUpload.FileName);
                fileName += extension;
                blog.image = "~/images/blog/" + fileName; ;
                ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/images/blog/"), fileName));

                db.SaveChanges();
                return RedirectToAction("editBlog", "Blog", new { id = Int32.Parse(frm["IDBlog"]) });
            }
            else
            {
                blog.image = "~/images/blog/none.png";
                db.SaveChanges();
                return RedirectToAction("BlogList", "Blog");
            }
        }
    }
}