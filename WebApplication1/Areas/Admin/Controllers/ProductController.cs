using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using WebApplication1.Models.Data;
using System.Web;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Product
        public ActionResult Index()
        {
            var productList = db.Product.ToList();
            return View(productList);
        }

        public ActionResult Create()
        {
            ViewBag.Category = db.Category.ToList();
            ViewBag.Brand = db.Brand.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult createProduct(Product productInfo, HttpPostedFileBase ImageUpload)
        {
            productInfo.dateCreate = DateTime.Now;
            if (ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                string extension = Path.GetExtension(ImageUpload.FileName);
                fileName += extension;
                productInfo.image = fileName;
                ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/images/sanpham"), fileName));
                db.Product.Add(productInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else {
                productInfo.image = "none.png";
                db.Product.Add(productInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Product prod = db.Product.Find(id);
            ViewBag.Category = db.Category.ToList();
            ViewBag.Brand = db.Brand.ToList();
            return View(prod);
        }

        [HttpPost]
        public ActionResult Edit(Product productInfo, HttpPostedFileBase ImageUpload)
        {
            productInfo.dateCreate = DateTime.Now;
            if (ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                string extension = Path.GetExtension(ImageUpload.FileName);
                fileName += extension;
                productInfo.image = fileName;
                ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/images/sanpham"), fileName));
            }
            db.Entry(productInfo).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            Product pro = db.Product.Find(id);
            db.Product.Remove(pro);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}