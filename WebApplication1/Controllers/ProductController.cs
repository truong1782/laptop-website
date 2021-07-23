using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
using PagedList;
using PagedList.Mvc;
namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        DBLAPTOPEntities db = new DBLAPTOPEntities();
        int pageSize = 6;
        // GET: Product
        public ActionResult ProductPage(int? page)
        {
            int pageNum = (page ?? 1);
            var products = db.Products.ToList();

            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            ViewBag.productTitle = "Tất cả sản phẩm";

            return View(products.ToPagedList(pageNum, pageSize));
        }

        public ActionResult filterByCategory(int id, int? page)
        {
            int pageNum = (page ?? 1);
            var products = db.Products.Where(p => p.categoryID == id).ToList();

            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            ViewBag.productTitle = db.Categories.Find(id).categoryName;
            return View("ProductPage", products.ToPagedList(pageNum, pageSize));
        }

        public ActionResult filterByBrand(int id, int? page)
        {
            int pageNum = (page ?? 1);
            var products = db.Products.Where(p => p.brandID == id).ToList();

            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            ViewBag.productTitle = "Laptop " + db.Brands.Find(id).brandName;

            return View("ProductPage", products.ToPagedList(pageNum, pageSize));
        }

        [HttpPost]
        public ActionResult Search(FormCollection frm, int? page)
        {
            string input = frm["search"];
            int pageNum = (page ?? 1);
            var products = db.Products.Where(p => p.productName.Contains(input)).ToList();

            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            ViewBag.productTitle = "Tìm kiếm sản phẩm theo " + "\"" + input +  "\"";
            return View("ProductPage", products.ToPagedList(pageNum, pageSize));
        }

        public ActionResult sortByDescending(int? page)
        {
            int pageNum = (page ?? 1);
            var products = db.Products.OrderByDescending(p => p.productPrice).ToList();

            ViewBag.productTitle = "Giá giảm dần";
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            return View("ProductPage", products.ToPagedList(pageNum, pageSize));
        }

        public ActionResult sortByIncreasing(int? page)
        {
            int pageNum = (page ?? 1);
            var products = db.Products.OrderBy(p => p.productPrice).ToList();

            ViewBag.productTitle = "Giá tăng dần";
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            return View("ProductPage", products.ToPagedList(pageNum, pageSize));
        }

        public ActionResult lastedProducts(int? page)
        {
            int pageNum = (page ?? 1);
            var products = db.Products.OrderByDescending(p => p.dateCreate).ToList();

            ViewBag.productTitle = "Sản phẩm mới nhất";
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            return View("ProductPage", products.ToPagedList(pageNum, pageSize));
        }
    }
}