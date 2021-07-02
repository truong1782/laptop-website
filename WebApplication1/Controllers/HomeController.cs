using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        DBContext db = new DBContext();
        private List<Product> newProducts(int count)
        {
            return db.Products.OrderByDescending(a => a.dateCreate).Take(count).ToList();
        }

        public ActionResult Index()
        {
            var Lm = newProducts(8);
            return View(Lm);
        }
    }
}