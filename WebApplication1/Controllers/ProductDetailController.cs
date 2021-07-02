using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
namespace WebApplication1.Controllers
{
    public class ProductDetailController : Controller
    {
        // GET: ProductDetail
        DBContext db = new DBContext();
        public ActionResult ProductDetailPage(int id)
        {
            var selectedProduct = db.Products.Find(id);
            ViewBag.relativeProducts = getRelativeProducts(selectedProduct.categoryID);
            return View(selectedProduct);
        }

        private List<Product> getRelativeProducts(int categoryID)
        {
            return db.Products.Where(p => p.categoryID == categoryID).Take(4).ToList();
        }
    }
}