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
        public ActionResult Index(int id)
        {
            ViewBag.Products = db.Product.Take(4).ToList();
            return View(db.Product.Find(id));
        }
    }
}