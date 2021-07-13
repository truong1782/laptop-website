using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
namespace WebApplication1.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Category
        public ActionResult listCategories()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var listcate = db.Categories.ToList();
            return View(listcate);
        }

        [HttpGet]
        public ActionResult insertCategory()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult insertCategory(Category cat)
        {
            db.Categories.Add(cat);
            db.SaveChanges();
            return RedirectToAction("listCategories");
        }

        [HttpGet]
        public ActionResult editCategory(int? id)
        {
            if (Session["userID"] == null || id == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(db.Categories.Find(id));
        }

        [HttpPost]
        public ActionResult editCategory(Category cate)
        {
            db.Entry(cate).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("listCategories");
        }

        public ActionResult removeCategory(int? id)
        {
            if (Session["userID"] == null || id == null)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                var cate = db.Categories.Find(id);
                db.Categories.Remove(cate);
                db.SaveChanges();
                return RedirectToAction("listCategories");
            }
            catch (Exception)
            {
                ViewBag.Alert = "<div class='alert alert-danger' role='alert'>Không thể xóa loại sản phẩm này</div>";
                return View("listCategories", db.Categories.ToList());
            }
        }
    }
}