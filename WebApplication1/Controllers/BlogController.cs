using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
namespace WebApplication1.Controllers
{
    public class BlogController : Controller
    {
        DBLAPTOPEntities db = new DBLAPTOPEntities();


        public ActionResult Blog(int? IDTopic)
        {
            ViewBag.Topics = db.Topics.ToList();
            List<Blog> listBlog = new List<Blog>();
            listBlog = db.Blogs.ToList();


            if (IDTopic != null)
            {
                listBlog = db.Blogs.Where(p => p.IDTopic == IDTopic).ToList();
            }
            else
            {
                listBlog = db.Blogs.ToList();
            }
            return View(listBlog);
        }

        public ActionResult filterByTopic(int id)
        {
            List<Blog> topics = db.Blogs.Where(p => p.IDTopic == id).ToList();
            ViewBag.Topics = db.Topics.ToList();
            ViewBag.TopicsName = db.Topics.Find(id).NameTopic;
            return View("Blog", topics);
        }

        // GET: Product
        public ActionResult TopicBlog()
        {
            List<Topic> Topic_Blog = new List<Topic>();
            Topic_Blog = db.Topics.ToList();
            return View(Topic_Blog);
        }

        public ActionResult BlogDetail(int id)
        {
            Blog blog = new Blog();
            blog = db.Blogs.Where(p => p.IDBlog == id).SingleOrDefault();

            return View(blog);
        }
    }
}