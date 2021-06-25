using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Admin.Models;
namespace WebApplication1.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Product
        public ActionResult Index()
        {
            var productList = db.Products.ToList();
            return View(productList);
        }

        public ActionResult Create()
        {
            ViewBag.Category = db.Categories.ToList();
            ViewBag.Brand = db.Brands.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult createProduct(Product productInfo)
        {
            productInfo.dateCreate = DateTime.Now;
            if (productInfo.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(productInfo.ImageUpload.FileName);
                string extension = Path.GetExtension(productInfo.ImageUpload.FileName);
                fileName += extension;
                productInfo.image = fileName;
                productInfo.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/images/sanpham"), fileName));
                db.Products.Add(productInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else {
                productInfo.image = "none.png";
                db.Products.Add(productInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}