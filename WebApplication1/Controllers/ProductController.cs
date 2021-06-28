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
        public ActionResult Index()
        {
            ViewBag.Categories = db.Category.ToList();
            ViewBag.Brands = db.Brand.ToList();
            ViewBag.productTitle = "Tất cả sản phẩm";
            return View(db.Product.ToList());
        }

        public ActionResult filterByCategory(int id)
        {
            List<Product> products = db.Product.Where(p => p.categoryID == id).ToList();
            ViewBag.Categories = db.Category.ToList();
            ViewBag.Brands = db.Brand.ToList();
            ViewBag.productTitle = db.Category.Find(id).categoryName;
            return View("Index", products);
        }

        public ActionResult filterByBrand(int id)
        {
            List<Product> products = db.Product.Where(p => p.brandID == id).ToList();
            ViewBag.Categories = db.Category.ToList();
            ViewBag.Brands = db.Brand.ToList();
            ViewBag.productTitle = "Laptop " + db.Brand.Find(id).brandName;
            return View("Index", products);
        }

    }
}