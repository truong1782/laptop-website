using System;
using System.Collections.Generic;
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
    }
}