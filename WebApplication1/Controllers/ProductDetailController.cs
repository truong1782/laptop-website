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
            ViewBag.Products = db.Products.Take(4).ToList();
            return View(db.Products.Find(id));
        }
    }
}