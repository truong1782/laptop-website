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
    public class HomeController : Controller
    {
        DBLAPTOPEntities db = new DBLAPTOPEntities();
        private List<Product> newProducts(int count)
        {
            return db.Products.OrderByDescending(a => a.dateCreate).Take(count).ToList();
        }

        public ActionResult Index(int? page)
        {
            //var Lm = newProducts(8);
            //return View(Lm);
            int pageSize = 8;
            int pageNum = (page ?? 1);
            var Lm = newProducts(32);
            return View(Lm.ToPagedList(pageNum, pageSize));
        }
    }
}