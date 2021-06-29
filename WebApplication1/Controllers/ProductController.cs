using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        DBContext db = new DBContext();
        // GET: Product
        public ActionResult ProductPage()
        {
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            ViewBag.productTitle = "Tất cả sản phẩm";
            return View(db.Products.ToList());
        }

        public ActionResult filterByCategory(int id)
        {
            List<Product> products = db.Products.Where(p => p.categoryID == id).ToList();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            ViewBag.productTitle = db.Categories.Find(id).categoryName;
            return View("ProductPage", products);
        }

        public ActionResult filterByBrand(int id)
        {
            List<Product> products = db.Products.Where(p => p.brandID == id).ToList();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            ViewBag.productTitle = "Laptop " + db.Brands.Find(id).brandName;
            return View("ProductPage", products);
        }

    }
}